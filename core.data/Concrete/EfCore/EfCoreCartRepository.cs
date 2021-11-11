using System.Linq;
using Microsoft.EntityFrameworkCore;
using core.data.Abstract;
using core.entity;

namespace core.data.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart>, ICartRepository
    {
        public EfCoreCartRepository(CoreContext context): base(context)
        {

        }

        private CoreContext CoreContext
        {
            get {return context as CoreContext; }
        }

        public decimal CalculateTotal(string userId)
        {
            Cart cart= GetByUserId(userId);
            return((decimal)cart.CartItems.Select(i => (i.Product.Price) * i.Quantity).Sum());
        }

        public void ClearCart(int cartId)
        {
               var cmd = @"delete from CartItems where CartId=@p0";
               CoreContext.Database.ExecuteSqlRaw(cmd,cartId);
        }

        public void DeleteFromCart(int cartId, int productId)
        {
               var cmd = @"delete from CartItems where CartId=@p0 and ProductId=@p1";
               CoreContext.Database.ExecuteSqlRaw(cmd,cartId,productId);
        }

        public Cart GetByUserId(string userId)
        {
                return CoreContext.Carts
                            .Include(i=>i.CartItems)
                            .ThenInclude(i=>i.Product)
                            .FirstOrDefault(i=>i.UserId==userId);
        }

        public override void Update(Cart entity)
        {
               CoreContext.Carts.Update(entity);
        }
    }
}