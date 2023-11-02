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

        private bool InsertAccount(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool DoesEmailExist(string email)
        {
            return _context.Accounts.Any(a => a.Email == email);
        }

        public bool RegisterAccount(string name, string email, string password)
        {
            if(!DoesEmailExist(email)){
                Account a = new()
                {
                    Name = name,
                    Email = email
                };
                var hashsalt = _encrypt.EncryptPassword(password);
                a.Password = hashsalt.Hash;
                a.StoredSalt = hashsalt.Salt;

                a.Role = _context.Roles.Find(1);
                return InsertAccount(a);
            }
            else{
                return false;
            }
        }

        public bool LoginAccount(string email, string password)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Email == email);
            return _encrypt.VerifyPassword(password, account.StoredSalt, account.Password);
        }

        public IEnumerable<Account> GetAccounts()
        {
            try
            {
                return _context.Accounts.Include(a => a.Role).ToArray();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Could not get accounts");
            }
        }

        public Account GetAccount(int id)
        {
            try
            {
                return _context.Accounts.Where(a => a.Id == id).Include(a => a.Role).Single();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Could not find account");
            }
        }

        public Account GetAccount(string email)
        {
            try
            {
                return _context.Accounts.Where(a => a.Email == email).Include(a => a.Role).Single();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Could not find account");
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _context.Roles.ToArray();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Could not get roles");
            }
        }

        public bool SetRole(int userid, int roleid)
        {
            try
            {
                Account acc = _context.Accounts.Where(a => a.Id == userid).Include(a => a.Role).Single();
                acc.Role = _context.Roles.Find(roleid);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}