using System.Linq;
using Microsoft.AspNetCore.Mvc;
using core.business.Abstract;
using core.entity;
using core.webui.Models;

namespace core.webui.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            this._productService = productService;
        }


        // localhost/products/telefon?page=1
        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 2;
            var productViewModel = new ProductListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                },
                Products = _productService.GetProductsByCategory(category, page, pageSize)
            };

            return View(productViewModel);
        }

        /*
         public IActionResult List(string category, int page = 1)
        {

            const int pageSize = 2;
            var productViewModel = new ProductListViewModel();

            if (category == "All")
            {
                productViewModel.Products = _productService.GetAll();
                productViewModel.PageInfo = new PageInfo()
                {
                    TotalItems = productViewModel.Products.Count(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                };
            }

            else
            {
                productViewModel.Products = _productService.GetProductsByCategory(category, page, pageSize);
                productViewModel.PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                };
            }

            return View(productViewModel);
        }
        */

        public IActionResult Details(string url)
        {
            if (url == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails(url);

            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailModel
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            });
        }

        public IActionResult Search(string q)
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetSearchResult(q)
            };

            return View(productViewModel);
        }
    }
}