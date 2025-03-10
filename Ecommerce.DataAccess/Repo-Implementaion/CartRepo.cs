using Ecommerce.DataAccess.Data;
using Ecommerce.Entities.Models;
using Ecommerce.Entities.Repo_Interfaces;
using Ecommerce.Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repo_Implementaion
{
    public class CartRepo : GenericRepo<Cart>, ICartRepo
    {
        private readonly ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context) : base(context)
        {
            _context= context;
        }
        public CartItem AddProductToCart(Guid userId, CartItemDto cartItemDto)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == cartItemDto.ProductId);
            if (product == null)
            {
                return null; 
            }

            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };

                _context.Carts.Add(cart);
                _context.SaveChanges(); 
            }

            // if the product exist in the cart
            var cartItemInDb = _context.CartItems.FirstOrDefault(
                ci => ci.CartId == cart.Id && ci.ProductId == cartItemDto.ProductId
            );

            if (cartItemInDb != null)
            {
                cartItemInDb.Quantity += cartItemDto.Quantity;
                _context.CartItems.Update(cartItemInDb);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id, 
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity
                };

                _context.CartItems.Add(cartItem);

                cartItemInDb = cartItem; 
            }

            _context.SaveChanges(); 

            return cartItemInDb;
        }


    }
}
