using System;
using System.ComponentModel.DataAnnotations;

namespace Asp_project.ViewModels.Accounts
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

