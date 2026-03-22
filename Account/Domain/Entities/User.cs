using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public string? DeviceId { get; set; }
        public int LoginAttempt { get; set; }
    }
}
