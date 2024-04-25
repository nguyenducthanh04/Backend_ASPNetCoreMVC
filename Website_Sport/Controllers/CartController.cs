using Microsoft.AspNetCore.Mvc;

namespace Website_Sport.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
