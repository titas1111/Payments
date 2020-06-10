using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public enum TypeOfRequest
    {
        [Display(Name = "Get Current Month")]
        GetCurrentMonth,
        [Display(Name = "Get Past Month")]
        GetPastMonth,
        [Display(Name = "Compare months")]
        CompareMonths
    }
}
