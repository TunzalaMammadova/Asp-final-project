using System;
using System.ComponentModel.DataAnnotations;

namespace Asp_project.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
        public string Title { get; set; }
    }
}

