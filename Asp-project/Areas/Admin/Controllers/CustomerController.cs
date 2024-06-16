using System;
using Asp_project.Data;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICustomerService _customerService;

        public CustomerController(AppDbContext context,
                                  ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _customerService.GetAllOrderByDescAsync();
            return View(datas);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            await _customerService.CreateAsync(request);

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var customer = await _customerService.GetByIdAsync((int)id);

            if (customer is null) return NotFound();

            _customerService.DeleteAsync(customer);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var customer = await _customerService.GetByIdAsync((int)id);

            if (customer is null) return NotFound();

            return View(new CustomerEditVM { Icon = customer.Icon, Title = customer.Title, Desc = customer.Desc });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CustomerEditVM editVM)
        {
            if (id is null) return BadRequest();

            var customer = await _customerService.GetByIdAsync((int)id);

            if (customer is null) return NotFound();

            await _customerService.EditAsync(customer, editVM);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var customer = await _customerService.GetByIdAsync((int)id);

            if (customer is null) return NotFound();

            return View(new CustomerDetailVM { Icon = customer.Icon, Title = customer.Title, Desc = customer.Desc });
        }
    }
}

