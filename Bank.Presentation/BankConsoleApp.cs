using ADO.NetDemoConsoleApp;
using Bank.Domain.Exceptions;
using w13_Quiz.Contracts;

namespace Bank.Presentation;

public class BankConsoleApp
{
    private readonly ICardService _cardService;
    private readonly ITransactionService _transactionService;
    private readonly ICardAuthenticationService _authenticationService;

    public BankConsoleApp(
        ICardService cardService,
        ITransactionService transactionService,
        ICardAuthenticationService authenticationService)
    {
        _cardService = cardService;
        _transactionService = transactionService;
        _authenticationService = authenticationService;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== BANK SYSTEM =====");
            Console.WriteLine();
            Console.WriteLine("1. Transfer Money");
            Console.WriteLine("2. Create Card");
            Console.WriteLine("3. Sent Transactions");
            Console.WriteLine("4. Received Transactions");
            Console.WriteLine("0. Exit");
            Console.WriteLine();

            Console.Write("Select: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Transfer();
                    break;

                case "2":
                    CreateCard();
                    break;

                case "3":
                    ShowSentTransactions();
                    break;

                case "4":
                    ShowReceivedTransactions();
                    break;

                case "0":
                    return;

                default:
                    ConsoleUi.ShowError("Invalid option.");
                    ConsoleUi.Pause();
                    break;
            }
        }
    }

    private void Transfer()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== TRANSFER =====");
            Console.WriteLine();

            var sourceCardNumber =
                ConsoleUi.ReadCardNumberOrBack("Source card number");

            if (sourceCardNumber is null)
                return;

            if (!Authenticate(sourceCardNumber))
                return;

            var destinationCardNumber =
                ConsoleUi.ReadCardNumberOrBack("Destination card number");

            if (destinationCardNumber is null)
                return;

            var amount =
                ConsoleUi.ReadPositiveAmountOrBack("Amount");

            if (amount is null)
                return;

            try
            {
                _transactionService.Transfer(
                    sourceCardNumber,
                    destinationCardNumber,
                    amount.Value);

                ConsoleUi.ShowSuccess(
                    "Transfer completed successfully.");

                ConsoleUi.Pause();
                return;
            }
            catch (NotFoundException ex)
            {
                ConsoleUi.ShowError(ex.Message);
            }
            catch (BusinessRuleException ex)
            {
                ConsoleUi.ShowError(ex.Message);
            }
            catch (ValidationException ex)
            {
                ConsoleUi.ShowError(ex.Message);
            }

            if (!ConsoleUi.AskRetry())
                return;
        }
    }

    private bool Authenticate(string cardNumber)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== AUTHENTICATION =====");
            Console.WriteLine();
            Console.WriteLine($"Card: {cardNumber}");
            Console.WriteLine();

            var password =
                ConsoleUi.ReadPasswordOrBack("Password");

            if (password is null)
                return false;

            try
            {
                _authenticationService.Authenticate(
                    cardNumber,
                    password);

                ConsoleUi.ShowSuccess(
                    "Authentication successful.");

                return true;
            }
            catch (InvalidCredentialsException ex)
            {
                ConsoleUi.ShowError(ex.Message);

                if (!ConsoleUi.AskRetry())
                    return false;
            }
            catch (NotFoundException ex)
            {
                ConsoleUi.ShowError(ex.Message);
                ConsoleUi.Pause();

                return false;
            }
            catch (BusinessRuleException ex)
            {
                ConsoleUi.ShowError(ex.Message);
                ConsoleUi.Pause();

                return false;
            }
        }
    }

    private void CreateCard()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== CREATE CARD =====");
            Console.WriteLine();

            var cardNumber =
                ConsoleUi.ReadCardNumberOrBack("Card number");

            if (cardNumber is null)
                return;

            var holderName =
                ConsoleUi.ReadRequiredTextOrBack("Holder name");

            if (holderName is null)
                return;

            var password =
                ConsoleUi.ReadPasswordOrBack("Password");

            if (password is null)
                return;

            var balance =
                ConsoleUi.ReadDecimalOrBack("Initial balance");

            if (balance is null)
                return;

            try
            {
                _cardService.CreateCard(
                    cardNumber,
                    holderName,
                    password,
                    balance.Value);

                ConsoleUi.ShowSuccess(
                    "Card created successfully.");

                ConsoleUi.Pause();
                return;
            }
            catch (DuplicateException ex)
            {
                ConsoleUi.ShowError(ex.Message);
            }
            catch (ValidationException ex)
            {
                ConsoleUi.ShowError(ex.Message);
            }

            if (!ConsoleUi.AskRetry())
                return;
        }
    }

    private void ShowSentTransactions()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== SENT TRANSACTIONS =====");
            Console.WriteLine();

            var cardNumber =
                ConsoleUi.ReadCardNumberOrBack("Card number");

            if (cardNumber is null)
                return;

            try
            {
                var transactions = _cardService
                    .GetCardSentTransactions(cardNumber)
                    .ToList();

                Console.Clear();

                if (transactions.Count == 0)
                {
                    Console.WriteLine(
                        "No sent transactions found.");
                }
                else
                {
                    ConsolePainter.WriteTable(
                        transactions,
                        ConsoleColor.Blue,
                        ConsoleColor.White);
                }

                ConsoleUi.Pause();
                return;
            }
            catch (NotFoundException ex)
            {
                ConsoleUi.ShowError(ex.Message);

                if (!ConsoleUi.AskRetry())
                    return;
            }
        }
    }

    private void ShowReceivedTransactions()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===== RECEIVED TRANSACTIONS =====");
            Console.WriteLine();

            var cardNumber =
                ConsoleUi.ReadCardNumberOrBack("Card number");

            if (cardNumber is null)
                return;

            try
            {
                var transactions = _cardService
                    .GetCardReceivedTransactions(cardNumber)
                    .ToList();

                Console.Clear();

                if (transactions.Count == 0)
                {
                    Console.WriteLine(
                        "No received transactions found.");
                }
                else
                {
                    ConsolePainter.WriteTable(
                        transactions,
                        ConsoleColor.Blue,
                        ConsoleColor.White);
                }

                ConsoleUi.Pause();
                return;
            }
            catch (NotFoundException ex)
            {
                ConsoleUi.ShowError(ex.Message);

                if (!ConsoleUi.AskRetry())
                    return;
            }
        }
    }
}