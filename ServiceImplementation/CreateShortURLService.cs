using System;
using System.Linq;
using DomainRepository;
using ServiceInterface;

namespace ServiceImplementation
{
    public class CreateShortURLService : BaseService, ICreateShortURLService
    {
        private static readonly Random Random = new Random();

        public CreateShortURLService(ShortenURlDbContext shortenURlDbContext) : base(shortenURlDbContext)
        {
        }

        public string MakeShortURL(int lenght = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+";
            return new string(Enumerable.Repeat(chars, lenght)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
