using Techblog.Models.DomainModels;

namespace Techblog.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<TechPost> TechPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
