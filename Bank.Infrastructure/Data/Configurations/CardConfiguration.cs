using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w13_Quiz.Entities;

namespace Bank.Infrastructure.Data.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder
                .HasKey(x => x.CardNumber);

            builder
                .Property(x => x.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(x => x.HolderName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(x => x.Balance)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder
                .Property(x => x.FailedPasswordAttempts)
                .HasDefaultValue(0)
                .IsRequired();

            builder
                .HasMany(x => x.SentTransactions)
                .WithOne(x => x.SourceCard)
                .HasForeignKey(x => x.SourceCardNumber)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.ReceivedTransactions)
                .WithOne(x => x.DestinationCard)
                .HasForeignKey(x => x.DestinationCardNumber)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Card_Balance_NonNegative",
                    "[Balance] >= 0");

                t.HasCheckConstraint(
                    "CK_Card_FailedPasswordAttempts",
                    "[FailedPasswordAttempts] >= 0 AND [FailedPasswordAttempts] <= 3");

                t.HasCheckConstraint(
                    "CK_Card_CardNumber_Valid",
                    "LEN([CardNumber]) = 16 AND [CardNumber] NOT LIKE '%[^0-9]%'");

                t.HasCheckConstraint(
                    "CK_Card_Password_Valid",
                    "LEN([Password]) = 4 AND [Password] NOT LIKE '%[^0-9]%'");
            });

            SeedCards(builder);
        }

        private static void SeedCards(EntityTypeBuilder<Card> builder)
        {
            builder.HasData(
                new
                {
                    CardNumber = "1000000000000001",
                    HolderName = "Amir Fayazi",
                    Balance = 25000m,
                    IsActive = true,
                    Password = "1111",
                    FailedPasswordAttempts = 0
                },
                new
                {
                    CardNumber = "1000000000000002",
                    HolderName = "Sara Ahmadi",
                    Balance = 40000m,
                    IsActive = true,
                    Password = "2222",
                    FailedPasswordAttempts = 0
                },
                new
                {
                    CardNumber = "1000000000000003",
                    HolderName = "Reza Mohammadi",
                    Balance = 18000m,
                    IsActive = true,
                    Password = "3333",
                    FailedPasswordAttempts = 1
                },
                new
                {
                    CardNumber = "1000000000000004",
                    HolderName = "Neda Karimi",
                    Balance = 65000m,
                    IsActive = true,
                    Password = "4444",
                    FailedPasswordAttempts = 0
                },
                new
                {
                    CardNumber = "1000000000000005",
                    HolderName = "Ali Hosseini",
                    Balance = 12000m,
                    IsActive = true,
                    Password = "5555",
                    FailedPasswordAttempts = 2
                },
                new
                {
                    CardNumber = "1000000000000006",
                    HolderName = "Mina Moradi",
                    Balance = 90000m,
                    IsActive = true,
                    Password = "6666",
                    FailedPasswordAttempts = 0
                },
                new
                {
                    CardNumber = "1000000000000007",
                    HolderName = "Arman Rahimi",
                    Balance = 35000m,
                    IsActive = true,
                    Password = "7777",
                    FailedPasswordAttempts = 0
                },
                new
                {
                    CardNumber = "1000000000000008",
                    HolderName = "Leila Akbari",
                    Balance = 50000m,
                    IsActive = true,
                    Password = "8888",
                    FailedPasswordAttempts = 0
                },
                new
                {
                    CardNumber = "1000000000000009",
                    HolderName = "Blocked Source",
                    Balance = 30000m,
                    IsActive = false,
                    Password = "9999",
                    FailedPasswordAttempts = 3
                },
                new
                {
                    CardNumber = "1000000000000010",
                    HolderName = "Blocked Destination",
                    Balance = 45000m,
                    IsActive = false,
                    Password = "1010",
                    FailedPasswordAttempts = 3
                }
            );
        }
    }
}