using Techblog.Models.DomainModels;

namespace Techblog.Repositories

{
    public interface ITagRepository
    {
        Task <IEnumerable<Tag>> GetTagsAsync();
        Task<Tag?> GetATagAsync(Guid TagId);

        Task<Tag> AddAsync(Tag tag );
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid TagId);
    }
}
