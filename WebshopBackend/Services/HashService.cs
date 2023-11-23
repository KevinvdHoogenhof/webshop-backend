using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public class HashService : IHashService
    {
        public HashSalt HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8]; //Generate a 128 bit salt 
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                numBytesRequested: 256/8
            ));
            return new HashSalt { Hash = PasswordHash, Salt = salt };
        }

        public bool VerifyPassword(string enteredpassword, byte[] salt, string storedpassword)
        {
            string PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredpassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                numBytesRequested: 256/8
            ));
            return PasswordHash == storedpassword;
        }
    }
}