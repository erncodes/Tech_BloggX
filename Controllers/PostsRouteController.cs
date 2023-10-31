using Microsoft.AspNetCore.Mvc;
using Techblog.Repositories;

namespace Techblog.Controllers
{
    public class PostsRouteController : Controller
    {
        private readonly IPostRepository postRepository;

        public PostsRouteController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        [HttpGet]
        public async Task <IActionResult> Index(string urlHandle)
        {
            var post = await postRepository.GetByUrlHandleAsync(urlHandle);
            return View(post);
        }
    }
}
