using Ecommerce.DataAccess.Data;
using Ecommerce.Entities.Repo_Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repo_Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IUserRepo UserRepo { get; private set; }
        public IProductRepo ProductRepo { get; private set; }
        public IProductTranslationRepo ProductTranslationRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IConfiguration configure)
        {
            this.context = context;
            this.ProductRepo = new ProductRepo(context);
            this.UserRepo = new UserRepo(context,configure);
            this.ProductTranslationRepo = new ProductTranslationRepo(context);
 
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
