using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Domain;
using DomainRepository;

namespace ServiceImplementation
{
    public class UrlService : BaseService, ServiceInterface.IUrlService
    {
        public UrlService(ShortenURlDbContext shortenURlDbContext) : base(shortenURlDbContext)
        {

        }
        public async Task<string> CreateShortUrlForIp(string userIp, string url)
        {
            var isExistUserIP = await ShortenURlDbContext.UserIp.FirstOrDefaultAsync(c => c.Ip == userIp);
            if (isExistUserIP != null)
            {
                isExistUserIP.ShortUrls.Add(new ShortUrl());
            }

            throw new NotImplementedException();
        }

        public Task<string> CreateShortUrlForUser(Guid userId, string url)
        {
            throw new NotImplementedException();
        }
    }
}
