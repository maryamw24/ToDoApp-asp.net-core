using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Controllers;
using ToDoList.Models;

namespace ToDoList.Pages
{
    public class MainPageModel : PageModel
    {
        private readonly ILogger<MainPageModel> _logger;

        [BindProperty]
        public task task { get; set; }
        [BindProperty]
        public string Name { get; set; }

        public MainPageModel(ILogger<MainPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Name = User.FindFirst("Name")?.Value;
            var id = int.Parse(User.FindFirst("UserId")?.Value);
            TaskController.LoadTasks(id);

        }

        public void OnPostAddTask()
        {
            int id = int.Parse(User.FindFirst("UserId")?.Value);
            try
            {
                TaskController.AddTask(task, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TaskController.LoadTasks(id);
        }
    }
}
