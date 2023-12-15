using FleetManagement.Api.MediatR.Commands;
using FluentValidation;

namespace FleetManagement.Api.FlValidation
{
    public class CreateBestuurderCommandValidator : AbstractValidator<CreateBestuurderCommand>
    {
        public CreateBestuurderCommandValidator()
        {
            RuleFor(b => b.Bestuurder.Voornaam)
                .NotEmpty()
                .Length(2, 50)
                .WithMessage("Voornaam is verplicht.");
            RuleFor(b => b.Bestuurder.Naam)
                .NotEmpty()
                .Length(2, 50)
                .WithMessage("Naam is verplicht.");
            RuleFor(b => b.Bestuurder.Adres)
                .NotEmpty()
                .WithMessage("Adres is verplicht.");
            RuleFor(b => b.Bestuurder.Rijksregisternummer)
                .NotEmpty()
                .Length(11)
                .WithMessage("Rijksregisternummer is verplicht.");
            RuleFor(b => b.Bestuurder.Rijbewijs)
                .NotEmpty()
                .Matches(@"^Type\s[A-Z\d]{1,3}$")
                .WithMessage("Rijbewijs is verplicht.");
        }
    }
}
