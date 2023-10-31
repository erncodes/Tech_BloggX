using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Techblog.Repositories;
using System.Net;

namespace Techblog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagePepo imagePepo;

        public ImagesController(IImagePepo imagePepo)
        {
            this.imagePepo = imagePepo;
        }
        [HttpPost]
        public async Task <IActionResult> UploadAsync(IFormFile file)
        {
            var imageurl = await imagePepo.UploadAsync(file);

            if (imageurl == null)
            {
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new {link = imageurl});
        }
    }
}
