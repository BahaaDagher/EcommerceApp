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
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(Category category)
        {
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
        public RedirectToActionResult Update(Category category)
        {
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


