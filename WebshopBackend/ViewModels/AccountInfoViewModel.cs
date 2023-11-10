using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.ViewModels
{
    public class AccountInfoViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public AccountInfoViewModel(string name, string email, string role)
        {
            Name = name;
            Email = email;
            RoleName = role;
        }
    }
}
