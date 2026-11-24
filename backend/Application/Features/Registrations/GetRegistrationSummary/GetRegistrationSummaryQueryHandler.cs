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

        var activeRegistrations = registrations
            .Where(r => r.Status != RegistrationStatus.Cancelled)
            .ToList();

        var totalFees = activeRegistrations.Sum(r => r.RegistrationFee);
        var totalPayments = activeRegistrations.Sum(r => r.AmountPaid);

        var summary = new GetRegistrationSummaryResponse(
            TotalRegistrations: registrations.Count,
            Confirmed: registrations.Count(r => r.Status == RegistrationStatus.Confirmed),
            CheckedIn: registrations.Count(r => r.Status == RegistrationStatus.CheckedIn),
            Pending: registrations.Count(r => r.Status == RegistrationStatus.Pending),
            Cancelled: registrations.Count(r => r.Status == RegistrationStatus.Cancelled),
            TotalFees: totalFees,
            TotalPayments: totalPayments,
            UncollectedPayments: totalFees - totalPayments);

        return Result<GetRegistrationSummaryResponse>.Success(summary);
    }
}
