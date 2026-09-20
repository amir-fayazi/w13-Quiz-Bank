using Bank.Application.Services;
using Bank.Infrastructure.Data;
using Bank.Infrastructure.Repositories;
using Bank.Presentation;

using var context = new AppDbContext();

var cardRepository = new CardRepository(context);
var transactionRepository = new TransactionRepository(context);

var cardService = new CardService(
    cardRepository,
    transactionRepository);

var transactionService = new TransactionService(
    cardRepository,
    transactionRepository);

var authenticationService = new CardAuthenticationService(
    cardRepository);

var app = new BankConsoleApp(
    cardService,
    transactionService,
    authenticationService);

app.Run();