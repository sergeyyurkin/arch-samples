using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Api.Data.Entities;

namespace Identity.Api.Data
{
    public class InitialDbBuilder
    {
        private readonly AuthDbContext _context;

        public InitialDbBuilder(AuthDbContext context)
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
                var dbUsers = new List<ApplicatinUser>
                {
                    new ApplicatinUser
                    {
                        Login = "alice",
                        Password = "alice",
                        Email = "alice@gmail.com",
                        FirstName = "Alice",
                        LastName = "Smith"
                    }
                };

                await _context.Users.AddRangeAsync(dbUsers);
            }
        }
    }
}
