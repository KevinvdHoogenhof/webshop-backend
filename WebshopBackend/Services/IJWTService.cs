using WebshopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public interface IJWTService
    {
        public string GenerateToken(Account account);
        public bool ValidateToken(string token);
        public string GetClaim(string token, string claimType);
    }
}