using System;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels;
using Asp_project.ViewModels.Baskets;
using Asp_project.ViewModels.HeaderVM;
using Microsoft.AspNetCore.Mvc;


namespace Asp_project.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _accessor;

        public HeaderViewComponent(ISettingService settingService,
                                   IHttpContextAccessor accessor)
        {
            _settingService = settingService;
            _accessor = accessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settingDatas = await _settingService.GetAllAsync();

            List<BasketVM> basketProducts = new();

            //if (_accessor.HttpContext?.Request.Cookies["basket"] is not null)
            //{
            //    basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            //}

            HeaderVM response = new()
            {
                Settings = settingDatas,
                BasketCount = basketProducts.Sum(m => m.Count),
                BasketTotalPrice = basketProducts.Sum(m => m.Count * m.Price)
            };

            return await Task.FromResult(View(response));
        }
    }

}

