using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Areas.Manage.ViewModels;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
                return View();
        }
        [HttpPost]
         public async Task<IActionResult> Login(AdminLoginViewModel adminLogin)
         {
            if(!ModelState.IsValid)  return View(); 
            AppUser user = await _userManager.FindByNameAsync(adminLogin.UserName);
            if (user == null) { ModelState.AddModelError("", "Username and password invalid"); return View(); }
            var result=  await _signInManager.PasswordSignInAsync(user,adminLogin.Password,false,false);
            if (!result.Succeeded) 
            { 
                ModelState.AddModelError("", "Username and password invalid"); 
                return View(); 
            }
            return RedirectToAction("Index","dashboard");
         }
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {

           await _signInManager.SignOutAsync();
            }
            return RedirectToAction("login", "account");
        }
    }
}
