using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_API.DTO_s
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [MinLength(3, ErrorMessage = "The minimum length has to be 3 letters!")]
        [RegularExpression(@"^[a-zA-Z- -ÇçşŞğĞİıöÖüÜ]+$", ErrorMessage = "Only letters allowed!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [MinLength(3, ErrorMessage = "The minimum length has to be 3 letters!")]
        [RegularExpression(@"^[a-zA-Z- -ÇçşŞğĞİıöÖüÜ]+$", ErrorMessage = "Only letters allowed!")]
        public string Description { get; set; }
    }
}
