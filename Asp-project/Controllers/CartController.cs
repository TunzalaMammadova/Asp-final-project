//using System;
//using Asp_project.Data;
//using Asp_project.Services.Interfaces;
//using Asp_project.ViewModels.Baskets;
//using Microsoft.AspNetCore.Mvc;

//namespace Asp_project.Controllers
//{
//    public class CartController : Controller
//    {
//        private readonly AppDbContext _context;
//        private readonly IHttpContextAccessor _accessor;
//        private readonly ICartService _cartService;


//        public CartController(AppDbContext context,
//               IHttpContextAccessor accessor,
//               ICartService cartService)

//        {
//            _context = context;
//            _accessor = accessor;
//            _cartService = cartService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            List<BasketVM> basketProducts = null;

//            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
//            {
//                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
//            }
//            else
//            {
//                basketProducts = new List<BasketVM>();
//            }

//            var data = await _cartService.GetAllAsync();

//            List<BasketProductsVM> basket = new();

//            foreach (var item in basketProducts)
//            {
//                var dbPorduct = products.FirstOrDefault(m => m.Id == item.Id);

//                basket.Add(new BasketProductsVM
//                {
//                    Id = dbPorduct.Id,
//                    Name = dbPorduct.Name,
//                    Price = dbPorduct.Price,
//                    Count = item.Count,
//                    Image = dbPorduct.ProductImage.FirstOrDefault(m => m.IsMain).Name
//                });
//            }

//            CartVM response = new()
//            {
//                BasketProducts = basket,
//                SubTotal = basketProducts.Sum(m => m.Count * m.Price)
//            };

//            return View(response);

//        }

//        [HttpPost]
//        public IActionResult DeleteProductFromBasket(int? id)
//        {
//            if (id is null) return BadRequest();

//            List<BasketVM> basketProducts = new();

//            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
//            {
//                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
//            }

//            basketProducts = basketProducts.Where(m => m.Id != id).ToList();

//            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

//            int count = basketProducts.Sum(m => m.Count);
//            decimal total = basketProducts.Sum(m => m.Count * m.Price);

//            return Ok(new { count, total });
//        }

//    }
//}

