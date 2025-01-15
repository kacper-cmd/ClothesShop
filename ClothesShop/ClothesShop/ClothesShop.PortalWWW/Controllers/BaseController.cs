using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.PortalWWW.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult RestrictedAction()
        {
            // Check if the user is authenticated
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }

}
