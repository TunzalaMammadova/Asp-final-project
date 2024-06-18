using System;
using Asp_project.Data;
using Asp_project.Helpers.Extensions;
using Asp_project.Helpers.Request;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public ProductService(AppDbContext context,
                              IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Category)
                                          .Include(m => m.ProductImage)
                                          .ToListAsync();
        }

        public async Task<List<Product>> GetAllWithImagesAsync()
        {
            return await _context.Products.Include(m => m.ProductImage).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Where(m => !m.SoftDeleted)
                                          .Include(m => m.Category)
                                          .Include(m => m.ProductImage)
                                          .FirstOrDefaultAsync(m => m.Id == id);
        }

        public List<ProductVM> GetMappedDatas(List<Product> products)
        {
            return products.Select(m => new ProductVM
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Image = m.ProductImage.FirstOrDefault(m => m.IsMain).Name,
                Category = m.Category.Name

            }).ToList();
        }

        public async Task<List<Product>> GetAllPaginateAsync(int page, int take = 4)
        {
            return await _context.Products.Include(m => m.Category)
                                          .Include(m => m.ProductImage)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task DeleteProductImageAsync(DeleteProductImageRequest request)
        {
            var product = await _context.Products.Where(m => m.Id == request.ProductId)
                                        .Include(m => m.ProductImage)
                                        .FirstOrDefaultAsync();

            var image = product.ProductImage.FirstOrDefault(m => m.Id == request.ImageId);

            string path = _env.GenerateFilePath("img", image.Name);

            path.DeleteFileFromToLocal();

            product.ProductImage.Remove(image);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Product product, ProductEditVM edited)
        {
            if (edited.NewImages != null)
            {
                foreach (var item in edited.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = _env.GenerateFilePath("img", fileName);

                    await item.SaveFileToLocalAsync(path);
                    product.ProductImage.Add(new ProductImage { Name = fileName });

                }
            }

            product.Name = edited.Name;
            product.Description = edited.Description;
            product.CategoryId = edited.CategoryId;
            product.Price = decimal.Parse(edited.Price.Replace(",", "."));
            await _context.SaveChangesAsync();

        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

