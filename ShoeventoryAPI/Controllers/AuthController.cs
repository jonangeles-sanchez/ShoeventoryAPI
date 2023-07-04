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

namespace ShoeventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Merchant user = new Merchant();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration config) 
        {
            _configuration = config;
            
        }

        [HttpPost("register")]
        public ActionResult<Merchant> Register(MerchantDto req)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.MerchantName = req.MerchantName;
            user.Password = passwordHash;
            user.Email = req.Email;
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<Merchant> Login(MerchantDto req)
        {
            if(user.Email != req.Email)
            {
                return BadRequest("Email does not exist.");
            }

            if(!BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
            {
                return BadRequest("Password is incorrect.");
            }

            string token = CreateToken(user);

            return Ok(token);
            
        }

        private string CreateToken(Merchant user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.MerchantName)
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
