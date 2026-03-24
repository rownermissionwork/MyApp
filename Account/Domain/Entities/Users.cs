using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    [Table("Users", Schema = "auth")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ? IsActive { get; set; }

    }
}
