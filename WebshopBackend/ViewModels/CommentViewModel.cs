using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopBackend.Models;

namespace WebshopBackend.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        //public AccountViewModel User { get; set; }
        //public ProductViewModel Product { get; set; }
        public CommentViewModel(Comment comment)
        {
            Id = comment.Id;
            Title = comment.Title;
            Review = comment.Review;
            Rating = comment.Rating;
        }

    }
}