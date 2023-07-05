using ShoeventoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using ShoeventoryAPI.DTOs;
using Microsoft.AspNetCore.Cors;
using ShoeventoryAPI.Services.AuthService;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ShoeventoryAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [EnableCors("NgOrigins")]
    public class AuthController : ControllerBase
    {
        public static Merchant user = new Merchant();
        public readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Merchant>> Register(MerchantDto req)
        {
            bool userExists = _authService.UserExists(req.Email).Result;
            if(userExists)
            {
                return BadRequest("Email already exists.");
            }
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.MerchantName = req.MerchantName;
            user.Password = passwordHash;
            user.Email = req.Email;
            var newMerchant = await _authService.Register(req, passwordHash);
           if(newMerchant is null)
            {
                return BadRequest("Error registering user.");
            }
            return Ok(newMerchant);
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(MerchantDto req)
        {

            bool exists = await _authService.UserExists(req.Email);
            if(!exists)
            {
                return BadRequest("Email does not exist.");
            }

            string userPassHash = await _authService.GetUserPassHash(req.Email);

            if(!BCrypt.Net.BCrypt.Verify(req.Password, userPassHash))
            {
                return BadRequest("Password is incorrect.");
            }

            string token = await CreateToken(req);

            // Combine the token with the user's email
            return Ok(new {Token = token, Email = req.Email});
            
        }

        private async Task<string> CreateToken(MerchantDto user)
        {

            string name = await _authService.GetUserName(user.Email); 
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken( 
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

    }
}
