using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Shop;
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
            var products = await _context.Products.Include(m => m.ProductImage).Include(m => m.Category).ToListAsync();
            var categories = await _context.Categories.Include(m=>m.Products).ToListAsync();

            ShopVM model = new() { Products = products, Categories = categories };


            return View(model);
        }
    }
}

