using System;
using Asp_project.Data;
using Asp_project.Helpers.Extensions;
using Asp_project.Models;
using Asp_project.ViewModels.SliderInfos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderInfoController(AppDbContext context,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderInfo> sliderInfos = await _context.SliderInfos.ToListAsync();

            List<SliderInfoVM> result = sliderInfos.Select(m => new SliderInfoVM { Id = m.Id, Image = m.Background, Title = m.Title, Description = m.Description }).ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfoCreateVM request)
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

            await _context.SliderInfos.AddAsync(new SliderInfo { Background = fileName, Title = request.Title , Description = request.Description });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", sliderInfo.Background, sliderInfo.Title, sliderInfo.Description);

            path.DeleteFileFromToLocal();

            _context.SliderInfos.Remove(sliderInfo);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo is null) return NotFound();

            return View(new SliderInfoEditVM { Image = sliderInfo.Background, Title = sliderInfo.Title, Description = sliderInfo.Description });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderInfoEditVM request)
        {
            if (id is null) return BadRequest();

            var sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo is null) return NotFound();

            if (request.NewImage is not null) return RedirectToAction(nameof(Index));

            if (request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "File must be only image format");
                return View(request);
            }

            if (request.NewImage.CheckFileSize(800))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 800kb");
                request.Image = sliderInfo.Background;
                return View(request);
            }

            string oldPath = Path.Combine(_env.WebRootPath, "img", sliderInfo.Background);

            oldPath.DeleteFileFromToLocal();

            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.NewImage.SaveFileToLocalAsync(newPath);

            sliderInfo.Background = fileName;

            var datas = new SliderInfoEditVM { Title = sliderInfo.Title, Image = sliderInfo.Background };

            await _context.SaveChangesAsync();

            return View(datas);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo is null) return NotFound();

            return View(new SliderInfoDetailVM { Image = sliderInfo.Background, Title = sliderInfo.Title, Description = sliderInfo.Description });
        }
    }
}


