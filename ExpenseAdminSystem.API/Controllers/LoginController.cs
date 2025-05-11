using ExpenseAdminSystem.API.Model;
using ExpenseAdminSystem.Model.Entities;
using ExpenseAdminSystem.Model.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAdminSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public LoginController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] Login credentials)
        {
            if (credentials == null || string.IsNullOrEmpty(credentials.Username) || string.IsNullOrEmpty(credentials.Password))
            {
                return BadRequest("Invalid client request");
            }

            var users = _userRepository.GetUsers(); 
            var user = users.FirstOrDefault(u => u.UserName == credentials.Username);

            if (user != null && user.Password == credentials.Password)
            {
                var text = $"{credentials.Username}:{credentials.Password}";
                var bytes = System.Text.Encoding.Default.GetBytes(text);
                var encodedCredentials = Convert.ToBase64String(bytes);

                var headerValue = $"Basic {encodedCredentials}";
                return Ok(new { 
                    headerValue = headerValue,
                    username = user.UserName,
                    id = user.Id
                 });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}






