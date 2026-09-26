using Bank.Domain.Exceptions;
using w13_Quiz.Contracts;
using w13_Quiz.Entities;

namespace Bank.Application.Services
{

    public class TransactionService : ITransactionService
    {
        private readonly ICardRepository _cardRepo;
        private readonly ITransactionRepository _transactionRepo;
        private const decimal DailyTransferLimit = 20_000m;
        private const decimal HighFeeRate = 1.5m;
        private const decimal LowFeeRate = 0.5m;
        private const decimal FeeThreshold = 10_000m;

        public TransactionService(
            ICardRepository cardRepo,
            ITransactionRepository transactionRepo)
        {
            _cardRepo = cardRepo;
            _transactionRepo = transactionRepo;
        }
        public void Transfer(string sourceCardNumber, string destinationCardNumber, decimal amount)
        {
            if (amount <= 0)
            {
                throw new BusinessRuleException(
                    "Transfer amount must be greater than zero.");
            }

            var sourceCard =
                _cardRepo.GetByCardNumber(sourceCardNumber);

            var destinationCard =
                _cardRepo.GetByCardNumber(destinationCardNumber);

            var transaction = new Transaction(
                sourceCardNumber,
                destinationCardNumber,
                amount);

            try
            {
                if (!sourceCard.IsActive)
                {
                    throw new BusinessRuleException(
                        "Source card is blocked.");
                }

                if (!destinationCard.IsActive)
                {
                    throw new BusinessRuleException(
                        "Destination card is blocked.");
                }

                if (sourceCard.Balance < amount)
                {
                    throw new BusinessRuleException(
                        "Source card does not have enough balance.");
                }

                EnsureDailyTransferLimit(sourceCardNumber,amount);
                    
                TransferBalance(sourceCard, destinationCard, amount);

                transaction.MarkAsSuccessful();

                _transactionRepo.Add(transaction);
            }
            catch (BusinessRuleException)
            {
                transaction.MarkAsFailed();

                _transactionRepo.Add(transaction);

                throw;
            }
        }

        private void TransferBalance(Card sourceCard, Card destinationCard, decimal amount)
        {

            var fee = CalculateTransferFee(amount);

            sourceCard.DecreaseBalance(amount + fee);

            destinationCard.IncreaseBalance(amount);
        }

        private decimal CalculateTransferFee(decimal amount)
        {
            var feeRate = amount < FeeThreshold? LowFeeRate : HighFeeRate;

            return amount * feeRate / 100;
        }
        private void EnsureDailyTransferLimit(string sourceCardNumber, decimal amount)

        {
            var todayTransferredAmount =
                _transactionRepo.GetTodayTransferredAmount(sourceCardNumber);

            if (todayTransferredAmount + amount > DailyTransferLimit)
            {
                throw new BusinessRuleException(
                    "Daily transfer limit exceeded.");
            }
        }


        public string? GetHolderNameByCardNumber(string cardNumber)
        {
            var holderName = _cardRepo.GetHolderNameByCardNumber(cardNumber);

            if (holderName is null)
                throw new NotFoundException("Destination card not found.");

            return holderName;
        }
    }
}
