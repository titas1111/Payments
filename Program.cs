using System;

namespace App.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerProcessor customerProcessor = new CustomerProcessor();

            var customer = customerProcessor.GetCustomerById(2);
            var payments = customerProcessor.FilterPaymentsByMonthId(customer, 1);

            foreach (var payment in payments)
            {
                Console.WriteLine($"{payment.MonthId}, {payment.Description}, {payment.Price}");
            }

        }
    }
}
