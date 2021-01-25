using FluentAssertions;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.IntegrationTests.TestData;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
{
    using static Testing;
    public class UpdateMeetingTests : TestBase
    {
        //private UpdateHostCommand _command => new UpdateHostCommand
        //{
        //    Blurb = "Updated Blurb",
        //    ContactInfo = "Updated Contact Info",
        //    IsDeleted = false,
        //    Name = "Updated Name",
        //};

        private Update


        //[Test]
        //public async Task ShouldUpdateHost()
        //{
        //    SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
        //    var testHost = await CreateTestHost();
        //    var command = _command;
        //    command.Id = testHost.Id;
        //    await SendAsync(command);

        //    var updatedHost = await FindAsync<Host>(testHost.Id);

        //    updatedHost.Name.Should().Be(command.Name);
        //    updatedHost.Blurb.Should().Be(command.Blurb);
        //    updatedHost.ContactInfo.Should().Be(command.ContactInfo);
        //    updatedHost.IsDeleted.Should().Be(command.IsDeleted);

        //    updatedHost.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
        //    updatedHost.UpdatedByUser.Should().Be("UserId");
        //}

        //[Test]
        //public void ShouldThrowIfHostNotFound()
        //{
        //    SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
        //    var command = _command;
        //    command.Id = 1;

        //    FluentActions.Invoking(() =>
        //        SendAsync(command)).Should().Throw<NotFoundException>();
        //}

        [Test]
        public void ShouldThrowIfUserIsNotHostAdmin()
        {
            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
