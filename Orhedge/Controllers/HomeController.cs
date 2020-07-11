using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Orhedge.Helpers;

namespace Orhedge.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.IsUserAuthenticated())
                return RedirectToAction("Index", "StudyMaterial");
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            if (this.IsUserAuthenticated())
                return RedirectToAction("Index", "StudyMaterial");
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
