using core.data.Abstract;
using core.entity;

namespace core.data.Concrete.EfCore
{
    public class EfCoreCommentRepository: EfCoreGenericRepository<Comment>, ICommentRepository
    {
        public EfCoreCommentRepository(CoreContext context): base(context)
        {

        }
        private CoreContext CoreContext
        {
            get {return context as CoreContext; }
        }
    }
}