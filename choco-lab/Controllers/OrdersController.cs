using choco_lab.Business.Services;
using choco_lab.Data.Cart;
using choco_lab.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace choco_lab.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IChocolatesService _chocolateService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        
        public OrdersController(IChocolatesService service, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _chocolateService = service;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders =await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            var item = await _chocolateService.GetChocolateByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveFromShoppingCart(int id)
        {
            var item = await _chocolateService.GetChocolateByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            
            var succeeded = await _ordersService.StoreOrderAsync(items, userId);
            if (!succeeded)
            {
                return View("OrderError");
            }
            else
            {
                await _shoppingCart.ClearShoppingCartAsync();

                return View("OrderCompleted");
            }
        }
    }
}
