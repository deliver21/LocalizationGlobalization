using LocalizationGlobalization.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Diagnostics;

namespace LocalizationGlobalization.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer <HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var test = _localizer["HelloWorld"];
            ViewData["HelloWorld"]= test;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //To change the language in the UI , we actually post thus HttpPost
        [HttpPost]
        //returnUrl Operation and logic , go to _ Cuture Partial View
        public IActionResult CultureManagment(string culture, string returnUrl)
        {
            //According to the culture parameter , set the cookie
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires=DateTimeOffset.Now.AddDays(30)});
            return LocalRedirect(returnUrl);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}