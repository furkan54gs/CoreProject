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



        //   localhost/products/telefon?page=1
        public IActionResult List(string category,string orderby, int page = 1)
        {
            const int pageSize = 3;

            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetProductsByCategory(category,orderby, page, pageSize),
                PageInfo = new PageInfo()
                {
                    TotalItems = (category == "list") ? _productService.GetCountByApproved(true) : _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category,
                    CurrentOrderBy = orderby
                }

            };
            return View(productViewModel);
        }


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