using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.Business.Abstract;
using PTS.Core.Messages;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Plugins.Authentication.Models;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Models.Auth;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authService.LoginAsync(model);
            if (result.Success) return Ok(result);
            return Unauthorized(result.Message);
        }

        [HttpPost("LoginByRefreshToken")]
        public async Task<IActionResult> LoginByRefreshToken([FromBody] RefreshTokenModel model)
        {
            var result = await _authService.LoginByRefreshTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(await _authService.LogoutAsync());
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            return Ok(await _authService.ChangePasswordAsync(model));
        }

        [HttpGet("User")]
        public async Task<IActionResult> UserInfo()
        {
            await Task.CompletedTask;
            if (_userService.IsLogin)
                return Ok(new SuccessDataResponse<UserInfo>(_userService.UserInfo));
            return Unauthorized(PersonnelMessage.AuthenticationError);
        }
    }
}