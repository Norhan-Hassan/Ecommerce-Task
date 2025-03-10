using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Repo_Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public IUserRepo UserRepo { get; }
        public IProductRepo ProductRepo { get; }
        public IProductTranslationRepo ProductTranslationRepo { get; }
        public ICartRepo CartRepo { get;  }
        public ICartItemRepo CartItemRepo { get; }
        public int Save();
    }
}
