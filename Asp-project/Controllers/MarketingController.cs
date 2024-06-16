using System;
using Asp_project.Models;
using Asp_project.Services;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Asp_project.Controllers
{
    public class MarketingController : Controller
    {
        private readonly IMarketingService _marketing;

        public MarketingController(MarketingService marketing)
        {
            _marketing = marketing;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Marketing data = await _marketing.GetByIdAsync((int)id);

            if (data is null) return NotFound();

            return View(data);
        }

    }

}

