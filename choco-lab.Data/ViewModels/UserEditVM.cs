using choco_lab.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.ViewModels
{
    public class UserEditVM
    {
        public string Id { get; set; }

        [Display(Name = "Е-пошта")]
        public string Email { get; set; }

        [Display(Name = "Пуно име")]
        public string FullName { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Корисничко име")]
        public string UserName { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Адреса")]
        public string Address { get; set; }

        public UserEditVM() { }

        public UserEditVM(ApplicationUser appUser)
        {
            Email = appUser.Email;
            FullName = appUser.FullName;
            UserName = appUser.UserName;
            PhoneNumber = appUser.PhoneNumber;
            Address = appUser.Address;
        }
    }
}
