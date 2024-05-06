using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Website_Sport.Models;
using X.PagedList;

namespace Website_Sport.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }
        ShopTheThaoContext context = new ShopTheThaoContext();
        public IActionResult Index(int? page, string type)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var products = context.Products.Include(p => p.Images).ToList();
            if(!string.IsNullOrEmpty(type))
            {
                 products = products.Where(x => x.Type.IndexOf(type, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            var uniqueProducts = products.GroupBy(p => p.Type)
                                          .Select(g => g.First())
                                          .ToList();
            PagedList<Product> list = new PagedList<Product>(uniqueProducts, pageNumber, pageSize);
            return View(list);
        }
        public IActionResult Details (int id)
        {
            return View(context.Products.Include("Images").Include("Category").Where(s => s.ProductId == id).FirstOrDefault());
        }
        [HttpGet]
        public IActionResult GetProductByCategory(string category)
        {
            var products = context.Products.Include(c => c.Category).Include(i => i.Images).Where(x => x.Category.CategoryName == category).OrderBy(p => p.ProductName).ToList();    
        return View(products);
        }
        [HttpPost]
        public IActionResult FeedBack(string content, int productId)
        {
            var userEmail = HttpContext.Session.GetString("Email");
            var user = context.Users.FirstOrDefault(u => u.Email == userEmail);
            var newFeedBack = new Feedback
            {
                Content = content,
                UserId = user.UserId,
                CreatedAt = DateTime.Now,
                ProductId = productId,
            };
            context.Feedbacks.Add(newFeedBack);
            context.SaveChanges();
            return Redirect("/product-detail/" + productId);
        }
        public IActionResult DeleteFeeback(int id)
        {
            var feedback = context.Feedbacks.Find(id);
            if (feedback != null)
            {
                context.Feedbacks.Remove(feedback);
                context.SaveChanges();
            }
            return Redirect("/product-detail/" + feedback.ProductId);
        }
    }
}
