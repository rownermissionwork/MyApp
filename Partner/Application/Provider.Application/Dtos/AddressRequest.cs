using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Dtos
{
    public class AddressRequest
    {
        public required string Street { get; set; }
        public required string Barangay { get; set; }
        public required string CityOrMunicipal { get; set; }
        public string? Province { get; set; }
        public required string PostalCode { get; set; }
        public required string Region { get; set; }
        public required string CountryCode { get; set; }
    }
}
