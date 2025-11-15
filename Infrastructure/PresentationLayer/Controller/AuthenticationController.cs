using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Login(RegisterDto RegisterDto)
        {
            var user = await _serviceManager.AuthenticationService.RgisterAsync(RegisterDto);
            return Ok(user);
        }
    
    
    
    
    }
}
