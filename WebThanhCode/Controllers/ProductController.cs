using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebThanhCode.Models;
using WebThanhCode.Models.Authentication;

namespace WebThanhCode.Controllers
{
    public class ProductController : Controller
    {

        private readonly IWebHostEnvironment _env;

        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }
        ShopTheThaoContext context = new ShopTheThaoContext();
        // GET: ProductController
        [Authentication]
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();
            products = context.Products.ToList();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(context.Products.Where(s => s.ProductId==id).FirstOrDefault());
        }

        // GET: ProductController/Create
        [Authentication]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prd)
        {
        context.Products.Add(prd);
        context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddImage(int id)
        {
            var image = new Image { ProductId = id };
            return View(image);
        }
        [HttpPost]
        public ActionResult AddImage(Image image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);//Lấy tên file ảnh
            string extension = Path.GetExtension(image.ImageFile.FileName);//Lấy phần path ảnh
            string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString() + extension; //Tạo ra url từ fileName + Path
            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");//Xác định mục tiêu lưu ảnh
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);//Tạo ra url từ thư mục + url(fileName + Path)

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.ImageFile.CopyTo(fileStream);
            }

            // Lưu đường dẫn tương đối vào thuộc tính Image1 của image
            image.Image1 = "/images/" + uniqueFileName;
            context.Images.Add(image);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public ActionResult AddImage(IEnumerable<IFormFile> images)
        //{
        //    foreach (var imageFile in images)
        //    {
        //        string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
        //        string extension = Path.GetExtension(imageFile.FileName);
        //        string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString() + extension;
        //        string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            imageFile.CopyTo(fileStream);
        //        }

        //        var image = new Image
        //        {
        //            Image1 = "/images/" + uniqueFileName
        //        };

        //        context.Images.Add(image);
        //    }

        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult AddRelatedPhoto(int id)
        {
            var image = new Image { ProductId = id };
            return View(image);
        }
        [HttpPost]
        public ActionResult AddRelatedPhoto(Image image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString() + extension;
            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.ImageFile.CopyTo(fileStream);
            }

            image.Image1 = "/images/" + uniqueFileName;
            context.Images.Add(image);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: ProductController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            return View(product);
        }
        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product prd)
        {
           var product = context.Products.Find(id);

            product.ProductName = prd.ProductName;
            product.Price = prd.Price;
            product.Type = prd.Type;
            product.Color = prd.Color;
            product.Size = prd.Size;
            product.Quantity = prd.Quantity;
            product.Saled = prd.Saled;
            product.Description = prd.Description;
            product.CategoryId = prd.CategoryId;
            context.SaveChanges();
                return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditImage(int id)
        {
            var image = context.Images.Find(id);
            return View(image);
        }
        [HttpPost]
        public ActionResult EditImage(int id, Image image)
        {
            // Lấy đối tượng Image từ cơ sở dữ liệu dựa trên id
            var existingImage = context.Images.Find(id);
            if (existingImage == null)
            {
                return NotFound(); // Xử lý khi không tìm thấy hình ảnh cần chỉnh sửa
            }

            // Kiểm tra xem có hình ảnh mới được chọn không
            if (image.ImageFile != null && image.ImageFile.Length > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string extension = Path.GetExtension(image.ImageFile.FileName);
                string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString() + extension;
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Lưu hình ảnh mới vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.ImageFile.CopyTo(fileStream);
                }

                // Cập nhật đường dẫn hình ảnh mới cho đối tượng Image
                existingImage.Image1 = "/images/" + uniqueFileName;
            }
            // Lưu các thay đổi vào cơ sở dữ liệu
            context.SaveChanges();

            return RedirectToAction("Index");
        }


        // POST: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
                return NotFound();
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet("/api/product/images/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine(_env.WebRootPath, "images", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); // Đổi loại ảnh tùy theo loại ảnh bạn muốn hỗ trợ
            }
            else
            {
                return NotFound(); // Trả về 404 nếu ảnh không tồn tại
            }
        }
        [HttpGet("api/get-product")]
        public IActionResult GetProducts(string color, string type)
        {
            // Lấy danh sách sản phẩm dựa trên màu và loại
            var products = context.Products
                .Where(p => p.Color == color && p.Type == type)
                .ToList();

            // Trả về danh sách sản phẩm dưới dạng JSON
            return Json(products);
        }
    }
    }

