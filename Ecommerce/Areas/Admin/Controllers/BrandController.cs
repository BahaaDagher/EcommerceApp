using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ViewResult Index()
        {
            var brands = _context.Brands.AsQueryable();
            return View(brands.AsEnumerable());
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(Brand brand ,IFormFile img)
        {
            if (img is not null )
            {
                if(img.Length >0 )
                { 
                    //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var fileName = Guid.NewGuid().ToString() + "-" + img.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        img.CopyTo(stream);
                    }
                    brand.Img = fileName;
                }
            }
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var brand = _context.Brands.FirstOrDefault(c=>c.Id == id);
            if (brand is null)
                return RedirectToAction("NotFoundPage" , "Home" );
            return View(brand);
        }
        [HttpPost]
        public RedirectToActionResult Update(Brand brand, IFormFile img)
        {
            var brandInDB = _context.Brands.AsNoTracking().FirstOrDefault(c => c.Id == brand.Id);

            if (img is not null) {
                if (img.Length > 0)
                {
                    //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var fileName = Guid.NewGuid().ToString() + "-" + img.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        img.CopyTo(stream);
                    }
                    brand.Img = fileName;
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", brandInDB.Img);

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
            }
            else
            {
                brand.Img = brandInDB.Img;
            }
                _context.Brands.Update(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(c => c.Id == id);
            if (brand is null)
                return RedirectToAction("NotFoundPage", "Home");

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", brand.Img);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}


