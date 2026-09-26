
using Bank.Domain.Exceptions;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using w13_Quiz.Contracts;
using w13_Quiz.DTOs;
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
            var card = _context.Cards
                .FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
                throw new NotFoundException($"Card with number: {cardNumber} not found.");

            return card;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public decimal GetDailyWithrow(string cardNumber)
        {
            return _context.Cards
                .Where(x => x.CardNumber == cardNumber)
                .SelectMany(x => x.SentTransactions)
                .Where(t => t.IsSuccessful && t.TransactionDate.Date == DateTime.Today)
                .Sum(t => t.Amount);
        }

        public CardDataDto GetCardData(string cardNumber)
        {
            var card = _context.Cards
                 .AsNoTracking()
                 .Where(x => x.CardNumber == cardNumber)
                 .Select(x => new CardDataDto()
                 {
                     CardNumber = x.CardNumber,
                     HolderName = x.HolderName,
                     Balance = x.Balance,
                     IsActive = x.IsActive,
                     Password = x.Password,
                     FailedPasswordAttempts = x.FailedPasswordAttempts
                 })
                 .FirstOrDefault();

            if (card is null)
                throw new NotFoundException($"Card with number: {cardNumber} not found.");

            return card;
        }

        public void ChangePassword(string cardNumber, string newPassword)
        {
            var affectedRows = _context.Cards
                .Where(x => x.CardNumber == cardNumber)
                .ExecuteUpdate(setters =>
                    setters.SetProperty(
                        x => x.Password,
                        newPassword));

            if (affectedRows == 0)
            {
                throw new NotFoundException(
                    $"Card with number {cardNumber} was not found.");
            }
        }
    }
}
