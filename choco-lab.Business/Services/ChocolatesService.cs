using choco_lab.Data;
using choco_lab.Data.BaseRepository;
using choco_lab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace choco_lab.Business.Services
{
    public class ChocolatesService : EntityBaseRepository<Chocolate>, IChocolatesService
    {
        private readonly AppDbContext _context;
        public ChocolatesService(AppDbContext context) : base(context) { _context = context; }

        public async Task AddNewChocolateAsync(NewChocolateMV data)
        {
            var newChocolate = new Chocolate()
            {
                Name = data.Name,
                CategoryId = data.CategoryId,
                ShortDescription = data.ShortDescription,
                DetailedDescription = data.DetailedDescription,
                Weight = data.Weight,
                ExpirationDate = data.ExpirationDate,
                Price = data.Price,
                Quantity = data.Quantity,
                Image = data.Image
            };
            await _context.Chocolates.AddAsync(newChocolate);
            await _context.SaveChangesAsync();
        }

        public async Task<Chocolate> GetChocolateByIdAsync(int id)
        {
            var chocolateDetails = await _context.Chocolates.Include(n=>n.Category).FirstOrDefaultAsync(n => n.Id == id);
            return chocolateDetails;
        }

        public async Task UpdateChocolateAsync(NewChocolateMV data)
        {
            var dbChocolate = await _context.Chocolates.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbChocolate != null)
            {
                dbChocolate.Name = data.Name;
                dbChocolate.CategoryId = data.CategoryId;
                dbChocolate.ShortDescription = data.ShortDescription;
                dbChocolate.DetailedDescription = data.DetailedDescription;
                dbChocolate.Weight = data.Weight;
                dbChocolate.ExpirationDate = data.ExpirationDate;
                dbChocolate.Price = data.Price;
                dbChocolate.Quantity = data.Quantity;
                dbChocolate.Image = data.Image;

                await _context.SaveChangesAsync();
            }
        }

        public override Task<IEnumerable<Chocolate>> GetAllAsync()
        {
            return Task.FromResult(_context.Chocolates.Include(n => n.Category).AsEnumerable());
        }
    }
}
