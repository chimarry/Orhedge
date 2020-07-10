using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Orhedge.Helpers;

namespace Orhedge.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // TODO: If user is authenticated redirect to materials main page
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            // TODO: Redirect to materials main page
            if (this.IsUserAuthenticated())
                return RedirectToAction("Index");
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
