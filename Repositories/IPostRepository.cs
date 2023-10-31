using Techblog.Models.DomainModels;

namespace Techblog.Repositories
{
    public interface IPostRepository
    {
        Task <IEnumerable<TechPost>> GetPostsAsync();
        Task <TechPost?> GetAPostAsync(Guid id);

        Task<TechPost> AddAsync(TechPost techPost);
        Task<TechPost?> UpdateAsync(TechPost techPost);
        Task<TechPost?> DeleteAsync(Guid id);
        Task<TechPost?> GetByUrlHandleAsync(string urlHandle);
    }
}
