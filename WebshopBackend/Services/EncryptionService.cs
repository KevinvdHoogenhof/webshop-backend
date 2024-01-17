using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
﻿using System.Security.Cryptography;
using Aes = System.Security.Cryptography.Aes;
using System.IO;

namespace WebshopBackend.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;
        public EncryptionService(byte[] key)
        {
            _key = key;
        }

        public string Encrypt(string plaintext)
        {
            //Check input
            if(plaintext == null || plaintext.Length <= 0)
                throw new ArgumentNullException("plaintext");
            if(_key == null || _key.Length <=0)
                throw new ArgumentNullException("key");

            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;

                //Encryptor
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                //Streams for encryption
                using (MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plaintext);
                        }
                        encrypted = ms.ToArray();
                    }
                }
                AesCipher aesCipher = new(Convert.ToBase64String(aes.IV), Convert.ToBase64String(encrypted));
                return aesCipher.ToString();
                //return Convert.ToBase64String(aes.IV) + "$" + Convert.ToBase64String(encrypted);
            }
        }
        public string Decrypt(string IVciphertext)
        {
            AesCipher aesCipher = new(IVciphertext);
            //Check input
            if(aesCipher.Ciphertext == null || aesCipher.Ciphertext.Length <= 0)
                throw new ArgumentNullException("ciphertext");
            if(_key == null || _key.Length <=0)
                throw new ArgumentNullException("key");
            if(aesCipher.IV == null || aesCipher.IV.Length <=0)
                throw new ArgumentNullException("iv");

            string plaintext;

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = Convert.FromBase64String(aesCipher.IV);

                //Decryptor
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                //Streams for decryption
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(aesCipher.Ciphertext)))
                {
                    using(CryptoStream cs = new CryptoStream(ms, decryptor,CryptoStreamMode.Read))
                    {
                        using(StreamReader sr = new StreamReader(cs))
                        {
                            plaintext = sr.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}