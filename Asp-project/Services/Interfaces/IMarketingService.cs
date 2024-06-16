using System;
using Asp_project.Models;
using Asp_project.ViewModels.Marketing;
using Asp_project.ViewModels.Sales;

namespace Asp_project.Services.Interfaces
{
	public interface IMarketingService
	{
        Task<Marketing> GetByIdAsync(int id);
        Task<List<Marketing>> GetAllAsync();
    }
}

