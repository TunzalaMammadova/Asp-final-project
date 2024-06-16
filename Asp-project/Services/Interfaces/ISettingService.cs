using System;
using Asp_project.Models;
using Asp_project.ViewModels.Settings;

namespace Asp_project.Services.Interfaces
{
	public interface ISettingService
	{
        Task<Setting> GetByIdAsync(int id);
        Task<List<Setting>> GetAllAsync();
        Task<List<SettingVM>> GetAllOrderByDescAsync();
        Task CreateAsync(SettingCreateVM customer);
        Task DeleteAsync(Setting setting);
        Task EditAsync(Setting setting, SettingEditVM editVM);
    }
}

