using System;
using Asp_project.Models;

namespace Asp_project.ViewModels.Shop
{
	public class ShopVM
	{
        public List<Product> Products { get; set; }
        public List<Models.Category> Categories { get; set; }
    }
}

