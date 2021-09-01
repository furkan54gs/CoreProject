using System.Collections.Generic;
using core.entity;

namespace core.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}