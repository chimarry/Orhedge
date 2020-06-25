using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Orhedge.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            string userIdStr = user
                .Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdStr == null)
                throw new InvalidOperationException("Unable to get user id, user is not authenticated");

            return int.Parse(userIdStr);
        }

        public static bool IsUserAuthenticated(this ClaimsPrincipal user)
            => user.Identity.IsAuthenticated;
    }
}
