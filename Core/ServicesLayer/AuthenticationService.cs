using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstractionLayer;
using Shared.DTOs.IdentityDtos;
using DomainLayer.Exceptions;

namespace ServicesLayer
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByNameAsync(loginDto.Email);
            if (User is null)
                throw new UserNotFoundException(loginDto.Email);

            var res = await _userManager.CheckPasswordAsync(User, loginDto.Password);

            if (!res)
                throw new UnauthorizedException();


        }

        public Task<UserDto> RgisterAsync(RegisterDto RegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
