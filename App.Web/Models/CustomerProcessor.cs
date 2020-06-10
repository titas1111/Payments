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
    }
}
