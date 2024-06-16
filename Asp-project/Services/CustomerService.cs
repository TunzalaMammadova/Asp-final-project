using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Advantages;
using Asp_project.ViewModels.Customers;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
	public class CustomerService : ICustomerService
	{
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.Where(m => !m.SoftDeleted).ToListAsync();

        }

        public async Task CreateAsync(CustomerCreateVM request)
        {
            await _context.Customers.AddAsync(new Customer { Icon = request.Icon, Desc = request.Desc, Title = request.Title });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Customer customer, CustomerEditVM editVM)
        {
            customer.Desc = editVM.Desc;
            customer.Icon = editVM.Icon;
            customer.Title = editVM.Title;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CustomerVM>> GetAllOrderByDescAsync()
        {
            List<Customer> customers = await _context.Customers.OrderByDescending(m => m.Id)
                                                               .ToListAsync();

            return customers.Select(m => new CustomerVM { Id = m.Id, Icon = m.Icon, Title = m.Title, Desc = m.Desc }).ToList();

        }
    }
}

