public class Transaction
{
    public char Type { get; set; } // Тип операции (+ или -)
    public decimal Amount { get; set; } // Сумма транзакции

    public Transaction(char type, decimal amount)
    {
        Type = type;
        Amount = amount;
    }
}