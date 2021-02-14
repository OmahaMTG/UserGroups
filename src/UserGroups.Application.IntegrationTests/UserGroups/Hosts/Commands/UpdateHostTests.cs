//using FluentAssertions;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.IntegrationTests.TestData;
//using UserGroups.Application.UserGroups.Hosts.Commands;
//using UserGroups.Domain.Entities;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Hosts.Commands
//{
//    using static TestDataManager;
//    using static Testing;
//    public class UpdateHostTests : TestBase
//    {
//        private UpdateHostCommand _command => new UpdateHostCommand
//        {
//            Blurb = "Updated Blurb",
//            ContactInfo = "Updated Contact Info",
//            IsDeleted = false,
//            Name = "Updated Name",
//        };


//        [Test]
//        public async Task ShouldUpdateHost()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var testHost = await CreateTestHost();
//            var command = _command;
//            command.Id = testHost.Id;
//            await SendAsync(command);

//            var updatedHost = await FindAsync<Host>(testHost.Id);

//            updatedHost.Name.Should().Be(command.Name);
//            updatedHost.Blurb.Should().Be(command.Blurb);
//            updatedHost.ContactInfo.Should().Be(command.ContactInfo);
//            updatedHost.IsDeleted.Should().Be(command.IsDeleted);

//            updatedHost.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
//            updatedHost.UpdatedByUser.Should().Be("UserId");
//        }

//        [Test]
//        public void ShouldThrowIfHostNotFound()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var command = _command;
//            command.Id = 1;

//            FluentActions.Invoking(() =>
//                SendAsync(command)).Should().Throw<NotFoundException>();
//        }

//        [Test]
//        public void ShouldThrowIfUserIsNotHostAdmin()
//        {
//            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
//        }
//    }
//}
