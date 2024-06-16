using System;
using Asp_project.Models;
using Asp_project.Services;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Customer data = await _customerService.GetByIdAsync((int)id);

            if (data is null) return NotFound();

            return View(data);
        }

    }
}

