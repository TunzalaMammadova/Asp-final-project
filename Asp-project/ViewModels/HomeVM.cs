using System;
using Asp_project.Models;
namespace Asp_project;


	public class HomeVM
	{
        public List<Adventage> Adventages { get; set; }
        public List<Sale> Sales { get;  set; }
        public List<Marketing> Marketings { get; set; }
        public List<Customer> Customers { get; set; }
    }


