// using ExpenseAdminSystem.API.Model;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;

// namespace ExpenseAdminSystem.API.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class LoginController : ControllerBase
//     {
//       // In real world application, these would be saved in a database
//       private const string USERNAME = "Admin";
//       private const string PASSWORD = "HardPassword";

//       [AllowAnonymous]
//       [HttpPost]
//       public ActionResult Login([FromBody] Login credentials) {
//          if (credentials.Username == USERNAME && credentials.Password == PASSWORD) {
//             // 1. Concatenate username and password with a semicolon
//             var text = $"{credentials.Username}:{credentials.Password}";

//             // 2. Base64encode the above
//             var bytes = System.Text.Encoding.Default.GetBytes(text);
//             var encodedCredentials = Convert.ToBase64String(bytes);  

//             // 3. Prefix with Basic
//             var headerValue = $"Basic {encodedCredentials}";
//             return Ok(new { headerValue = headerValue });          
//          }
//          else {
//             return Unauthorized();
//          }
//       }
//     }
// }


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

            // Fetch the user from the database
            var users = _userRepository.GetUsers(); // You could create a GetUserByUsername method if needed for optimization
            var user = users.FirstOrDefault(u => u.UserName == credentials.Username);

            if (user != null && user.Password == credentials.Password)
            {
                // User exists and password matches
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
                // User not found or password mismatch
                return Unauthorized();
            }
        }
    }
}






