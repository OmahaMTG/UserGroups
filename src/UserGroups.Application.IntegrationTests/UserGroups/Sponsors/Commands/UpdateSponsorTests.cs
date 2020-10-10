using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Sponsors.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Sponsors.Commands
{
    using static Testing;

    public class UpdateSponsorTests : TestBase
    {
        private UpdateSponsorCommand _command => new UpdateSponsorCommand
        {
            Blurb = "Updated Blurb",
            ContactInfo = "Updated Contact Info",
            IncludeInBannerRotation = false,
            IsDeleted = false,
            Name = "Updated Name",
            ShortBlurb = "Updated Short Blurb",
            SponsorUrl = "http://Updated.url"
        };

        private async Task<Sponsor> CreateTestSponsor()
        {
            return await AddAsync(new Sponsor
            {
                Blurb = "Test Blurb",
                ContactInfo = "Test Contact Info",
                IncludeInBannerRotation = true,
                IsDeleted = true,
                Name = "Test Name",
                ShortBlurb = "Test Blurb",
                SponsorUrl = "http://test.url"
            });
        }



        [Test]
        public async Task ShouldUpdateSponsor()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testSponsor = await CreateTestSponsor();
            var command = _command;
            command.Id = testSponsor.Id;
            await SendAsync(command);

            var updatedSponsor = await FindAsync<Sponsor>(testSponsor.Id);

            updatedSponsor.Name.Should().Be(command.Name);
            updatedSponsor.Blurb.Should().Be(command.Blurb);
            updatedSponsor.ContactInfo.Should().Be(command.ContactInfo);
            updatedSponsor.IncludeInBannerRotation.Should().Be(command.IncludeInBannerRotation);
            updatedSponsor.IsDeleted.Should().Be(command.IsDeleted);
            updatedSponsor.ShortBlurb.Should().Be(command.ShortBlurb);
            updatedSponsor.SponsorUrl.Should().Be(command.SponsorUrl);

            updatedSponsor.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            updatedSponsor.UpdatedByUser.Should().Be("UserId");
        }

        [Test]
        public void ShouldThrowIfSponsorNotFound()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = 1;

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfShortBlurbIsTooLong()
        {
            var command = _command;
            command.ShortBlurb = new string('a', 21);

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowIfSponsorUrlIsNotValidUrl()
        {
            var command = _command;
            command.SponsorUrl = "NotAUrl";

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotSponsorAdmin()
        {
            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
