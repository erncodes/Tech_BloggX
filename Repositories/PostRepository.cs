using Microsoft.EntityFrameworkCore;
using Techblog.Data;
using Techblog.Models.DomainModels;

namespace Techblog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly TechBloggsDbContext techBloggsDbContext;

        public PostRepository(TechBloggsDbContext techBloggsDbContext)
        {
            this.techBloggsDbContext = techBloggsDbContext;
        }
        public async Task<TechPost> AddAsync(TechPost techPost)
        {
            var post = await techBloggsDbContext.AddAsync(techPost);
            await techBloggsDbContext.SaveChangesAsync();
            return techPost;
        }

        public async Task<TechPost?> DeleteAsync(Guid Id)
        {
            var existingpost = await techBloggsDbContext.TechPosts.FindAsync(Id);
            if(existingpost != null)
            {
                techBloggsDbContext.TechPosts.Remove(existingpost);
                await techBloggsDbContext.SaveChangesAsync();
                return existingpost;
            }
            return null;
        }

        public async Task<TechPost?> GetAPostAsync(Guid id)
        {
           return await techBloggsDbContext.TechPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TechPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await techBloggsDbContext.TechPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

        }

        public async Task <IEnumerable<TechPost>> GetPostsAsync()
        {
            return await techBloggsDbContext.TechPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<TechPost?> UpdateAsync(TechPost techPost)
        {
            var existblog = await techBloggsDbContext.TechPosts.Include(x => x.Tags).
                FirstOrDefaultAsync(x => x.Id == techPost.Id);

            if(existblog != null)
            {
                existblog.Id = techPost.Id;
                existblog.Content = techPost.Content;
                existblog.PageHeading = techPost.PageHeading;
                existblog.Heading = techPost.Heading;
                existblog.Author = techPost.Author;
                existblog.ShortDescription = techPost.ShortDescription;
                existblog.PublishedDate = techPost.PublishedDate;
                existblog.Visible = techPost.Visible;
                existblog.UrlHandle = techPost.UrlHandle;
                existblog.FeaturedImageUrl = techPost.FeaturedImageUrl;
                existblog.Tags = techPost.Tags;

                await techBloggsDbContext.SaveChangesAsync();
                return existblog;

            }
            return null;
        }
    }
}
