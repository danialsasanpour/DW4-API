using DW4.DAL;
using DW4.Model;
using DW4.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace DW4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public UsersController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserCreateViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Password))

                return BadRequest("model is not valid");

            if (!IsValid(model.Email))
            {
                return BadRequest("email is not valid");
            }

            if (_applicationDbContext.Users.Any(x => x.Email.Equals(model.Email)))
            {
                return BadRequest("Email is duplicate");
            }


            var newUser = new User()
            {
                Email = model.Email,
                Name = model.Name,
                Password = model.Password,
            };
            _applicationDbContext.Users.Add(newUser);
            _applicationDbContext.SaveChanges();

            return Ok(newUser);
        }



        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("model is not valid");

            var user = _applicationDbContext.Users.FirstOrDefault(x =>
                x.Email.Equals(model.Email) && x.Password.Equals(model.Password));

            if (user == null)
            {
                return BadRequest("Email or password is not valid!");
            }

            var token = GenerateToken(user);

            return Ok(token);
        }

        private string GenerateToken(User user)
        {
            var session = _applicationDbContext.Sessions.FirstOrDefault(x => x.Email == user.Email);
            if (session == null)
            {
                var newSession = new Session()
                {
                    Email = user.Email,
                    Token = Guid.NewGuid().ToString(),
                };
                _applicationDbContext.Sessions.Add(newSession);
                _applicationDbContext.SaveChanges();
                return newSession.Token;
            }
            else
            {
                session.Token = Guid.NewGuid().ToString();
                _applicationDbContext.SaveChanges();
                return session.Token;
            }
        }

        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;

        }


    }
}