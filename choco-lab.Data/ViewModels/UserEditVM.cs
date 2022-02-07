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
        [Required(ErrorMessage = "E-пошта је обавезна")]
        public string Email { get; set; }

        [Display(Name = "Пуно име")]
        [Required(ErrorMessage = "Пуно име је обавезно")]
        public string FullName { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Број телефона је обавезан")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Корисничко име")]
        [Required(ErrorMessage = "Корисничко име је обавезно")]
        public string UserName { get; set; }

        [Display(Name = "Град")]
        [Required(ErrorMessage = "Град је обавезан")]
        public string City { get; set; }

        [Display(Name = "Адреса")]
        [Required(ErrorMessage = "Адреса је обавезна")]
        public string Address { get; set; }

        public UserEditVM() { }

        public UserEditVM(ApplicationUser appUser)
        {
            Email = appUser.Email;
            FullName = appUser.FullName;
            UserName = appUser.UserName;
            PhoneNumber = appUser.PhoneNumber;
            City = appUser.City;   
            Address = appUser.Address;
        }
    }
}
