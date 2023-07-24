using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value));

        builder.Property(c => c.FirstName).HasMaxLength(100);

        builder.Property(c => c.LastName).HasMaxLength(100);

        builder.Property(c => c.Email).HasMaxLength(255);

        builder.Property(c => c.Status)
            .HasMaxLength(25)
            .HasConversion<string>();

        builder.HasIndex(c => c.Email)
            .IsUnique();
    }
}

