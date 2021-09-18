using core.entity;

namespace core.data.Abstract
{
    public interface ICartRepository: IRepository<Cart>
    {
        Cart GetByUserId(string userId);
        void DeleteFromCart(int cartId, int productId);
        void ClearCart(int cartId);
        double CalculateTotal(string userId);
    }
}