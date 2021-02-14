//using FluentAssertions;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.IntegrationTests.TestData;
//using UserGroups.Application.UserGroups.Meetings.Commands;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
//{
//    using static TestDataManager;
//    using static Testing;
//    public class DeleteMeetingTests : TestBase
//    {

//        [Test]
//        public async Task HardDeleteShouldDeleteMeeting()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var testHost = await CreateTestHost();

//            var createdAsTestUser = await CreateTestUser(new List<ApplicationRoles>() { ApplicationRoles.Admin });
//            var testMeeting = await CreateTestMeeting(createdAsTestUser.Id);

//            var result = await SendAsync(new DeleteMeetingCommand()
//            {
//                HardDelete = true,
//                Id = testMeeting.Id
//            });

//            var dbMeeting = await FindAsync<Domain.Entities.Meeting>(testMeeting.Id);

//            dbMeeting.Should().BeNull();
//        }


//        [Test]
//        public async Task SoftDeleteShouldFlagMeetingAsDeleted()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var testHost = await CreateTestHost();

//            var createdAsTestUser = await CreateTestUser(new List<ApplicationRoles>() { ApplicationRoles.Admin });
//            var testMeeting = await CreateTestMeeting(createdAsTestUser.Id);


//            var result = await SendAsync(new DeleteMeetingCommand()
//            {
//                HardDelete = false,
//                Id = testMeeting.Id
//            });

//            var dbMeeting = await FindAsync<Domain.Entities.Meeting>(testMeeting.Id);

//            dbMeeting.Should().NotBeNull();
//            dbMeeting.IsDeleted.Should().Be(true);
//        }

//        [Test]
//        public void ShouldThrowIfMeetingIdDoesNotExist()
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
//        public void ShouldThrowIfUserIsNotAdmin()
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
