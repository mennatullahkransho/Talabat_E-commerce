using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjets.IdentityModuleDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager , IConfiguration configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (isPasswordValid)
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await CreateTokenAsync(user)
                };
            else throw new UnauthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result =await userManager.CreateAsync(User, registerDto.Password);
            if(result.Succeeded)
            {
                return new UserDto
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors= result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
           {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName)

           };
            var Roles = await userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            };
            var SecretKey = configuration["JWTOptions:SecretKey"];
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Cards= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: configuration["JWTOptions:Issuer"],
                audience: configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Cards
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
