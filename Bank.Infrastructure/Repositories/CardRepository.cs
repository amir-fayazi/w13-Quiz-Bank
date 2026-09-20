
using Bank.Infrastructure.Data;
using Bank.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using w13_Quiz.Contracts;
using w13_Quiz.Entities;

namespace Bank.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
        }

        public bool Exists(string cardNumber)
        {
            return _context.Cards.Any(x => x.CardNumber == cardNumber);
        }

        public Card GetByCardNumber(string cardNumber)
        {
            var card =  _context.Cards
                .FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
                throw new NotFoundException($"Card with number: {cardNumber} not found.");

            return card;
        }

        public void Update(Card updatedCard)
        {
            var card = GetByCardNumber(updatedCard.CardNumber);

            card.UpdateBalance(updatedCard.Balance);
            card.UpdateActiveStatus(updatedCard.IsActive);

            _context.SaveChanges();
        }
    }
}
