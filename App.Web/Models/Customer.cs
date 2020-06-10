using App.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Models
{
    public class Customer
    {
        public string Name { get; set; }
        [Display(Name = "Customer Id")]
        [Required(ErrorMessage = "Please input id")]
        public int CustomerId { get; set; }
        public List<Payment> Payments { get; set; }

        public Customer()
        {
            Payments = new List<Payment>();
        }
    }
}
