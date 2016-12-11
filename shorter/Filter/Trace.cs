using System.Net;
using System.Web.Mvc;
using Domain;
using ServiceInterface;
using UAParser;

namespace shorter.Filter
{
    public class TraceActionFilter : ActionFilterAttribute
    {
        public ITraceService TraceService { get; set; }

        private string GetCountry(string IP)
        {
            string xmlResult = new WebClient().DownloadString("https://freegeoip.net/xml/" + IP);
            xmlResult = xmlResult.Substring(xmlResult.IndexOf("<CountryName>") + 13);
            int indexOfClosingTag = xmlResult.IndexOf("</CountryName>");
            xmlResult = xmlResult.Substring(0, indexOfClosingTag);
            return xmlResult;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(request.UserAgent);
            TraceService.Trace(request.CurrentExecutionFilePath, new RequestHistory()
            {
                Browser = request.Browser.Browser,
                Country = GetCountry(request.UserHostAddress),
                Platforms = clientInfo.OS.Family,
            });
            base.OnActionExecuted(filterContext);
        }
    }
}