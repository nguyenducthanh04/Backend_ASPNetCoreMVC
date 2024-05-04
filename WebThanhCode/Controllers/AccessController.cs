using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using WebThanhCode.Models;
using WebThanhCode.Models.Authentication;
using BCrypt.Net;
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
                var existingUser = context.Users.FirstOrDefault(x => x.Email == user.Email);
                if (existingUser != null && BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
                {
                    if (existingUser.PositionId == 1)
                    {
                        HttpContext.Session.SetString("Email", existingUser.Email);
                        ViewBag.Email = HttpContext.Session.GetString("Email");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Bạn không được phép đăng nhập!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Email hoặc mật khẩu không chính xác.";
                    return View();
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
        [HttpGet]
        public IActionResult ResetPass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPass(string NewPass)
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                var userEmail = HttpContext.Session.GetString("Email");
                var user = context.Users.FirstOrDefault(u => u.Email == userEmail);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(NewPass);
                user.Password = hashedPassword;
                context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
    }
}
