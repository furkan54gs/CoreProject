using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using core.business.Abstract;
using core.data.Abstract;
using core.webui.Models;

namespace core.webui.Controllers
{
    // localhost:5000/home
    public class HomeController:Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            this._productService=productService;
        }

        public IActionResult Index()
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetHomePageProducts()
            };

            return View(productViewModel);
        }

        public IActionResult About()
        {
            return View();
        }

         public IActionResult Contact()
        {
            return View("MyView");
        }
    }
}