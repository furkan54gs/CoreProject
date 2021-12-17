using System.Collections.Generic;
using core.entity;

namespace core.data.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {
        List<Comment> GetComments(int productId);
        Comment GetByUserProdId(string userId, int productId);
    }
}