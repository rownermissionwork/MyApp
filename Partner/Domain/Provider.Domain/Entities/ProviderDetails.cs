using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Provider.Domain.Entities
{
    public class ProviderDetails
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public int ProviderType { get; set; }
        public string? BusinessName { get; set; }
        public string? BusinessRegistrationNumber { get; set; }
        public string? Description { get; set; }
        public int YearsOfExperience { get; set; }
        public string Street { get; set; }
        public string Barangay { get; set; }
        public string CityOrMunicipal { get; set; }
        public string? Province { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string CountryCode { get; set; }
        public int VerificationStatus { get; set; }

        public ProviderDetails(int userId 
            ,string firstName
            , string lastName
            , string? phone
            , int providerType
            , string? businessName
            , string? businessRegistrationNumber
            , string? description
            , int yearsOfExperience
            , string street
            , string barangay
            , string cityOrMunicipal
            , string? province
            , string postalCode
            , string region
            , string countryCode
            , int verificationStatus)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            ProviderType = providerType;
            BusinessName = businessName;
            BusinessRegistrationNumber = businessRegistrationNumber;
            Description = description;
            YearsOfExperience = yearsOfExperience;
            Street = street;
            Barangay = barangay;
            CityOrMunicipal = cityOrMunicipal;
            Province = province;
            PostalCode = postalCode;
            Region = region;
            CountryCode = countryCode;
            VerificationStatus = verificationStatus;

        }

        public void ValidateProviderDetails()
        {
            string pattern = @"^[\p{L}\s'-]+$";

            if (UserId <= 0)
            {
                throw new ArgumentException("Invalid User ID.");
            }

            if (string.IsNullOrWhiteSpace(FirstName)) {
                throw new ArgumentException("First Name is required.");
            }
            else if (FirstName.Length > 100)
            {
                throw new ArgumentException("First Name cannot exceed 100 characters.");
            }
            else if (!Regex.IsMatch(FirstName, pattern))
            {
                throw new ArgumentException("First Name can only contain letters, spaces, apostrophes, and hyphens.");
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                throw new ArgumentException("Last Name is required.");
            }
            else if (LastName.Length > 100)
            {
                throw new ArgumentException("Last Name cannot exceed 100 characters.");
            }
            else if (!Regex.IsMatch(LastName, pattern))
            {
                throw new ArgumentException("Last Name can only contain letters, spaces, apostrophes, and hyphens.");
            }


            if (ProviderType <= 0)
                throw new ArgumentException("Invalid Provider Type.");
            if (YearsOfExperience < 0)
                throw new ArgumentException("Years of Experience cannot be negative.");
            if (string.IsNullOrWhiteSpace(Street)) {
                throw new ArgumentException("Street is required.");
            }
            if (string.IsNullOrWhiteSpace(Barangay))
            {
                throw new ArgumentException("Barangay is required.");
            }
            if (string.IsNullOrWhiteSpace(CityOrMunicipal))
            {
                throw new ArgumentException("City/Municipal is required.");
            }
            //if (string.IsNullOrWhiteSpace(Province))
            //{
            //    throw new ArgumentException("Province is required.");
            //}

            if (string.IsNullOrWhiteSpace(PostalCode))
                throw new ArgumentException("Postal Code is required.");
            if (string.IsNullOrWhiteSpace(Region))
                throw new ArgumentException("Region is required.");
            if (string.IsNullOrWhiteSpace(CountryCode))
                throw new ArgumentException("Country Code is required.");
        }

    }
}
