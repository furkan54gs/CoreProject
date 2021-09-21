using System;
using System.Collections.Generic;
using System.Linq;
using core.data.Abstract;
using core.entity;

namespace core.data.Concrete.EfCore
{
    public class EfCoreCommentRepository : EfCoreGenericRepository<Comment>, ICommentRepository
    {
        public EfCoreCommentRepository(CoreContext context) : base(context)
        {

        }
        private CoreContext CoreContext
        {
            get { return context as CoreContext; }
        }

        public List<Comment> GetComments(int productId)
        {
            var comments = CoreContext.Comments.AsQueryable();

            if (productId != 0)
            {
                comments = comments.Where(i => i.ProductId == productId);
            }
            return comments.ToList();
        }

    }
}