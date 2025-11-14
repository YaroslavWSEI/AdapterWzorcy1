using AdapterZadanie1;
using System;
namespace Payments
{
    public class SwiftPayment : IBankPayment
    {
        private readonly int _amount;
        private readonly string _bankAccount;
        public SwiftPayment(int amount, string bankAccount)
        {
            _amount = amount;
            _bankAccount = bankAccount;
        }
        public int Amount() => _amount;
        public string BankAccount() => _bankAccount;
    }
}
