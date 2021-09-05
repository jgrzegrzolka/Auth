using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationAuth.Pages
{
    public class Privacy3Model : PageModel
    {
        private readonly IAuthorizationService _authService;
        public Privacy3Model(IAuthorizationService service)
        {
            _authService = service;
        }

        public async Task OnGetAsync()
        {
            var requirement = new OperationAuthorizationRequirement()
            {
                Name = CookieJarOperation.Open
            };

            await _authService.AuthorizeAsync(User, null, requirement);
        }
    }

    public class CookieJarAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            if (requirement.Name == CookieJarOperation.Look)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Succeed(requirement);
                }
            }

            // ...

            return Task.CompletedTask;
        }
    }

    public static class CookieJarOperation
    {
        public static string Open = "Open";
        public static string TakeCookie = "TakeCookie";
        public static string Look = "Look";
        public static string ComeNear = "ComeNear";
    }
}
