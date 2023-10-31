namespace Techblog.Repositories
{
    public interface IImagePepo
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
