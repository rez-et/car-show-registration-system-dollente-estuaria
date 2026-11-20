namespace Application.Features.Registrations.GetRegistrations;

using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;

public sealed record GetRegistrationsQuery(
    string? ParticipantName = null,
    RegistrationType? Type = null,
    RegistrationStatus? Status = null,
    int Page = 1,
    int PageSize = 10) : IQuery<Result<GetRegistrationsResponse>>;

public sealed record RegistrationListItem(
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

public sealed record GetRegistrationsResponse(
    IReadOnlyList<RegistrationListItem> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);
