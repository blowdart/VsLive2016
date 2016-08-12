using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization
{
    public class BuildingEntryAsEmployeeHandler
        : AuthorizationHandler<EnterBuildingRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EnterBuildingRequirement requirement)
        {
            var badgeNumber =
                context.User.Claims.FirstOrDefault(c => c.Type == "BadgeNumber" &&
                                                        c.Issuer == "https://contoso.com");

            if (badgeNumber != null)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
