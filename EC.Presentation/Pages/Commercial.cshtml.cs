using EC.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC.Presentation.Pages
{
    [Authorize(Roles = RoleBasedManager.B2BUserSchema)]
    public class CommercialModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
