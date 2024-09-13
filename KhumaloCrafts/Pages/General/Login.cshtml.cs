using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10258321_KhumaloCrafts.Pages.Customer;
using ST10258321_KhumaloCrafts.Pages.Manufacturer;
using ST10258321_KhumaloCrafts.Services;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ST10258321_KhumaloCrafts.Pages.General
{
    public class LoginModel : PageModel
    {
  
        public CustomerDomainModel customer = new CustomerDomainModel();
        
        public List<Orders> oList = new List<Orders>();
        public OrderService orders = new OrderService();    
        private ManuDomainModel man = new ManuDomainModel() ;
        public ManufactureService manuA = new ManufactureService() ;
        public ProductService productService = new ProductService();
        public List<Product> pros = new List<Product>();
        public string active_user;
        public string loginMessage = "";
        public Boolean IsManULoggedIn = false;
        public Boolean IsCusLoggedIn = false;
        public int x;
        public int index;
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }
        public string active_userID;

        public void OnGet()
        {
            active_user = Request.Cookies["email"];
        }
        public IActionResult OnPost(string email)
        {
            CookieOptions options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("email", email, options);
            

            customer.OnGet();
            man.OnGet();
            oList = orders.populateOrders();
            pros = productService.populateProducts();

            foreach (var item in customer.customerList)
            {
                if (item.CustomerEmail == email && item.Password == password)
                {
               
                    active_userID = item.CustomerID.ToString();
                    Response.Cookies.Append("active_ID",active_userID,options);
                    customer.activeCustomer = item;
                    IsCusLoggedIn = true;
                    //return RedirectToPage("Work");
                    break;
                }

                x++; 
            }
            //check for manufacturer admin
            foreach (var item in man.manufacturerList)
            {
                if (item.Email == email && item.Password == password)
                {
                    manuA.activeMan = item;
                    IsManULoggedIn = true;
                    
                    int y = man.manufacturerList.IndexOf(item);
                    index = y;
                    break;
                }

                ///++;  
            }
            if(IsCusLoggedIn == false && IsManULoggedIn == false)
                loginMessage = "Invalid Login attempt.";

            return Page();
        }

    }
}
