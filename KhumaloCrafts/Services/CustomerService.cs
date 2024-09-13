using ST10258321_KhumaloCrafts.Pages.Customer;
using System.Data.SqlClient;

namespace ST10258321_KhumaloCrafts.Services
{
    public class CustomerService : Customer
    {
        public List<Customer> customerList = new List<Customer>();

        public List<Customer> populateCustomer()
        {
            try
            {
                string connectionString = "connection string";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * from Customer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer();
                                customer.CustomerID = reader.GetInt32(0);
                                customer.CustomerName = reader.GetString(1);
                                customer.CustomerSurname = reader.GetString(2);
                                customer.CustomerEmail = reader.GetString(3);
                                customer.Password = reader.GetString(4);

                                customerList.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "...connection is faulty.");
            }
            return customerList;
        }
    }
}
