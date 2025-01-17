using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using UnitTestsAndDBIntegration.Interfaces;
using UnitTestsAndDBIntegration.Models;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository()
    {

        _connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
    }

    public void PlaceOrder(Order order)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = @"
                INSERT INTO Orders (ProductId, Quantity, OrderDate)
                VALUES (@ProductId, @Quantity, @OrderDate);

                UPDATE Products
                SET Stock = Stock - @Quantity
                WHERE ProductId = @ProductId;
            ";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductId", order.ProductId);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.ExecuteNonQuery();
            }
        }
    }

    public Order GetOrderById(int orderId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Order
                        {
                            OrderId = (int)reader["OrderId"],
                            ProductId = (int)reader["ProductId"],
                            Quantity = (int)reader["Quantity"],
                            OrderDate = (DateTime)reader["OrderDate"]
                        };
                    }
                }
            }
        }
        return null;
    }

    public IEnumerable<Order> GetOrders()
    {
        var orders = new List<Order>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders";
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderId = (int)reader["OrderId"],
                            ProductId = (int)reader["ProductId"],
                            Quantity = (int)reader["Quantity"],
                            OrderDate = (DateTime)reader["OrderDate"]
                        });
                    }
                }
            }
        }
        return orders;
    }
}
