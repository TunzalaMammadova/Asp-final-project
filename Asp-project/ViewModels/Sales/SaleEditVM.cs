using System;
namespace Asp_project.ViewModels.Sales
{
	public class SaleEditVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}

