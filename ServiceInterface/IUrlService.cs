using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace ServiceInterface
{
    public interface IUrlService
    {
        Task<string> CreateShortUrlForIp(string userIp, string url);
        Task<string> CreateShortUrlForUser(Guid userId, string url);
        Task<string> UrlOriginal(string shorturl);
        Task<List<URLViewModel>> GetUrlByClient(string id,bool isUser);
    }
}


