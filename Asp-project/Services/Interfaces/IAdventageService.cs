using System;
using Asp_project.Models;
using Asp_project.ViewModels.Advantages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp_project.Services.Interfaces
{
    public interface IAdventageService
    {
        Task<Adventage> GetByIdAsync(int id);
        Task<List<Adventage>> GetAllAsync();
        Task<List<AdventageVM>> GetAllOrderByDescAsync();
        Task CreateAsync(AdventageCreateVM adventage);
        Task DeleteAsync(Adventage adventage);
        Task EditAsync(Adventage adventage, AdventageEditVM adventageEdit);
    }

}