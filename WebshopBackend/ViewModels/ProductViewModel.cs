using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopBackend.Models;

namespace WebshopBackend.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Image = product.Image;
            Comments = new();
            if (product.Comments != null)
            {
                for (int i = 0; i < product.Comments.Count; i++)
                {
                    Comments.Add(new(product.Comments[i]));
                }
            }
        }
    }
}