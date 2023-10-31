using Microsoft.EntityFrameworkCore;
using Techblog.Models.DomainModels;

namespace Techblog.Data
{
    public class TechBloggsDbContext : DbContext
    {
        public TechBloggsDbContext(DbContextOptions<TechBloggsDbContext> options) : base(options)
        {
        }
        public DbSet<TechPost> TechPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
    
}
