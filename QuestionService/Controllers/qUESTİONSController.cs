using Microsoft.AspNetCore.Mvc;

namespace QuestionService.Controllers;

public class qUESTİONSController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}