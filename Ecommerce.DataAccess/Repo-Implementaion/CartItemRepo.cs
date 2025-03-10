using Ecommerce.DataAccess.Data;
using Ecommerce.Entities.Models;
using Ecommerce.Entities.Repo_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repo_Implementaion
{
    public class CartItemRepo : GenericRepo<CartItem>, ICartItemRepo
    {
        private readonly ApplicationDbContext context;
        public CartItemRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;

        }
    }
}
