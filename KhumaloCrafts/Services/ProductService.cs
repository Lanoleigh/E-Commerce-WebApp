using ST10258321_KhumaloCrafts.Pages.General;
using System.Data.SqlClient;

namespace ST10258321_KhumaloCrafts.Services
{
    public class ProductService
    {
        public List<Product> productList = new List<Product>();
        public List<Product> cart = new List<Product>();
        public List<Product> populateProducts()
        {
            try
            {
                string connectionString = "connection string";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from Product";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product();
                                product.ProductID = reader.GetInt32(0);
                                product.Name = reader.GetString(1);
                                product.Category = reader.GetString(2);
                                product.Description = reader.GetString(3);
                                product.Availability = reader.GetString(4);
                                product.Price = reader.GetDouble(5);
                                product.ProductImg = reader.GetString(6);
                                product.ManufacturerID = reader.GetInt32(7);

                                productList.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " Your connection is faulty.");
            }
            return productList;
        }
    }
}
