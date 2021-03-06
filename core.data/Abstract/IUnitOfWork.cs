using System;

namespace core.data.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
         ICartRepository Carts {get;}
         ICategoryRepository Categories {get;}
         IOrderRepository Orders {get;}
         IProductRepository Products {get;}
         ICommentRepository Comments {get;}
         IImageRepository Images {get;}
         void Save();

    }
}