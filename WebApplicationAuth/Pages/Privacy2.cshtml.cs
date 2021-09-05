using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationAuth.Pages
{
    [Authorize(Policy = "Claim.DoB")]
    public class Privacy2Model : PageModel
    {   
        public void OnGet()
        {
        }
    }
}
