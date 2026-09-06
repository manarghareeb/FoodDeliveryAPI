using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => Ok(await _serviceManager.AuthenticationService.LoginAsync(loginDto));
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
            => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> ChexkEmailExistAsync(string email)
            => Ok(await _serviceManager.AuthenticationService.CheckEmailExistAsync(email));
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
            => Ok(await _serviceManager.AuthenticationService.GetCurrentUserAsync(User.FindFirstValue(ClaimTypes.Email)));
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
            => Ok(await _serviceManager.AuthenticationService.GetUserAddressAsync(User.FindFirstValue(ClaimTypes.Email)));
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto)
            => Ok(await _serviceManager.AuthenticationService.UpdateUserAddressAsync(User.FindFirstValue(ClaimTypes.Email), addressDto));

    }
}
