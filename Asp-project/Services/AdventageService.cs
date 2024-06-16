using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Advantages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class AdventageService : IAdventageService
    {
        private readonly AppDbContext _context;

        public AdventageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Adventage> GetByIdAsync(int id)
        {
            return await _context.Advantages.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Adventage>> GetAllAsync()
        {
            return await _context.Advantages.Where(m => !m.SoftDeleted).ToListAsync();
                                          
        }

        public async Task CreateAsync(AdventageCreateVM request)
        {
            await _context.Advantages.AddAsync(new Adventage { Icon = request.Icon, Desc = request.Desc, Title = request.Title });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Adventage adventage)
        {
            _context.Advantages.Remove(adventage);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Adventage adventage, AdventageEditVM editVM)
        {
            adventage.Desc = editVM.Desc;
            adventage.Icon = editVM.Icon;
            adventage.Title = editVM.Title;

            await _context.SaveChangesAsync();
        }

        public async Task<List<AdventageVM>> GetAllOrderByDescAsync()
        {
            List<Adventage> adventages = await _context.Advantages.OrderByDescending(m => m.Id)
                                                                  .ToListAsync();

            return adventages.Select(m => new AdventageVM { Id = m.Id, Icon = m.Icon, Title = m.Title, Desc = m.Desc }).ToList();

        }
    }
}

