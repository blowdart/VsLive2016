using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthenticationAndValidation.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "NoSuchRoleExists")]
        public IActionResult FailAuthorization()
        {
            return View();
        }

        [Authorize]
        public IActionResult MustBeAuthenticated()
        {
            return View();
        }
    }
}
