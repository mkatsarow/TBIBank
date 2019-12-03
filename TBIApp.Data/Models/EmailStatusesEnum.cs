using System.ComponentModel.DataAnnotations;

namespace TBIApp.Data.Models
{
    public enum EmailStatusesEnum
    {
        [Display(Name = "Not Reviewed")]
        NotReviewed = 1,

        [Display(Name = "Invalid Application")]
        InvalidApplication = 2,

        [Display(Name = "New Application")]
        New = 3,

        [Display(Name = "Open Application")]
        Open = 4,

        [Display(Name = "Closed Application")]
        Closed = 5
    }
}
