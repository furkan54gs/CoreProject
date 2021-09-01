using System.Collections.Generic;
using core.entity;

namespace core.business.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
        List<Order> GetOrders(string userId);
    }
}