using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using webApiJwt.Models;

namespace webApiJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            _config= configuration;
        }
         private Users Authentication(Users user)
        {
            Users _user = null;
        
            if (user.username == "admin" && user.password == "1234")
            {
                _user = new Users { username = "shahinda" };
            }
            return _user;
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var crentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],null,
                expires:DateTime.Now.AddMinutes(1),
                signingCredentials:crentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult login(Users user)
        {
            IActionResult response = Unauthorized();
            var user_ = Authentication(user);
            if(user_ != null)
            {
                var token = GenerateToken(user_);
                response = Ok(new { token = token });

            }
            return response;
        }
    }
}
