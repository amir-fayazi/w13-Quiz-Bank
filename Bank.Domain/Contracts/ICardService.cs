

namespace w13_Quiz.Contracts
{
    public interface 

    {
        void CreateCard(string cardNumber,  string holderName,  string password, decimal balance);

        IEnumerable<ReceivedTransactionDto> GetCardReceivedTransactions(string cardNumber);
        IEnumerable<SentTransactionDto> GetCardSentTransactions(string cardNumber);

    }
}
