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
        private readonly IEncryptionService _encrypt;
        public HashService()
        {
            _encrypt = new EncryptionService(Convert.FromBase64String("PqpY2n5zViisrdaEF7cH2a5g1mKTJvzwx5xCDqN6E6s="));
        }
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
            return new HashSalt { Hash = _encrypt.Encrypt(PasswordHash), Salt = _encrypt.Encrypt(Convert.ToBase64String(salt)) };
        }

        public bool VerifyPassword(string enteredpassword, string salt, string storedpassword)
        {
            string PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredpassword,
                salt: Convert.FromBase64String(_encrypt.Decrypt(salt)),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                numBytesRequested: 256/8
            ));
            return PasswordHash == _encrypt.Decrypt(storedpassword);
        }
    }
}