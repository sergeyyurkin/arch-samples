using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Application.Buyers.Dtos;
using Ordering.Core.Aggregates.BuyerAggregate;
using Ordering.EFCore;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BuyersController : ControllerBase
    {
        private readonly OrderingDbContext _context;

        public BuyersController(OrderingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Buyer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Buyer>> GetAsync(int id)
        {
            var buyer = await _context.Buyers.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }

            return buyer;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAsync([FromBody] CreateBuyerInput input)
        {
            var buyer = Buyer.Create(input.UserId, input.Name);

            await _context.Buyers.AddAsync(buyer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = buyer.Id }, buyer.Id);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
