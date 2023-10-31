using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Techblog.Repositories
{
    public class CloudinaryRepo : IImagePepo
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public CloudinaryRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.account = new Account(configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadparams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadresult = await client.UploadAsync(uploadparams);
            if(uploadresult != null && uploadresult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadresult.SecureUri.ToString();
            }
            return null;
        }
    }
}
