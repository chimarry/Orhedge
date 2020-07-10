using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Orhedge.Controllers
{
    public class LocalizationController : Controller
    {
        public IActionResult SwitchToSerbian(string redirectController)
        {
            SetLanguage("sr");
            return RedirectToAction("Index", redirectController);
        }

        public IActionResult SwitchToEnglish(string redirectController)
        {
            SetLanguage("en");
            return RedirectToAction("Index", redirectController);
        }

        private void SetLanguage(string culture)
        {
            Response.Cookies.Append(
              CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
              new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true }
          );
        }
    }
}
