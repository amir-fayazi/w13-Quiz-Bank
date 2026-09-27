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

app.Run();



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