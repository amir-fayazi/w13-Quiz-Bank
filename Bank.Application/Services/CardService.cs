using Bank.Domain.Exceptions;
using w13_Quiz.Contracts;
using w13_Quiz.Entities;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepo;
    private readonly ITransactionRepository _transactionRepo;

    public CardService(
        ICardRepository cardRepo,
        ITransactionRepository transactionRepo)
    {
        _cardRepo = cardRepo;
        _transactionRepo = transactionRepo;
    }

    public void CreateCard(
        string cardNumber,
        string holderName,
        string password,
        decimal balance)
    {
        if (_cardRepo.Exists(cardNumber))
            throw new DuplicateException(
                $"Card with number {cardNumber} already exists.");

        var card = new Card(
            cardNumber,
            holderName,
            password,
            balance);

        _cardRepo.Add(card);
    }

    public IEnumerable<ReceivedTransactionDto> GetCardReceivedTransactions(string cardNumber)

    {
        if (!_cardRepo.Exists(cardNumber))
            throw new NotFoundException(
                $"Card with number {cardNumber} not found.");

        return _transactionRepo
            .GetReceivedTransactions(cardNumber);
    }

    public IEnumerable<SentTransactionDto> GetCardSentTransactions(string cardNumber)
    {

        if (!_cardRepo.Exists(cardNumber))
            throw new NotFoundException(
                $"Card with number {cardNumber} not found.");

        return _transactionRepo
            .GetSentTransactions(cardNumber);
    }


    public void ChangeCardPassword(
    string cardNumber,
    string currentPassword,
    string newPassword)
    {
        var card = _cardRepo.GetCardData(cardNumber);
        
        if (!card.IsActive)
        {
            throw new BusinessRuleException(
                "This card is blocked.");
        }

        if (card.Password != currentPassword)
        {
            throw new InvalidCredentialsException(
                "Current password is incorrect.");
        }

        if (currentPassword == newPassword)
        {
            throw new BusinessRuleException(
                "New password must be different from the current password.");
        }

        ValidatePassword(newPassword);

        _cardRepo.ChangePassword(cardNumber, newPassword);
    }
    private void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) ||
            password.Length != 4 ||
            !password.All(char.IsDigit))
        {
            throw new ValidationException(
                "Password must be exactly 4 digits.");
        }
    }
}

    
