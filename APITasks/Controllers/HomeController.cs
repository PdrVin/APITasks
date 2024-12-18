using Microsoft.AspNetCore.Mvc;

namespace APITasks.Controllers;

[ApiController]
[Route("/home")]
public class HomeController : ControllerBase
{
    [HttpGet("/index")]
    public string Index()
    {
        return "hi";
    }
}