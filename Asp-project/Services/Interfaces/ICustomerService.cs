using System;
using Asp_project.Models;
using Asp_project.ViewModels.Customers;

namespace Asp_project.Services.Interfaces
{
	public interface ICustomerService
	{
        Task<Customer> GetByIdAsync(int id);
        Task<List<Customer>> GetAllAsync();
        Task<List<CustomerVM>> GetAllOrderByDescAsync();
        Task CreateAsync(CustomerCreateVM customer);
        Task DeleteAsync(Customer customer);
        Task EditAsync(Customer customer, CustomerEditVM editVM);
    }
}

