using ShoeventoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

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
        public ActionResult<Merchant> Register(Merchant req)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.MerchantName = req.MerchantName;
            user.Password = passwordHash;
            user.Email = req.Email;
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<Merchant> Login(Merchant req)
        {
            if(user.Email != req.Email)
            {
                return BadRequest("Email does not exist.");
            }

            if(!BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
            {
                return BadRequest("Password is incorrect.");
            }

            return Ok(user);
            
        }

    }
}
