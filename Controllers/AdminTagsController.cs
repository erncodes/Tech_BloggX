using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Techblog.Data;
using Techblog.Models.DomainModels;
using Techblog.Models.ViewModels;
using Techblog.Repositories;

namespace Techblog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
                 this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Add(AddTagRequest addTagRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                //Mapping AddTagRequest to Tag in DomainModels
                var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await tagRepository.AddAsync(tag);
           
            return RedirectToAction("List");
        }
        public async Task <IActionResult> List()
        {
            var tags = await tagRepository.GetTagsAsync();
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetATagAsync(id);
            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    TagId = tag.TagId,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }
        [HttpPost]
        public async Task <IActionResult> Edit(EditTagRequest editTagRequest)
        {

            var tag = new Tag
            {
                TagId = editTagRequest.TagId,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var updatedTag = await tagRepository.UpdateAsync(tag);
            if(updatedTag != null)
            {
                //Show Success
            }
            else
            {
                //Show failure
            }

            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task <IActionResult> Delete(EditTagRequest editTagRequest)
        {
           var deleted = await tagRepository.DeleteAsync(editTagRequest.TagId);
            if(deleted != null)
            {
                //Show success
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { TagId = editTagRequest.TagId});
        }
    }
}
