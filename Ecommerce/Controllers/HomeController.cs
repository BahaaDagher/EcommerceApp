using System.Diagnostics;
using Ecommerce.DataAccess;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context = new ApplicationDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products  = _context.Products.Include(p=> p.Category).AsQueryable();
            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ViewResult Welcome()
        {
            return View();
        }
        public ViewResult PersonalInfo(FilerPersonVM filter )
        {
            var persons = new List<Person>()
            {
                new Person() { Id = 1, Name = "bahaa ", Age = 30, Email = "Person@t.com" } ,
                new Person() { Id = 2, Name = "ahmed", Age = 30, Email = "Person@t.com" } ,
                new Person() { Id = 3, Name = "mona", Age = 30, Email = "Person@t.com" } ,
            };
            var personsDB = persons.AsQueryable();

            personsDB = personsDB.Where(p => p.Id == filter.Id && p.Name.Contains(filter.Name )); 

            var personVm = new PersonVM()
            {
                Persons = personsDB.ToList(),
                Count = personsDB.ToList().Count
            }; 
            return View(personVm);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
