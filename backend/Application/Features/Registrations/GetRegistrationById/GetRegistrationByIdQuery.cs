namespace Application.Features.Registrations.GetRegistrationById;

using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;

public sealed record GetRegistrationByIdQuery(Guid Id) : IQuery<Result<GetRegistrationByIdResponse>>;

public sealed record GetRegistrationByIdResponse(
    Guid Id,
    string RegistrationNumber,
    DateTime RegistrationDate,
    string ParticipantName,
    string ContactNumber,
    string Email,
    string Address,
    string CarMakeModel,
    string PlateNumber,
    RegistrationType RegistrationType,
    RegistrationStatus Status,
    decimal RegistrationFee,
    decimal AmountPaid,
    PaymentStatus PaymentStatus);
