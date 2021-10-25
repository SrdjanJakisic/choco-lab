using choco_lab.Data.BaseRepository;
using choco_lab.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.Models
{
    public class NewChocolateMV
    {
        public int Id { get; set; }

        [Display(Name = "Унесите назив чоколаде")]
        [Required(ErrorMessage = "Назив је обавезан")]    
        public string Name { get; set; }

        [Display(Name = "Изаберите категорију")]
        [Required(ErrorMessage = "Категорија је обавезна")]     
        public Category Category { get; set; }

        [Display(Name = "Унесите кратак опис")]
        [Required(ErrorMessage = "Кратак опис је обавезан")]
        public string ShortDescription { get; set; }

        [Display(Name = "Унесите детаљан опис")]
        [Required(ErrorMessage = "Детаљан опис је обавезан")]        
        public string DetailedDescription { get; set; }

        [Display(Name = "Унесите тежину")]
        [Required(ErrorMessage = "Тежина је обавезна")]        
        public int Weight { get; set; }

        [Display(Name = "Унесите рок трајања")]
        [Required(ErrorMessage = "Рок трајања је обавезан")]        
        public string ExpirationDate { get; set; }

        [Display(Name = "Унесите цену чоколаде")]
        [Required(ErrorMessage = "Цена је обавезна")]       
        public double Price { get; set; }

        [Display(Name = "Додајте слику чоколаде")]
        public string Image { get; set; }

        //[Display(Name = "Слика чоколаде")]
        //[Required(ErrorMessage = "Слика је обавезна")]       
        public IFormFile ImageUpload { get; set; }
    }
}
