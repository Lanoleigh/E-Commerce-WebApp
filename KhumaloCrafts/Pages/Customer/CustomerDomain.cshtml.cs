using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10258321_KhumaloCrafts.Pages.Manufacturer;
using ST10258321_KhumaloCrafts.Services;
using System.Data.SqlClient;

namespace ST10258321_KhumaloCrafts.Pages.Customer
{
    public class CustomerDomainModel : PageModel
    {
        public List<Customer> customerList = new List<Customer>();
        public Customer activeCustomer = new Customer();
        public CustomerService customerService = new CustomerService();
        public ProductService productService = new ProductService();
        public List<Orders> orderList = new List<Orders>();
        public OrderService orderService = new OrderService();
        public string active_id;

        public int index;
        public void OnGet()
        {
            //here is where we retrieve all the data from database
            customerList = customerService.populateCustomer();
            orderList = orderService.populateOrders();
            
        }
    }
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string Password { get; set; }
    }
}
