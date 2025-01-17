using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.Data.SqlClient;

namespace UnitTestsAndDBIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-J6AOG6P\\SQLEXPRESS;Database=BlankfactorAutomationCourse;Integrated Security=True;TrustServerCertificate=True;";

            var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString);


            connection.Open();
            Console.WriteLine("Connected to database.");

            string query = "SELECT * FROM Products";
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name:{reader["Name"]}");

                    }
                }
            }
        }
    }
}