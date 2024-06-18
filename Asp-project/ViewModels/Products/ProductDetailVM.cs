using System;
using Asp_project.Models;

namespace Asp_project.ViewModels.Products
{
	public class ProductDetailVM
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public object Price { get; set; }
        public string Category { get; set; }
        public List<ProductImageVM> Images { get; set; }
    }
}

