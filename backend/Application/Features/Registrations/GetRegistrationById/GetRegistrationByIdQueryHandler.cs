namespace Application.Features.Registrations.GetRegistrationById;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationByIdQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetRegistrationByIdQuery, Result<GetRegistrationByIdResponse>>
{
    public async Task<Result<GetRegistrationByIdResponse>> HandleAsync(
        GetRegistrationByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var registration = await dbContext.Registrations
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == query.Id, cancellationToken);

        if (registration is null)
        {
            return Result<GetRegistrationByIdResponse>.Failure(
                Error.NotFound(
                    "Registration.NotFound",
                    $"Registration with ID '{query.Id}' was not found."));
        }

        return Result<GetRegistrationByIdResponse>.Success(
            new GetRegistrationByIdResponse(
                registration.Id,
                registration.RegistrationNumber,
                registration.RegistrationDate,
                registration.ParticipantName,
                registration.ContactNumber,
                registration.Email,
                registration.Address,
                registration.CarMakeModel,
                registration.PlateNumber,
                registration.RegistrationType,
                registration.Status,
                registration.RegistrationFee,
                registration.AmountPaid,
                registration.PaymentStatus));
    }
}
