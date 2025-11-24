using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObjets.IdentityModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager) : IAuthenticationService
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
                    Token = "FakeToken"
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
                    Token = "FakeToken"
                };
            }
            else
            {
                var Errors= result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }
    }
}
