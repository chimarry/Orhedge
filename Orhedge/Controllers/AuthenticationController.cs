using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orhedge.ViewModels;
using ServiceLayer.Models;
using IAuthenticationService = ServiceLayer.Students.Interfaces.IAuthenticationService;

namespace Orhedge.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authService, IMapper mapper)
            => (_authService, _mapper) = (authService, mapper);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login, [FromQuery] string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                LoginResponse loginResponse = await _authService.Login(_mapper.Map<LoginRequest>(login));
                if (loginResponse == null)
                {
                    // TODO: Display login page with message which says that credentials are not valid
                    return Content("Invalid credentials");
                }
                else
                {
                    ClaimsPrincipal claimsPrincipal = GetClaimsPrincipal(loginResponse);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : Url.Action("Index", "Home"));
                }
            }
            else
                // TODO: Display login page again with message about
                return Content("Login view model invalid");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Simply delete authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private ClaimsPrincipal GetClaimsPrincipal(LoginResponse loginResponse)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginResponse.Id.ToString()),
                    new Claim(ClaimTypes.Role, loginResponse.Privilege.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
        
    }
}