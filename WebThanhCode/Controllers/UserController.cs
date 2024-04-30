using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebThanhCode.Models;
using BCrypt.Net;

namespace WebThanhCode.Controllers
{
    public class UserController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();
        public IActionResult Index()
        {
            var users = context.Users.Include(u => u.Position).ToList();
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            //context.Users.Add(user);
            //context.SaveChanges();
            //return RedirectToAction("Index");
            if (context.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email người dùng đã tồn tại.");
                return BadRequest(ModelState);
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = hashedPassword,
                Address = user.Address,
                Phone = user.Phone,
                PositionId = user.PositionId,
            };
            context.Users.Add(newUser);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userCurrent = context.Users.Find(id);
            return View(userCurrent);
        }
        [HttpPost]
        public IActionResult Edit(int id, User user)
        {
            var userCurrent = context.Users.Find(id);
            userCurrent.UserName = user.UserName;
            userCurrent.Password = user.Password;
            userCurrent.Email = user.Email;
            userCurrent.Phone = user.Phone;
            userCurrent.Address = user.Address;
            userCurrent.PositionId = user.PositionId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
