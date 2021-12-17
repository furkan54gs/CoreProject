using System.Collections.Generic;
using core.data.Abstract;
using core.entity;

namespace core.data.Concrete.EfCore
{
    public class EfCoreImageRepository : EfCoreGenericRepository<Image>, IImageRepository
    {
        public EfCoreImageRepository(CoreContext context) : base(context)
        {

        }

        private CoreContext CoreContext
        {
            get { return context as CoreContext; }
        }

        List<Image> IImageRepository.GetByProductId(int productId)
        {
            throw new System.NotImplementedException();
        }
    }
}