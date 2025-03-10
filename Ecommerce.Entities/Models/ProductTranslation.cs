using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Models
{
    public class ProductTranslation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
