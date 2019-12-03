using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TBIApp.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
        public bool IsChangedPassword { get; set; }
        public DateTime LastLogIn { get; set; }  
        public ICollection<Email> UserEmails { get; set; }
    }
}
