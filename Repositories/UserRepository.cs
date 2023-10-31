using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Techblog.Data;

namespace Techblog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext uthDbContext;

        public UserRepository(AuthDbContext uthDbContext)
        {
            this.uthDbContext = uthDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
           var users = await uthDbContext.Users.ToListAsync();

           var superAdmin = await uthDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@techblogs.com");

            if(superAdmin is not null)
            {
                users.Remove(superAdmin);
            }

            return users;
        }
    }
}
