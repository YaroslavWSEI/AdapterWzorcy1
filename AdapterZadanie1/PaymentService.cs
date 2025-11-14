using AdapterZadanie1;
using System;
using System.IO;
using System.Linq;
namespace Payments
{
    public class PaymentService
    {
        public void ProcessPayment(IBankPayment payment)
        {
            Console.WriteLine("Przetwarzanie płatności...");

            if (payment == null)
            {
                Console.WriteLine("Błąd: obiekt płatności jest null.");
                return;
            }
            if (payment.Amount() <= 0)
            {
                Console.WriteLine("Błąd: kwota musi być większa od 0.");
                return;
            }
            string account = payment.BankAccount();
            if (!ValidateBankAccount(account))
            {
                Console.WriteLine("Błąd: nieprawidłowy numer konta bankowego.");
                return;
            }
            Console.WriteLine($"Wysłano płatność {payment.Amount()} PLN na konto {account}.");
            LogTransaction(payment);
        }
        private bool ValidateBankAccount(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                Console.WriteLine("Walidacja: konto jest puste lub null.");
                return false;
            }
            Console.WriteLine($"Walidacja: konto='{account}', długość={account.Length}");
            if (!account.StartsWith("PL"))
            {
                Console.WriteLine("Walidacja: brak prefiksu 'PL'.");
                return false;
            }
            if (account.Length != 28)
            {
                Console.WriteLine($"Walidacja: niepoprawna długość (oczekiwano 28, jest {account.Length}).");
                return false;
            }
            string digitsPart = account.Substring(2);
            if (!digitsPart.All(char.IsDigit))
            {
                var badChars = digitsPart.Where(c => !char.IsDigit(c)).Distinct().ToArray();
                Console.WriteLine($"Walidacja: część po 'PL' powinna być cyframi. Znaleziono niecyfrowe znaki: [{string.Join(", ", badChars)}]");
                return false;
            }
            return true;
        }
        private void LogTransaction(IBankPayment payment)
        {
            try
            {
                string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Kwota: {payment.Amount()} | Konto: {payment.BankAccount()}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logowanie nie powiodło się: {ex.Message}");
            }
        }
    }
}
