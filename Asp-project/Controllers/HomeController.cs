using System;
using System.Reflection.Metadata;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAdventageService _adventageService;

        public HomeController(AppDbContext context,
                               IAdventageService adventageService)
                              
        {
            _context = context;
            _adventageService = adventageService;
           
        }

        public async Task<IActionResult> Index()
        {
            List<Adventage> adventages = await _adventageService.GetAllAsync();

            HomeVM model = new()
            {
                Adventages = adventages
            };

            return View(model);
        }
    }
}

