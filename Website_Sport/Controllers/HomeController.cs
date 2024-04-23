using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website_Sport.Models;

namespace Website_Sport.Controllers
{
    public class HomeController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();

        public IActionResult Index()
        {
            var top10Products = context.Products.Include(p => p.Images)
                                   .OrderByDescending(p => p.Saled)
                                   .Take(8)
                                   .ToList();

            return View(top10Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}