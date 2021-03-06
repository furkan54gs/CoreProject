using System.Collections.Generic;
using core.entity;

namespace core.business.Abstract
{
    public interface IProductService : IValidator<Product>
    {
        Product GetById(int id);
        Product GetByIdWithAll(int id);
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name, string orderby, int page, int pageSize);
        int GetCountByCategory(string category);
        int GetCountByApproved(bool isApproved);
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string searchString);
        List<Product> GetAll();
        void StockUpdate(int productId, int quantity);
        void CommentUpdate(int productId, double rate);
        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        bool Update(Product entity, int[] categoryIds);
        bool Create(Product entity, int[] categoryIds,List<string> imagesName);
    }
}