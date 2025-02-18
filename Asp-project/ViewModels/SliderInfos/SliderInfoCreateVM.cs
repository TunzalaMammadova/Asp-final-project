﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Asp_project.ViewModels.SliderInfos
{
    public class SliderInfoCreateVM
    {
        [Required]
        public IFormFile Background { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

