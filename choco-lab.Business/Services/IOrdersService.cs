using choco_lab.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Business.Services
{
    public interface IOrdersService
    {
        Task<bool> StoreOrderAsync(List<ShoppingCartItem> items, string userId);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
