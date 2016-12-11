using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainRepository;
using ServiceInterface;

namespace ServiceImplementation
{
    public class UrlService : BaseService, IUrlService
    {
        private readonly ICreateShortURLService createShortUrlService;

        public UrlService(ShortenURlDbContext shortenURlDbContext, ICreateShortURLService createShortUrlService) : base(shortenURlDbContext)
        {
            this.createShortUrlService = createShortUrlService;
        }
        public async Task<string> CreateShortUrlForIp(string userIp, string url)
        {
            var isExistUserIp = await ShortenURlDbContext.UserIp.FirstOrDefaultAsync(c => c.Ip == userIp);
            var shortUrl = createShortUrlService.MakeShortURL();
            var newShortUrl = new ShortUrl()
            {
                ShortUrlString = shortUrl,
                Url = url
            };
            if (isExistUserIp != null)
            {
                isExistUserIp.ShortUrls.Add(newShortUrl);
                await ShortenURlDbContext.SaveChangesAsync();
            }
            else
            {
                var newUserIp = new UserIp()
                {
                    Ip = userIp,
                };
                newUserIp.ShortUrls.Add(newShortUrl);
                ShortenURlDbContext.UserIp.Add(newUserIp);
                await ShortenURlDbContext.SaveChangesAsync();
            }
            return shortUrl;
        }

        public async Task<string> CreateShortUrlForUser(Guid userId, string url)
        {
            var isExistUser = await ShortenURlDbContext.UserUrl.FirstOrDefaultAsync(c => c.UserId == userId);
            var shortUrl = createShortUrlService.MakeShortURL();
            var newShortUrl = new ShortUrl()
            {
                ShortUrlString = shortUrl,
                Url = url
            };
            if (isExistUser != null)
            {
                isExistUser.ShortUrls.Add(newShortUrl);
                await ShortenURlDbContext.SaveChangesAsync();
            }
            else
            {
                var newUserUrl = new UserUrl()
                {
                    UserId = userId
                };
                newUserUrl.ShortUrls.Add(newShortUrl);
                ShortenURlDbContext.UserUrl.Add(newUserUrl);
                await ShortenURlDbContext.SaveChangesAsync();
            }
            return shortUrl;
        }

        public async Task<string> UrlOriginal(string shorturl)
        {
            var firstOrDefault = await ShortenURlDbContext.ShortUrl.FirstOrDefaultAsync(c => c.ShortUrlString == shorturl);
            var url = firstOrDefault?.Url;
            return url;
        }

        public async Task<List<URLViewModel>> GetUrlByClient(string key, bool isUser)
        {
            if (isUser)
            {
                var guid = Guid.Parse(key);
                var urlViewModel =
                    await ShortenURlDbContext.ShortUrl.Include(url => url.RequestHistorie).Where(c => c.UserUrl.UserId == guid).Select(d => new URLViewModel()
                    {
                        Created = d.CreatedAt,
                        ShortURL = d.ShortUrlString,
                        URLId = d.Id,
                        AllClick = d.RequestHistorie.Count,
                        OriginalURL = d.Url
                    }).ToListAsync();
                return urlViewModel;
            }
            else
            {
                var urlViewModel =
                    await ShortenURlDbContext.ShortUrl.Include(url => url.RequestHistorie).Where(c => c.UserIp.Ip == key).Select(d => new URLViewModel()
                    {
                        Created = d.CreatedAt,
                        ShortURL = d.ShortUrlString,
                        URLId = d.Id,
                        AllClick = d.RequestHistorie.Count,
                        OriginalURL = d.Url
                    }).ToListAsync();
                return urlViewModel;
            }
        }
    }
}
