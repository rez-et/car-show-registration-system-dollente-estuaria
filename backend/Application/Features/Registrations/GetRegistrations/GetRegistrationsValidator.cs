namespace Application.Features.Registrations.GetRegistrations;

using FluentValidation;

public sealed class GetRegistrationsValidator : AbstractValidator<GetRegistrationsQuery>
{
    public GetRegistrationsValidator()
    {
        RuleFor(x => x.ParticipantName)
            .MaximumLength(150)
            .When(x => x.ParticipantName is not null);

        RuleFor(x => x.Type)
            .IsInEnum()
            .When(x => x.Type.HasValue);

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue);

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50);
    }
}
