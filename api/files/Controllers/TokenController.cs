using System.Collections.Generic;
using System.Security.Claims;
using Api.Auth;
using Api.Config;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private AppConfig _config;

        private UserService _userService;

        public TokenController(UserService userService, AppConfig config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return Unauthorized("Invalid Credentials.");

            var user = _userService.FindByEmail(model.Email);
            if (user?.Password != model.Password)
                return Unauthorized("Invalid Credentials.");

            var claims = new Dictionary<string, string>();
            claims.Add(ClaimTypes.Sid, user.Id.ToString());
            var token = new JwtTokenBuilder(_config.JwtKey, _config.TokenExpiryMinutes, claims).Build();
            return Ok(new { token = token.Value });
        }
    }
}