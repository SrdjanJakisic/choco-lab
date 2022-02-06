using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using choco_lab.Data.Models;

namespace choco_lab.Data.ViewModels
{
    public class PasswordEditVM
    {
        [Display(Name = "Нова шифра")]
        [Required(ErrorMessage = "Шифра је обавезна")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Потврдите шифру")]
        [Required(ErrorMessage = "Морате потврдити шифру")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Шифре нису исте!")]
        public string ConfirmedPassword { get; set; }

        public PasswordEditVM() { }

        public PasswordEditVM(ApplicationUser appUser)
        {
            Password = appUser.PasswordHash;
        }
    }
}
