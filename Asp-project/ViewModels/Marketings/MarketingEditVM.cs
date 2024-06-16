using System;
namespace Asp_project.ViewModels.Marketing
{
	public class MarketingEditVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}

