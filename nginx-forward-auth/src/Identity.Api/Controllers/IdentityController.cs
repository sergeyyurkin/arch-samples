using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Api.Authentication;
using Identity.Api.Data;
using Identity.Api.Data.Entities;
using Identity.Api.Models.Identities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityDbContext _context;

        public IdentityController(IdentityDbContext context)
        {
            _context = context;
        }

        [HttpPost("/register")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel input)
        {
            // Пароль для упрощения храниться в чистом виде!
            // В реальных проектах, необходимо хранить пароль в виде хэша с солью.

            if (!await _context.Users.AnyAsync(x => x.Login == input.Login))
            {
                var user = new IdentityUser
                {
                    Login = input.Login,
                    Email = input.Email,
                    Password = input.Password
                };

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }

            return BadRequest("User already exist.");
        }

        [HttpPost("/login")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel input)
        {
            // Пароль для упрощения храниться в чистом виде!
            // В реальных проектах, необходимо хранить пароль в виде хэша с солью.

            var user = await GetUserByCredentialsAsync(input.Login, input.Password);
            if (user != null)
            {
                await Authenticate(user);

                return NoContent();
            }

            return NotFound("User not found.");
        }

        [HttpGet("/logout")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }

        [HttpGet("/signin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Signin()
        {
            return Ok("Please go to login and provide Login/Password");
        }

        //[Authorize]
        [HttpGet("/auth")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> AuthenticateAsync()
        {
            var login = User.FindFirstValue(CastomClaimTypes.Login);
            if (login != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
                if (user != null)
                {
                    Response.Headers.Add("X-User", user.Login);
                    Response.Headers.Add("X-UserId", user.Id.ToString());
                    Response.Headers.Add("X-Email", user.Email);

                    return NoContent();
                }
            }

            return Unauthorized();
        }



        private async Task<IdentityUser> GetUserByCredentialsAsync(string login, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }

        private async Task Authenticate(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(CastomClaimTypes.Login, user.Login)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());
        }
    }
}
