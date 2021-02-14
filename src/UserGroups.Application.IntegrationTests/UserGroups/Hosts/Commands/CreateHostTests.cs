//using FluentAssertions;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.UserGroups.Hosts.Commands;
//using UserGroups.Domain.Entities;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Hosts.Commands
//{
//    using static Testing;

//    public class CreateHostTests : TestBase
//    {
//        private CreateHostCommand _command => new CreateHostCommand
//        {
//            Blurb = "Test Blurb",
//            ContactInfo = "Test Contact Info",
//            IsDeleted = true,
//            Name = "Test Name"
//        };


//        [Test]
//        public async Task ShouldCreateHost()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var result = await SendAsync(_command);

//            var created = await FindAsync<Host>(result);

//            created.Name.Should().Be(_command.Name);
//            created.Blurb.Should().Be(_command.Blurb);
//            created.ContactInfo.Should().Be(_command.ContactInfo);
//            created.IsDeleted.Should().Be(_command.IsDeleted);
//            created.CreatedDate.Should().BeCloseTo(DateTime.Now, 10000);
//            created.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
//            created.CreatedByUser.Should().Be("UserId");
//            created.UpdatedByUser.Should().Be("UserId");
//        }


//        [Test]
//        public void ShouldThrowIfUserIsNotSponsorAdmin()
//        {
//            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
//        }
//    }
//}
