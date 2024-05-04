using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Website_Sport.Models;
using Website_Sport.Models.Authentication;
using BCrypt.Net;
namespace Website_Sport.Controllers
{
    public class AccessController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();
        [Authentication]
        [HttpGet]
        public IActionResult Account()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                var userEmail = HttpContext.Session.GetString("Email");
                var user = context.Users.FirstOrDefault(u => u.Email == userEmail);
                return View(user);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Account", "Access");
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
                    HttpContext.Session.SetString("Email", existingUser.Email);
                    ViewBag.Email = HttpContext.Session.GetString("Email");
                    return RedirectToAction("Account", "Access");
                }
                else
                {
                    ViewBag.ErrorMessage = "Email hoặc mật khẩu không chính xác.";
                    return View();
                }
            }
            return RedirectToAction("Account", "Access");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "Access");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string username, string email, string password, string address, int phone)
        {
            if (context.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("Email", "Email người dùng đã tồn tại.");
                return BadRequest(ModelState);
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var newUser = new User
            {
                UserName = username,
                Email = email,
                Password = hashedPassword,
                Address = address,
                Phone = phone,
                PositionId = 2,
            };
            context.Users.Add(newUser);
            context.SaveChanges();
            return RedirectToAction("Login");
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
                return RedirectToAction("Account");
            }
            return View();
        }
        [HttpGet]
        public IActionResult ChangeInfo()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                var userEmail = HttpContext.Session.GetString("Email");
                var user = context.Users.FirstOrDefault(u => u.Email == userEmail);
                return View(user);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ChangeInfo(User us)
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                var userEmail = HttpContext.Session.GetString("Email");
                var user = context.Users.FirstOrDefault(u => u.Email == userEmail);
                user.UserName = us.UserName;
                user.Email = us.Email;
                user.Address = us.Address;
                user.Phone = us.Phone;
                context.SaveChanges();
                return RedirectToAction("Account");
            }
            return View();
        }
    }
}
