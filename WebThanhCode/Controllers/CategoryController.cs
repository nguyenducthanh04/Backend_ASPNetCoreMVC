using Microsoft.AspNetCore.Mvc;
using WebThanhCode.Models;

namespace WebThanhCode.Controllers
{
    public class CategoryController : Controller
    {
        ShopTheThaoContext context = new ShopTheThaoContext();
        public IActionResult Index()
        {
            var categoryList = context.Categories.ToList();
            return View(categoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id) {
            var category = context.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(int id, Category ct) {
            var category = context.Categories.Find(id);
            category.CategoryName = ct.CategoryName;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
