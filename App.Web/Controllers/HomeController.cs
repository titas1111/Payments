using App.Core;
using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerProcessor customerProcessor;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            customerProcessor = new CustomerProcessor();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Customer customerModel, TypeOfRequest typeOfRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var filteredCustomerById = customerProcessor.GetCustomerById(customerModel.CustomerId);

            if(filteredCustomerById == null)
            {
                ViewBag.CustomerNotFound = $"{customerModel.CustomerId} does not exist";
                return View();
            }

            if (typeOfRequest == TypeOfRequest.GetCurrentMonth)
            {
                var filteredPayments = customerProcessor.FilterPaymentsByMonthId(filteredCustomerById, 1);
                Customer customer = customerProcessor.CreateCustomerModelWithFilteredPayments(filteredPayments);
                ViewBag.TypeOfRequest = typeOfRequest;

                return View(customer);
            }
            else if(typeOfRequest == TypeOfRequest.GetPastMonth)
            {
                var filteredPayments = customerProcessor.FilterPaymentsByMonthId(filteredCustomerById, 0);
                Customer customer = customerProcessor.CreateCustomerModelWithFilteredPayments(filteredPayments);
                ViewBag.TypeOfRequest = typeOfRequest;

                return View(customer);
            }
            else if (typeOfRequest == TypeOfRequest.CompareMonths)
            {
                var monthlyAggregatedPayments = customerProcessor.GeneratePaymentAggregationByMonth(filteredCustomerById);
                Customer customer = customerProcessor.CreateCustomerModelWithFilteredPayments(monthlyAggregatedPayments);
                ViewBag.TypeOfRequest = typeOfRequest;

                return View(customer);
            }

                return View(customerModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
