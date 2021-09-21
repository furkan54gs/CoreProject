using System.Collections.Generic;
using core.entity;

namespace core.business.Abstract
{
    public interface ICommentService : IValidator<Comment>
    {
        List<Comment> GetComments(int productId);
        Comment GetById(int id);

        List<Comment> GetAll();

        void Create(Comment entity);

        void Update(Comment entity);

        void Delete(Comment entity);
    }
}