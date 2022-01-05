using System.Collections.Generic;
using core.entity;

namespace core.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(string url);
        Product GetByIdWithAll(int id);
        List<Product> GetProductsByCategory(string name, string orderby, int page, int pageSize);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetHomePageProducts();
        int GetCountByCategory(string category);
        void Update(Product entity, int[] categoryIds);
        void Create(Product entity, int[] categoryIds,List<string> imagesName);
        int GetCountByApproved(bool isApproved);
        void StockUpdate(int productId, int quantity);
        void CommentUpdate(int productId, double rate);
    }
}