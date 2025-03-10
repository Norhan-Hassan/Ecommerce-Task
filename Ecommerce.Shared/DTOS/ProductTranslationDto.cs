using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOS
{
    public class ProductTranslationDto
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
