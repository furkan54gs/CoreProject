using System.Collections.Generic;
using core.entity;

namespace core.business.Abstract
{
    public interface IImageService
    {
        List<Image> GetByProductId(int productId);
        void Create(Image entity);
    }
}