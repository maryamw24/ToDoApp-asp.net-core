using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class UserController
    {
        private static IConfiguration configuration { get; set; }
        public static List<User> users = new List<User>();

        public static void Initialize(IConfiguration config)
        {
            configuration = config;
        }   

        public static void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC stpAddUser @UserName, @Password, @Name";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static void LoadUsers()
        {
            users.Clear();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * From Users";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        User u = new User();
                        u.Id = (int)reader["UserID"];
                        u.UserName = (string)reader["UserName"];
                        u.Password = (string)reader["Password"];
                        u.Name = (string)reader["FullName"];
                        users.Add(u);

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static User GetUser(string username, string password)
        {
            return users.Find(u => u.UserName == username && u.Password == password);
        }

        public static bool isUserAvailable(string username)
        {
            if (users.Find(u => u.UserName == username) != null)
            {
                return false;
            }
            return true;
        }
    }
}
