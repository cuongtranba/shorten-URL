using System;
using System.Threading.Tasks;

namespace ServiceInterface
{
    public interface IUrlService
    {
        Task<string> CreateShortUrlForIp(string userIp, string url);
        Task<string> CreateShortUrlForUser(Guid userId, string url);
    }
}
