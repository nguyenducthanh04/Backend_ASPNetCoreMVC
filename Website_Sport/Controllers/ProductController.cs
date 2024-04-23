using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Lấy tất cả sản phẩm từ cơ sở dữ liệu và eager load dữ liệu hình ảnh
            var products = context.Products.Include(p => p.Images).ToList();

            // Lọc ra các sản phẩm duy nhất dựa trên trường Type
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
        public IActionResult GetProductsByColor(string color, string type)
        {
            // Lấy danh sách sản phẩm có Color = color và Type = type từ database
            var products = context.Products
                .Where(p => p.Color == "Xanh" && p.Type == "Áo thi đấu bóng đá CAHN 2024")
                .ToList();
            return Json(products); // Trả về danh sách sản phẩm dưới dạng JSON
        }
    }
}
