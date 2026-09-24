
using Bank.Infrastructure.Data;
using w13_Quiz.Entities;
using w13_Quiz.Contracts;

namespace Bank.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public Transaction Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public IEnumerable<ReceivedTransactionDto> GetReceivedTransactions(string cardNumber)
        {
            return [.._context.Transactions
                .Where(x => x.DestinationCardNumber == cardNumber)
                .Select(x => new ReceivedTransactionDto
                {
                    TransactionId = x.TransactionId,
                    SourceCardNumber = x.SourceCardNumber,
                    Amount = x.Amount,
                    IsSuccessful = x.IsSuccessful,
                    TransactionDate = x.TransactionDate,
                    
                })];
        }

        public IEnumerable<SentTransactionDto> GetSentTransactions(string cardNumber)
        {
            return [.._context.Transactions
                .Where(x => x.SourceCardNumber == cardNumber)
                .Select(x => new SentTransactionDto
                {
                    TransactionId = x.TransactionId,
                    DestinationCardNumber = x.DestinationCardNumber,
                    Amount = x.Amount,
                    IsSuccessful = x.IsSuccessful,
                    TransactionDate = x.TransactionDate,

                })];
        }

        public decimal GetTodayTransferredAmount(string cardNumber)
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            return _context.Transactions
                .Where(x =>
                    x.SourceCardNumber == cardNumber &&
                    x.IsSuccessful &&
                    x.TransactionDate >= today &&
                    x.TransactionDate < tomorrow)
                .Sum(x => (decimal?)x.Amount) ?? 0m;
        }
    }
}
