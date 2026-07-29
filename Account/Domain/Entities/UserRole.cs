using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public string? PublicId { get; set; }
        public string? Role { get; set; }
    }
}
