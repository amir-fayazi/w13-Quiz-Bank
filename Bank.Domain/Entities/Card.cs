using Bank.Domain.Exceptions;

namespace w13_Quiz.Entities
{
    public class Card
    {

        public string CardNumber { get; private set; } = null!;
        public string HolderName { get; private set; } = null!;
        public decimal Balance { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string Password { get; private set; } = null!;
        public int FailedPasswordAttempts { get; private set; }

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
            decimal balance)
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

        private void ValidateBalance(decimal balance)
        {
            if (balance < 0)
            {
                throw new ValidationException(
                    "Balance cannot be negative.");
            }
        }

        public void DecreaseBalance(decimal amount)
        {
            Balance -= amount;
        }

        public void IncreaseBalance(decimal amount)
        {
            Balance += amount;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void UpdateBalance(decimal balance)
        {
            ValidateBalance(balance);
                
            Balance = balance;
        }

        public void UpdateActiveStatus(bool isActive)
        {
            IsActive = isActive;
        }
        public void RegisterFailedPasswordAttempt()
        {
            FailedPasswordAttempts++;

            if (FailedPasswordAttempts >= 3)
                Deactivate();
        }

        public void ResetFailedPasswordAttempts()
        {
            FailedPasswordAttempts = 0;
        }


        public void ChangePassword(string newPassword)
        {
            ValidatePassword(newPassword);
            Password = newPassword;
        }

    }
}
