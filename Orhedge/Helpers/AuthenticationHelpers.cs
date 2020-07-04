using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Orhedge.Helpers
{
    public static class AuthenticationHelpers
    {
        public static ClaimsPrincipal GetClaimsPrincipal(int userId, StudentPrivilege privilege)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
               new Claim[]
               {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, privilege.ToString())
               }, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
