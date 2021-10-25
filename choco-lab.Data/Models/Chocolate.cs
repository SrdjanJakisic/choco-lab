using choco_lab.Data.BaseRepository;
using choco_lab.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.Models
{
    public class Chocolate : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public int Weight { get; set; }
        public string ExpirationDate { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
