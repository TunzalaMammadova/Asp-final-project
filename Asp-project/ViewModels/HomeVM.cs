using System;
using Asp_project.Models;

namespace Asp_project.ViewModels
{
	public class HomeVM
	{
        public List<Adventage> Adventages { get; set; }
        public List<Sale> Sales { get; internal set; }
    }
}

