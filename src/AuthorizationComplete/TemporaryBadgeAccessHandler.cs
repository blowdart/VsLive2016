using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization
{
    public class TemporaryBadgeAccessHandler : AuthorizationHandler<EnterBuildingRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "TemporaryBadgeExpiry" &&
                                            c.Issuer == "https://contoso.com"))
            {
                return Task.FromResult(0);
            }

            var temporaryBadgeExpiry =
                Convert.ToDateTime(context.User.FindFirst(
                                       c => c.Type == "TemporaryBadgeExpiry" &&
                                       c.Issuer == "https://contoso.com").Value);

            if (temporaryBadgeExpiry > DateTime.Now)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
