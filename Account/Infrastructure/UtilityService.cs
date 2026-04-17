using Account.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Account.Infrastructure
{
    public class UtilityService : IUtilityService
    {
        private readonly IConfiguration _config;
        public UtilityService(IConfiguration config) {
            _config = config;
        }
        public string Encrypt(string plainText)
        {
            try {
                var key = Convert.FromBase64String(GeCurrentKey());

                var nonce = RandomNumberGenerator.GetBytes(12);
                var plaintextBytes = Encoding.UTF8.GetBytes(plainText);

                var cipher = new byte[plaintextBytes.Length];
                var tag = new byte[16];

                using var aes = new AesGcm(key, 16);
                aes.Encrypt(nonce, plaintextBytes, cipher, tag);

                var result = new byte[nonce.Length + tag.Length + cipher.Length];
                Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
                Buffer.BlockCopy(tag, 0, result, nonce.Length, tag.Length);
                Buffer.BlockCopy(cipher, 0, result, nonce.Length + tag.Length, cipher.Length);

                var text = Convert.ToBase64String(result);

                return text;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string Decrypt(string cipherText)
        {
            try {
                var key = Convert.FromBase64String(GeCurrentKey()); 

                var data = Convert.FromBase64String(cipherText);

                var nonce = data[..12];
                var tag = data[12..28];
                var cipher = data[28..];

                var plaintext = new byte[cipher.Length];

                using var aes = new AesGcm(key, 16);
                aes.Decrypt(nonce, cipher, tag, plaintext);
                var text = Encoding.UTF8.GetString(plaintext);

                return text;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool VerifyHashed(string userName, string password, string passwordHash)
        {
            var hasher = new PasswordHasher<object>();

            var user = new { UserName = userName };

            var result = hasher.VerifyHashedPassword(user, passwordHash, password);
            return result != PasswordVerificationResult.Failed;
        }
        public string HashPassword(string userName, string password)
        {
            var hasher = new PasswordHasher<object>();
            var user = new { UserName = userName };
            string hash = hasher.HashPassword(user, password);
            var result = hasher.VerifyHashedPassword(user, hash, password);

            if (result == PasswordVerificationResult.Failed) {
                hash = string.Empty;
            }
            return hash;
        }

        private string GetKey()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }
        private string GeCurrentKey() {
            return _config.GetSection("CurrentKey").Value??"";
        }
    }
}
