using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w13_Quiz.Entities;

namespace Bank.Infrastructure.Data.Configurations
{
    public class TransactionConfiguration
        : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasKey(x => x.TransactionId);

            builder
                .Property(x => x.SourceCardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(x => x.DestinationCardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(x => x.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(x => x.TransactionDate)
                .IsRequired();

            builder
                .Property(x => x.IsSuccessful)
                .IsRequired();

            builder
                .HasIndex(x => x.SourceCardNumber);

            builder
                .HasIndex(x => x.DestinationCardNumber);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Transaction_Amount_Positive",
                    "[Amount] > 0");

                t.HasCheckConstraint(
                    "CK_Transaction_DifferentCards",
                    "[SourceCardNumber] <> [DestinationCardNumber]");
            });

            SeedTransactions(builder);
        }

        private static void SeedTransactions(
            EntityTypeBuilder<Transaction> builder)
        {
            var cards = new[]
            {
                "1000000000000001",
                "1000000000000002",
                "1000000000000003",
                "1000000000000004",
                "1000000000000005",
                "1000000000000006",
                "1000000000000007",
                "1000000000000008"
            };

            var transactions = new List<object>();

            var startDate =
                new DateTime(
                    2026, 9, 1,
                    8, 0, 0,
                    DateTimeKind.Utc);

            for (int i = 1; i <= 80; i++)
            {
                var sourceIndex = (i - 1) % cards.Length;

                var offset = (i % 7) + 1;

                var destinationIndex =
                    (sourceIndex + offset) % cards.Length;

                transactions.Add(new
                {
                    TransactionId = i,

                    SourceCardNumber =
                        cards[sourceIndex],

                    DestinationCardNumber =
                        cards[destinationIndex],

                    Amount =
                        100m + ((i % 20) * 75m),

                    TransactionDate =
                        startDate.AddHours(i * 6),

                    IsSuccessful =
                        i % 10 != 0
                });
            }

            builder.HasData(transactions.ToArray());
        }
    }
}