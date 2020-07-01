using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShoppingCart shoppingCart;
        public IOrderRepository OrderRepository { get; set; }

        public OrderController(IOrderRepository orderRepository,ShoppingCart shoppingCart )
        {
            OrderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }

        public IActionResult Checkout() => View();
        
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCart.GetShoppingItems();
            shoppingCart.ShoppingCartItems = items;

            if (shoppingCart.ShoppingCartItems.Count >0)
            {
                ModelState.AddModelError("", "Your cart is empty, Add some Pies First.");
            }

            if (ModelState.IsValid)
            {
                OrderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order.You'll soon enjoy our delicious pies.";
            return View();
        }
    }
}
