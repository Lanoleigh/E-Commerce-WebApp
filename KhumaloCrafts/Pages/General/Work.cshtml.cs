
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10258321_KhumaloCrafts.Pages.Customer;
using ST10258321_KhumaloCrafts.Pages.Manufacturer;
using ST10258321_KhumaloCrafts.Services;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace ST10258321_KhumaloCrafts.Pages.General
{
    public class WorkModel : PageModel
    {
        public CustomerService customerService = new CustomerService();
        public ProductService productService = new ProductService();
        public List<Product> productList = new List<Product>();
        public LoginModel login = new LoginModel();
        public Product product1 = new Product();
        public Orders orders = new Orders();
        public List<Product> productCart = new List<Product>();
        public int index;

        
        public int CustId { get; set; }
        [BindProperty]
        public int ProID { get; set; }
        [BindProperty]
        public int quantity { get; set; }
        public string active_user;

        public List<Product> cartItemsList = new List<Product>();



        public async Task<IActionResult> OnPostAddToCartAsync(int productID)
        {
            var product = productList.FirstOrDefault(pros => pros.ProductID == productID);
            if(product != null)
            {
                cartItemsList.Add(product);
                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false });
        }
        public void OnPostPurchase()
        {

            if(quantity == 0)
                quantity = 1;

            foreach(var item in productList)
            {
                if(ProID == item.ProductID)
                {
                    orders.Amount = item.Price * quantity;

                    break;
                }
            }
            login.active_userID = Request.Cookies["active_ID"];
            orders.NumOfItems = quantity;
            orders.CustomerID = Int32.Parse(login.active_userID);
            orders.Status = "Processing";
            orders.DateOrdered = DateTime.Parse("2024-05-20 00:00:00");
            try
            {
                string connectionString = "connection string";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Orders" +
                        "(CustomerID,Status,DateOrdered,Amount,NumOfItems) VALUES" +
                        "(@CustomerID,@Status,@DateOrdered,@Amount,@NumOfItems)";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", orders.CustomerID);
                        command.Parameters.AddWithValue("@Status",orders.Status);
                        command.Parameters.AddWithValue("@DateOrdered", orders.DateOrdered);
                        command.Parameters.AddWithValue("@Amount", orders.Amount);
                        command.Parameters.AddWithValue("@NumOfItems", orders.NumOfItems);

                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
       
        public void OnGet()
        {
            //active_user = Request.Cookies["email"];
            customerService.customerList = customerService.populateCustomer();
            index = customerService.customerList.Count();
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
                                product.Category =  reader.GetString(2);
                                product.Description = reader.GetString(3);
                                product.Availability = reader.GetString(4);
                                product.Price = reader.GetDouble(5);
                                product.ProductImg = reader.GetString(6);
                                product.ManufacturerID =  reader.GetInt32(7);

                                productList.Add(product);
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex +" Your connection is faulty.");
            }
        }
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Availability { get; set; }
        public double Price { get; set; }
        public string ProductImg { get; set; }
        public int ManufacturerID { get; set; }
    }
}
