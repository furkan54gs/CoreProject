using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using core.data.Abstract;
using core.entity;

namespace core.data.Concrete.EfCore
{
    public class EfCoreProductRepository :
        EfCoreGenericRepository<Product>, IProductRepository
    {
        public EfCoreProductRepository(CoreContext context) : base(context)
        {

        }

        private CoreContext CoreContext
        {
            get { return context as CoreContext; }
        }
        public Product GetByIdWithCategories(int id)
        {
            return CoreContext.Products
                            .Where(i => i.ProductId == id)
                            .Include(i => i.ProductCategories)
                            .ThenInclude(i => i.Category)
                            .FirstOrDefault();
        }

        public int GetCountByApproved(bool isApproved)
        {
            if (isApproved == false)
            {
                return CoreContext.Products.
                                Where(i => i.IsApproved == false)
                                .Count();
            }
            else
            {
                return CoreContext.Products.
                               Where(i => i.IsApproved == true)
                               .Count();
            }
        }

        public int GetCountByCategory(string category)
        {

            var products = CoreContext.Products.Where(i => i.IsApproved).AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(a => a.Category.Url == category));
            }

            return products.Count();

        }
        public List<Product> GetHomePageProducts()
        {
            return CoreContext.Products
                .Where(i => i.IsApproved && i.IsHome).ToList();
        }
        public Product GetProductDetails(string url)
        {
            return CoreContext.Products
                            .Where(i => i.Url == url)
                            .Include(i => i.ProductCategories)
                            .ThenInclude(i => i.Category)
                            .FirstOrDefault();

        }
        public List<Product> GetProductsByCategory(string name, string orderby, int page, int pageSize)
        {
            var products = CoreContext
                .Products
                .Where(i => i.IsApproved)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                if (name != "list")
                {
                    products = products.Include(i => i.ProductCategories)
                                       .ThenInclude(i => i.Category)
                                       .Where(i => i.ProductCategories.Any(a => a.Category.Url == name));
                }
            }

            switch (orderby)
            {
                case "pricedesc":
                    return products.OrderByDescending(i => i.Price).Skip((page - 1) * pageSize).Take(pageSize).ToList();


                case "priceasc":
                    return products.OrderBy(i => i.Price).Skip((page - 1) * pageSize).Take(pageSize).ToList();


                case "rate":
                    return products.OrderByDescending(i => i.Rate).Skip((page - 1) * pageSize).Take(pageSize).ToList();


                case "date":
                    return products.OrderByDescending(i => i.DateAdded).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                default:
                    return products.OrderBy(i => i.ProductId).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            }

        }
        public List<Product> GetSearchResult(string searchString)
        {
            var products = CoreContext
                .Products
                .Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())))
                .AsQueryable();

            return products.ToList();
        }

        public void Update(Product entity, int[] categoryIds)
        {
            var product = CoreContext.Products
                                .Include(i => i.ProductCategories)
                                .FirstOrDefault(i => i.ProductId == entity.ProductId);


            if (product != null)
            {
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Rate = entity.Rate;
                product.Stock = entity.Stock;
                product.Description = entity.Description;
                product.Url = entity.Url;
                product.ImageUrl = entity.ImageUrl;
                product.IsApproved = entity.IsApproved;
                product.IsHome = entity.IsHome;

                product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                {
                    ProductId = entity.ProductId,
                    CategoryId = catid
                }).ToList();

            }
        }

    }
}