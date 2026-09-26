

namespace w13_Quiz.Contracts
{
    public interface ICardService

    {
        void CreateCard(string cardNumber,  string holderName,  string password, decimal balance);

        IEnumerable<ReceivedTransactionDto> GetCardReceivedTransactions(string cardNumber);
        IEnumerable<SentTransactionDto> GetCardSentTransactions(string cardNumber);
        void ChangeCardPassword(string cardNumber, string currentPassword, string newPassword);
    
    
   
    }
}
