using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public class AesCipher
    {
        public string IV { get; set; }
        public string Ciphertext { get; set; }

        public AesCipher(string iv, string ciphertext)
        {
            IV = iv;
            Ciphertext = ciphertext;
        }
        public AesCipher(string IVCiphertext)
        {
            var parts = IVCiphertext.Split("$");
            IV = parts[0];
            Ciphertext = parts[1];
        }
        public override string ToString()
        {
            return $"{IV}${Ciphertext}";
        }
    }
}