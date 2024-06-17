using System;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            return View(category);
        }
    }
}

