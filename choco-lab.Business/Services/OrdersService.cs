using choco_lab.Data;
using choco_lab.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Business.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public OrdersService(AppDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Chocolate).Include(n => n.User).ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task<bool> StoreOrderAsync(List<ShoppingCartItem> items, string userId)
        {
            var isSucceeded = false;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = await _manager.FindByIdAsync(userId);
                    var order = new Order()
                    {
                        UserId = userId,
                        Email = user.Email,
                        FullName = user.FullName,
                        Address = user.Address,
                    };
                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();

                    foreach (var item in items)
                    {
                        var orderItem = new OrderItem()
                        {
                            Amount = item.Amount,
                            ChocolateId = item.Chocolate.Id,
                            OrderId = order.Id,
                            Price = item.Chocolate.Price
                        };

                        await _context.OrderItems.AddAsync(orderItem);

                        var chocolate = _context.Chocolates.FirstOrDefault(n => n.Id == item.Chocolate.Id);
                        chocolate.Quantity = chocolate.Quantity - item.Amount;

                        if (chocolate.Quantity < 0)
                        {
                            throw new Exception("Нема на стању!");
                        }

                        await _context.SaveChangesAsync();            
                    }
                    transaction.Commit();
                    isSucceeded = true;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    isSucceeded = false;
                }
            }
            return isSucceeded;
        }
    }
}
