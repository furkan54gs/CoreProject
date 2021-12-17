using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using core.data.Abstract;
using core.entity;

namespace core.data.Concrete.EfCore
{
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order>, IOrderRepository
    {

        public EfCoreOrderRepository(CoreContext context) : base(context)
        {

        }

        private CoreContext CoreContext
        {
            get { return context as CoreContext; }
        }
        public List<Order> GetOrders(string userId)
        {

            var orders = CoreContext.Orders
                                .Include(i => i.OrderItems)
                                .ThenInclude(i => i.Product)
                                .ThenInclude(i => i.Images.Take(1))
                                .AsSplitQuery()
                                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(i => i.UserId == userId);
            }

            return orders.ToList();
        }
    }
}