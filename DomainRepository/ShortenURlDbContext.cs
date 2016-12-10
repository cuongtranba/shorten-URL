using System.Data.Entity;
using Domain;

namespace DomainRepository
{
    public class ShortenURlDbContext:DbContext
    {
        public ShortenURlDbContext():base("ShortenURL")
        {
            
        }

        public DbSet<ShortUrl> ShortUrl { get; set; }
        public DbSet<UserIp> UserIp { get; set; }
        public DbSet<UserUrl> UserUrl { get; set; }
    }
}
