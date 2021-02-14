//using FluentAssertions;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.IntegrationTests.TestData;
//using UserGroups.Application.UserGroups.Presenters.Commands;
//using UserGroups.Domain.Entities;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Presenters.Commands
//{
//    using static TestDataManager;
//    using static Testing;
//    public class DeletePresenterTests : TestBase
//    {

//        [Test]
//        public async Task HardDeleteShouldDeletePresenter()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var testPresenter = await CreateTestPresenter();

//            var result = await SendAsync(new DeletePresenterCommand
//            {
//                HardDelete = true,
//                Id = testPresenter.Id
//            });

//            var dbPresenter = await FindAsync<Presenter>(testPresenter.Id);

//            dbPresenter.Should().BeNull();
//        }

//        [Test]
//        public async Task SoftDeleteShouldFlagPresenterAsDeleted()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

//            var testPresenter = await CreateTestPresenter();

//            var result = await SendAsync(new DeletePresenterCommand
//            {
//                HardDelete = false,
//                Id = testPresenter.Id
//            });

//            var dbPresenter = await FindAsync<Presenter>(testPresenter.Id);

//            dbPresenter.Should().NotBeNull();

//            dbPresenter.IsDeleted.Should().BeTrue();
//        }


//        [Test]
//        public void ShouldThrowIfPresenterIdDoesNotExist()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var deleteCommand = new DeletePresenterCommand
//            {
//                HardDelete = false,
//                Id = 1
//            };

//            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotFoundException>();
//        }

//        [Test]
//        public void ShouldThrowIfUserIsNotPresenterAdmin()
//        {
//            var deleteCommand = new DeletePresenterCommand
//            {
//                HardDelete = false,
//                Id = 1
//            };

//            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotAuthorizedException>();
//        }
//    }
//}
