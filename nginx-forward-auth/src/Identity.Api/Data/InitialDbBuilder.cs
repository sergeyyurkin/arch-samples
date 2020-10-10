using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Api.Data.Entities;

namespace Identity.Api.Data
{
    public class InitialDbBuilder
    {
        private readonly IdentityDbContext _context;

        public InitialDbBuilder(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync()
        {
            await CreateUsersAsync();
            await _context.SaveChangesAsync();
        }

        private async Task CreateUsersAsync()
        {
            if (!_context.Users.Any())
            {
                var dbUsers = new List<IdentityUser>
                {
                    new IdentityUser
                    {
                        Login = "alice",
                        Password = "alice",
                        Email = "alice@gmail.com"
                    }
                };

                await _context.Users.AddRangeAsync(dbUsers);
            }
        }
    }
}
