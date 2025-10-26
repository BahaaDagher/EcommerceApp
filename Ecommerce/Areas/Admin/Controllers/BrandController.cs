using Mapster;
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
        public IActionResult Create(CreateBrandVM CreateBrandVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(CreateBrandVM); 
            }
            //var brand = new Brand()
            //{
            //    Name = CreateBrandVM.Name,
            //    Description = CreateBrandVM.Description,
            //    Status = CreateBrandVM.Status
            //};
            var brand = CreateBrandVM.Adapt<Brand>(); 
            if (CreateBrandVM.FormImg is not null )
            {
                if(CreateBrandVM.FormImg.Length >0 )
                { 
                    //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var fileName = Guid.NewGuid().ToString() + "-" + CreateBrandVM.FormImg.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        CreateBrandVM.FormImg.CopyTo(stream);
                    }
                    brand.Img = fileName;
                }
            }
            _context.Brands.Add(brand);
            _context.SaveChanges();
            Response.Cookies.Append("Theme", "Dark", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(7)
            });  
            TempData["Success"] = "Brand Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var brand = _context.Brands.FirstOrDefault(c=>c.Id == id);
            if (brand is null)
                return RedirectToAction("NotFoundPage" , "Home" );
            //var updateBrandVM = new UpdateBrandVM()
            //{
            //    Id = brand.Id,
            //    Name = brand.Name,
            //    Description = brand.Description,
            //    Status = brand.Status , 
            //    Img = brand.Img
            //};
            var updateBrandVM = brand.Adapt<UpdateBrandVM>();
            return View(updateBrandVM);
        }
        [HttpPost]
        public IActionResult Update(UpdateBrandVM UpdateBrandVM)
        {
            var brandInDB = _context.Brands.AsNoTracking().FirstOrDefault(c => c.Id == UpdateBrandVM.Id);
            if (!ModelState.IsValid)
            {
                UpdateBrandVM.Img = brandInDB.Img; 
                return View(UpdateBrandVM);
            }
            //var brand = new Brand()
            //{
            //    Id = UpdateBrandVM.Id,
            //    Name = UpdateBrandVM.Name,
            //    Description = UpdateBrandVM.Description,
            //    Status = UpdateBrandVM.Status
            //};
            var brand = UpdateBrandVM.Adapt<Brand>(); 
            if (UpdateBrandVM.FormImg is not null) {
                if (UpdateBrandVM.FormImg.Length > 0)
                {
                    //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var fileName = Guid.NewGuid().ToString() + "-" + UpdateBrandVM.FormImg.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        UpdateBrandVM.FormImg.CopyTo(stream);
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


