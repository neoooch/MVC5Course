using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class LocalOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                //若不是本機電腦連線至Action，就跳回首頁，不再執行Action
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}