using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Orhedge.Helpers
{
    public static class ControllerHelpers
    {
        public static int GetUserId(this Controller controller)
        {
            string userIdStr = controller
                .User
                .Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdStr == null)
                throw new InvalidOperationException("Unable to get user id, user is not authenticated");

            return int.Parse(userIdStr);
        }

        public static bool IsUserAuthenticated(this Controller controller)
            => controller.User.Identity.IsAuthenticated;
    }
}
