using choco_lab.Data.BaseRepository;
using choco_lab.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Business.Services
{
    public interface IChocolatesService : IEntityBaseRepository<Chocolate>
    {
        Task<Chocolate> GetChocolateByIdAsync(int id);
        Task AddNewChocolateAsync(NewChocolateMV data);
        Task UpdateChocolateAsync(NewChocolateMV data);
    }
}
