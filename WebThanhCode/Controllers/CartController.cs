using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebThanhCode.Models;

namespace WebThanhCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("add-to-cart")]
        public IActionResult AddToCart([FromBody] JObject data)
        {
            try
            {
                // Giải mã dữ liệu JSON nhận được từ Fetch API
                string type = data["Type"].ToString();
                string price = data["Price"].ToString();
                string size = data["Size"].ToString();
                string color = data["Color"].ToString();
                string quantity = data["Quantity"].ToString();

                // Truy vấn thông tin sản phẩm từ bảng Products dựa trên các trường dữ liệu nhận được
                var product = context.Products.FirstOrDefault(p =>
                    p.Type == type &&
                    p.Price == price &&
                    p.Size == size &&
                    p.Color == color
                );

                // Kiểm tra xem sản phẩm có tồn tại không
                if (product == null)
                {
                    return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
                }

                // Tạo một đối tượng CartItem để thêm vào bảng Carts
                var cartItem = new Cart
                {
                    UserId = 1,
                    ProductId = product.ProductId, // Giả sử Id của sản phẩm là ProductId trong bảng Carts
                    Quantity = Convert.ToInt32(quantity),
                    Price = price,
                    // Các trường dữ liệu khác nếu cần
                };

                // Thêm cartItem vào bảng Carts
                context.Carts.Add(cartItem);

                // Lưu thay đổi vào cơ sở dữ liệu
                context.SaveChanges();

                // Trả về kết quả thành công nếu mọi thứ diễn ra đúng
                return Ok("Product added to cart successfully");
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có bất kỳ lỗi nào xảy ra
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }
    }
}
