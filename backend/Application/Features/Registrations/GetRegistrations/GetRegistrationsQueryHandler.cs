namespace Application.Features.Registrations.GetRegistrations;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationsQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>>
{
    public async Task<Result<GetRegistrationsResponse>> HandleAsync(
        GetRegistrationsQuery query,
        CancellationToken cancellationToken = default)
    {
        var registrationsQuery = dbContext.Registrations.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.ParticipantName))
        {
            var searchTerm = query.ParticipantName.Trim().ToLower();
            registrationsQuery = registrationsQuery.Where(
                r => r.ParticipantName.ToLower().Contains(searchTerm));
        }

        if (query.Type.HasValue)
        {
            registrationsQuery = registrationsQuery.Where(r => r.RegistrationType == query.Type.Value);
        }

        if (query.Status.HasValue)
        {
            registrationsQuery = registrationsQuery.Where(r => r.Status == query.Status.Value);
        }

        var totalCount = await registrationsQuery.CountAsync(cancellationToken);

        var registrations = await registrationsQuery
            .OrderByDescending(r => r.RegistrationDate)
            .ThenByDescending(r => r.RegistrationNumber)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var items = registrations
            .Select(r => new RegistrationListItem(
                r.Id,
                r.RegistrationNumber,
                r.RegistrationDate,
                r.ParticipantName,
                r.ContactNumber,
                r.Email,
                r.Address,
                r.CarMakeModel,
                r.PlateNumber,
                r.RegistrationType,
                r.Status,
                r.RegistrationFee,
                r.AmountPaid,
                r.PaymentStatus))
            .ToList();

        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)query.PageSize);

        return Result<GetRegistrationsResponse>.Success(
            new GetRegistrationsResponse(
                items,
                query.Page,
                query.PageSize,
                totalCount,
                totalPages));
    }
}
