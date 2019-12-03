using System;

namespace TBIApp.Services.Models
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
        public bool IsChangedPassword { get; set; }
        public DateTime LastLogIn { get; set; }
        public int  UserEmailsCount { get; set; }
        public string Role { get; set; }
    }
}
