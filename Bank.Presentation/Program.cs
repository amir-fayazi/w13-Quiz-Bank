using Bank.Application.Services;
using Bank.Infrastructure.Data;
using Bank.Infrastructure.Repositories;
using Bank.Presentation;
using Newtonsoft.Json;
using w13_Quiz.DTOs;

using var context = new AppDbContext();

var cardRepository = new CardRepository(context);
var transactionRepository = new TransactionRepository(context);

var rootPath = FindProjectRoot();
var filePath = Path.Combine( rootPath,"verification-codes.json");
var verificationCodeRepository = new VerificationCodeRepository(filePath);

var cardService = new CardService(
    cardRepository,
    transactionRepository);

var transactionService = new TransactionService(
    cardRepository,
    transactionRepository,
    verificationCodeRepository);

var authenticationService = new CardAuthenticationService(
    cardRepository);

var app = new BankConsoleApp(
    cardService,
    transactionService,
    authenticationService);

//app.Run();


Console.WriteLine("source card: ");
var sCard = Console.ReadLine();

Console.WriteLine("password");
var password = Console.ReadLine();

Console.WriteLine("des card: ");
var dCard = Console.ReadLine();

try
{
    string name = transactionService.GetHolderNameByCardNumber(dCard);
    Console.WriteLine(name);
    Console.WriteLine("Mikhay edame bedi(0/1): ");
    var userChoice = Console.ReadLine();
    if (userChoice == "0")
        return;
    var id = transactionService.GenerateVerificationCode();

    Console.WriteLine("Enter verify code: ");
    var userCode = Console.ReadLine();

    transactionService.VerificationCode(id, userCode);

    transactionService.Transfer(sCard, dCard, 2000);

    Console.WriteLine("trasfer success");
}
catch (Exception)
{

    throw;
}


static string FindProjectRoot()
{
    var directory = new DirectoryInfo(AppContext.BaseDirectory);


    while (directory is not null)
    {
        if (directory.GetFiles("*.sln*").Any())
        {
            return directory.FullName;
        }

        directory = directory.Parent;
    }

    throw new DirectoryNotFoundException("Project root was not found.");

}