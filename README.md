### Task 1: Build the System

1.  **Create a Database Schema**:
    *   **Tables**:
        *   Products:
            *   ProductId (int, primary key)
            *   Name (string)
            *   Price (decimal)
            *   Stock (int)
        *   Orders:
            *   OrderId (int, primary key)
            *   ProductId (foreign key to Products)
            *   Quantity (int)
            *   OrderDate (datetime)
2.  **Develop Repository Interfaces**:
    *   Create an interface IProductRepository with methods:
        *   IEnumerable<Product> GetAllProducts();
        *   Product GetProductById(int productId);
        *   void AddProduct(Product product);
        *   void UpdateProduct(Product product);
        *   void DeleteProduct(int productId);
    *   Create an interface IOrderRepository with methods:
        *   void PlaceOrder(Order order);
        *   IEnumerable<Order> GetOrders();
        *   Order GetOrderById(int orderId);
3.  **Implement Services**:
    *   ProductService:
        *   Handles product-related logic (e.g., checking stock before updating).
    *   OrderService:
        *   Validates if sufficient stock is available before placing an order.
        *   Reduces stock in the Products table after an order is placed.

### Task 2: Write Unit Tests

1.  **Mock the Repositories**:
    *   Use a mocking library (e.g., Moq) to create mocks for IProductRepository and IOrderRepository.
2.  **Write Tests for ProductService**:
    *   Test GetAllProducts() to ensure it returns the correct data.
    *   Test AddProduct() with valid and invalid data (e.g., negative price or stock).
    *   Test UpdateProduct() to verify that the correct product is updated.
3.  **Write Tests for OrderService**:
    *   Test PlaceOrder() to:
        *   Validate that stock is updated after placing an order.
        *   Throw an exception if the stock is insufficient.

### Task 3: Perform Database Integration

1.  **CRUD Operations**:
    *   Implement methods in the repositories (IProductRepository, IOrderRepository) to interact with the database.
    *   Test these methods with actual database connections:
        *   Add products to the database.
        *   Retrieve products by ID.
        *   Place an order and validate that stock decreases in the Products table.
2.  **Integration Test**:
    *   Write a test that:
        *   Inserts a product into the database.
        *   Places an order for the product.
        *   Verifies that the stock decreases correctly in the database.
