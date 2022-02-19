using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkBahceAPI.Abstract;
using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Controllers
{   [Authorize]
    [Route("api/v{version:apiVersion}/user")]

    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userrepo;

        public UserController(IUserRepository userrepo)
        {
            _userrepo = userrepo;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {

            var user = _userrepo.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Kullanıcı adı veya şifre hatalı" }); 
            return Ok(user);
        }
        [AllowAnonymous]
       [HttpPost]
       public IActionResult Register([FromBody] User model)
        {
            bool isunique = _userrepo.IsUniqueUser(model.Username);
            if (!isunique)
            {
                return BadRequest(new { message = "Username exists" });
            }
            var user = _userrepo.Register(model.Username, model.Password);
            if(user==null)
            {
                return BadRequest(new { message = "Registering Error" });

            }
            return Ok();
        }
    }
}
