using DomainRepository;

namespace ServiceImplementation
{
    public abstract class BaseService
    {
        protected ShortenURlDbContext ShortenURlDbContext;
        protected BaseService(ShortenURlDbContext shortenURlDbContext)
        {
            this.ShortenURlDbContext = shortenURlDbContext;
        }
    }
}
