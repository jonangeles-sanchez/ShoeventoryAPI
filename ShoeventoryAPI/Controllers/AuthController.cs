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
        public ActionResult<Merchant> Login(MerchantDto req)
        {
            if(!_authService.UserExists(req.Email).Result)
            {
                return BadRequest("Email does not exist.");
            }

            string reqPassHash 
                = BCrypt.Net.BCrypt.HashPassword(req.Password);
            string userPassHash = _authService.GetUserPassHash(req.Email);

            if(!BCrypt.Net.BCrypt.Verify(reqPassHash, userPassHash))
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
