using JWT_Authentication.Interfaces;
using JWT_Authentication.Models;
using JWT_Authentication.RequestModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        // POST api/<AuthController>
        [HttpPost("login")]
        public string Ligin([FromBody] LoginRequestEntity loginmodel)
        {
            var result=_authService.Login(loginmodel);
            return result;
        }

        // PUT api/<AuthController>/5
        [HttpPost("addUser")]
        public User AddUser([FromBody] User value)
        {
            var user=_authService.AddUser(value);
            return user;    
        }

       
    }
}
