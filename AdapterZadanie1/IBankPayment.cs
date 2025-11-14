using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterZadanie1
{
    public interface IBankPayment
    {
        int Amount();
        string BankAccount();
    }

}
