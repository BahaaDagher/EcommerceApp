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
        public ViewResult NotFoundPage()
        {
            return View();
        }
    }
}
//means: “Open a writable stream to a new file at that path.”