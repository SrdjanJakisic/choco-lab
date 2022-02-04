using choco_lab.Data.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.Models
{
    public class Category : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
