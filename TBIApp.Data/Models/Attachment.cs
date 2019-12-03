using System;
using System.Collections.Generic;
using System.Text;

namespace TBIApp.Data.Models
{
    public class Attachment
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public double? SizeMb { get; set; }
        public double? SizeKb { get; set; }
        public string EmailId { get; set; }
        public Email Email { get; set; }

    }
}
