using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Persons.Api.Data;
using Persons.Api.Data.Entities;

namespace Persons.Api.Controllers
{
    [Route("me")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonDbContext _context;

        public PersonsController(PersonDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Persone>> Get()
        {
            if (int.TryParse(Request.Headers["X-UserId"], out int userId))
            {
                var persone = await _context.Persons.FirstOrDefaultAsync(p => p.UserId == userId);
                if (persone == null)
                {
                    return NotFound("Persone not found.");
                }

                return persone;
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Persone>> Post([FromBody] Persone persone)
        {
            if (int.TryParse(Request.Headers["X-UserId"], out int userId))
            {
                if (PersoneExists(userId))
                {
                    return BadRequest("Persone already exists.");
                }

                persone.UserId = userId;
                await _context.Persons.AddAsync(persone);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Post), new { id = persone.Id }, persone);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Persone persone)
        {
            if (int.TryParse(Request.Headers["X-UserId"], out int userId))
            {
                var dbPersone = await _context.Persons.FirstOrDefaultAsync(p => p.UserId == userId);
                if (dbPersone == null)
                {
                    return NotFound("Persone not found.");
                }

                if (userId == dbPersone.UserId)
                {
                    dbPersone.FirstName = persone.FirstName;
                    dbPersone.LastName = persone.LastName;
                    dbPersone.BirthDate = persone.BirthDate;

                    await _context.SaveChangesAsync();

                    return NoContent();
                }
            }

            return BadRequest();
        }

        private bool PersoneExists(int userId)
        {
            return _context.Persons.Any(p => p.UserId == userId);
        }
    }
}
