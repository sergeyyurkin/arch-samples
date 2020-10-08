using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Data.Entities;

namespace Auth.Data
{
    public class AuthDbContextSeed
    {
        public async Task SeedAsync(AuthDbContext context)
        {
            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(GetInitialUsers());
                await context.SaveChangesAsync();
            }
        }

        private List<User> GetInitialUsers()
        {
            return new List<User>
            {
                new User
                {
                    Login = "alice",
                    Password = "alice",
                    Email = "alice@gmail.com",
                    FirstName = "Alice",
                    LastName = "Smith"
                }
            };
        }
    }
}
