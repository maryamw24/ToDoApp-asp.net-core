using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Claims;
using ToDoList.Controllers;
using ToDoList.Models;


namespace ToDoList.Pages
{
    public class loginPageModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogin()
        {
            User user = UserController.GetUser(Username, Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Password", user.Password),
                        new Claim("Name", user.Name),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("/MainPage");
            }
            else
            {
                TempData["ErrorOnServer"] = "Invalid Credentials..!!";
                return RedirectToPage("/loginPage");
            }
        }


    }
}
