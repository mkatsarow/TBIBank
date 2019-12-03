using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBIApp.Data.Models
{
    public enum LoanApplicationStatus
    {
        [Display(Name = "Under Review")]
        NotReviewed = 1,

        [Display(Name = "Approved")]
        Accepted = 2,

        [Display(Name = "Rejected")]
        Rejected = 3
    }
}
