using Techblog.Data;
using Techblog.Models.DomainModels;
using Microsoft.EntityFrameworkCore;


namespace Techblog.Repositories
{
   
    public class TagRepository : ITagRepository
    {
        private readonly TechBloggsDbContext techBloggsDbContext;

        public TagRepository(TechBloggsDbContext techBloggsDbContext)
        {
            this.techBloggsDbContext = techBloggsDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await techBloggsDbContext.Tags.AddAsync(tag);
            await techBloggsDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid TagId)
        {
           var existing = await techBloggsDbContext.Tags.FindAsync(TagId);
            if(existing != null)
            {
                techBloggsDbContext.Remove(existing);
                await techBloggsDbContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }

        public async Task<Tag?> GetATagAsync(Guid TagId)
        {
           return await techBloggsDbContext.Tags.FirstOrDefaultAsync(x => x.TagId == TagId);
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await techBloggsDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
           var existss = await techBloggsDbContext.Tags.FindAsync(tag.TagId);
            if(existss != null)
            {
                existss.Name = tag.Name;
                existss.DisplayName = tag.DisplayName;
                await techBloggsDbContext.SaveChangesAsync();
                return existss;
            }
            return null;
        }
    }
}
