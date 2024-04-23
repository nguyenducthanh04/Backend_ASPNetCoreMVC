using Admin_Sport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin_Sport.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopSportContext _context;
        public ProductController(ShopSportContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var prds = _context.Products.ToList();
            return View(prds);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Products prd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Add(prd);
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(prd);
        }

    }
}
