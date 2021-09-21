using System.Collections.Generic;
using core.business.Abstract;
using core.business.Extensions;
using core.data.Abstract;
using core.data.Concrete.EfCore;
using core.entity;

namespace core.business.Concrete
{
    public class CommentManager : ICommentService
    {

        private readonly IUnitOfWork _unitofwork;
        public CommentManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public string ErrorMessage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Create(Comment entity)
        {
            _unitofwork.Comments.Create(entity);
            _unitofwork.Save();
        }

        public void Delete(Comment entity)
        {
            _unitofwork.Comments.Delete(entity);
            _unitofwork.Save();
        }

        public List<Comment> GetAll()
        {
            return _unitofwork.Comments.GetAll();
        }

        public Comment GetById(int id)
        {
            return _unitofwork.Comments.GetById(id);
        }

        public List<Comment> GetComments(int productId)
        {
           return  _unitofwork.Comments.GetComments(productId);
        }

        public void Update(Comment entity)
        {
            _unitofwork.Comments.Update(entity);
            _unitofwork.Save();
        }

        public bool Validation(Comment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}