using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.ViewModels.Member;

namespace Pustok.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly PustokContext _pustokContext;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager,PustokContext pustokContext,SignInManager<AppUser> signInManager)
        {
			_userManager = userManager;
			_pustokContext = pustokContext;
			_signInManager = signInManager;
		}
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Register(MemberRegisterViewModel viewModel)
		{
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(viewModel.UserName);
            if(user != null) 
            {
                ModelState.AddModelError("UserName", "UserName istifade olunur!"); 
                return View();
            }
            user = await _pustokContext.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == viewModel.Email.ToUpper());
            //user = await _userManager.FindByEmailAsync(viewModel.Email);

            user = new AppUser
            {
                FullName = viewModel.FullName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
            };
            var result=await _userManager.CreateAsync(user,viewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }
            var resultRole = await _userManager.AddToRoleAsync(user, "User");
            if (!resultRole.Succeeded)
			{
				foreach (var item in resultRole.Errors)
				{
					ModelState.AddModelError("", item.Description);
					return View();
				}
			}
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("index","home");
		}


        public IActionResult LoginAcc()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAcc(MemberLoginViewModel memberLogin)
        {
			if (!ModelState.IsValid) return View();
			AppUser user = await _userManager.FindByNameAsync(memberLogin.UserName);
			if (user == null) { ModelState.AddModelError("", "Username and password invalid"); return View(); }
			var result = await _signInManager.PasswordSignInAsync(user, memberLogin.Password, false, false);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "Username and password invalid");
				return View();
			}
			return RedirectToAction("index", "home");
		}
    }
}
