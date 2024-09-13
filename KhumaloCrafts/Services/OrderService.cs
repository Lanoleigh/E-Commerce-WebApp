using ST10258321_KhumaloCrafts.Pages.Manufacturer;
using System.Data.SqlClient;

namespace ST10258321_KhumaloCrafts.Services
{
    public class OrderService : Orders
    {
        public List<Orders> orderList = new List<Orders>();
        public List<ItemOrdered> itemsList = new List<ItemOrdered>();

        public List<Orders> populateOrders()
        {
            try
            {
                string connectionString = "connection string";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Orders";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Orders order = new Orders();
                                order.OrderID = reader.GetInt32(0);
                                order.CustomerID = reader.GetInt32(1);
                                order.Status = reader.GetString(2);
                                order.DateOrdered = reader.GetDateTime(3);
                                order.Amount = reader.GetDouble(4);
                                order.NumOfItems = reader.GetInt32(5);

                                orderList.Add(order);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return orderList;
        }

        public List<ItemOrdered> populate()
        {
            try
            {
                string connectionString = "connection string";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM ItemOrdered";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using(SqlDataReader reader  = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemOrdered itemOrdered = new ItemOrdered();
                                itemOrdered.ItemOrderedID = reader.GetInt32(0);
                                itemOrdered.CustomerID = reader.GetInt32(1);
                                itemOrdered.OrderID = reader.GetInt32(2);
                                itemOrdered.ManufacturerID = reader.GetInt32(3);
                                itemOrdered.ProductID = reader.GetInt32(4);

                                itemsList.Add(itemOrdered);
                            }
                        }
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return itemsList;
        }

    }
    public class ItemOrdered
    {
        public int ItemOrderedID { get; set; }
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int ManufacturerID { get; set; }


    }
}
