﻿using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;

        public CategoryController(AppDbContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _categoryService.GetAllOrderByDescAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existCategory = await _categoryService.ExistAsync(category.Name);

            if (existCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }

            await _categoryService.CreateAsync(category);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetWithProductAsync((int)id);

            if (category is null) return NotFound();

            await _categoryService.DeleteAsync(category);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            return View(new CategoryEditVM { Id = category.Id, Name = category.Name });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, CategoryEditVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            Category existCategory = await _categoryService.GetByIdAsync((int)id);

            if (existCategory is null) return NotFound();

            await _categoryService.EditAsync(existCategory, category);
            return RedirectToAction(nameof(Index)); ;
        }


        [HttpGet]
        public async Task<ActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();

            Category category = await _categoryService.GetWithProductAsync((int)id);

            if (category is null) return NotFound();

            CategoryDetailVM model = new()
            {
                Name = category.Name,
                ProductCount = category.Products.Count()
            };

            return View(model);
        }
    }
}

