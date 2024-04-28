using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebThanhCode.Models;

namespace WebThanhCode.Controllers
{
    public class OrderController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();
        [HttpGet]
        public IActionResult Index()
        {
            var orderList = context.Orders.Include(o => o.User).ToList();
            return View(orderList);
        }
        [HttpGet]
        public IActionResult Details (int id)
        {
            var orderDetail = context.Orders.Include(od => od.User).Where(od => od.OrderId == id).FirstOrDefault();
            return View(orderDetail);
        }
        [HttpPost("api/orders/{id?}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            if (updatedOrder == null || updatedOrder.OrderId != id)
            {
                return BadRequest();
            }
            var existingOrder = context.Orders
                                .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                .SingleOrDefault(o => o.OrderId == id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.Status = updatedOrder.Status;
            foreach (var orderDetail in existingOrder.OrderDetails)
            {
                var product = orderDetail.Product;
                product.Quantity -= orderDetail.Quantity;
                product.Saled += orderDetail.Quantity;
                // Update other properties as needed
            }
            context.SaveChanges();
            return Json(existingOrder); 
        }
        public IActionResult Delete (int id)
        {
            var order = context.Orders.Find(id);
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
