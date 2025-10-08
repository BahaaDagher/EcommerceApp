using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
