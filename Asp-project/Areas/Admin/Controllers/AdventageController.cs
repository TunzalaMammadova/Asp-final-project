using System;
using Asp_project.Data;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Advantages;
using Microsoft.AspNetCore.Mvc;
namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdventageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAdventageService _adventageService;

        public AdventageController(AppDbContext context,
                                   IAdventageService adventageService)
        {
            _context = context;
            _adventageService = adventageService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _adventageService.GetAllOrderByDescAsync();
            return View(datas);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdventageCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            await _adventageService.CreateAsync(request);

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var adventage = await _adventageService.GetByIdAsync((int)id);

            if (adventage is null) return NotFound();

            _adventageService.DeleteAsync(adventage);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var adventage = await _adventageService.GetByIdAsync((int)id);

            if (adventage is null) return NotFound();

            return View(new AdventageEditVM { Icon = adventage.Icon, Title = adventage.Title, Desc = adventage.Desc });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AdventageEditVM editVM)
        {
            if (id is null) return BadRequest();

            var adventage = await _adventageService.GetByIdAsync((int)id);

            if (adventage is null) return NotFound();

            await _adventageService.EditAsync(adventage, editVM);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var advantage = await _adventageService.GetByIdAsync((int)id);

            if (advantage is null) return NotFound();

            return View(new AdventageDetailVM { Icon = advantage.Icon, Title = advantage.Title, Desc = advantage.Desc});
        }
    }
}
