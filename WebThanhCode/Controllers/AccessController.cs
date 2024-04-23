using Microsoft.AspNetCore.Mvc;
using WebThanhCode.Models;

namespace WebThanhCode.Controllers
{
    public class AccessController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var us = context.Users.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (us != null)
                {
                    if (us.PositionId == 1) // Kiểm tra positionId của người dùng
                    {
                        HttpContext.Session.SetString("Email", us.Email.ToString());
                        ViewBag.Email = HttpContext.Session.GetString("Email");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Bạn không được phép đăng nhập!";
                        return View();
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "Access");
        }
    }
}
