using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Persistence.Entities
{
    [Table("UserProfile", Schema = "dbo")]
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid PublicId { get; set; }
        public int UserRoleID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Phone { get; set; }
        public int AddressId { get; set; }
        public bool IsActive { get; set; }
    }
}
