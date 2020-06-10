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
        //Atfiltruoti kategorijas, kuriose klientas šį mėnesį išleido bent 50% daugiau nei praeitą
        //Work in progress
        public void CompareCategoriesBetweenMonths(Customer customer, int threshold)
        {
            // month category and price aggragation
            var payments = customer
                .Payments.GroupBy(x => new { x.MonthId, x.Category, x.Price})
                .Select(p =>
                    new Payment
                    { 
                        MonthId = p.Key.MonthId,
                        Category = p.Key.Category,
                        Price = p.Sum(p => p.Price)               
                }).ToList();

            var firstMonthPayments = payments.Where(p => p.MonthId == 0);

            int aa = 1;
        }
    }
}
