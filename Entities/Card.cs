using LibraryManagement.Domain.Exceptions;

namespace w13_Quiz.Entities
{
    public class Card
    {
        
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; } = true;
        public string Password { get; set; }

        public ICollection<Transaction> SentTransactions { get; set; }
            = [];

        public ICollection<Transaction> ReceivedTransactions { get; set; }
            = [];

        private Card()
        {
        }

        public Card(
            string cardNumber,
            string holderName,
            string password,
            float balance)
        {
            ValidateCardNumber(cardNumber);
            ValidateHolderName(holderName);
            ValidatePassword(password);
            ValidateBalance(balance);

            CardNumber = cardNumber;
            HolderName = holderName;
            Password = password;
            Balance = balance;
        }

        private void ValidateCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) ||
                cardNumber.Length != 16 ||
                !cardNumber.All(char.IsDigit))
            {
                throw new ValidationException(
                    "Card number must be exactly 16 digits.");
            }
        }

        private void ValidateHolderName(string holderName)
        {
            if (string.IsNullOrWhiteSpace(holderName))
            {
                throw new ValidationException(
                    "Holder name cannot be empty.");
            }
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

        private void ValidateBalance(float balance)
        {
            if (balance < 0)
            {
                throw new ValidationException(
                    "Balance cannot be negative.");
            }
        }
    }
}