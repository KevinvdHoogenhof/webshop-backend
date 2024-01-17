using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopBackend.Models;

namespace WebshopBackend.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StoredSalt { get; set; }
        public string RoleName { get; set; }
        public AccountViewModel(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Email = account.Email;
            Password = account.Password;
            StoredSalt = account.StoredSalt;
            if (account.Role != null)
            {
                RoleName = account.Role.Name;
            }
            else
            {
                RoleName = "notfound";
            }
        }

    }
}