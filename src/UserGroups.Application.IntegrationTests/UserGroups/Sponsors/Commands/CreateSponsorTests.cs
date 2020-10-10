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

    public class CreateSponsorTests : TestBase
    {
        private CreateSponsorCommand _command => new CreateSponsorCommand
        {
            Blurb = "Test Blurb",
            ContactInfo = "Test Contact Info",
            IncludeInBannerRotation = true,
            IsDeleted = true,
            Name = "Test Name",
            ShortBlurb = "Test Short Blurb",
            SponsorUrl = "http://test.url"
        };


        [Test]
        public async Task ShouldCreateSponsor()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await SendAsync(_command);

            var created = await FindAsync<Sponsor>(result);

            created.Name.Should().Be(_command.Name);
            created.Blurb.Should().Be(_command.Blurb);
            created.ContactInfo.Should().Be(_command.ContactInfo);
            created.IncludeInBannerRotation.Should().Be(_command.IncludeInBannerRotation);
            created.IsDeleted.Should().Be(_command.IsDeleted);
            created.ShortBlurb.Should().Be(_command.ShortBlurb);
            created.SponsorUrl.Should().Be(_command.SponsorUrl);
            created.CreatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.CreatedByUser.Should().Be("UserId");
            created.UpdatedByUser.Should().Be("UserId");
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
