using WebshopBackend.Data;
using WebshopBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IWebshopContext _context;
        private readonly IEncryptionService _encrypt;
        public AccountService(IWebshopContext context)
        {
            _context = context;
            _encrypt = new EncryptionService();
        }

        bool IAccountService.RegisterAccount(string name, string email, string password)
        {
            throw new NotImplementedException();
        }

        bool IAccountService.LoginAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Account> IAccountService.GetAccounts()
        {
            throw new NotImplementedException();
        }

        Account IAccountService.GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        Account IAccountService.GetAccount(string email)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Role> IAccountService.GetRoles()
        {
            throw new NotImplementedException();
        }

        bool IAccountService.SetRole(int userid, int roleid)
        {
            throw new NotImplementedException();
        }
    }
}