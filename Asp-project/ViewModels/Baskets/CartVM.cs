using System;
namespace Asp_project.ViewModels.Baskets
{
    public class CartVM
    {
        public List<BasketProductsVM> BasketProducts { get; set; }
        public decimal SubTotal { get; set; }
    }
}

