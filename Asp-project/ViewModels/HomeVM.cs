﻿using System;
using System.Reflection.Metadata;
using Asp_project.Models;
namespace Asp_project;


public class HomeVM
{
    public List<Adventage> Adventages { get; set; }
    public List<Sale> Sales { get; set; }
    public List<Marketing> Marketings { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Category> Categories { get; set; }
    public List<Product> Products { get; set; }
    public List<ProductImage> ProductImage { get; set; }
    public List<Setting> Settings { get; set; }
}

