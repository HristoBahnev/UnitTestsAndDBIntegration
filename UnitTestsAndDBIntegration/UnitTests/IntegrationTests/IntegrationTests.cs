using System;
using System.Linq;
using System.Configuration;
using NUnit.Framework;
using UnitTestsAndDBIntegration.Models;


public class IntegrationTests
{
    private ProductRepository _productRepository;
    private OrderRepository _orderRepository;

    [SetUp]
    public void SetUp()
    {
        _productRepository = new ProductRepository();
        _orderRepository = new OrderRepository();
    }

    [Test]
    public void AddProduct_ShouldInsertProduct()
    {
        var product = new Product
        {
            Name = "Test Product",
            Price = 20.99m,
            Stock = 100
        };

        _productRepository.AddProduct(product);

        var addedProduct = _productRepository.GetAllProducts()
            .FirstOrDefault(p => p.Name == "Test Product");

        Assert.NotNull(addedProduct);
        Assert.AreEqual("Test Product", addedProduct.Name);
        Assert.AreEqual(20.99m, addedProduct.Price);
        Assert.AreEqual(100, addedProduct.Stock);
    }

    [Test]
    public void GetAllProducts_ShouldReturnProducts()
    {
        var products = _productRepository.GetAllProducts();
        Assert.IsTrue(products.Any());
    }

    [Test]
    public void PlaceOrder_ShouldUpdateStock()
    {
        var product = _productRepository.GetAllProducts().First();
        int initialStock = product.Stock;

        var order = new Order
        {
            ProductId = product.ProductId,
            Quantity = 2,
            OrderDate = DateTime.Now
        };

        _orderRepository.PlaceOrder(order);

        var updatedProduct = _productRepository.GetProductById(product.ProductId);
        Assert.AreEqual(initialStock - 2, updatedProduct.Stock);
    }

    [TearDown]
    public void CleanUp()
    {
        var product = _productRepository.GetAllProducts()
            .FirstOrDefault(p => p.Name == "Test Product");
        if (product != null)
        {
            _productRepository.DeleteProduct(product.ProductId);
        }
    }
}
