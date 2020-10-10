using System;
using FluentValidation;

namespace UserGroups.Application.UserGroups.Sponsors.Commands
{
    public class CreateSponsorCommandValidator : AbstractValidator<CreateSponsorCommand>
    {
        public CreateSponsorCommandValidator()
        {
            RuleFor(v => v.ShortBlurb)
                .MaximumLength(20);

            RuleFor(v => v.SponsorUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage(x => "Valid URL Required")
                ;
        }
    }
}