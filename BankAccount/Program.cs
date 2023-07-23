Bank bank = new Bank();

Console.WriteLine("Добро пожаловать в банк!");

while (true)
{
    Console.WriteLine("Выберите действие:");
    Console.WriteLine("1. Создать счет");
    Console.WriteLine("2. Пополнить счет");
    Console.WriteLine("3. Снять со счета");
    Console.WriteLine("4. Перевести средства");
    Console.WriteLine("5. Показать информацию о счете");
    Console.WriteLine("6. Выход");

    int choice;
    if (!int.TryParse(Console.ReadLine(), out choice))
    {
        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
        continue;
    }

    switch (choice)
    {
        case 1:
            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Введите имя владельца счета: ");
            string ownerName = Console.ReadLine();
            bank.CreateAccount(accountNumber, ownerName);
            break;
        case 2:
            Console.Write("Введите номер счета: ");
            accountNumber = Console.ReadLine();
            Console.Write("Введите сумму для пополнения: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
            {
                Console.WriteLine("Некорректный ввод суммы. Попробуйте снова.");
                continue;
            }
            bank.Deposit(accountNumber, depositAmount);
            break;
        case 3:
            Console.Write("Введите номер счета: ");
            accountNumber = Console.ReadLine();
            Console.Write("Введите сумму для снятия: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
            {
                Console.WriteLine("Некорректный ввод суммы. Попробуйте снова.");
                continue;
            }
            bank.Withdraw(accountNumber, withdrawalAmount);
            break;
        case 4:
            Console.Write("Введите номер счета отправителя: ");
            string sourceAccountNumber = Console.ReadLine();
            Console.Write("Введите номер счета получателя: ");
            string destinationAccountNumber = Console.ReadLine();
            Console.Write("Введите сумму для перевода: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            {
                Console.WriteLine("Некорректный ввод суммы. Попробуйте снова.");
                continue;
            }
            bank.Transfer(sourceAccountNumber, destinationAccountNumber, transferAmount);
            break;
        case 5:
            Console.Write("Введите номер счета: ");
            accountNumber = Console.ReadLine();
            bank.ShowAccountInfo(accountNumber);
            break;
        case 6:
            Console.WriteLine("Спасибо за использование нашего приложения!");
            return;
        default:
            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            break;
    }
}