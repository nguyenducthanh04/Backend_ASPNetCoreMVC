using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Website_Sport.Models;
using X.PagedList;

namespace Website_Sport.Controllers
{
    public class ProductController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();
        public IActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var products = context.Products.Include(p => p.Images).ToList();
            var uniqueProducts = products.GroupBy(p => p.Type)
                                          .Select(g => g.First())
                                          .ToList();
            PagedList<Product> list = new PagedList<Product>(uniqueProducts, pageNumber, pageSize);
            return View(list);
        }
        public IActionResult Details (int id)
        {
            return View(context.Products.Include("Images").Where(s => s.ProductId == id).FirstOrDefault());
        }
        [HttpGet]
        public IActionResult GetProductByCategory(string category)
        {
            var products = context.Products.Include(c => c.Category).Include(i => i.Images).Where(x => x.Category.CategoryName == category).OrderBy(p => p.ProductName).ToList();    
        return View(products);
        }
    }
}
