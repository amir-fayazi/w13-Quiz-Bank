public class ReceivedTransactionDto
{
    public int TransactionId { get; set; }

    public string SourceCardNumber { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public bool IsSuccessful { get; set; }
}