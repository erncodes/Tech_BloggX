﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Techblog.Models.ViewModels;
using Techblog.Repositories;
using Techbloggs.Models;

namespace Techbloggs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository postRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,IPostRepository postRepository,ITagRepository tagRepository)
        {
            _logger = logger;
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var techPosts = await postRepository.GetPostsAsync();
            var tags = await tagRepository.GetTagsAsync();

            var model = new HomeViewModel
            {
                TechPosts = techPosts,
                Tags = tags
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}