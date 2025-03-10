using Ecommerce.Entities.Models;
using Ecommerce.Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Repo_Interfaces
{
    public interface ICartRepo:IGenericRepo<Cart>
    {
        CartItem AddProductToCart(Guid userId, CartItemDto cartItemDto);
    }
}
