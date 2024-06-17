using System;
using System.ComponentModel.DataAnnotations;

namespace Asp_project.ViewModels.Category
{
	public class CategoryEditVM
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Can't be empty,please try again")]
        [StringLength(10, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
    }
}

