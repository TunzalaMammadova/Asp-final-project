using System;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels;
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

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value); ;
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

    }
}

