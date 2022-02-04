using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using choco_lab.Data.BaseRepository;
using choco_lab.Data.Models;

namespace choco_lab.Business.Services
{
    public interface ICategoriesService : IEntityBaseRepository<Category>
    {
    }
}
