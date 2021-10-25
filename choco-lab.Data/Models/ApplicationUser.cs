using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Пуно име")]
        public string FullName { get; set; }
        [Display(Name = "Адреса")]
        public string Address { get; set; }
        [Display(Name = "Корисничко име")]
        public override string UserName { get; set; }
    }
}
