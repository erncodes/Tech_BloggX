using Microsoft.AspNetCore.Mvc;
using Techblog.Models.ViewModels;
using Techblog.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Techblog.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;

namespace Techblog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IPostRepository postRepository;

        public AdminPostsController(ITagRepository tagRepository, IPostRepository postRepository)
        {
            this.tagRepository = tagRepository;
            this.postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {   //geting tags from repository
            var tags = await tagRepository.GetTagsAsync();
            var model = new AddPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.TagId.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {   //Map view model to domain model
            var techpost = new TechPost
            {
                Heading = addPostRequest.Heading,
                PageHeading = addPostRequest.PageHeading,
                ShortDescription = addPostRequest.ShortDescription,
                Content = addPostRequest.Content,
                UrlHandle = addPostRequest.UrlHandle,
                FeaturedImageUrl = addPostRequest.FeaturedImageUrl,
                PublishedDate = addPostRequest.PublishedDate,
                Author = addPostRequest.Author,
                Visible = addPostRequest.Visible,


            };
            //Add tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selelctedTagId in addPostRequest.SelectedTags)
            {
                var selectedAsGuid = Guid.Parse(selelctedTagId);
                var existingtag = await tagRepository.GetATagAsync(selectedAsGuid);

                if (existingtag != null)
                {
                    selectedTags.Add(existingtag);
                }

            }

            techpost.Tags = selectedTags;

            await postRepository.AddAsync(techpost);
            return RedirectToAction("AddPost");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var techPost = await postRepository.GetPostsAsync();
            return View(techPost);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //retrieve data from repository
            var techpost = await postRepository.GetAPostAsync(id);
            var tags = await tagRepository.GetTagsAsync();
            //map the domain model to the view model
            if (techpost != null)
            {
                var modell = new EditPostRequest
                {
                    Id = techpost.Id,
                    Heading = techpost.Heading,
                    PageHeading = techpost.PageHeading,
                    ShortDescription = techpost.ShortDescription,
                    Content = techpost.Content,
                    UrlHandle = techpost.UrlHandle,
                    FeaturedImageUrl = techpost.FeaturedImageUrl,
                    PublishedDate = techpost.PublishedDate,
                    Author = techpost.Author,
                    Visible = techpost.Visible,
                    Tags = tags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.TagId.ToString()
                    }),
                    SelectedTags = techpost.Tags.Select(x => x.TagId.ToString()).ToArray()
                };
                return View(modell);
            }
            return View(null);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPostRequest editPostRequest)
        {
            //map view model to domain model
            var soccermodel = new TechPost
            {
                Id = editPostRequest.Id,
                Heading = editPostRequest.Heading,
                PageHeading = editPostRequest.PageHeading,
                PublishedDate = editPostRequest.PublishedDate,
                Content = editPostRequest.Content,
                ShortDescription = editPostRequest.ShortDescription,
                Author = editPostRequest.Author,
                UrlHandle = editPostRequest.UrlHandle,
                FeaturedImageUrl = editPostRequest.FeaturedImageUrl,
                Visible = editPostRequest.Visible
            };
            var selectedtags = new List<Tag>();
            foreach (var selectedt in editPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedt, out var tag))
                {
                    var foundtag = await tagRepository.GetATagAsync(tag);
                    if (foundtag != null)
                    {
                        selectedtags.Add(foundtag);
                    }
                }
            }

            soccermodel.Tags = selectedtags;
            //submit to repository
            var updated = await postRepository.UpdateAsync(soccermodel);

            if(updated != null)
            {
                return RedirectToAction("Edit");
            }
            //redirect to GET
            return RedirectToAction("Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditPostRequest editPostRequest)
        {//Interact with repository to delete the post and its tags 

           var deletedp= await postRepository.DeleteAsync(editPostRequest.Id);
            if(deletedp != null)
            {
                //Show Success
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = editPostRequest.Id});
        }
    }     
}
