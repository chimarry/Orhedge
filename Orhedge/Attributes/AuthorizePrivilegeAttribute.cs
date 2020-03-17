using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Text;

namespace Orhedge.Attributes
{
    public class AuthorizePrivilegeAttribute : AuthorizeAttribute
    {
        public AuthorizePrivilegeAttribute(params StudentPrivilege[] roles)
        {
            Roles = string.Join(',', roles.Select(s => s.ToString()));
        }
    }
}
