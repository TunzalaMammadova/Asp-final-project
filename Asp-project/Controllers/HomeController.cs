using System;
using System.Reflection.Metadata;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAdventageService _adventageService;
        private readonly ISaleService _saleService;
        private readonly IMarketingService _marketingService;
        private readonly ICustomerService _customerService;
        private readonly ICategoryService _categoryService;


        public HomeController(AppDbContext context,
                              IAdventageService adventageService,
                              ISaleService saleService,
                              IMarketingService marketing,
                              ICustomerService customer,
                              ICategoryService categoryService)
                              
        {
            _context = context;
            _adventageService = adventageService;
            _saleService = saleService;
            _marketingService = marketing;
            _customerService = customer;
            _categoryService = categoryService;
           
        }

        public async Task<IActionResult> Index()
        {
            List<Adventage> adventages = await _adventageService.GetAllAsync();
            List<Sale> sales = await _saleService.GetAllAsync();
            List<Marketing> marketings = await _marketingService.GetAllAsync();
            List<Customer> customers = await _customerService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();


            HomeVM model = new()
            {
                Adventages = adventages,
                Sales = sales,
                Marketings = marketings,
                Customers = customers,
                Categories = categories
            };

            return View(model);
        }
    }
}

