using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        public ActionResult Index()
        {
            return View();
        }

        
        public async Task<JsonResult> GetShortUrl(string url)
        {
            var shortUrl = await urlService.CreateShortUrlForIp(User.Identity.GetUserId(),url);
            return Json(shortUrl);
        }
    }
}