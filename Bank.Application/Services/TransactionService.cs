using LibraryManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using w13_Quiz.Contracts;
using w13_Quiz.Entities;

namespace Bank.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ICardRepository _cardRepo;
        private readonly ITransactionRepository _transactionRepo;

        public TransactionService(
            ICardRepository cardRepo,
            ITransactionRepository transactionRepo)
        {
            _cardRepo = cardRepo;
            _transactionRepo = transactionRepo;
        }
        public void Transfer(string sourceCardNumber, string destinationCardNumber, decimal amount)
        {

            var sourceCard = _cardRepo.GetByCardNumber(sourceCardNumber);
            var destinationCard = _cardRepo.GetByCardNumber(destinationCardNumber);

            if (amount <= 0)
                throw new BusinessRuleException(
                    "Transfer amount must be greater than zero.");

            if (sourceCard.Balance < amount)
                throw new BusinessRuleException(
                    "Source card does not have enough balance.");



            TransferBalance(sourceCard, destinationCard, amount);


            var transaction = new w13_Quiz.Entities.Transaction(sourceCardNumber, destinationCardNumber, amount);

            transaction.MarkAsSuccessful();

            _transactionRepo.Add(transaction);
        }

        private void TransferBalance(Card sourceCard, Card destinationCard, decimal amount)
        {
            sourceCard.DecreaseBalance(amount);
            destinationCard.IncreaseBalance(amount);
        }
    }
}
