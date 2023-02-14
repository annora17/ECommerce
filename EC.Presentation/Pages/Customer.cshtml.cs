using EC.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC.Presentation.Pages
{
    [Authorize(Roles = RoleBasedManager.B2CUserSchema)]
    public class CustomerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
