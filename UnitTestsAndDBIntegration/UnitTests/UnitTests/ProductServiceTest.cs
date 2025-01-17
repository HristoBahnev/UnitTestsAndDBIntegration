using Moq;
using UnitTestsAndDBIntegration.Interfaces;
using UnitTestsAndDBIntegration.Models;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> mockRepository;
    private ProductService productService;

    [SetUp]
    public void SetUp()
    {
        mockRepository = new Mock<IProductRepository>();
        productService = new ProductService(mockRepository.Object);
    }

    [Test]
    public void GetAllProducts_ReturnsCorrectData()
    {
        mockRepository
            .Setup(repo => repo.GetAllProducts())
            .Returns(new List<Product>
            {
            new Product { ProductId = 1, Name = "Toilet paper", Price = 100, Stock = 10 },
            new Product { ProductId = 2, Name = "Pork soap", Price = 200, Stock = 20 }
            });

        var products = productService.GetAllProducts();

        Assert.That(products.Count(), Is.EqualTo(2));
        Assert.That(products.First().Name, Is.EqualTo("Toilet paper"));
    }
    [Test]
    public void AddProduct_WithValidData_AddsProduct()
    {
        var product = new Product { Name = "Pork soap", Price = 50, Stock = 10 };

        productService.AddProduct(product);

        mockRepository.Verify(repo => repo.AddProduct(It.IsAny<Product>()), Times.Once);
    }

    [Test]
    public void AddProduct_WithNegativePrice_ThrowsArgumentException()
    {
        var product = new Product { Name = "Invalid Product", Price = -50, Stock = 10 };

        Assert.Throws<ArgumentException>(() => productService.AddProduct(product));
    }

    [Test]
    public void AddProduct_WithNegativeStock_ThrowsArgumentException()
    {
        var product = new Product { Name = "Invalid Product", Price = 50, Stock = -10 };

        Assert.Throws<ArgumentException>(() => productService.AddProduct(product));
    }

    [Test]
    public void UpdateProduct_UpdatesTheCorrectProduct()
    {
        var product = new Product { ProductId = 1, Name = "Updated Product", Price = 200, Stock = 15 };

        productService.UpdateProduct(product);

        mockRepository.Verify(repo => repo.UpdateProduct(It.Is<Product>(p => p.ProductId == 1)), Times.Once);
    }


}