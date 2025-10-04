using System.Diagnostics;
using Ecommerce.Models;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ViewResult Welcome()
        {
            return View();
        }
        public ViewResult PersonalInfo()
        {
            var persons = new List<Person>()
            {
                new Person() { Id = 1, Name = "John Doe", Age = 30, Email = "Person@t.com" } ,
                new Person() { Id = 2, Name = "John Doe", Age = 30, Email = "Person@t.com" } ,
                new Person() { Id = 3, Name = "John Doe", Age = 30, Email = "Person@t.com" } ,
            };

            var personVm = new PersonVM()
            {
                Persons = persons,
                Count = persons.Count
            }; 
            return View(viewName: "PersonalInformation");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
