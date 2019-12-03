using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Data.Models;

namespace TBIBankApp.Models.Emails
{
    public class EmailListModel
    {

        public string Status { get; set; }
        public int PreviousPage { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public int LastPage { get; set; }
        public bool ContainAttachment { get; set; }
        public User CurrentUser { get; set; }
        public ICollection<EmailViewModel> EmailViewModels { get; set; }
        public bool IsOpne { get; set; }
        public DateTime LastOpen { get; set; }

    }
}
