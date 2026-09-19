using LibraryManagement.Domain.Exceptions;

namespace w13_Quiz.Entities
{
    public class Transaction
    {
        public int TransactionId { get; private set; }

        public string SourceCardNumber { get; private set; } = null!; // FK
        public Card SourceCard { get; private set; } = null!;

        public string DestinationCardNumber { get; private set; } = null!; // FK
        public Card DestinationCard { get; private set; } = null!;

        public decimal Amount { get; private set; }

        public DateTime TransactionDate { get; private set; }
            = DateTime.UtcNow;

        public bool IsSuccessful { get; private set; }

        private Transaction()
        {
        }

        public Transaction(
            string sourceCardNumber,
            string destinationCardNumber,
            decimal amount)
        {
            ValidateCardNumber(sourceCardNumber, "Source card number");
            ValidateCardNumber(destinationCardNumber, "Destination card number");
            ValidateDifferentCards(sourceCardNumber, destinationCardNumber);
            ValidateAmount(amount);

            SourceCardNumber = sourceCardNumber;
            DestinationCardNumber = destinationCardNumber;
            Amount = amount;
        }

        private void ValidateCardNumber(string cardNumber, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) ||
                cardNumber.Length != 16 ||
                !cardNumber.All(char.IsDigit))
            {
                throw new ValidationException(
                    $"{fieldName} must be exactly 16 digits.");
            }
        }

        private void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ValidationException(
                    "Transaction amount must be greater than zero.");
            }
        }

        private void ValidateDifferentCards(string sourceCardNumber,string destinationCardNumber)
        {
            if (sourceCardNumber == destinationCardNumber)
            {
                throw new BusinessRuleException(
                    "Source and destination cards cannot be the same.");
            }
        }

        public void MarkAsSuccessful()
        {
            IsSuccessful = true;
        }
    }
}