using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using Website_Sport.Models;
using Website_Sport.Models.Authentication;

namespace Website_Sport.Controllers
{
    public class PayMentController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();
        [Authentication]
        [HttpPost("/api-post-data")]
        public IActionResult Index([FromBody] List<Product> products)
        {

            var userEmail = HttpContext.Session.GetString("Email");
            var user = context.Users.FirstOrDefault(u => u.Email == userEmail);
            var oder = new Order
            {
                OrderName = "Đơn hàng-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                UserId = user.UserId,
                Status = "0",
                CreatedAt = DateTime.Now,
            };
            context.Orders.Add(oder);
            context.SaveChanges();
            var idOrder = oder.OrderId;
            foreach (var item in products)
            {
                    // Tìm sản phẩm trong cơ sở dữ liệu dựa trên Type
                    var product = context.Products.FirstOrDefault(p => p.Type == item.Type && p.Color == item.Color && p.Size == item.Size);
                Console.WriteLine("product" + product);
                    // Tạo một đối tượng OrderDetail từ dữ liệu trong mảng và thêm vào cơ sở dữ liệu
                    var orderDetail = new OrderDetail
                    {
                        OrderId = idOrder,
                        ProductId = product.ProductId,
                        Quantity = item.Quantity.ToString(),
                    };
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                    // Thêm đối tượng OrderDetail vào cơ sở dữ liệu thông qua DbContext
            }
            return Ok();
        }
        [Authentication]
        [HttpGet]
        public IActionResult Pay()
        {
            var userEmail = HttpContext.Session.GetString("Email");
            var user = context.Users.FirstOrDefault(u => u.Email == userEmail);
            return View(user);
        }
        [HttpGet]
        public IActionResult OderSuccess()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }

}
