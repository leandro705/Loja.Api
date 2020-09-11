using Microsoft.AspNetCore.Mvc;

namespace Loja.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
