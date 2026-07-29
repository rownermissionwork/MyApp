using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Domain.Entities
{
    public class Service()
    {
        public int Id { get; set; }
        public  Guid PublicId { get; set; }
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public void ValidateService(int categoryId, string name, string createdBy) {
            if (categoryId <= 0) 
                throw new ArgumentException("CategoryId should not be less than or equal to zero.");    
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Service Name is required.");
            if (name.Length > 100)
                throw new ArgumentException("Service Name must not exceed 100 characters.");
            if (string.IsNullOrWhiteSpace(createdBy)) 
                throw new ArgumentException("Created By is required.");

            CategoryId = categoryId;
            Name = name;
            CreatedBy = createdBy;

        }
    }
}
