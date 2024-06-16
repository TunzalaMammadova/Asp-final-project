using System;
using Asp_project.Data;
using Asp_project.Helpers.Extensions;
using Asp_project.Models;
using Asp_project.ViewModels.Marketing;
using Asp_project.ViewModels.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarketingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MarketingController(AppDbContext context,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Marketing> marketings = await _context.Marketings.ToListAsync();

            List<MarketingVM> result = marketings.Select(m => new MarketingVM { Id = m.Id, Image = m.Image, Title = m.Title, Description = m.Description }).ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarketingCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File must be only image format");
                return View();
            }

            if (!request.Image.CheckFileSize(800))
            {
                ModelState.AddModelError("Image", "Image size must be max 800kb");
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            ViewBag.fileName = path;

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Marketings.AddAsync(new Marketing { Image = fileName, Title = request.Title, Description = request.Description });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var marketing = await _context.Marketings.FirstOrDefaultAsync(m => m.Id == id);

            if (marketing is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", marketing.Image, marketing.Title, marketing.Description);

            path.DeleteFileFromToLocal();

            _context.Marketings.Remove(marketing);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var marketing = await _context.Marketings.FirstOrDefaultAsync(m => m.Id == id);

            if (marketing is null) return NotFound();

            return View(new SaleEditVM { Image = marketing.Image, Title = marketing.Title, Description = marketing.Description });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, MarketingEditVM request)
        {
            if (id is null) return BadRequest();

            var marketing = await _context.Marketings.FirstOrDefaultAsync(m => m.Id == id);

            if (marketing is null) return NotFound();

            if (request.NewImage is not null) return RedirectToAction(nameof(Index));

            if (request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "File must be only image format");
                return View(request);
            }

            if (request.NewImage.CheckFileSize(800))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 800kb");
                request.Image = marketing.Image;
                return View(request);
            }

            string oldPath = Path.Combine(_env.WebRootPath, "img", marketing.Image);

            oldPath.DeleteFileFromToLocal();

            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.NewImage.SaveFileToLocalAsync(newPath);

            marketing.Image = fileName;

            var datas = new SaleEditVM { Title = marketing.Title, Image = marketing.Image, Description = marketing.Description };

            await _context.SaveChangesAsync();

            return View(datas);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var marketing = await _context.Marketings.FirstOrDefaultAsync(m => m.Id == id);

            if (marketing is null) return NotFound();

            return View(new MarketingDetailVM { Image = marketing.Image, Title = marketing.Title, Description = marketing.Description });
        }
    }

}



