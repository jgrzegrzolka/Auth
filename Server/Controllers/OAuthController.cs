using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string response_type, // authorization flow
            string client_id,
            string redirect_uri,
            string scope, // what information I want
            string state) // random string generated to confirm that we are going back to the same client
        {
            var query = new QueryBuilder();

            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);

            return View();
        }

        [HttpPost]

        public IActionResult Authorize(
            string username,
            string redirect_uri,
            string state)
        {
            var code = "BABABABABA";

            var query = new QueryBuilder();

            query.Add("redirectUri", redirect_uri);
            query.Add("code", code);
            query.Add("state", state);

            return Redirect($"{redirect_uri}{query.ToString()}");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Token()
        {
            return View();
        }
    }
}
