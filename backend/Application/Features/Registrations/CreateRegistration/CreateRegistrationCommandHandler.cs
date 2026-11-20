namespace Application.Features.Registrations.CreateRegistration;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public sealed class CreateRegistrationCommandHandler(IAppDbContext dbContext)
    : ICommandHandler<CreateRegistrationCommand, Result<CreateRegistrationResponse>>
{
    public async Task<Result<CreateRegistrationResponse>> HandleAsync(
        CreateRegistrationCommand command,
        CancellationToken cancellationToken = default)
    {
        var normalizedPlate = command.PlateNumber.Trim().ToUpperInvariant();

        var plateExists = await dbContext.Registrations
            .AnyAsync(r => r.PlateNumber.ToUpper() == normalizedPlate, cancellationToken);

        if (plateExists)
        {
            return Result<CreateRegistrationResponse>.Failure(
                Error.Conflict(
                    "Registration.Conflict",
                    $"A registration with plate number '{normalizedPlate}' already exists."));
        }

        var registrationType = command.RegistrationType!.Value;
        var registrationFee = Registration.GetFeeForType(registrationType);
        var nextRegistrationNumber = await GenerateNextRegistrationNumberAsync(cancellationToken);

        var registration = new Registration
        {
            Id = Guid.NewGuid(),
            RegistrationNumber = nextRegistrationNumber,
            RegistrationDate = DateTime.UtcNow,
            ParticipantName = command.ParticipantName.Trim(),
            ContactNumber = command.ContactNumber.Trim(),
            Email = command.Email.Trim(),
            Address = command.Address.Trim(),
            CarMakeModel = command.CarMakeModel.Trim(),
            PlateNumber = normalizedPlate,
            RegistrationType = registrationType,
            Status = RegistrationStatus.Pending,
            RegistrationFee = registrationFee,
            AmountPaid = command.AmountPaid
        };

        dbContext.Registrations.Add(registration);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<CreateRegistrationResponse>.Success(
            new CreateRegistrationResponse(
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

    private async Task<string> GenerateNextRegistrationNumberAsync(CancellationToken cancellationToken)
    {
        var existingNumbers = await dbContext.Registrations
            .Select(r => r.RegistrationNumber)
            .ToListAsync(cancellationToken);

        var maxSequence = 0;
        foreach (var number in existingNumbers)
        {
            if (number.StartsWith("CS27-", StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(number[5..], out var parsed) &&
                parsed > maxSequence)
            {
                maxSequence = parsed;
            }
        }

        return $"CS27-{(maxSequence + 1):D4}";
    }
}
