using System;
namespace Asp_project.ViewModels.Baskets
{
    public class BasketProductsVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}

