using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Е-пошта")]
        [Required(ErrorMessage = "E-пошта је обавезна")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Пуно име")]
        [Required(ErrorMessage = "Пуно име је обавезно")]
        public string FullName { get; set; }

        [Display(Name = "Корисничко име(мора бити без размака)")]
        [Required(ErrorMessage = "Корисничко име је обавезно")]       
        public string UserName { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Број телефона је обавезан")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Град")]
        [Required(ErrorMessage = "Град је обавезан")]
        public string City { get; set; }

        [Display(Name = "Адреса")]
        [Required(ErrorMessage = "Адреса је обавезна")]
        public string Address { get; set; }

        [Display(Name = "Шифра")]
        [Required(ErrorMessage = "Шифра је обавезна")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Потврдите шифру")]
        [Required(ErrorMessage = "Морате потврдити шифру")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Шифре нису исте!")]
        public string ConfirmedPassword { get; set; }
    }
}
