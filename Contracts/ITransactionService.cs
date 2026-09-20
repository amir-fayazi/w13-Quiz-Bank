
using w13_Quiz.Entities;

namespace w13_Quiz.Contracts
{
    public interface ITransactionService
    {
        void Transfer(string sourseCardNumber, string destinationCardNumber, decimal amount);

    }
}
