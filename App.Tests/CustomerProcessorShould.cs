using App.Core;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace App.Tests
{
    public class CustomerProcessorShould
    {
        private CustomerProcessor customerProcessor;

        public CustomerProcessorShould()
        {
            customerProcessor = new CustomerProcessor();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetExistingCustomerById(int customerId)
        {
            //Act
            var result = customerProcessor.GetCustomerById(customerId);
            //Assert
            var actual = Assert.IsType<Customer>(result);
            Assert.Equal(customerId, actual.CustomerId);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(20000)]
        public void GetCustomerByIdReturnNullForNonExistingCustomers(int customerId)
        {
            //Act
            var result = customerProcessor.GetCustomerById(customerId);
            //Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(0, 2)]
        [InlineData(1, 2)]
        public void FilterPaymentsByMonthId(int monthId, int customerId)
        {
            //Arrange
            var customer = customerProcessor.GetCustomerById(customerId);
            //Act
            var result = customerProcessor.FilterPaymentsByMonthId(customer, monthId);
            //Assert
            var actual = Assert.IsType<Payment>(result.FirstOrDefault());
            Assert.Equal(monthId, actual.MonthId);
        }

        [Fact]
        public void CreateCustomerModelWithFilteredPayments()
        {
            //Arrange
            var customers = CustomersRepository.GenerateCustomerData();
            var customer = customers.FirstOrDefault();
            var payments = customer.Payments;

            //Act
            var result = customerProcessor.CreateCustomerModelWithFilteredPayments(payments);

            //Assert
            Assert.IsType<Customer>(result);
            Assert.Equal(payments, result.Payments);
        }
    }
}
