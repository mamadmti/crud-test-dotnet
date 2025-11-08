using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.DateOfBirth)
            .IsRequired()
            .HasColumnType("date");

        // Phone number stored as varchar E.164 format (minimal space: +14155552671)
        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion(
                v => v.Value,
                v => new PhoneNumber(v))
            .HasColumnName("PhoneNumber")
            .HasColumnType("varchar(20)");

        // Email stored as lowercase varchar
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255)
            .HasConversion(
                v => v.Value,
                v => new Email(v))
            .HasColumnName("Email")
            .HasColumnType("varchar(255)");

        // Bank account stored as normalized uppercase varchar
        builder.Property(c => c.BankAccountNumber)
            .IsRequired()
            .HasMaxLength(34)
            .HasConversion(
                v => v.Value,
                v => new BankAccountNumber(v))
            .HasColumnName("BankAccountNumber")
            .HasColumnType("varchar(34)");

        // Indexes for uniqueness checks
        builder.HasIndex(c => c.Email)
            .IsUnique()
            .HasDatabaseName("IX_Customers_Email");

        builder.HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth })
            .HasDatabaseName("IX_Customers_Name_DOB");
    }
}

