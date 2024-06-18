using System;
using Asp_project.Helpers;
using Asp_project.Helpers.Extensions;
using Asp_project.Helpers.Request;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;


        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IWebHostEnvironment env)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var paginateDatas = await _productService.GetAllPaginateAsync(page);
            var mappedDatas = _productService.GetMappedDatas(paginateDatas);

            int pageCount = await GetPageCountAsync(4);
            Paginate<ProductVM> model = new(mappedDatas, page, pageCount);

            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)count / take);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();

            List<ProductImageVM> productImages = new();

            foreach (var item in product.ProductImage)
            {
                productImages.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }

            ProductDetailVM model = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category.Name,
                Images = productImages
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(DeleteProductImageRequest request)
        {

            await _productService.DeleteProductImageAsync(request);

            return Ok();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();

            foreach (var item in product.ProductImage)
            {
                string path = Path.Combine(_env.WebRootPath, "img", item.Name);
                path.DeleteFileFromToLocal();
            }

            await _productService.DeleteAsync(product);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.categories = await _categoryService.GetAllBySelectAsync();

            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();

            ProductEditVM response = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString().Replace(",", "."),
                CategoryId = product.CategoryId,
                ExistImages = product.ProductImage.Select(m => new ProductEditImageVM
                {
                    Id = m.Id,
                    ProductId = m.ProductId,
                    Image = m.Name,
                    IsMain = m.IsMain
                }).ToList()
            };

            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductEditVM request)
        {
            ViewBag.categories = await _categoryService.GetAllBySelectAsync();

            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistImages = product.ProductImage.Select(m => new ProductEditImageVM
                {
                    Id = m.Id,
                    ProductId = m.ProductId,
                    Image = m.Name,
                    IsMain = m.IsMain
                }).ToList();


                return View(request);
            }

            if (request.NewImages is not null)
            {
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileSize(500))
                    {
                        request.ExistImages = product.ProductImage.Select(m => new ProductEditImageVM
                        {
                            Id = m.Id,
                            ProductId = m.ProductId,
                            Image = m.Name,
                            IsMain = m.IsMain
                        }).ToList();

                        ModelState.AddModelError("NewImages", "Image size must be max 500kb");
                        return View(request);
                    }

                    if (!item.CheckFileType("image/"))
                    {
                        request.ExistImages = product.ProductImage.Select(m => new ProductEditImageVM
                        {
                            Id = m.Id,
                            ProductId = m.ProductId,
                            Image = m.Name,
                            IsMain = m.IsMain
                        }).ToList();

                        ModelState.AddModelError("NewImages", "File must be only image format");
                        return View(request);
                    }

                }
            }

            await _productService.EditAsync(product, request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _categoryService.GetAllBySelectAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            ViewBag.categories = await _categoryService.GetAllBySelectAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Image)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File must be only image format");
                    return View();
                }

                if (!item.CheckFileSize(800))
                {
                    ModelState.AddModelError("Images", "Image size must be max 800kb");
                }

            }

            List<ProductImage> images = new();
            foreach (var item in request.Image)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new ProductImage
                {
                    Name = fileName
                });
            }


            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
                ProductImage = images
            };


            await _productService.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }

}

