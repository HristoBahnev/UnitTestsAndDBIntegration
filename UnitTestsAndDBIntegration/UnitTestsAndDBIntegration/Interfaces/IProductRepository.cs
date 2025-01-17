using UnitTestsAndDBIntegration.Models;

namespace UnitTestsAndDBIntegration.Interfaces
{
    public interface IProductRepository
    {
       public IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}
