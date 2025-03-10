using Ecommerce.Entities.Models;
using Ecommerce.Entities.Repo_Interfaces;
using Ecommerce.Shared.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public productsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDto productDto)
        {
            var product = new Product();

            product.Price= productDto.Price;
            product.Quantity= productDto.Quantity;
            product.CreatedAt= DateTime.UtcNow;

            unitOfWork.ProductRepo.Add(product);
            int result=unitOfWork.Save();
            if(result > 0)
            {
   
                return CreatedAtAction(nameof(AddProduct), productDto);
            }
            return BadRequest("Error in Adding Product");
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products= unitOfWork.ProductRepo.GetAll();

            if(products != null)
            {
                List<ProductDto> productDtos = new List<ProductDto>();
                foreach (var product in products)
                {
                    productDtos.Add(new ProductDto
                    {
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CreatedAt= product.CreatedAt
                    });
                }
                return Ok(productDtos);
            }
            return NotFound("There is no products available");
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetProductDetails(Guid id)
        {
            var product = unitOfWork.ProductRepo.GetFirstOrDefault(p => p.Id == id);
            if(product != null)
            {
                ProductDto productDto = new ProductDto();
                productDto.Price = product.Price;
                productDto.Quantity = product.Quantity;
                productDto.CreatedAt = product.CreatedAt;
                return Ok(productDto);
            }
            return NotFound("No Product with this provided id");
        }

        //PUT /api/products/{id} → Update a product

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateProduct(Guid id,[FromBody]ProductDto productDto)
        {
            var product= unitOfWork.ProductRepo.GetFirstOrDefault(p=>p.Id == id);
            if( product != null )
            {
                product.Price = productDto.Price;
                product.Quantity = productDto.Quantity;
                product.CreatedAt = productDto.CreatedAt;// assume that update is another way of create so the time will change

                unitOfWork.ProductRepo.Update(product);
                unitOfWork.Save();
                return Ok("Product Updated Successfully");
            }
            return NotFound("This product is not found");
        }

        //DELETE /api/products/{id} → Remove a product

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = unitOfWork.ProductRepo.GetFirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                unitOfWork.ProductRepo.Remove(product);
                int result=unitOfWork.Save();
                if(result > 0)
                {
                    return Ok("Prodcut is deleted successfully");
                }
                return BadRequest("Error in deleting product");
            }
            return NotFound("No Product Found with this id");

           
        }

        // POST /api/products/{productId}/translations → Add a translation for a product
        [HttpPost("{productId:guid}/translations")]
        public IActionResult AddTranslationToProduct(Guid productId, ProductTranslationDto productTranslationDto)
        {
            var product= unitOfWork.ProductRepo.GetFirstOrDefault(p=>p.Id==productId);
            if( product != null )
            {
                var TranslationInDb =  unitOfWork.ProductTranslationRepo.GetFirstOrDefault(
                        t => t.ProductId == productId && t.LanguageCode == productTranslationDto.LanguageCode
                    );

                if(TranslationInDb != null )
                {
                    return BadRequest("There is already a translation with this code");
                }

                ProductTranslation translation = new ProductTranslation();

                translation.ProductId= productId;
                translation.Name= productTranslationDto.Name;
                translation.Description= productTranslationDto.Description;
                translation.LanguageCode= productTranslationDto.LanguageCode;

                unitOfWork.ProductTranslationRepo.Add(translation);
                var result=unitOfWork.Save();

                if(result > 0 )
                {
                    return CreatedAtAction(nameof (AddTranslationToProduct),productTranslationDto);
                }
                return BadRequest("Error in Adding product translation");

            }
            return NotFound("No Product Found with this id");
        }


        // GET /api/products/{productId}/translations → List all translations for a product
        [HttpGet("{productId:guid}/translations")]
        public IActionResult GetAllTranslations(Guid productId)
        {
            var product = unitOfWork.ProductRepo.GetFirstOrDefault(p => p.Id == productId,include: "Translations");
            if(product != null )
            {
                List<ProductTranslationDto> productTranslations = new List<ProductTranslationDto>();
                foreach (var translation in product.Translations)
                {
                    productTranslations.Add(new ProductTranslationDto
                    {
                        Description = translation.Description,
                        LanguageCode = translation.LanguageCode,
                        Name = translation.Name,
                    });
                }
                return Ok(productTranslations);
            }
            return NotFound("No Product Found with this id");
        }
    }
}
