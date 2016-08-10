using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;

namespace MultipleAuthentication.Middleware
{
    public class SimpleBearerHandler : AuthenticationHandler<SimpleBearerOptions>
    {
        public SimpleBearerHandler()
        {
        }

        protected override Task<bool> HandleUnauthorizedAsync(ChallengeContext context)
        {
            Response.StatusCode = 401;
            Response.Headers["WWW-Authenticate"] = "Bearer";
            return Task.FromResult(false);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var header = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(header) || !header.StartsWith("Bearer "))
            {
                return Task.FromResult(AuthenticateResult.Skip());
            }

            var userName = header.Substring(7);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName, ClaimValueTypes.String, "https://contoso.com/"));
            var identity = new ClaimsIdentity(
                claims,
                Options.AuthenticationScheme,
                ClaimTypes.Name,
                ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(
                principal,
                new AuthenticationProperties(),
                Options.AuthenticationScheme);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

}
