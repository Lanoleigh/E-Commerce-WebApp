using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10258321_KhumaloCrafts.Services;
using System.Data.SqlClient;

namespace ST10258321_KhumaloCrafts.Pages.Customer
{
    public class RegisterModel : PageModel
    {
        public Customer customer = new Customer();
        public CustomerService customerService = new CustomerService();
        public List<Customer> customerList = new List<Customer>();
        public string errorMsg = "";
        public string sMsg = "";
        Boolean bRegistered = false;
        int lastCustomerIndex;
        public void OnGet()
        {
            customerList = customerService.populateCustomer();
            lastCustomerIndex = customerList.Count - 1;
        }
        public void OnPost()
        {
            customer.CustomerName = Request.Form["name"];
            customer.CustomerSurname = Request.Form["surname"];
            customer.CustomerEmail = Request.Form["email"];
            customer.Password = Request.Form["password"];

            if (customer.CustomerName.Length == 0 ||
                customer.CustomerSurname.Length == 0 ||
                customer.CustomerEmail.Length == 0 ||
                customer.Password.Length == 0)
            {
                errorMsg = "All fields are required";
                return;
            }
            else
                bRegistered = true;

            try
            {
                string connectionString = "/connection string/";
                using(SqlConnection connection  = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Customer" +
                        "(CustomerName,CustomerSurname,CustomerEmail,Password) VALUES" +
                        "(@name,@surname,@email,@password);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customer.CustomerName);
                        command.Parameters.AddWithValue("@surname", customer.CustomerSurname);
                        command.Parameters.AddWithValue("@email", customer.CustomerEmail);
                        command.Parameters.AddWithValue("@password", customer.Password);

                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception e)
            {
                errorMsg = e.Message;
                return;
            }
            sMsg = "You have successfully registered!";
            
        }

    }

}
