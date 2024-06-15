﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Asp_project.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        public string Title { get; set; }
    }
}

