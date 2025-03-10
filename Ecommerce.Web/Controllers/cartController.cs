using Ecommerce.Entities.Repo_Interfaces;
using Ecommerce.Shared.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class cartController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public cartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //POST /api/cart/{userId}/items → Add a product to the cart

        [HttpPost("{userId:guid}/items")]
        public IActionResult AddCartProduct(Guid userId,[FromBody]CartItemDto cartItemDto)
        {
            if (cartItemDto == null || cartItemDto.Quantity <= 0)
            {
                return BadRequest("Inavalid Cart Data ");
            }
            var cartItem =unitOfWork.CartRepo.AddProductToCart(userId, cartItemDto);
            if(cartItem != null)
            {
                return Ok("Cart Item Saved Susccessfully");

            }
            return BadRequest("Error in saving cart ");

        }

        // GET /api/cart/{userId} → Get cart details

        [HttpGet("{userId:guid}")]
        public IActionResult GetCartDetails(Guid userId)
        {
            var cart = unitOfWork.CartRepo.GetFirstOrDefault(
                c => c.UserId == userId,
                include: "CartItems.Product"
            );

            if (cart == null)
            {
                return NotFound("No Data In this Cart");
            }

            var cartItemDetails = cart.CartItems
                .Where(item => item.Product != null) 
                .Select(item => new CartItemDetailsDto
                {
                    Productprice = item.Product.Price,
                    Quantity = item.Quantity,
                    CreatedAt= cart.CreatedAt
                }).ToList();

            return Ok(cartItemDetails); 
        }



        // DELETE /api/cart/{userId}/items/{productId} → Remove a product from the cart

        [HttpDelete("{userId:guid}/items/{productId:guid}")]
        public IActionResult DeleteProductFromCart(Guid userId, Guid productId)
        {
            var cart = unitOfWork.CartRepo.GetFirstOrDefault(
                c => c.UserId == userId, include: "CartItems"
            );

            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                return NotFound("Product not found in this cart");
            }

            unitOfWork.CartItemRepo.Remove(cartItem);
            unitOfWork.Save();

            return Ok("Product removed successfully");
        }


    }
}
