using System;
using Asp_project.Data;
using Asp_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderData = new SliderVMVC
            {
                Sliders = await _context.Sliders.ToListAsync(),
                SliderInfos = await _context.SliderInfos.FirstOrDefaultAsync()
            };

            return await Task.FromResult(View(sliderData));
        }
    }

    public class SliderVMVC
    {
        public List<Slider> Sliders { get; set; }
        public SliderInfo SliderInfos { get; set; }
    }
}

