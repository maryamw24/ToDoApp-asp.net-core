using Microsoft.AspNetCore.Identity;
using ToDoList.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using ToDoList.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options=>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginPage";
    });
builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

LookupController.Initialize(builder.Configuration);
LookupController.loadLookups();

TaskController.Initialize(builder.Configuration);
TaskController.checkIncomplete();


UserController.Initialize(builder.Configuration);
UserController.LoadUsers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();


app.Run();
