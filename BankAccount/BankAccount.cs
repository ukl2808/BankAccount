using Newtonsoft.Json;

public class BankAccount
{
    public string AccountNumber { get; }
    public string OwnerName { get; }
    public decimal Balance { get; private set; }
    private List<Transaction> transactions; // История операций
    private decimal InitialBalance; // Начальный баланс (баланс на момент создания счета)

    public BankAccount(string accountNumber, string ownerName, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        OwnerName = ownerName;
        InitialBalance = initialBalance;
        transactions = new List<Transaction>(); // Инициализация списка операций
        LoadTransactionHistory(); // Load transaction history during initialization
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        transactions.Add(new Transaction('+', amount));
        SaveTransactionHistory(); // Save transaction history after each transaction
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            transactions.Add(new Transaction('-', amount));
            SaveTransactionHistory(); // Save transaction history after each transaction
            return true;
        }
        return false;
    }

    public bool TransferTo(BankAccount destinationAccount, decimal amount)
    {
        if (Withdraw(amount))
        {
            destinationAccount.Deposit(amount);
            transactions.Add(new Transaction('-', amount));
            transactions.Add(new Transaction('+', amount));
            return true;
        }
        return false;
    }

    public string GetTransactionHistory()
    {
        List<string> transactionStrings = new List<string>();
        foreach (Transaction transaction in transactions)
        {
            string transactionType = transaction.Type == '+' ? "Пополнение" : "Снятие";
            transactionStrings.Add($"{transactionType}: {transaction.Amount:C}");
        }
        return string.Join(Environment.NewLine, transactionStrings);
    }

    public override string ToString()
    {
        return $"Account Number: {AccountNumber}, Owner: {OwnerName}, Balance: {Balance:C}";
    }

    public void LoadTransactionHistory()
    {
        string transactionsFilePath = $"{AccountNumber}_transactions.json";
        if (File.Exists(transactionsFilePath))
        {
            string jsonData = File.ReadAllText(transactionsFilePath);
            transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonData);

            // Update the current balance based on the transaction history
            Balance = InitialBalance;
            foreach (var transaction in transactions)
            {
                if (transaction.Type == '+')
                {
                    Balance += transaction.Amount;
                }
                else if (transaction.Type == '-')
                {
                    Balance -= transaction.Amount;
                }
            }
        }
        else
        {
            transactions = new List<Transaction>();
        }
    }

    public void SaveTransactionHistory()
    {
        string transactionsFilePath = $"{AccountNumber}_transactions.json";
        string jsonData = JsonConvert.SerializeObject(transactions, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(transactionsFilePath, jsonData);
    }
}