using System.Threading.Tasks;
using System.Web.Routing;
using Domain;

namespace ServiceInterface
{
    public interface ITraceService
    {
        void Trace(string shortUrl, RequestHistory requestHistory);
    }
}
