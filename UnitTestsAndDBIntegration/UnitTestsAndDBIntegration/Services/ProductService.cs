using UnitTestsAndDBIntegration.Interfaces;
using UnitTestsAndDBIntegration.Models;

public class ProductService
{
    private IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public void AddProduct(Product product)
    {
        if (product.Price < 0 || product.Stock < 0)
        {
            throw new ArgumentException("Price and stock must be non-negative.");
        }

        _productRepository.AddProduct(product);
    }

    public void UpdateProduct(Product product)
    {
        if (product.Price < 0 || product.Stock < 0)
        {
            throw new ArgumentException("Price and stock must be non-negative.");
        }

        _productRepository.UpdateProduct(product);
    }

    public void DeleteProduct(int productId)
    {
        _productRepository.DeleteProduct(productId);
    }

    public Product GetProductById(int productId)
    {
        return _productRepository.GetProductById(productId);
    }
}
