using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Е-пошта")]
        [Required(ErrorMessage = "E-пошта је обавезан")]
        public string EmailAddress { get; set; }

        [Display(Name = "Шифра")]
        [Required(ErrorMessage = "Шифра је обавезна")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
