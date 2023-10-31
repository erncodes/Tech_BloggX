namespace Techblog.Models.DomainModels
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ICollection<TechPost> TechPosts { get; set; }
    }
}
