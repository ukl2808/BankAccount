using Newtonsoft.Json;

public class Bank
{
    private List<BankAccount> accounts;
    private string accountsFilePath = "bank_accounts.json"; // Файл для сохранения/загрузки данных

    public Bank()
    {
        LoadAccountsFromFile();
    }

    public void CreateAccount(string accountNumber, string ownerName, decimal initialBalance = 0)
    {
        BankAccount account = new BankAccount(accountNumber, ownerName, initialBalance);
        accounts.Add(account);
        SaveAccountsToFile();
    }

    public BankAccount GetAccountByNumber(string accountNumber)
    {
        return accounts.Find(account => account.AccountNumber == accountNumber);
    }

    public void Deposit(string accountNumber, decimal amount)
    {
        BankAccount account = GetAccountByNumber(accountNumber);
        if (account != null)
        {
            account.Deposit(amount);
            SaveAccountsToFile();
        }
        else
        {
            Console.WriteLine("Счет не найден.");
        }
    }

    public void Withdraw(string accountNumber, decimal amount)
    {
        BankAccount account = GetAccountByNumber(accountNumber);
        if (account != null)
        {
            if (account.Withdraw(amount))
            {
                SaveAccountsToFile();
            }
            else
            {
                Console.WriteLine("Недостаточно средств на счете.");
            }
        }
        else
        {
            Console.WriteLine("Счет не найден.");
        }
    }

    public void Transfer(string sourceAccountNumber, string destinationAccountNumber, decimal amount)
    {
        BankAccount sourceAccount = GetAccountByNumber(sourceAccountNumber);
        BankAccount destinationAccount = GetAccountByNumber(destinationAccountNumber);

        if (sourceAccount != null && destinationAccount != null)
        {
            if (sourceAccount.TransferTo(destinationAccount, amount))
            {
                SaveAccountsToFile();
            }
            else
            {
                Console.WriteLine("Недостаточно средств на счете для перевода.");
            }
        }
        else
        {
            Console.WriteLine("Один из счетов не найден.");
        }
    }

    public void ShowAccountInfo(string accountNumber)
    {
        BankAccount account = GetAccountByNumber(accountNumber);
        if (account != null)
        {
            Console.WriteLine(account);
            Console.WriteLine("История операций:");
            Console.WriteLine(account.GetTransactionHistory());
        }
        else
        {
            Console.WriteLine("Счет не найден.");
        }
    }

    private void LoadAccountsFromFile()
    {
        if (File.Exists(accountsFilePath))
        {
            string jsonData = File.ReadAllText(accountsFilePath);
            accounts = JsonConvert.DeserializeObject<List<BankAccount>>(jsonData);
            foreach (var account in accounts)
            {
                account.LoadTransactionHistory(); // Загружаем историю транзакций для каждого счета
            }
        }
        else
        {
            accounts = new List<BankAccount>();
        }
    }

    private void SaveAccountsToFile()
    {
        string jsonData = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(accountsFilePath, jsonData);
    }
}