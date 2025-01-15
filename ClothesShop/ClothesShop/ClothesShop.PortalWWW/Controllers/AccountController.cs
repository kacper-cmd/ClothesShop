using ClothesShop.PortalWWW.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.PortalWWW.Controllers
{
   // [Route("Login")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

       // [Route("")]
       // [Route("index")]
       //[Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[Route("Login")]
        public IActionResult Login(string username, string password, bool rememberMe)
        {
            var user = _accountService.Login(username, password);
            if (user.IsSuccessful ==true)
            {
                HttpContext.Session.SetString("username", username);
                ViewBag.username = HttpContext.Session.GetString("username");
                if (rememberMe)
                {
                    // Create a cookie to remember the login
                    var options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1) // Set the cookie to expire in 1 day
                    };
                    Response.Cookies.Append("rememberedUser", username, options);
                }
                else
                {
                    var options = new CookieOptions();
                    Response.Cookies.Append("rememberedUser", username, options);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Username = username;
                ViewBag.Message = user.ErrorMessage;
                HttpContext.Session.Remove("username");
                return View("Index");
            }
        }
      //  [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string username, string password, string email, bool flexCheckDefault)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                ViewBag.Message = "Enter name, email and password !";
                return View("Register");
            }

            if (!flexCheckDefault)
            {
                ViewBag.Message = "Please accept the terms and conditions";
                return View("Register");
            }
            var registerResult = _accountService.Register(username, password, email);
            if (registerResult.IsSuccessful)
            {
                // Registration successful, proceed with login
                return RedirectToAction("Index");
            }
            else
            {
                // ViewBag.Message = "Registration failed";
                ViewBag.Message = registerResult.ErrorMessage;
                ViewBag.Username = username;
                ViewBag.Email = email;
                return View("Register");
            }

        }
        //[Route("home")]
        //public IActionResult Home()
        //{
        //    if (HttpContext.Session.GetString("username") != null)
        //    {
        //        ViewBag.username = HttpContext.Session.GetString("username");

        //        return View("Index", "Home");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

    }
    //public IActionResult Logout()
    //{
    //    // Clear the user's authentication state from the session
    //    HttpContext.Session.Clear();

    //    // Redirect to the desired page after logout
    //    return RedirectToAction("Index", "Home");
    //}
}
