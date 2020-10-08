using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Auth.Data;
using Auth.Data.Entities;
using Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public AuthController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            // Пароль для упрощения храниться в чистом виде!
            // В реальных проектах, необходимо хранить пароль в виде хэша с солью.

            if (!await _context.Users.AnyAsync(x => x.Login == user.Login))
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }

            return BadRequest("User already exist.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            // Пароль для упрощения храниться в чистом виде!
            // В реальных проектах, необходимо хранить пароль в виде хэша с солью.

            var dbUser = _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == user.Password);
            if (dbUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie", "login", null);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                return Ok();
            }

            return NotFound("User not found.");
        }

        [HttpGet("signin")]
        public IActionResult Signin()
        {
            return Ok("Please go to login and provide Login/Password");
        }

        [Authorize]
        [HttpGet("auth")]
        public async Task<IActionResult> Auth()
        {
            var claim = User.Claims.FirstOrDefault();
            if (claim != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == claim.Value);
                if (user != null)
                {
                    Response.Headers.Add("X-UserId", user.Id.ToString());
                    Response.Headers.Add("X-User", user.Login);
                    Response.Headers.Add("X-Email", user.Email);
                    Response.Headers.Add("X-First-Name", user.FirstName);
                    Response.Headers.Add("X-Last-Name", user.LastName);

                    return NoContent();
                }
            }

            return Unauthorized();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok();
        }
    }
}
