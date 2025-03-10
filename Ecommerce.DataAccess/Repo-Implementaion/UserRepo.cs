using Ecommerce.DataAccess.Data;
using Ecommerce.Entities.Models;
using Ecommerce.Entities.Repo_Interfaces;
using Ecommerce.Shared.DTOS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repo_Implementaion
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configure;
        public UserRepo(ApplicationDbContext context, IConfiguration configure) : base(context)
        {
            this.context = context;
            this.configure = configure;
        }

        

        public async Task<bool> RegisterUser(RegisterUserDto userDto)
        {
            if (await context.Users.AnyAsync(u => u.Email == userDto.Email))
                //return "this email already exists with another user";
                return false;

            var passwordToHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                FullName= userDto.FullName,
                Email= userDto.Email,
                Password= passwordToHash
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            //return "User registered successfully";
            return true;
        }
        public async Task<string> LoginUser(LoginUserDto userdto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userdto.Email);
            if (user != null)
            {
                if(BCrypt.Net.BCrypt.Verify(userdto.Password, user.Password))
                {
                    return await GenerateJwtToken(user);
                }
            }
            return null;
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            SymmetricSecurityKey signkey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configure["JWT:key"]));


            SigningCredentials signingCredentials = new SigningCredentials(signkey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("FullName", user.FullName),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configure["JWT:issuer"],
                            audience: configure["JWT:audience"],
                            expires: DateTime.Now.AddHours(1),
                            claims: claims,
                            signingCredentials: signingCredentials
                        );
            var generatedtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return generatedtoken;
        }
    }
}
