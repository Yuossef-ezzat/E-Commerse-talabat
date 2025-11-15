using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstractionLayer;
using Shared.DTOs.IdentityDtos;
using DomainLayer.Exceptions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace ServicesLayer
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null)
                throw new UserNotFoundException(loginDto.Email);

            var res = await _userManager.CheckPasswordAsync(User, loginDto.Password);

            if (!res)
                throw new UnauthorizedException();

            return new UserDto
            {
                Email = User.Email!,
                DisplayName = User.DisplayName,
                Token = await GenerateJwtToken(User)
            };

        }

        public async Task<UserDto> RgisterAsync(RegisterDto RegisterDto)
        {
            var user = new ApplicationUser
            {
                DisplayName = RegisterDto.DisplayName,
                Email = RegisterDto.Email,
                UserName = RegisterDto.Email,
                PhoneNumber = RegisterDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, RegisterDto.Password);
            if (result.Succeeded)
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.DisplayName,
                    Token = await GenerateJwtToken(user)
                };
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
        }
        
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            // create the payload from the user info {Claims}
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id!)
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var SecretKey = _configuration["JwtOptions:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));

            var Credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials
            );

            var TokenHandler = new JwtSecurityTokenHandler().WriteToken(Token);

            return TokenHandler;
        }
    }
}
