using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Models
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
       
    }
}
