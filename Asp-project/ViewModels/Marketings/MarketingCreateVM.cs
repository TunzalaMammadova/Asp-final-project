using System;
namespace Asp_project.ViewModels.Marketing
{
	public class MarketingCreateVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

