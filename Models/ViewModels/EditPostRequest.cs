﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Techblog.Models.ViewModels
{
    public class EditPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageHeading { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public bool Visible { get; set; }

        //Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        //collect tags
        public string[] SelectedTags { get; set; } = Array.Empty<string>();

    }
}
