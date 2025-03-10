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
    public class ProductTranslationRepo : GenericRepo<ProductTranslation>, IProductTranslationRepo
    {
        private readonly ApplicationDbContext _context;
        public ProductTranslationRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
