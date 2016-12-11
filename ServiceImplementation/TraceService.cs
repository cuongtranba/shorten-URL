using System.Linq;
using System.Web.Routing;
using Domain;
using DomainRepository;
using ServiceInterface;

namespace ServiceImplementation
{
    public class TraceService : BaseService, ITraceService
    {
        public TraceService(ShortenURlDbContext shortenURlDbContext) : base(shortenURlDbContext)
        {
        }

        public void Trace(string shortUrl, RequestHistory requestHistory)
        {
            shortUrl = shortUrl.Split('/')[2];
            var url = ShortenURlDbContext.ShortUrl.FirstOrDefault(c => c.ShortUrlString == shortUrl);
            if (url != null)
            {
                url.RequestHistorie.Add(requestHistory);
                ShortenURlDbContext.SaveChanges();
            }
        }

    }
}
