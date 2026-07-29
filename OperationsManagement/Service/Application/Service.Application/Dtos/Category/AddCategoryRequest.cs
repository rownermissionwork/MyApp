using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Application.Dtos.Category
{
    public class AddCategoryRequest
    {
        //[Required(ErrorMessage = "Category is required.")]
        //[StringLength(100,ErrorMessage = "{0} must not exceed {1} characters.")]
        //[RegularExpression(@"^[a-zA-Z0-9 !@#$%^&*()]+$",ErrorMessage = "Only letters, numbers, spaces are allowed.")]
        public required string Category { get; set; }
    }
}
