using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels;
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
            return View(await _settingService.GetAllAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();

            Setting data = await _settingService.GetByIdAsync((int)id);

            if (data is null) return NotFound();

            SettingDetailVM model = new()
            {
                Address = data.Key,

            };

            return View(model);
        }

    }
}
