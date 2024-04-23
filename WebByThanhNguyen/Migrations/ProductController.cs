using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebByThanhNguyen.Models;

namespace WebByThanhNguyen.Migrations
{

    public class ProductController : Controller
    {
        // GET: ProductController
        private readonly WebShopContext _db;
        public ProductController(WebShopContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var result = _db.Products.ToList();
            return View(result);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products prd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Products.Add(prd);
                    _db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(prd);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
