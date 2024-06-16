using System;
using Asp_project.Models;

namespace Asp_project.ViewModels.HeaderVM
{
    public class HeaderVM
    {
        public List<Setting> Settings { get; set; }
        public int BasketCount { get; set; }
        public decimal BasketTotalPrice { get; set; }
    }
}



