
using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //[AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userTologin = _authService.Login(userForLoginDto);
            if (!userTologin.Success)
            {
                return BadRequest(userTologin);
            }
            var result = _authService.CreateAccessToken(userTologin.Data);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExist(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
