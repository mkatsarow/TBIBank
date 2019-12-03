using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBIBankApp.Models.Attachments
{
    public class AttachmentViewModel
    {
        public string FileName { get; set; }
        public double? SizeMb { get; set; }
        public double? SizeKb { get; set; }
    }
}
