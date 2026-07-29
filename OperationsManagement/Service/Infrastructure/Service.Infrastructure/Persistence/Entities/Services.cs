using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Infrastructure.Persistence.Entities
{
    [Table("Service", Schema = "references")]
    public class Services
    {
        [Key]
        public int Id { get; set; }
        public  Guid PublicId { get; set; } = Guid.NewGuid();
        public int CategoryId { get; set; }
        public required string Service { get; set; }
        public bool IsActive { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ? ModifiedBy { get; set; }
        public DateTime ? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string ?  DeletedBy { get; set; }
        public DateTime ? DeletedAt { get; set; }
    }
}
