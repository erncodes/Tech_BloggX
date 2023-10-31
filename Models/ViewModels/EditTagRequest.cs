namespace Techblog.Models.ViewModels
{
    public class EditTagRequest
    {
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
