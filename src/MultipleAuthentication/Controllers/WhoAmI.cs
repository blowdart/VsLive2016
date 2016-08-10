using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MultipleAuthentication.Models;

namespace MultipleAuthentication.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "Cookie,Bearer")]
    [ResponseCache(NoStore = true, Duration = 0, VaryByHeader = "Authorization,Cookie")]
    [Route("api/[controller]")]
    public class WhoAmI : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public NameSchemePair[] Get()
        {
            var identities = new List<NameSchemePair>();

            if (User == null ||
                !User.Identities.Any(i => i.IsAuthenticated))
            {
                identities.Add(new NameSchemePair("Anonymous", "(None)"));
            }
            else
            {
                foreach (var identity in User.Identities)
                {
                    identities.Add(new NameSchemePair(identity.Name, identity.AuthenticationType));
                }
            }

            return identities.ToArray();
        }
    }
}
