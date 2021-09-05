using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationAuth.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task OnGetAsync()
        {
                await HttpContext.SignOutAsync();
        }
    }
}
