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

        public bool RegisterAccount(string name, string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool LoginAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public bool SetRole(int userid, int roleid)
        {
            throw new NotImplementedException();
        }
    }
}