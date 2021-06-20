using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace music_app.Data
{
    public class AuthManagerData
    {
        public readonly string COOKIE_NAME = "access_token";
        public readonly string SCHEMA_NAME = "AuthCookie";

        public ClaimsPrincipal SetToken(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(COOKIE_NAME, value)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }

        public string GetAuthToken(IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var token = claimsIdentity?.FindFirst(COOKIE_NAME)?.Value;

            return string.IsNullOrEmpty(token) ? null : token;
        }
        
        public bool ExistsAuthToken(IIdentity identity)
        {
            var authCookie = GetAuthToken(identity);

            if (!string.IsNullOrEmpty(authCookie))
                return true;

            return false;
        }

        public void DeleteAuthToken(HttpContext context)
        {
            context.Response.Cookies.Delete(COOKIE_NAME);
        }
    }
}
