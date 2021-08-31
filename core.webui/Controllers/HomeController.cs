using Microsoft.AspNetCore.Mvc;

namespace core.webui.Controllers
{
    public class HomeController:Controller
    {

         public IActionResult Index()
        {
            return View();
        }

    }
}