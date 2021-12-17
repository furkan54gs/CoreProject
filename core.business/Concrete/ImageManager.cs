using System.Collections.Generic;
using core.business.Abstract;
using core.data.Abstract;
using core.entity;

namespace core.business.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly IUnitOfWork _unitofwork;
        public ImageManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public List<Image> GetByProductId(int productId)
        {
            throw new System.NotImplementedException();
        }

        public void Create(Image entity)
        {
            _unitofwork.Images.Create(entity);
            _unitofwork.Save();
        }
    }
}