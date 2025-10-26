using Microsoft.AspNetCore.Mvc;
using System.Xml.Schema;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ViewResult Index()
        {
            var categories = _context.Categories.AsQueryable();
            return View(categories.AsEnumerable());
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.Description)
            {
                ModelState.AddModelError("" ,"name must not equeal to  decription");
                return View(category);
            }
            if (!ModelState.IsValid)
            {
                return View(category); 
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id )
        {
            var category = _context.Categories.FirstOrDefault(c=>c.Id == id);
            if (category is null)
                return RedirectToAction("NotFoundPage" , "Home" );
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
                return RedirectToAction("NotFoundPage", "Home");

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}


