using System.Collections.Generic;
using core.business.Abstract;
using core.business.Extensions;
using core.data.Abstract;
using core.data.Concrete.EfCore;
using core.entity;

namespace core.business.Concrete
{
    public class ProductManager : IProductService
    {

        private readonly IUnitOfWork _unitofwork;
        public ProductManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public bool Create(Product entity)
        {
            if (Validation(entity))
            {
                entity.Url = Replacements.ConvertUrl(entity.Name);
                _unitofwork.Products.Create(entity);
                _unitofwork.Save();
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            // iş kuralları
            _unitofwork.Products.Delete(entity);
            _unitofwork.Save();
        }

        public List<Product> GetAll()
        {
            return _unitofwork.Products.GetAll();
        }
        public int GetCountByApproved(bool isApproved)
        {
            return _unitofwork.Products.GetCountByApproved(isApproved);
        }
        public Product GetById(int id)
        {
            return _unitofwork.Products.GetById(id);
        }

        public Product GetByIdWithAll(int id)
        {
            return _unitofwork.Products.GetByIdWithAll(id);
        }

        public int GetCountByCategory(string category)
        {
            return _unitofwork.Products.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _unitofwork.Products.GetHomePageProducts();
        }

        public Product GetProductDetails(string url)
        {
            return _unitofwork.Products.GetProductDetails(url);
        }

        public List<Product> GetProductsByCategory(string name, string orderby, int page, int pageSize)
        {
            return _unitofwork.Products.GetProductsByCategory(name, orderby, page, pageSize);
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _unitofwork.Products.GetSearchResult(searchString);
        }

        public void Update(Product entity)
        {
            _unitofwork.Products.Update(entity);
            _unitofwork.Save();
        }

        public bool Update(Product entity, int[] categoryIds)
        {
            if (Validation(entity))
            {
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "Ürün için en az bir kategori seçmelisiniz.";
                    return false;
                }
                _unitofwork.Products.Update(entity, categoryIds);
                _unitofwork.Save();
                return true;
            }
            return false;
        }
        public bool Create(Product entity, int[] categoryIds, List<string> imagesName)
        {
            if (Validation(entity))
            {
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "Ürün için en az bir kategori seçmelisiniz.";
                    return false;
                }
                entity.Url = Replacements.ConvertUrl(entity.Name);
                _unitofwork.Products.Create(entity, categoryIds, imagesName);
                _unitofwork.Save();

                return true;
            }
            return false;
        }

        public string ErrorMessage { get; set; }

        public bool Validation(Product entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "ürün ismi girmelisiniz.\n";
                isValid = false;
            }

            if (entity.Price < 0)
            {
                ErrorMessage += "ürün fiyatı negatif olamaz.\n";
                isValid = false;
            }

            return isValid;
        }

        public void StockUpdate(int productId, int quantity)
        {
            _unitofwork.Products.StockUpdate(productId, quantity);
            _unitofwork.Save();
        }

        public void CommentUpdate(int productId, double rate)
        {
            _unitofwork.Products.CommentUpdate(productId, rate);
            _unitofwork.Save();
        }

    }
}