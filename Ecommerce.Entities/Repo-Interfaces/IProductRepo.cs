using Ecommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Repo_Interfaces
{
    public interface IProductRepo:IGenericRepo<Product>
    {
       void Update(Product product);
    }
}
