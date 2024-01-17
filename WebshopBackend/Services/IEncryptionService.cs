using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public interface IEncryptionService
    {
        public string Encrypt(string plaintext);
        public string Decrypt(string IVciphertext);
    }
}