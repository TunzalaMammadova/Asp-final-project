using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Sales;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
	public class MarketingService : IMarketingService
	{
        private readonly AppDbContext _context;

        public MarketingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Marketing>> GetAllAsync()
        {
            return await _context.Marketings.Where(m => !m.SoftDeleted).ToListAsync();
        }

        public async Task<Marketing> GetByIdAsync(int id)
        {
            return await _context.Marketings.Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}

