using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Controllers;
using ToDoList.Models;


namespace ToDoList.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string userName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public void OnPostSignUp()
        {
            if (Password != "Not matched" && Password != null) {
                if (UserController.isUserAvailable(userName))
                {
                    User u = new User();
                    u.UserName = userName;
                    u.Password = Password;
                    u.Name = Name;
                    UserController.AddUser(u);
                    UserController.LoadUsers();
                }
            }
        }
    }
}
