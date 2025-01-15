using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ClothesShop.PortalWWW.Helpers
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the user is authenticated
            var username = context.HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                // User is not logged in, redirect to the login page or show access denied message
                context.Result = new RedirectToActionResult("Index", "Account", null);
            }
        }

        
    }

}
