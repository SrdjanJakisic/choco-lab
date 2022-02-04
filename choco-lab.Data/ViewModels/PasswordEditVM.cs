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
        [Required]
        public string Password { get; set; }

        public PasswordEditVM() { }

        public PasswordEditVM(ApplicationUser appUser)
        {
            Password = appUser.PasswordHash;
        }
    }
}
