using AdapterZadanie1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AdapterZadanie1
{
    internal class MobileToBankPayment
    {
    }
}
namespace Payments
{
    public class MobileToBankPaymentAdapter : IBankPayment
    {
        private readonly IMobilePayment _mobilePayment;
        public MobileToBankPaymentAdapter(IMobilePayment mobilePayment)
        {
            _mobilePayment = mobilePayment ?? throw new ArgumentNullException(nameof(mobilePayment));
        }
        public int Amount() => _mobilePayment.Amount();
        public string BankAccount()
        {
            string phone = _mobilePayment.PhoneNumber() ?? string.Empty;
            var digitsOnly = string.Concat(Array.FindAll(phone.ToCharArray(), char.IsDigit));
            if (digitsOnly.Length > 26)
            {
                digitsOnly = digitsOnly.Substring(digitsOnly.Length - 26);
            }
            string padded = digitsOnly.PadLeft(26, '0');
            string bankAccount = "PL" + padded;
            return bankAccount;
        }
    }
}
