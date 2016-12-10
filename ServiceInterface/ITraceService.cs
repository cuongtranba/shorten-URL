using System.Threading.Tasks;
using System.Web.Routing;
using Domain;

namespace ServiceInterface
{
    public interface ITraceService
    {
        void Trace(RequestHistory requestHistory);
    }
}
