using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    public partial class UserProfile
    {
        public  string EmailAddress { get; set; }
        public  string MobileNumber { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public string? DeviceId { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }

        public UserProfile(string emailAddress, string mobileNumber, string firstName, string lastName, string password, string ? deviceId, int roleId)
        {
            EmailAddress = emailAddress;
            MobileNumber = mobileNumber;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            DeviceId = deviceId;
            UserRoleId = roleId;
        }

        public void ValidateUserProfile()
        {
            if (string.IsNullOrWhiteSpace(EmailAddress))
                throw new ArgumentException("Email Address is required.");

            if (!Regex.IsMatch(EmailAddress, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                throw new ArgumentException("Invalid Email Address");
            }

            if (string.IsNullOrWhiteSpace(MobileNumber)) {
                throw new ArgumentException("Mobile Number is required.");
            }
                

            if (!Regex.IsMatch(MobileNumber, @"^(09|\+639)\d{9}$"))
            {
                throw new ArgumentException("Invalid mobile number");
            }
            if (string.IsNullOrWhiteSpace(FirstName)) {
                throw new ArgumentException("First Name is required.");
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                throw new ArgumentException("Last Name is required.");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new ArgumentException("Password is required.");
            }

            if (Password.Length < 8) {
                throw new ArgumentException("Password must have atleast 8 characters.");
            }

            if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])[A-Za-z\d!@#$%^&*()]{8,}$"))
            {
                throw new ArgumentException("Invalid Password.");
            }

            if (UserRoleId <= 0) {
                throw new ArgumentException("Role ID is required.");
            }
        }

    }
}
