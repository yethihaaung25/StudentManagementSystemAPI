using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.Models.DataModel;
using StudentManagementSystem.Models.ViewModel;
using StudentManagementSystem.Services.Domains;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StudentManagementSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentServices _studentServices;
        private readonly IConfiguration _configuration;
        public StudentController(IStudentServices studentServices, IConfiguration configuration) 
        {
            _studentServices = studentServices;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public JsonResult StudentRegister([FromBody] StudentViewModel studentViewModel)
        {
            var email = _studentServices.GetByEmail(studentViewModel.Email);
            if(email != null)
            {
                return Json(new {Message = "Your Email is already exist. Try another one!!!"});
            }
            if(studentViewModel.Password == studentViewModel.ConfirmPassword)
            {
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(studentViewModel.Password);
                StudentEntity student = new StudentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = studentViewModel.Name,
                    Email = studentViewModel.Email,
                    Password = hashPassword,
                };
                _studentServices.Create(student);
                return Json(new { Message = "Registraion success." });
            }
            else
            {
                return Json(new { Message = "Registration fails!!!." });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public JsonResult StudentLogin([FromBody] LoginViewModel loginViewModel)
        {
            ActionResult response = Unauthorized();
            var user = _studentServices.GetByEmail(loginViewModel.Email);
            if(user != null)
            {
                var password = BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password);
                if (!password)
                {
                    response = Json(new { Message = "Password is incorrect!!!" });
                }
                else
                {
                    var token = GenerateToken(loginViewModel);
                    response = Json(new { Message = "Login Success.", Token = token });
                }
            }
            else
            {
                response = Json(new { Message = "User Not Found." });
            }
            return Json(response);
        }

        [NonAction]
        public string GenerateToken(LoginViewModel login)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],_configuration["Jwt:Audience"],null,expires : DateTime.Now.AddHours(1), signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
