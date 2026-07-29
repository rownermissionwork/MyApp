using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid PublicId { get; set; }


        public void ValidateCategory(string categoryName)
        {

            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category is required.");

            if (categoryName.Length > 100)
                throw new ArgumentException("Category must not exceed 100 characters.");

            if (!Regex.IsMatch(categoryName, @"^[a-zA-Z0-9 !@#$%^&*()]+$")) {
                throw new ArgumentException("Only letters, numbers, spaces and !@#$%^&*() are allowed.");
            }


            Name = categoryName;
        }
    }
}
