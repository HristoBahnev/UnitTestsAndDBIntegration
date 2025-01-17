using Moq;
using NUnit.Framework;
using UnitTestsAndDBIntegration.Interfaces;
using UnitTestsAndDBIntegration.Models;


[TestFixture]
public class OrderServiceTests
{
    private Mock<IProductRepository> mockRepository;
    private ProductService productService;
    private Mock<IOrderRepository> mockOrderRepository;
    private OrderService orderService;

    [SetUp]
    public void SetUp()
    {
        mockRepository = new Mock<IProductRepository>();
        productService = new ProductService(mockRepository.Object);
        mockOrderRepository = new Mock<IOrderRepository>();
        orderService = new OrderService(mockOrderRepository.Object, mockRepository.Object);
    }

    [Test]
    public void PlaceOrder_WithSufficientStock_UpdatesStock()
    {
        mockRepository
            .Setup(repo => repo.GetProductById(1))
            .Returns(new Product { ProductId = 1, Name = "Knife", Stock = 10, Price = 100 });

        var order = new Order { ProductId = 1, Quantity = 5, OrderDate = DateTime.Now };

        orderService.PlaceOrder(order);

        mockRepository.Verify(repo => repo.UpdateProduct(It.Is<Product>(p => p.Stock == 5)), Times.Once);
        mockOrderRepository.Verify(repo => repo.PlaceOrder(order), Times.Once);
    }

    [Test]
    public void PlaceOrder_WithInsufficientStock_ThrowsInvalidOperationException()
    {
        mockRepository
            .Setup(repo => repo.GetProductById(1))
            .Returns(new Product { ProductId = 1, Name = "Fork", Stock = 3, Price = 100 });

        var order = new Order { ProductId = 1, Quantity = 5, OrderDate = DateTime.Now };

        Assert.Throws<InvalidOperationException>(() => orderService.PlaceOrder(order));
    }

    [Test]
    public void PlaceOrder_WithNonExistentProduct_ThrowsArgumentException()
    {
        mockRepository
            .Setup(repo => repo.GetProductById(1))
            .Returns((Product)null);

        var order = new Order { ProductId = 1, Quantity = 5, OrderDate = DateTime.Now };

        Assert.Throws<ArgumentNullException>(() => orderService.PlaceOrder(order));
    }
}
