
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Asp_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAdventageService _adventageService;
        private readonly ISaleService _saleService;
        private readonly IMarketingService _marketingService;
        private readonly ICustomerService _customerService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _accessor;
        private readonly ISettingService _settingService;


        public HomeController(AppDbContext context,
                              IAdventageService adventageService,
                              ISaleService saleService,
                              IMarketingService marketing,
                              ICustomerService customer,
                              ICategoryService categoryService,
                              IProductService productService,
                              IHttpContextAccessor accessor,
                              ISettingService settingService)

        {
            _context = context;
            _adventageService = adventageService;
            _saleService = saleService;
            _marketingService = marketing;
            _customerService = customer;
            _categoryService = categoryService;
            _productService = productService;
            _accessor = accessor;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            List<Adventage> adventages = await _adventageService.GetAllAsync();
            List<Sale> sales = await _saleService.GetAllAsync();
            List<Marketing> marketings = await _marketingService.GetAllAsync();
            List<Customer> customers = await _customerService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();
            List<Product> products = await _productService.GetAllAsync();
            List<Setting> settings = await _settingService.GetAllAsync();



            HomeVM model = new()
            {
                Adventages = adventages,
                Sales = sales,
                Marketings = marketings,
                Customers = customers,
                Categories = categories,
                Products = products,
                Settings = settings
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> AddProductBasket(int? id)
        {

            if (id is null) return BadRequest();

            List<BasketVM> basketProducts = null;

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
               
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }

            var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == (int)id);

            var existProduct = basketProducts.FirstOrDefault(m => m.Id == (int)id);

            if (existProduct is not null)
            {
                existProduct.Count++;
            }
            else
            {
                basketProducts.Add(new BasketVM
                {
                    Id = (int)id,
                    Count = 1,
                    Price = dbProduct.Price
                });
            }

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m => m.Count * m.Price);

            return Ok(new { count, total });

        }
    }
}


