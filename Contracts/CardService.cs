

namespace w13_Quiz.Contracts
{
    public interface ICardService
    {
        void CreateCard(string cardNumber,  string holderName,  string password, float balance);

        IEnumerable<ReceivedTransactionDto> GetCardReceivedTransactions(string cardNumber);
        IEnumerable<SentTransactionDto> GetCardSentTransactions(string cardNumber);

        void DeactivateCard(string cardNumber);



    }
}
