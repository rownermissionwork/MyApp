using Account.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Security
{
    public class SecretGeneratorService : ISecretGeneratorService
    {
        public string GenerateSecretKey(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashBytes = SHA256.HashData(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
