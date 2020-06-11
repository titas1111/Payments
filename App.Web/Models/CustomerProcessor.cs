using App.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace App.Core
{
    public class CustomerProcessor
    {
        public Customer GetCustomerById(int id)
        {
            var customerRepository = CustomersRepository.GenerateCustomerData();

            var customer = customerRepository.Where(c => c.CustomerId == id).FirstOrDefault();

            return customer;
        }

        public IEnumerable<Payment> FilterPaymentsByMonthId(Customer customer, int monthId)
        {
            var result = customer.Payments.Where(p => p.MonthId == monthId);

            return result;
        }

        public Customer CreateCustomerModelWithFilteredPayments(IEnumerable<Payment> payments)
        {
            Customer customer = new Customer();

            foreach (var payment in payments)
            {
                customer.Payments.Add(payment);
            }

            return customer;
        }

        public List<Payment> GeneratePaymentAggregationByMonth(Customer customer)
        {
            var payments = customer
                 .Payments.GroupBy(x => x.MonthId,
                 (key, values) => new Payment
                 {
                     MonthId = key,
                     Price = values.Sum(x => x.Price),
                     Description = values.Select(x => $"{x.MonthId} aggregation").FirstOrDefault().ToString()
                 }).ToList();

            return payments;
        }

        public IEnumerable<Payment> CompareCategoriesBetweenMonths(Customer customer, double threshold)
        {
            IEnumerable<Payment> groupedPaymentsByMonthAndCategory = GroupCustomerPaymentsByMonthAndCategory(customer);

            var firstMonthPayments = groupedPaymentsByMonthAndCategory.Where(p => p.MonthId == 0).ToDictionary(t => t.Category, t => t.Price);
            var secondMonthPayments = groupedPaymentsByMonthAndCategory.Where(p => p.MonthId == 1).ToDictionary(t => t.Category, t => t.Price);

            List<Payment> payments = new List<Payment>();

            foreach (var payment in secondMonthPayments)
            {
                if (firstMonthPayments.ContainsKey(payment.Key))
                {
                    if (firstMonthPayments[payment.Key] * threshold <= payment.Value)
                    {
                        payments.Add(new Payment {
                            Description = $"Category: {payment.Key} in month 0 total amount: {firstMonthPayments[payment.Key]}, in month 1 total amount is {payment.Value}",
                            Price = payment.Value,
                            MonthId = 1                        
                        });
                    }
                }
            }
            return payments;
        }

        private static IEnumerable<Payment> GroupCustomerPaymentsByMonthAndCategory(Customer customer)
        {
            return customer
                .Payments.GroupBy(x => new { x.MonthId, x.Category })
                .Select(p =>
                    new Payment
                    {
                        MonthId = p.Key.MonthId,
                        Category = p.Key.Category,
                        Price = p.Sum(p => p.Price)
                    });
        }
    }
}
