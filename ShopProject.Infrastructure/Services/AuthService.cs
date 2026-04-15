using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Common.Models;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }


        public async Task<AuthModel> GetTokenAsync(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthModel { Message = "Invalid email or password." };
            }

            var jwttoken = await CreateJwtToken(user);

            return new AuthModel
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwttoken),
                ExpiresOn = jwttoken.ValidTo,
                Username = user.UserName,
                Email = user.Email,
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        
        {

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<System.Security.Claims.Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));

            return new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"]!)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );


        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await userManager.FindByEmailAsync(model.Email) != null) { 
                return new AuthModel { Message = "Email already exists." };
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };  

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                { 
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = $"User creation failed: {errors}" };
            }
            await userManager.AddToRoleAsync(user, "User");
            return await GetTokenAsync(new LoginModel { Email = model.Email, Password = model.Password });

        }


    }
}
