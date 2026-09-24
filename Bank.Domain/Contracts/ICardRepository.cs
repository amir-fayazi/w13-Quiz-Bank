
using w13_Quiz.Entities;

namespace w13_Quiz.Contracts
{
    public interface ICardRepository
    {
        bool Exists(string cardNumber);

        Card GetByCardNumber(string cardNumber);

        void Add(Card card);

        void Update(Card card);

        void SaveChanges();
    }
}
