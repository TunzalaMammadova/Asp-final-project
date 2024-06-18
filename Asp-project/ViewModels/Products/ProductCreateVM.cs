using System;
using System.ComponentModel.DataAnnotations;

namespace Asp_project.ViewModels.Products
{
	public class ProductCreateVM
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public List<IFormFile> Image { get; set; }
    }
}

