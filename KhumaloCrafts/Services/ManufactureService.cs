using ST10258321_KhumaloCrafts.Pages.General;
using ST10258321_KhumaloCrafts.Pages.Manufacturer;
using System.Data.SqlClient;

namespace ST10258321_KhumaloCrafts.Services
{
    public class ManufactureService :Manufacturer
    {
       public Manufacturer activeMan= new Manufacturer();
        public List<Manufacturer> manList= new List<Manufacturer>();
        private int z;
        public List<Manufacturer> getManufactures()
        {
            try
            {
                string connectionString = "connection string";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * from Manufacturer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Manufacturer man = new Manufacturer();
                                man.ManufacturerID = reader.GetInt32(0);
                                man.ManName = reader.GetString(1);
                                man.Email = reader.GetString(2);
                                man.Password = reader.GetString(3);
                                manList.Add(man);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "something went wrong buddy");
            }
            return manList;
        }

            
    }
}
