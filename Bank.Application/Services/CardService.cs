using LibraryManagement.Domain.Exceptions;
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

    public void DeactivateCard(string cardNumber)
    {
        var card = _cardRepo.GetByCardNumber(cardNumber);

        card.Deactivate();

        _cardRepo.Update(card);
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
}