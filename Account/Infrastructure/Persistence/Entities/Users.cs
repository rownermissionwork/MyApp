using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Persistence.Entities
{
    [Table("Users", Schema = "auth")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public required string PasswordHash { get; set; }
        public string? DeviceID { get; set; }
        public DateTime? LastLogin { get; set; }
        public int FailedAttempts { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
