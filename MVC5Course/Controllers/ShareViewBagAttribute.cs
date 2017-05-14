using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ShareViewBagAttribute : ActionFilterAttribute
    {
        public string MyProperty { get; set; }

        //Action執行之前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "安安 Your application description page.";
        }

        //Action執行之後
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

    }
}