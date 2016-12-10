using System;
using System.Linq;
using DomainRepository;
using ServiceInterface;

namespace ServiceImplementation
{
    public class CreateShortURLService : ICreateShortURLService
    {
        private static readonly Random Random = new Random();
        public string MakeShortURL(int lenght = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
            var value = new string(Enumerable.Repeat(chars, lenght)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
            return value;
        }
    }
}
