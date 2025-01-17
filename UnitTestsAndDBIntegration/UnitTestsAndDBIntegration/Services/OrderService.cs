using UnitTestsAndDBIntegration.Interfaces;
using UnitTestsAndDBIntegration.Models;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public void PlaceOrder(Order order)
    {
        var product = _productRepository.GetProductById(order.ProductId);

        if (product == null)
        {
            throw new ArgumentNullException("Product not found.");
        }

        if (product.Stock < order.Quantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }

        product.Stock -= order.Quantity;
        _productRepository.UpdateProduct(product);

        _orderRepository.PlaceOrder(order);
    }

    public IEnumerable<Order> GetOrders()
    {
        return _orderRepository.GetOrders();
    }

    public Order GetOrderById(int orderId)
    {
        return _orderRepository.GetOrderById(orderId);
    }
}
