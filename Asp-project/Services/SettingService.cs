using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Customers;
using Asp_project.ViewModels.Settings;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings.Where(m => !m.SoftDeleted).ToListAsync();

        }

        public async Task CreateAsync(SettingCreateVM request)
        {
            await _context.Settings.AddAsync(new Setting { LogoName = request.LogoName, Email = request.Email, Address = request.Address });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Setting Setting)
        {
            _context.Settings.Remove(Setting);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Setting setting, SettingEditVM editVM)
        {
            setting.Email = editVM.Email;
            setting.LogoName = editVM.LogoName;
            setting.Address = editVM.Address;

            await _context.SaveChangesAsync();
        }

        public async Task<List<SettingVM>> GetAllOrderByDescAsync()
        {
            List<Setting> settings = await _context.Settings.OrderByDescending(m => m.Id)
                                                             .ToListAsync();

            return settings.Select(m => new SettingVM { Id = m.Id, LogoName = m.LogoName, Email = m.Email, Address = m.Address }).ToList();

        }
    }
}




