using System;
using Asp_project.Data;
using Asp_project.Helpers.Extensions;
using Asp_project.Models;
using Asp_project.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();

            List<SliderVM> result = sliders.Select(m => new SliderVM { Id = m.Id, Image = m.Image, Title = m.Title }).ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Images", "File must be only image format");
                return View();
            }

            if (!request.Image.CheckFileSize(800))
            {
                ModelState.AddModelError("Images", "Image size must be max 800kb");
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            ViewBag.fileName = path;

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Sliders.AddAsync(new Slider { Image = fileName, Title = request.Title });

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image, slider.Title);

            path.DeleteFileFromToLocal();

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(new SliderEditVM { Image = slider.Image, Title = slider.Title });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File must be only image format");
                    request.Image = slider.Image;
                    return View(request);
                };

                if (!request.NewImage.CheckFileSize(500))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 500kb");
                    request.Image = slider.Image;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

                oldPath.DeleteFileFromToLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);

                slider.Image = fileName;
            }

            slider.Title = request.Title;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(new SliderDetailVM { Image = slider.Image, Title = slider.Title });
        }
    }
}