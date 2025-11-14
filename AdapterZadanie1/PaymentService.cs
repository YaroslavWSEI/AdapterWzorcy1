using AdapterZadanie1;
using System;
using System.IO;

public class PaymentService
{
    public void ProcessPayment(IBankPayment payment)
    {
        Console.WriteLine("Przetwarzanie płatności...");
        if (payment.Amount() <= 0)
        {
            Console.WriteLine("Błąd: kwota musi być większa od 0.");
            return;
        }
        if (!ValidateBankAccount(payment.BankAccount()))
        {
            Console.WriteLine("Błąd: nieprawidłowy numer konta bankowego.");
            return;
        }
        Console.WriteLine($"Wysłano płatność {payment.Amount()} PLN na konto {payment.BankAccount()}.");

        LogTransaction(payment);
    }
    private bool ValidateBankAccount(string account)
    {
        return account.StartsWith("PL") && account.Length == 28 && long.TryParse(account.Substring(2), out _);
    }
    private void LogTransaction(IBankPayment payment)
    {
        string log = $"{DateTime.Now}: Kwota: {payment.Amount()}, Konto: {payment.BankAccount()}";
        File.AppendAllText("transactions.log", log + Environment.NewLine);
    }
}
