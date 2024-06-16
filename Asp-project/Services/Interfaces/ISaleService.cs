using System;
using Asp_project.Models;
using Asp_project.ViewModels.Sales;

namespace Asp_project.Services.Interfaces
{
	public interface ISaleService
	{
        Task<Sale> GetByIdAsync(int id);
        Task<List<Sale>> GetAllAsync();
        List<SaleVM> GetMappedDatas(List<Sale> sales);
        Task<int> GetCountAsync();
    }
}

