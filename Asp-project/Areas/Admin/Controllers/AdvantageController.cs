using System;
using Asp_project.Data;
using Asp_project.Helpers.Extensions;
using Asp_project.Models;
using Asp_project.ViewModels.Advantages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvantageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdvantageController(AppDbContext context,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Advantage> advantages = await _context.Advantages.ToListAsync();

            List<AdvantageVM> result = advantages.Select(m => new AdvantageVM { Id = m.Id, Icon = m.Icon, Title = m.Title, Desc = m.Desc }).ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvantageCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            await _context.Advantages.AddAsync(new Advantage { Icon = request.Icon, Desc = request.Desc, Title = request.Title });

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    var advantage = await _context.Advantages.FirstOrDefaultAsync(m => m.Id == id);

        //    if (advantage is null) return NotFound();

        //    string path = Path.Combine(_env.WebRootPath, "img", advantage.Icon, advantage.Title, advantage.Desc);
        //    path.DeleteFileFromToLocal();

        //    _context.Advantages.Remove(advantage);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));

        //}


        //[HttpGet]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        //    if (slider is null) return NotFound();

        //    return View(new AdvantageEditVM { Image = slider.Image, Title = slider.Title });
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int? idc, AdvantageEditVM request)
        //{
        //    if (id is null) return BadRequest();

        //    var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

        //    if (slider is null) return NotFound();

        //    if (request.NewImage is not null) return RedirectToAction(nameof(Index));

        //    if (request.NewImage.CheckFileType("image/"))
        //    {
        //        ModelState.AddModelError("NewImage", "File must be only image format");
        //        return View(request);
        //    }

        //    if (request.NewImage.CheckFileSize(800))
        //    {
        //        ModelState.AddModelError("NewImage", "Image size must be max 800kb");
        //        request.Image = slider.Image;
        //        return View(request);
        //    }

        //    string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

        //    oldPath.DeleteFileFromToLocal();

        //    string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

        //    string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

        //    await request.NewImage.SaveFileToLocalAsync(newPath);

        //    slider.Image = fileName;

        //    var datas = new SliderEditVM { Title = slider.Title, Image = slider.Image };

        //    await _context.SaveChangesAsync();

        //    return View(datas);
        //}


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var advantage = await _context.Advantages.FirstOrDefaultAsync(m => m.Id == id);

            if (advantage is null) return NotFound();

            return View(new AdvantageDetailVM { Icon = advantage.Icon, Title = advantage.Title, Desc = advantage.Desc});
        }
    }
}
