using System;
using Asp_project.Helpers.Request;
using Asp_project.Models;
using Asp_project.ViewModels.Products;

namespace Asp_project.Services.Interfaces
{
	public interface IProductService
	{
        Task<List<Product>> GetAllWithImagesAsync();
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        List<ProductVM> GetMappedDatas(List<Product> products);
        Task<List<Product>> GetAllPaginateAsync(int page, int take = 3);
        Task<int> GetCountAsync();
        Task CreateAsync(Product product);
        Task DeleteAsync(Product product);
        Task DeleteProductImageAsync(DeleteProductImageRequest request);
        Task EditAsync(Product product, ProductEditVM edited);
    }
}

