using System;
using System.Diagnostics;
using System.Security.Claims;

namespace NS.WebApi.Core.User
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            Claim claim = null;

            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            try
            {
                claim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst("sub");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}
