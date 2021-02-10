using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediaStore.Demo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MediaStore.Demo.API.Controllers
{
    // Based on https://dotnetthoughts.net/token-based-authentication-in-aspnet-core/
    // Hard-coded JWT token - eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiZWxsYXRyaXhVc2VyIiwianRpIjoiNjEyYjIzOTktNDUzMS00NmU0LTg5NjYtN2UxYmRhY2VmZTFlIiwibmJmIjoxNTE4NTI0NDg0LCJleHAiOjE1MjM3MDg0ODQsImlzcyI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSIsImF1ZCI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSJ9.Nq6OXqrK82KSmWNrpcokRIWYrXHanpinrqwbUlKT_cs
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration) => _configuration = configuration;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserIdFromCredentials(loginViewModel);
                if (userId == -1)
                {
                    return Unauthorized();
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, loginViewModel.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Issuer"],
                    audience: _configuration["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(60),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),
                         SecurityAlgorithms.HmacSha256));

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest();
        }

        private int GetUserIdFromCredentials(LoginViewModel loginViewModel)
        {
            var userId = -1;
            if (loginViewModel.Username == "bellatrixUser" && loginViewModel.Password == "SuperSecret")
            {
                userId = 5;
            }

            return userId;
        }
    }
}
