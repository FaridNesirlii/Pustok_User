using Microsoft.AspNetCore.Identity;

namespace Pustok.Areas.Manage.Services
{
    public class LayautService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public LayautService(UserManager<AppUser> userManager,IHttpContextAccessor httpContext )
        {
            this._userManager = userManager;
            this._httpContext = httpContext;
        }

        public async Task<AppUser> GetUser()
        {
            AppUser user = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);
            return user;
        }
    }
}
