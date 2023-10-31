using Microsoft.AspNetCore.Identity;

namespace Techblog.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
