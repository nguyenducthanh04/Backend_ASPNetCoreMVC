using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebByThanhNguyen.Models;

namespace WebByThanhNguyen.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebShopContext _shopContext;
        public ProductController(WebShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var result = _shopContext.Products.ToList();
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
            _shopContext.Products.Add(prd);
            _shopContext.SaveChanges();
            return RedirectToAction("Index");
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
