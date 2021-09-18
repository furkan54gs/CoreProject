using core.entity;

namespace core.business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        bool AddToCart(string userId, int productId, int quantity);
        void DeleteFromCart(string userId, int productId);
        void ClearCart(int cartId);
        double CalculateTotal(string userId);
    }
}