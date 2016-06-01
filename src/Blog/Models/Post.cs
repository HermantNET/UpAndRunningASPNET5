using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Blog.Models
{
    public class Post
    {
        public long Id { get; set; }

        public string Key
        {
            get
            {
                if (Title == null)
                    return null;

                var key = Regex.Replace(Title, @"[^a-zA-Z0-9\- ]", string.Empty);
                return key.Replace(" ", "-").ToLower();
            }
        }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = "Title field is required.")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Title must be between 5 and 100 characters long.")]
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime Posted { get; set; }

        [Display(Name = "Body:")]
        [Required(ErrorMessage = "Body field is required.")]
        [DataType(DataType.MultilineText)]
        [MinLength(5,
            ErrorMessage = "Must be at least 5 characters long.")]
        public string Body { get; set; }
    }
}
