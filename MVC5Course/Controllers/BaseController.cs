using System.Web;
using MVC5Course.Models;
using System.Web.Mvc;
using MVC5Course.Models.ViewModels;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace MVC5Course.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    [HandleError(ExceptionType = typeof(HttpException), View = "Error")]
    [HandleError(ExceptionType = typeof(SqlException), View = "Error_Sql")]
    public abstract class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        [LocalOnly]
        public ActionResult Debug()
        {
            return Content("Hello");
        }

        //若找不到網頁 指定到某一網頁(不建議這麼做)
        //protected override void HandleUnknownAction(string actionName)
        //{
        //    this.RedirectToAction("Index","Home").ExecuteResult(this.ControllerContext);
        //}
    }
}