//using System;
//using Asp_project.Data;
//using Asp_project.Services.Interfaces;

//namespace Asp_project.Services
//{
//	public class CartService : ICartService
//	{
//        private readonly AppDbContext _context;

//        public CartService(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<Product>> GetAllAsync()
//        {
//            return await _context.Products.Include(m => m.Category)
//                                                  .Include(m => m.ProductImage)
//                                                  .ToListAsync();
//        }
//    }
//}

