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
            if (UserController.isUserAvailable(userName))
            {
                if (Password != "Not matched" && Password != null)

                {
                    User u = new User();
                    u.UserName = userName;
                    u.Password = Password;
                    u.Name = Name;
                    UserController.AddUser(u);
                    UserController.LoadUsers();
                }
                else
                {
                    TempData["ErrorOnServer"] = "Your passwords doesnot match!!";
                }
            }
            else
            {
                TempData["ErrorOnServer"] = "This username is already occupied. Choose a new one.";
            }
        }
    }
}
