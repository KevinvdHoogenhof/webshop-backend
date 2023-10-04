using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public class EncryptionService : IEncryptionService
    {
        HashSalt IEncryptionService.EncryptPassword(string password)
        {
            throw new NotImplementedException();
        }

        bool IEncryptionService.VerifyPassword(string enteredpassword, byte[] salt, string storedpassword)
        {
            throw new NotImplementedException();
        }
    }
}