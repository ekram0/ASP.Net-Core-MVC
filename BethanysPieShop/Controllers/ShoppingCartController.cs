using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BethanysPieShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository pieRepository;
        public ShoppingCart ShoppingCart { get; }

        public ShoppingCartController(IPieRepository pieRepository , ShoppingCart shoppingCart)
        {
            this.pieRepository = pieRepository;
            ShoppingCart = shoppingCart;
        }


        public IActionResult Index()
        {
            var items = ShoppingCart.GetShoppingItems();
            ShoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = ShoppingCart,
                ShopingCartTotal = ShoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int PieId)
        {
            var selectPie = pieRepository.AllPies.FirstOrDefault(p => p.PieId == PieId);
            if (selectPie != null)
            {
                ShoppingCart.AddToCart(selectPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);
            if (selectedPie != null)
            {
                ShoppingCart.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }
    }
}
