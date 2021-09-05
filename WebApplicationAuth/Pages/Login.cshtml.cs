using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebApplicationAuth.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;
        public LoginModel(IAuthenticationService authentication, IAuthorizationService authorization)
        {
            _authenticationService = authentication;
            _authorizationService = authorization;
        }

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Login),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, "Email")
            };

            var identity = new ClaimsIdentity(claims, "myIdentity");
            var principal = new ClaimsPrincipal(new[] { identity });

            HttpContext.SignInAsync(principal);

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> DoStuff()
        {
            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();
            await _authorizationService.AuthorizeAsync(User, customPolicy);
            return Page();
        }
    }
}
