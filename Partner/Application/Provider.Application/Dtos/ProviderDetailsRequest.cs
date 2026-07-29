using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Dtos
{
    public class ProviderDetailsRequest : AddressRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string ? Phone { get; set; }
        public int ProviderType { get; set; }
        public string ? BusinessName { get; set; }
        public string ? BusinessRegistrationNumber { get; set; }
        public string ? Description { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
