using UnitTestsAndDBIntegration.Models;

namespace UnitTestsAndDBIntegration.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int orderId);
    }
}
