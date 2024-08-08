using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class LookupController
    {
        private static IConfiguration configuration { get; set; }
        public static List<Lookup> Lookups = new List<Lookup>();

        public static void Initialize(IConfiguration config)
        {
            configuration = config;
        }
        public static void loadLookups()
        {

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * From LookUp";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lookup L = new Lookup((int)reader["ID"], (string)reader["Value"], (string)reader["Category"]);
                        Lookups.Add(L);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public static string getValueById(int id)
        {
            Lookup L = Lookups.Find(l => l.Id == id);
            return L.Value;
        }

        public static int getIdByValue(string value)
        {
            Lookup L = Lookups.Find(l => l.Value == value);
            return L.Id;
        }
    }
}
