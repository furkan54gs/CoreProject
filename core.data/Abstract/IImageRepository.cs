using System.Collections.Generic;
using core.entity;

namespace core.data.Abstract
{
    public interface IImageRepository: IRepository<Image>
    {
        List<Image> GetByProductId(int productId);

    }
}