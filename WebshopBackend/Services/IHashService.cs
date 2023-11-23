using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public interface IHashService
    {
        public HashSalt HashPassword(string password);
        public bool VerifyPassword(string enteredpassword, string salt, string storedpassword);
    }
}