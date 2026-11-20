namespace Application.Features.Registrations.GetRegistrationSummary;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationSummaryQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>>
{
    public async Task<Result<GetRegistrationSummaryResponse>> HandleAsync(
        GetRegistrationSummaryQuery query,
        CancellationToken cancellationToken = default)
    {
        var registrations = await dbContext.Registrations
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var totalFees = registrations.Sum(r => r.RegistrationFee);
        var totalCollected = registrations.Sum(r => r.AmountPaid);

        var summary = new GetRegistrationSummaryResponse(
            TotalRegistrations: registrations.Count,
            PendingCount: registrations.Count(r => r.Status == RegistrationStatus.Pending),
            ConfirmedCount: registrations.Count(r => r.Status == RegistrationStatus.Confirmed),
            CheckedInCount: registrations.Count(r => r.Status == RegistrationStatus.CheckedIn),
            CancelledCount: registrations.Count(r => r.Status == RegistrationStatus.Cancelled),
            ExhibitorCount: registrations.Count(r => r.RegistrationType == RegistrationType.Exhibitor),
            CompetitorCount: registrations.Count(r => r.RegistrationType == RegistrationType.Competitor),
            VipCount: registrations.Count(r => r.RegistrationType == RegistrationType.VIP),
            UnpaidCount: registrations.Count(r => r.PaymentStatus == PaymentStatus.Unpaid),
            PartialCount: registrations.Count(r => r.PaymentStatus == PaymentStatus.Partial),
            PaidCount: registrations.Count(r => r.PaymentStatus == PaymentStatus.Paid),
            TotalFees: totalFees,
            TotalCollected: totalCollected,
            TotalBalance: totalFees - totalCollected);

        return Result<GetRegistrationSummaryResponse>.Success(summary);
    }
}
