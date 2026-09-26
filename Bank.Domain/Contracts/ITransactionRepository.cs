
using w13_Quiz.Entities;

namespace w13_Quiz.Contracts
{
    public interface ITransactionRepository
    {
        IEnumerable<SentTransactionDto> GetSentTransactions(string cardNumber);
        IEnumerable<ReceivedTransactionDto> GetReceivedTransactions(string cardNumber);

        Transaction Add(Transaction transaction);

        decimal GetTodayTransferredAmount(string cardNumber);
    }
}
