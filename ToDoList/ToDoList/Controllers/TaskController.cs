using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using ToDoList.Models;


namespace ToDoList.Controllers
{
    public class TaskController
    {
        private static IConfiguration configuration { get; set; }
        public static List<task> tasks = new List<task>();

        public static void Initialize(IConfiguration config)
        {
            configuration = config;
        }   

        public static void AddTask(task task, int id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC stpAddTask @Title, @DueDate,@Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@DueDate", task.DueDate);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
       public static void LoadTasks(int id)
        {
            tasks.Clear();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * From Tasks Where isActive = (SELECT id From Lookup Where value = 'Yes') AND UserId = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        task t = new task();
                        t.Id = (int)reader["TaskID"];
                        t.Title = (string)reader["Title"];
                        t.DueDate = (DateTime)reader["DueDate"];
                        t.Status = LookupController.getValueById((int)reader["statusid"]);
                        tasks.Add(t);

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void UpdateTask(task task)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC stpUpdateTask @TaskID, @Title, @DueDate, @StatusID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TaskID", task.Id);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@DueDate", task.DueDate);
                    command.Parameters.AddWithValue("@StatusID", LookupController.getIdByValue(task.Status));
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void markAsComplete(task Task)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "Update Tasks set StatusId = (SELECT ID From Lookup WHERE value = 'Completed') Where TaskID = @TaskID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TaskID", Task.Id);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void DeleteTask(task Task)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC stpDeleteTask @TaskID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TaskID", Task.Id);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void checkIncomplete()
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                try
                {
                    connection.Open();
                    string query = "EXEC stpCheckIfTaskIsIncomplete";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
            
        public static task getTaskById(int id)
        {
            return tasks.Find(t => t.Id == id);

        }
    }
}
