using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOS
{
    public class UserDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }
    }
}
