using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewApiProject.Api.Entites;
using NewApiProject.Api.Models;
using NewApiProject.Api.Repositories;
using NewApiProject.Api.Services;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace NewApiProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly PasswordHasher<User> _Hasher;

        public AuthController(IUserRepository repository, PasswordHasher<User> _hasher)
        {
            _repository = repository;
            _Hasher = _hasher;
        }
        [NonAction]
        public async Task<string> CreateJwtRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);

            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = await _repository.CheckJwtRefreshToken(refreshToken);

            if (tokenInUser)
            {
                return await CreateJwtRefreshToken();
            }

            return refreshToken;
        }
        [NonAction]
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes("veryveryverysecretkeythatnooneknows");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false,
            };

            var JWtTokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;

            var principal = JWtTokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("This is a invalid token");
            }
            return principal;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }


            var user = await _repository.GetUser(userObj);

            if (user == null)
            {
                return BadRequest(new { Message = "Wrong Username Or Wrong Password" });
            }

            if (_Hasher.VerifyHashedPassword(user, user.Password, userObj.Password) == 0)
            {
                return BadRequest(new { Message = "Wrong Username Or Wrong Password" });
            }


            user.Token = JwtService.CreateJwtTOKEN(user);
            user.RefreshToken = await CreateJwtRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
            await _repository.SaveChanges();

            return Ok(new TokenDto
            {
                AccessToken = user.Token,
                RefreshToken = user.RefreshToken,
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            userObj.Password = _Hasher.HashPassword(userObj, userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";

            await _repository.RegisterUser(userObj);

            return Ok(new { Message = $"User: {userObj.Username} is registered" });
        }
        
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenDto token)
        {

            if (token is null)
            {
                return BadRequest("Invalid token request");
            }

            var principal = GetPrincipalFromExpiredToken(token.AccessToken);

            var username = principal.Identity.Name;

            var user = await _repository.GetUserByUsername(username);

            if (user is null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid token request");
            }

            var newAccessToken = JwtService.CreateJwtTOKEN(user);

            var newRefreshToken = await CreateJwtRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _repository.SaveChanges();

            return Ok(new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

    }
}
