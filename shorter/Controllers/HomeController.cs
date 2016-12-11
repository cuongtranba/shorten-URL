using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Domain;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using shorter.Filter;
using ServiceInterface;

namespace shorter.Controllers
{
    public class HomeController : Controller
    {
        private IUrlService urlService;
        private IComponentContext ComponentContext;
        public HomeController(IUrlService urlService, IComponentContext componentContext)
        {
            this.urlService = urlService;
            ComponentContext = componentContext;
        }

        public async Task<ActionResult> Index()
        {
            var model = new List<URLViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                model = await urlService.GetUrlByClient(User.Identity.GetUserId(), true);
            }
            else
            {
                model = await urlService.GetUrlByClient(Request.UserHostAddress, false);
            }
            return View(model);
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

        public async Task<ActionResult> Report(int urlId)
        {
            var modelByDate = await ComponentContext.ResolveNamed<IReport>(ReportType.ReportByLast7Days.ToString()).GetReport(urlId);

            var modelByCountry = await ComponentContext.ResolveNamed<IReport>(ReportType.ReportByCountry.ToString()).GetReport(urlId);

            var modelByBrowser = await ComponentContext.ResolveNamed<IReport>(ReportType.ReportByBrowser.ToString()).GetReport(urlId);

            var modelByPlatform = await ComponentContext.ResolveNamed<IReport>(ReportType.ReportByPlatforms.ToString()).GetReport(urlId);
            return View(Tuple.Create(modelByDate, modelByCountry, modelByBrowser, modelByPlatform));
        }
    }
}