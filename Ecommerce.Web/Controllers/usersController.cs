using Ecommerce.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Shared.DTOS;
namespace Ecommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public usersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            bool result = await unitOfWork.UserRepo.RegisterUser(userDto);
            if (result == true)

                return CreatedAtAction(nameof(Register), userDto);
            else
                return BadRequest("this email already exists with another user");

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            var Token = await unitOfWork.UserRepo.LoginUser(userDto);
            if (Token != null)
            {
                return Ok(new {Token});
            }
            else
            { return Unauthorized("Invalid credentials"); }

        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetUserInfo(Guid id)
        {
            var user= unitOfWork.UserRepo.GetFirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                var userDto = new UserDto() {
                    FullName = user.FullName,
                    Email = user.Email,
                };
                return Ok(userDto);
            }
            return BadRequest("No User Found with this id");
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = unitOfWork.UserRepo.GetFirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                unitOfWork.UserRepo.Remove(user);
                unitOfWork.Save();
                return Ok("User with this id deleted successfully");
            }
            return BadRequest("No User Found with this id");
        }



    }
}
