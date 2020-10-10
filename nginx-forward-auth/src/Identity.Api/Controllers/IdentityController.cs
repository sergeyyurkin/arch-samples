using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Api.Data;
using Identity.Api.Data.Entities;
using Identity.Api.Models.Identities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityDbContext _context;

        public IdentityController(IdentityDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
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

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel input)
        {
            // Пароль для упрощения храниться в чистом виде!
            // В реальных проектах, необходимо хранить пароль в виде хэша с солью.

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == input.Login && x.Password == input.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Ok();
            }

            return NotFound("User not found.");
        }

        [HttpGet("logout")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpGet("signin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Signin()
        {
            return Ok("Please go to login and provide Login/Password");
        }

        [Authorize]
        [HttpGet("auth")]
        [ProducesResponseType(typeof(IdentityUser), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<IdentityUser>> AuthenticateAsync()
        {
            var login = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            if (login != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
                if (user != null)
                {
                    Response.Headers.Add("X-UserId", user.Id.ToString());
                    Response.Headers.Add("X-User", user.Login);
                    Response.Headers.Add("X-Email", user.Email);

                    return Ok(user);
                }
            }

            return Unauthorized();
        }
    }
}
