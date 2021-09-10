using core.business.Abstract;
using core.data.Abstract;
using core.entity;

namespace core.business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IUnitOfWork _unitofwork;
        public CartManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public bool AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            var product = _unitofwork.Products.GetById(productId);

            if (product.Stock > quantity)
            {
                if (cart != null)
                {
                    // eklenmek isteyen ürün sepette varmı (güncelleme)
                    // eklenmek isteyen ürün sepette var ve yeni kayıt oluştur. (kayıt ekleme)

                    var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                    if (index < 0)
                    {
                        cart.CartItems.Add(new CartItem()
                        {
                            ProductId = productId,
                            Quantity = quantity,
                            CartId = cart.Id
                        });
                    }
                    else
                    {
                        cart.CartItems[index].Quantity += quantity;
                    }

                    _unitofwork.Carts.Update(cart);
                    _unitofwork.Save();
                    return true;
                }
            }
            return false;
        }

        public void ClearCart(int cartId)
        {
            _unitofwork.Carts.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _unitofwork.Carts.DeleteFromCart(cart.Id, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _unitofwork.Carts.GetByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _unitofwork.Carts.Create(new Cart() { UserId = userId });
            _unitofwork.Save();
        }
    }
}