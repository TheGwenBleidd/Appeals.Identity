using Appeals.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Appeals.Identity.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var vm = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model) 
        {
            return View(model);
        }
    }
}
