
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Interfaces
{
    public interface IUtilityService
    {
        string Encrypt(string plainText);
        string Decrypt(string encrypted);
        string HashPassword(string userName, string password);
        bool VerifyHashed(string userName, string password, string passwordHash);
    }
}
