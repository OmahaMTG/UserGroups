using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Meetings.Commands;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
{

    public class DeleteMeetingTests : TestBase
    {

        [Test]
        public async Task HardDeleteShouldDeleteMeeting()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testMeeting = await arrange.CreateTestMeeting();

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new DeleteMeetingCommand()
            {
                HardDelete = true,
                Id = testMeeting.Id
            });

            var assert = new Assert();
            var dbMeeting = await assert.FindAsync<Domain.Entities.Meeting>(testMeeting.Id);
            dbMeeting.Should().BeNull();
        }


        [Test]
        public async Task SoftDeleteShouldFlagMeetingAsDeleted()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testMeeting = await arrange.CreateTestMeeting();

            var act = new Act();
            var result = await act.SendAsync(new DeleteMeetingCommand()
            {
                HardDelete = false,
                Id = testMeeting.Id
            });

            var assert = new Assert();
            var dbMeeting = await assert.FindAsync<Domain.Entities.Meeting>(testMeeting.Id);
            dbMeeting.Should().NotBeNull();
            dbMeeting.IsDeleted.Should().Be(true);
        }

        [Test]
        public async Task ShouldThrowIfMeetingIdDoesNotExist()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();

            var deleteCommand = new DeleteMeetingCommand()
            {
                HardDelete = false,
                Id = 1
            };

            var act = new Act();
            FluentActions.Invoking(() => act.SendAsync(deleteCommand)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldThrowIfUserIsNotAdmin()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });

            var deleteCommand = new DeleteMeetingCommand()
            {
                HardDelete = false,
                Id = 1
            };

            FluentActions.Invoking(() => act.SendAsync(deleteCommand)).Should().Throw<NotAuthorizedException>();
        }
    }
}
