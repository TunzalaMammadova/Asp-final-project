using System;
using Asp_project.Data;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels;
using Asp_project.ViewModels.Customers;
using Asp_project.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISettingService _settingService;

        public SettingController(AppDbContext context,
                                  ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _settingService.GetAllOrderByDescAsync();
            return View(datas);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            await _settingService.CreateAsync(request);

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            _settingService.DeleteAsync(setting);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            return View(new SettingEditVM { LogoName = setting.LogoName, Email = setting.Email, Address = setting.Address });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SettingEditVM editVM)
        {
            if (id is null) return BadRequest();

            var setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            await _settingService.EditAsync(setting, editVM);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            return View(new SettingDetailVM { LogoName = setting.LogoName, Email = setting.Email, Address = setting.Address });
        }
    }
}

