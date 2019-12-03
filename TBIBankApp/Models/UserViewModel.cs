using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBIBankApp.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
        public bool IsChangedPassword { get; set; }
        public DateTime LastLogIn { get; set; }
        public int UserEmailsCount { get; set; }
        public string Role { get; set; }
    }
}
