
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

            builder
                .Property(x => x.Balance)
                .HasPrecision(18, 2);
                
        }
    }
}
