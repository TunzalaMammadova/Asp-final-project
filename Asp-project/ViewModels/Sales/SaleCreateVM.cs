using System;
namespace Asp_project.ViewModels.Sales
{
	public class SaleCreateVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

