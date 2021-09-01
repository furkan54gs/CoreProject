using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using core.data.Abstract;
using core.entity;

namespace core.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {

        public EfCoreCategoryRepository(CoreContext context): base(context)
        {

        }

        private CoreContext CoreContext
        {
            get {return context as CoreContext; }
        }
        public void DeleteFromCategory(int productId, int categoryId)
        {
                var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                CoreContext.Database.ExecuteSqlRaw(cmd,productId,categoryId);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
                return CoreContext.Categories
                            .Where(i=>i.CategoryId==categoryId)
                            .Include(i=>i.ProductCategories)
                            .ThenInclude(i=>i.Product)
                            .FirstOrDefault();
        }


    }
}