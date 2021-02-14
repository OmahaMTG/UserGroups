//using FluentAssertions;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.IntegrationTests.TestData;
//using UserGroups.Application.UserGroups.Sponsors.Commands;
//using UserGroups.Domain.Entities;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Sponsors.Commands
//{
//    using static TestDataManager;
//    using static Testing;
//    public class DeleteSponsorTests : TestBase
//    {
//        [Test]
//        public async Task HardDeleteShouldDeleteSponsor()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var testSponsor = await CreateTestSponsor();

//            var result = await SendAsync(new DeleteSponsorCommand
//            {
//                HardDelete = true,
//                Id = testSponsor.Id
//            });

//            var dbSponsor = await FindAsync<Sponsor>(testSponsor.Id);

//            dbSponsor.Should().BeNull();
//        }

//        [Test]
//        public async Task SoftDeleteShouldFlagSponsorAsDeleted()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

//            var testSponsor = await CreateTestSponsor();

//            var result = await SendAsync(new DeleteSponsorCommand
//            {
//                HardDelete = false,
//                Id = testSponsor.Id
//            });

//            var dbSponsor = await FindAsync<Sponsor>(testSponsor.Id);

//            dbSponsor.Should().NotBeNull();

//            dbSponsor.IsDeleted.Should().BeTrue();
//        }


//        [Test]
//        public void ShouldThrowIfSponsorIdDoesNotExist()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var deleteCommand = new DeleteSponsorCommand
//            {
//                HardDelete = false,
//                Id = 1
//            };

//            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotFoundException>();
//        }

//        [Test]
//        public void ShouldThrowIfUserIsNotSponsorAdmin()
//        {
//            var deleteCommand = new DeleteSponsorCommand
//            {
//                HardDelete = false,
//                Id = 1
//            };

//            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotAuthorizedException>();
//        }
//    }
//}
