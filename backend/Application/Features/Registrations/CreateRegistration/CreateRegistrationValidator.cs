namespace Application.Features.Registrations.CreateRegistration;

using Domain.Entities;
using FluentValidation;

public sealed class CreateRegistrationValidator : AbstractValidator<CreateRegistrationCommand>
{
    public CreateRegistrationValidator()
    {
        RuleFor(x => x.ParticipantName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.ContactNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Matches(@"^09\d{9}$")
            .WithMessage("ContactNumber must be in PH mobile format 09XXXXXXXXX (11 digits).");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.CarMakeModel)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PlateNumber)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.RegistrationType)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.AmountPaid)
            .GreaterThanOrEqualTo(0);

        When(x => x.RegistrationType.HasValue && Enum.IsDefined(x.RegistrationType.Value), () =>
        {
            RuleFor(x => x.AmountPaid)
                .Must((command, amountPaid) => amountPaid <= Registration.GetFeeForType(command.RegistrationType!.Value))
                .WithMessage(command =>
                {
                    var fee = Registration.GetFeeForType(command.RegistrationType!.Value);
                    return $"AmountPaid must not exceed the {command.RegistrationType} registration fee of {fee:0.##}.";
                });
        });
    }
}
