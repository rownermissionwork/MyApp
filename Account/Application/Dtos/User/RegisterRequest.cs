using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Dtos.User
{
    public class RegisterRequest
    {
        public required string EmailAddress { get; set; }
        public required string MobileNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
       // public required string UserName { get; set; }
        public required string Password { get; set; }
        public string ? DeviceId { get; set; }
        public required string RoleId { get; set; }
    }
}
