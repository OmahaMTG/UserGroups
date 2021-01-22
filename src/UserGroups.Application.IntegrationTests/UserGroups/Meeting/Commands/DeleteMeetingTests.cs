//using FluentAssertions;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.UserGroups.Meetings.Commands;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
//{
//    using static TestDataManager;
//    using static Testing;
//    public class DeleteMeetingTests : TestBase
//    {

//        [Test]
//        public async Task HardDeleteShouldDeletePresenter()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var testMeeting = await CreateTestMeeting();

//            var result = await SendAsync(new DeleteMeetingCommand()
//            {
//                HardDelete = true,
//                Id = testMeeting.Id
//            });

//            var dbPresenter = await FindAsync<Domain.Entities.Meeting>(testMeeting.Id);

//            dbPresenter.Should().BeNull();
//        }


//        [Test]
//        public async Task SoftDeleteShouldFlagMeetingAsDeleted()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var testMeeting = await CreateTestMeeting();

//            var result = await SendAsync(new DeleteMeetingCommand()
//            {
//                HardDelete = false,
//                Id = testMeeting.Id
//            });

//            var dbPresenter = await FindAsync<Domain.Entities.Meeting>(testMeeting.Id);

//            dbPresenter.Should().NotBeNull();
//            dbPresenter.IsDeleted.Should().Be(true);
//        }

//        [Test]
//        public void ShouldThrowIfPresenterIdDoesNotExist()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

//            var deleteCommand = new DeleteMeetingCommand()
//            {
//                HardDelete = false,
//                Id = 1
//            };


//            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotFoundException>();
//        }

//        [Test]
//        public void ShouldThrowIfUserIsNotPresenterAdmin()
//        {
//            var deleteCommand = new DeleteMeetingCommand()
//            {
//                HardDelete = false,
//                Id = 1
//            };

//            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotAuthorizedException>();
//        }
//    }
//}
