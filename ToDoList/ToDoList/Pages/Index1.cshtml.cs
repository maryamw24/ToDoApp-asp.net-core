using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Models;
using ToDoList.Controllers;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ToDoList.Pages
{
    public class Index1Model : PageModel
    {
        public List<task> Tasks { get; set; } = TaskController.tasks;
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime DueDate { get; set; }   

        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ToDisplay { get; set; }

        [BindProperty(SupportsGet = true)]
        public string mainHeading { get; set; } = "All Tasks";


        public void OnGet()
        {
            task Task = TaskController.getTaskById(Id);

            if (SelectedDate.HasValue)
            {
                // Filter tasks based on SelectedDate
                Tasks = Tasks.FindAll(t => t.DueDate.Date == SelectedDate.Value.Date);
                mainHeading = "Tasks for " + SelectedDate.Value.ToString("dd/MM/yyyy");
            }

            if (ToDisplay == "Today")
            {
                // Filter tasks based on today's date
                Tasks = Tasks.FindAll(t => t.DueDate.Date == DateTime.Today.Date);
                mainHeading = "Tasks for Today";
            }
            if(ToDisplay == "Tomorrow")
            {
                // Filter tasks based on tomorrow's date
                Tasks = Tasks.FindAll(t => t.DueDate.Date == DateTime.Today.Date.AddDays(1));
                mainHeading = "Tasks for Tomorrow";
            }
            if(ToDisplay == "All")
            {
                // Show all tasks
                Tasks = TaskController.tasks;
                mainHeading = "All Tasks";
            }
        }

        public void OnPostEditTask()
        {
            if (Title != null)
            {
                int id = int.Parse(User.FindFirst("UserId")?.Value);
                task Task = TaskController.getTaskById(Id);
                Task.Title = Title;
                TaskController.UpdateTask(Task);
                TaskController.LoadTasks(id);
                Response.Redirect("#");
            }
            else
            {
                TempData["ErrorOnServer"] = "It can not be empty";
            }
        }

        public void OnPostResTask()
        {
            if (DueDate >= DateTime.Now)
            {
                int id = int.Parse(User.FindFirst("UserId")?.Value);
                task task = TaskController.getTaskById(Id);
                task.DueDate = DueDate;
                TaskController.UpdateTask(task);
                TaskController.LoadTasks(id);
                Response.Redirect("#");
            }
            else
            {
                TempData["ErrorOnServer"] = "Reschedualling date cannot be before today.";
            }
        }

        public void OnPostMarkIncomplete()
        {
            int id = int.Parse(User.FindFirst("UserId")?.Value);
            task Task = TaskController.getTaskById(Id);
            TaskController.UpdateTask(Task);
            TaskController.LoadTasks(id);
            Response.Redirect("#");
        }
        public void OnPostMarkcomplete()
        {
            int id = int.Parse(User.FindFirst("UserId")?.Value);
            task Task = TaskController.getTaskById(Id);
            TaskController.markAsComplete(Task);
            TaskController.LoadTasks(id);
            Response.Redirect("#");
        }

        public void OnPostDelete()
        {
            int id = int.Parse(User.FindFirst("UserId")?.Value);
            task Task = TaskController.getTaskById(Id);
            TaskController.DeleteTask(Task);
            TaskController.LoadTasks(id);
            Response.Redirect("#");
        }
    }
}
