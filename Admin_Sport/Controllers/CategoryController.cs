using Admin_Sport.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Sport.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShopSportContext _context;
        public CategoryController(ShopSportContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cate = _context.Categories.ToList();
            return View(cate);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categories cate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Categories.Add(cate);
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(cate);
        }
    }
}
