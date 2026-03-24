using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    [Table("UserLogin", Schema = "auth")]
    public class UserLogin
    {
        [Key]
        public int LoginID { get; set; }
        public int UserID { get; set; }
        public string ? UserName { get; set; }
        public string? DeviceID { get; set; }
        public DateTime LastLogin { get; set; }
        public int FailedAttempts { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
    }
}
