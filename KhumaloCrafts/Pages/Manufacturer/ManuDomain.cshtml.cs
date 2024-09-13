using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10258321_KhumaloCrafts.Pages.General;
using ST10258321_KhumaloCrafts.Services;
using System.Data.SqlClient;

namespace ST10258321_KhumaloCrafts.Pages.Manufacturer
{
    public class ManuDomainModel : PageModel
    {
        public List<Manufacturer> manufacturerList = new List<Manufacturer>();
        public List<Orders> oList = new List<Orders>();
        public Manufacturer manActive = new  Manufacturer();
        public OrderService orderService = new OrderService();
        //public List<ItemsOrdered> itemsList = new List<ItemsOrdered>();
        public List<Product> productsList = new List<Product>();
        public List<ItemOrdered> prodsOrdersList = new List<ItemOrdered>();
        public ProductService productService = new ProductService();
        public ManufactureService manufactureService = new ManufactureService();
        public int index;
        public string active_manufacturer;

        public bool processOrders = false;
        public void OnPostProcesssing(int orderId)
        {
            Orders order = new Orders();
            order.OrderID = orderId;
            order.Status = Request.Form["processInput"];

            try
            {
                string connectionString = "connection string";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID;";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Status",order.Status);
                        command.Parameters.AddWithValue("OrderID", order.OrderID);

                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            processOrders = true;
        }
        public void OnGet()
        {
            
            manufacturerList = manufactureService.getManufactures();
            oList =  orderService.populateOrders();
           // itemsList = orderService.populateItemsOrdered();
            productsList = productService.populateProducts();
            prodsOrdersList = orderService.populate();
        }
        

    }
    public class Orders
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Status { get; set; }
        public DateTime DateOrdered { get; set; }
        public double Amount { get; set; }
        public int NumOfItems { get; set; }
    }
        public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string ManName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
