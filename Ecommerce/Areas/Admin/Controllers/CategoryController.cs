using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //ApplicationDbContext _context = new ApplicationDbContext();
        Repository<Category> _categoryRepository = new Repository<Category>(); 
        public async Task<IActionResult> Index(CancellationToken cancellationToken )
        {
            //var categories = _context.Categories.AsQueryable();
            var categories = await _categoryRepository.GetAsync(cancellationToken: cancellationToken) ;

            return View(categories.AsEnumerable());
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category , CancellationToken cancellationToken)
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
            //_context.Categories.Add(category);
            //_context.SaveChanges();
            await _categoryRepository.AddAsync(category, cancellationToken);
            await _categoryRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id  ,CancellationToken  cancellationToken)
        {
            //var category = _context.Categories.FirstOrDefault(c=>c.Id == id);
            var category = await _categoryRepository.GetOneAsync(c => c.Id == id , cancellationToken: cancellationToken);
            if (category is null)
                return RedirectToAction("NotFoundPage" , "Home" );
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category category, CancellationToken cancellationToken )
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            //_context.Categories.Update(category);
            //_context.SaveChanges();
            _categoryRepository.Update(category);
            await _categoryRepository.CommitAsync(cancellationToken: cancellationToken ); 
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id ,CancellationToken cancellationToken )
        {
            //var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            var category = await _categoryRepository.GetOneAsync(c => c.Id == id , cancellationToken: cancellationToken );
            if (category is null)
                return RedirectToAction("NotFoundPage", "Home");

            //_context.Categories.Remove(category);
            //_context.SaveChanges();
            _categoryRepository.Delete(category);
            await _categoryRepository.CommitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }

}


