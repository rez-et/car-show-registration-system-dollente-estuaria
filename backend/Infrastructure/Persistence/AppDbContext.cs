namespace Infrastructure.Persistence;

using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Registration> Registrations => Set<Registration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(20);
            entity.HasIndex(e => e.RegistrationNumber)
                .IsUnique();

            entity.Property(e => e.RegistrationDate)
                .IsRequired()
                .HasConversion(
                    v => v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            entity.Property(e => e.ParticipantName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.ContactNumber)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(e => e.CarMakeModel)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PlateNumber)
                .IsRequired()
                .HasMaxLength(15);
            entity.HasIndex(e => e.PlateNumber)
                .IsUnique();

            entity.Property(e => e.RegistrationType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.RegistrationFee)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(e => e.AmountPaid)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Ignore(e => e.PaymentStatus);
        });
    }
}
