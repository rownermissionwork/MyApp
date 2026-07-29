using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Persistence.Entities
{
    [Table("UserRole", Schema = "auth")]
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public Guid PublicId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

    }
}
