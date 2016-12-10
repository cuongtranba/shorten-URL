using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using shorter.Filter;
using ServiceInterface;

namespace shorter.Controllers
{
    public class HomeController : Controller
    {
        private IUrlService urlService;

        public HomeController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        public ActionResult Index(string shorturl)
        {
            return View();
        }
        [TraceActionFilter]
        [Route("go/{shorturl?}", Name = "go")]
        public async Task<ActionResult> Go(string shorturl)
        {
            if (!string.IsNullOrEmpty(shorturl))
            {
                var urlOriginal = await urlService.UrlOriginal(shorturl);
                if (string.IsNullOrEmpty(urlOriginal))
                {
                    return RedirectToAction("Index");
                }
                return Redirect(urlOriginal);
            }
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetShortUrl(string url)
        {
            string shortUrl;
            if (User.Identity.IsAuthenticated)
            {
                shortUrl = await urlService.CreateShortUrlForUser(Guid.Parse(User.Identity.GetUserId()), url);
            }
            else
            {
                shortUrl = await urlService.CreateShortUrlForIp(Request.UserHostAddress, url);
            }
            return Json(Url.RouteUrl("go", new { shorturl = shortUrl }), JsonRequestBehavior.AllowGet);
        }
    }
}