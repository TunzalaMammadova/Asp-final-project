using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Sales;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales.Where(m => !m.SoftDeleted).ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _context.Sales.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public List<SaleVM> GetMappedDatas(List<Sale> sales)
        {
            throw new NotImplementedException();
        }
    }
}

