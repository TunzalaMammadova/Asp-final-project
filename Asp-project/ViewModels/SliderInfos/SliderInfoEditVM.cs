using System;
namespace Asp_project.ViewModels.SliderInfos
{
    public class SliderInfoEditVM
    {
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

