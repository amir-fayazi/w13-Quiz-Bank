
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w13_Quiz.Entities;

namespace Bank.Infrastructure.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
            .HasKey(x => x.TransactionId);

            builder
                .Property(x => x.SourceCardNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder
                .Property(x => x.DestinationCardNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder
                .Property(x => x.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .ToTable(t =>
                    t.HasCheckConstraint(
                "CK_Transaction_Amount_Positive",
                "[Amount] > 0"));
        }
    }
}
