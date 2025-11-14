using AdapterZadanie1;
using System;

namespace Payments
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService service = new PaymentService();
            Console.WriteLine("=== Płatność SWIFT (poprawny numer) ===");
            IBankPayment swiftOk = new SwiftPayment(500, "PL12345678901234567890123456");
            service.ProcessPayment(swiftOk);
            Console.WriteLine("\n=== Płatność SWIFT (zły numer - brak PL) ===");
            IBankPayment swiftBad = new SwiftPayment(100, "12345678901234567890123456");
            service.ProcessPayment(swiftBad);
            Console.WriteLine("\n=== Płatność BLIK (adapter) ===");
            IMobilePayment blik = new BlikPayment(75, "+48 500 600 700");
            IBankPayment blikAdapter = new MobileToBankPaymentAdapter(blik);
            service.ProcessPayment(blikAdapter);
            Console.WriteLine("\n=== Płatność BLIK (adapter) - długi numer telefonu ===");
            IMobilePayment blikLong = new BlikPayment(80, "0048123456789012345678901234567890");
            service.ProcessPayment(new MobileToBankPaymentAdapter(blikLong));
        }
    }
}
