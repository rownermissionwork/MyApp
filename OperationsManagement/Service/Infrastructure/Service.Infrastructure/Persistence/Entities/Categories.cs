using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Infrastructure.Persistence.Entities
{
    [Table("Category", Schema = "references")]
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
    }
}
