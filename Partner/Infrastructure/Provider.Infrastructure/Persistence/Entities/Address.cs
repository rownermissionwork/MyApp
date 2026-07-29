using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Persistence.Entities
{
    [Table("Address", Schema = "Provider")]
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        public required string AddressLine1 { get; set; }
        public string ? AddressLine2 { get; set; }
        public string ? AddressLine3 { get; set; }
        public string ? City { get; set; }
        public string ? StateProvince { get; set; }
        public string ? DistrictRegion { get; set; }
        public string ? PostalCode { get; set; }
        public required string CountryCode { get; set; }
    }
}
