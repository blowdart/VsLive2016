using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookieAuthenticationAndValidation
{
    public class CookieValidator
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            // Do my validation/refresh checks.
            if (context.Principal.Identity.Name == "refresh")
            {
                const string Issuer = "https://contoso.com";
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, "barry", ClaimValueTypes.String, Issuer));
                var userIdentity = new ClaimsIdentity("Cookie");
                userIdentity.AddClaims(claims);
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                context.ReplacePrincipal(userPrincipal);
                context.ShouldRenew = true;
            }
            else if (context.Principal.Identity.Name == "fail")
            {
                context.RejectPrincipal();
                await context.HttpContext.Authentication.SignOutAsync("Cookie");
            }
        }
    }

}
