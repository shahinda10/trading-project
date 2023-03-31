using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiJwt.Models;

namespace webApiJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]

        public string GetData()
        {
            return "authenticated with JWT";
        }

        [HttpGet]
        [Route("Details")]

        public string Details()
        {
            return "authenticated with JWT";
        }

        [Authorize]
        [HttpPost]
       

        public string AddUser(Users user)
        {
            return "user added with username"+ user.username;
        }


    }
}
