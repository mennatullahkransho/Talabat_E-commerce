using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjets.IdentityModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class AuthenticationController (IServiceManager serviceManager): ApiBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User =  await serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }
    }
}
