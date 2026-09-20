using Bank.Domain.Exceptions;
using w13_Quiz.Contracts;

namespace Bank.Application.Services
{
    public class CardAuthenticationService : ICardAuthenticationService
    {

        private readonly ICardRepository _cardRepo;
       
        public CardAuthenticationService(ICardRepository cardRepo)
        {
            _cardRepo = cardRepo;
          
        }

        public void Authenticate(string cardNumber, string password)
        {
            var card = _cardRepo.GetByCardNumber(cardNumber);

            if (!card.IsActive)
                throw new BusinessRuleException(
                    "This card is blocked.");

            if (card.Password != password)
            {
                card.RegisterFailedPasswordAttempt();
                _cardRepo.Update(card);

                throw new InvalidCredentialsException(
                   "Card number or password is incorrect.");
            }

            card.ResetFailedPasswordAttempts();

            _cardRepo.Update(card);
        }
    }
}
