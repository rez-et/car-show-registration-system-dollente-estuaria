namespace Application.Features.Registrations.GetRegistrationSummary;

using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record GetRegistrationSummaryQuery() : IQuery<Result<GetRegistrationSummaryResponse>>;

public sealed record GetRegistrationSummaryResponse(
    int TotalRegistrations,
    int PendingCount,
    int ConfirmedCount,
    int CheckedInCount,
    int CancelledCount,
    int ExhibitorCount,
    int CompetitorCount,
    int VipCount,
    int UnpaidCount,
    int PartialCount,
    int PaidCount,
    decimal TotalFees,
    decimal TotalCollected,
    decimal TotalBalance);
