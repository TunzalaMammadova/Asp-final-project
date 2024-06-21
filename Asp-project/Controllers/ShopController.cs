using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Controllers
{
	public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;


        public ShopController(IProductService productService,
                              AppDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
    }
}

