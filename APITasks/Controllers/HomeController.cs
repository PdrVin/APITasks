using Microsoft.AspNetCore.Mvc;
using APITasks.Views;

namespace APITasks.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public HomeView Index()
    {
        return new HomeView
        {
            Message = "Welcome",
            Documentation = "/swagger"
        };
    }
}