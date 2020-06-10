using App.Web.Models;
using System.Collections.Generic;

namespace App.Core
{
    public class CustomersRepository
    {
        public static IEnumerable<Customer> GenerateCustomerData()
        {
            List<Customer> ListOfCustomers = new List<Customer>();

            var customer1 = new Customer
            {
                CustomerId = 1,
                Name = "Foo"
            };

            var customer1Payment1 = new Payment
            {
                Price = 100,
                Description = "Food",
                MonthId = 1,
                Category = CategoryEnum.Food
            };

            var customer1Payment2 = new Payment
            {
                Price = 50,
                Description = "Drinks",
                MonthId = 0,
                Category = CategoryEnum.Transportation
            };

            var customer1Payment3 = new Payment
            {
                Price = 50,
                Description = "Cola cola",
                MonthId = 0,
                Category = CategoryEnum.Utilities
            };

            customer1.Payments.Add(customer1Payment1);
            customer1.Payments.Add(customer1Payment2);
            customer1.Payments.Add(customer1Payment3);
            customer1.Payments.Add(customer1Payment1);
            customer1.Payments.Add(customer1Payment2);
            customer1.Payments.Add(customer1Payment3);
            customer1.Payments.Add(customer1Payment1);
            customer1.Payments.Add(customer1Payment2);
            customer1.Payments.Add(customer1Payment3);
            customer1.Payments.Add(customer1Payment2);
            customer1.Payments.Add(customer1Payment3);

            ListOfCustomers.Add(customer1);

            var customer2 = new Customer
            {
                CustomerId = 2,
                Name = "Foo"
            };

            var customer2Payment1 = new Payment
            {
                Price = 500,
                Description = "Food and drinks",
                MonthId = 1,
                Category = CategoryEnum.Utilities
            };

            var customer2Payment2 = new Payment
            {
                Price = 50,
                Description = "Cat Food",
                MonthId = 0,
                Category = CategoryEnum.Utilities
            };

            customer2.Payments.Add(customer2Payment1);
            customer2.Payments.Add(customer2Payment2);

            ListOfCustomers.Add(customer2);

            return ListOfCustomers;
        }
    }
}
