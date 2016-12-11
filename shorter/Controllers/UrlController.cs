using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ServiceInterface;

namespace shorter.Controllers
{
    [RoutePrefix("v1/url")]
    public class UrlController : ApiController
    {
        private IUrlService service;

        public UrlController(IUrlService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("ShortUrl")]
        public async Task<IHttpActionResult> ShortUrl([FromUri]string url)
        {
            var responseUrl = await service.CreateShortUrlForIp(HttpContext.Current.Request.UserHostAddress, url);
            return Ok("/go/" + responseUrl);
        }
    }
}
