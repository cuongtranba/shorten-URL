using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain;
using ServiceInterface;

namespace shorter.Filter
{
    public class TraceActionFilter : ActionFilterAttribute
    {
        public ITraceService TraceService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TraceService.Trace(new RequestHistory());
            base.OnActionExecuting(filterContext);
        }
    }
} 