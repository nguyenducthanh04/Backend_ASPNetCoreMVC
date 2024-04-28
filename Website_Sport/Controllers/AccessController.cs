using Microsoft.AspNetCore.Mvc;
using Website_Sport.Models;
using Website_Sport.Models.Authentication;

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
                var us = context.Users.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (us != null)
                {

                    HttpContext.Session.SetString("Email", us.Email.ToString());
                    ViewBag.Email = HttpContext.Session.GetString("Email");
                    return RedirectToAction("Account", "Access");
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
        public IActionResult Register()
        {
            return View();
        }
    }
}
