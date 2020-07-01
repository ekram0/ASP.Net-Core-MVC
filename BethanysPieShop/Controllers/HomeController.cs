using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPieRepository pieRepository;
        private readonly ICategoryRepository categoryRepository;

        public HomeController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            this.pieRepository = pieRepository;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            PiesListViewModel piesListViewModel = new PiesListViewModel();
            piesListViewModel.Pies = pieRepository.AllPies;

            ViewBag.currentCategory = "Cheese Cake";

            return View(piesListViewModel);
        }

        public IActionResult Index(int id)
        {
            var homeViewModel = new HomeViewModel
            {
                PiesOfWeek = pieRepository.PiesOfWeek
            };
            return View(homeViewModel);
        }
    }
}
