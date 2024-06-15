using System;
using Asp_project.Models;
using Asp_project.ViewModels;

namespace Asp_project.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
    }
}

