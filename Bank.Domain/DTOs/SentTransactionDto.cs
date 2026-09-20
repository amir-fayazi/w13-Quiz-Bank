public class SentTransactionDto
{
    public int TransactionId { get; set; }

    public string DestinationCardNumber { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public bool IsSuccessful { get; set; }
}