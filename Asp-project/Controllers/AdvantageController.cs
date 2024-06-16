using System;
using Asp_project.Models;
using Asp_project.Services;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
    public class AdventageController : Controller
    {
        private readonly IAdventageService _adventageService;

        public AdventageController(AdventageService adventageService)
        {
            _adventageService = adventageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Adventage data = await _adventageService.GetByIdAsync((int)id);

            if (data is null) return NotFound();

            return View(data);
        }

    }
}

