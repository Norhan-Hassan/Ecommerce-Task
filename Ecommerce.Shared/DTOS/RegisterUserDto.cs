using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOS
{
    public class RegisterUserDto
    {
        [Required]
        [DisplayName("Full Name")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]

        [MaxLength(10,ErrorMessage ="Password Should be 10 length maximum")]
        public string Password { get; set; }
    }
}
