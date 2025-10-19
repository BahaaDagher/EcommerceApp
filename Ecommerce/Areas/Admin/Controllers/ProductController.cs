using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Schema;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ViewResult Index()
        {
            var products = _context.Products.Include(p=>p.Category).Include(p=>p.Brand).AsQueryable();
            return View(products.AsEnumerable());
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View( new ProductVM()
            {
                Categories = _context.Categories.ToList(),
                Brands = _context.Brands.ToList()
            } );
        }
        [HttpPost]
        public RedirectToActionResult Create(Product product ,IFormFile img , List<IFormFile> SubImages , List<string> Colors)
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
                    product.MainImg = fileName;
                }
            }
            var AddedProduct = _context.Products.Add(product);
            _context.SaveChanges();
            if (SubImages is not null)
            {
                foreach(var item in SubImages)
                {
                    if (item.Length > 0)
                    {
                        //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                        var SubImageName = Guid.NewGuid().ToString() + "-" + item.FileName;
                        var SubImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\product_sub_images", SubImageName);
                        using (var stream = System.IO.File.Create(SubImagePath))
                        {
                            item.CopyTo(stream);
                        }
                        var productSubImage = new ProductSubImage()
                        {
                            Img = SubImageName,
                            ProductId = AddedProduct.Entity.Id
                        };
                        _context.ProductSubImages.Add(productSubImage);
                        _context.SaveChanges();
                    }
                }
                
            }
            if (Colors is not null && Colors.Count > 0 )
            {
                Colors = Colors.Distinct().ToList(); 
                foreach (var item in Colors)
                {
                    var productColor = new ProductColor()
                    {
                        Color = item,
                        ProductId = AddedProduct.Entity.Id
                    };
                    _context.ProductColors.Add(productColor);
                }
                _context.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Include(p=>p.ProductColors).Include(p=>p.ProductSubImages).FirstOrDefault(c=>c.Id == id);
            if (product is null)
                return RedirectToAction("NotFoundPage" , "Home" );
            //return View(product);
            return View(new ProductVM()
            {
                Categories = _context.Categories.ToList(),
                Brands = _context.Brands.ToList() , 
                Product = product
            });
        }
        [HttpPost]
        public RedirectToActionResult Update(Product product, IFormFile img  ,List<IFormFile> SubImages , List<string> Colors)
        {
            var productInDB = _context.Products.AsNoTracking().FirstOrDefault(c => c.Id == product.Id);
            var productColorsDB = _context.ProductColors.Where(p => p.ProductId == product.Id); 
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
                    product.MainImg = fileName;
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", productInDB.MainImg);

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
            }
            else
            {
                product.MainImg = productInDB.MainImg;
            }

            if (SubImages is not null)
            {
                foreach (var item in SubImages)
                {
                    if (item.Length > 0)
                    {
                        //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                        var SubImageName = Guid.NewGuid().ToString() + "-" + item.FileName;
                        var SubImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\product_sub_images", SubImageName);
                        using (var stream = System.IO.File.Create(SubImagePath))
                        {
                            item.CopyTo(stream);
                        }
                        var productSubImage = new ProductSubImage()
                        {
                            Img = SubImageName,
                            ProductId = product.Id
                        };
                        _context.ProductSubImages.Add(productSubImage);
                        _context.SaveChanges();
                    }
                }

            }

            if (Colors is not null && Colors.Count > 0)
            {
                foreach(var item in productColorsDB)
                {
                    _context.ProductColors.Remove(item);
                }
                Colors = Colors.Distinct().ToList();
                foreach (var item in Colors)
                {
                    var productColor = new ProductColor()
                    {
                        Color = item,
                        ProductId = product.Id
                    };
                    _context.ProductColors.Add(productColor);
                }
                _context.SaveChanges();
            }
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Include(p=>p.ProductSubImages).FirstOrDefault(c => c.Id == id);
            if (product is null)
                return RedirectToAction("NotFoundPage", "Home");

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", product.MainImg);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            if (product.ProductSubImages.Count > 0 )
            {
                foreach (var img in product.ProductSubImages)
                {
                    var subImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\product_sub_images", img.Img);
                    if (System.IO.File.Exists(subImgPath))
                    {
                        System.IO.File.Delete(subImgPath);
                    }
                }
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteSubImage(int productId , string img)
        {
             var productSubImage = _context.ProductSubImages.FirstOrDefault(p => p.ProductId == productId && p.Img == img);
            if (productSubImage is null)
                return RedirectToAction("NotFoundPage", "Home");
            _context.ProductSubImages.Remove(productSubImage);

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\product_sub_images", productSubImage.Img);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Update) , new { id = productId } );

        }
    }

}


