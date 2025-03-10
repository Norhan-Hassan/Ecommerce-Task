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
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private ApplicationDbContext context;
        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Product product)
        {
            context.Update(product);
        }
    }
}
