using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Persistence.Entities
{
    [Table("ServiceProviderDetails", Schema = "Provider")]
    public class ServiceProviderDetails
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("UserId")]
        public int UserID { get; set; }
        public int ProviderType { get; set; }
        public string ? BusinessName { get; set; }
        public string ? BusinessRegistrationNumber { get; set; }
        public string ? Description { get; set; }
        public int YearsOfExperience { get; set; }
        public int VerificationStatus { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}
