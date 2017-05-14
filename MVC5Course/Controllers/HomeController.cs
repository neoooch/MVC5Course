using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [ShareViewBag]
        //[LocalOnly]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            //測試例外錯誤訊息
            throw new ArgumentException("Error Handled!!");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Unknown()
        {
            return View();
        }

        //沒有Layout的
        public ActionResult PartialAbout()
        {
            ViewBag.Message = "Your application description page.";
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        //跳轉至 顯示 成功 訊息 的頁面
        public ActionResult SuccessRedirect()
        {
            return PartialView("SuccessRedirect", "/");
        }

        public ActionResult GetImgFile()
        {
            //顯示檔案
            //return File(Server.MapPath("~/Content/E龍.png"), "image/png");
            //強迫下載
            return File(Server.MapPath("~/Content/E龍.png"), "image/png", "EDragon.png");
        }

        public ActionResult GetJson()
        {
            //關閉延遲載入
            db.Configuration.LazyLoadingEnabled = false;
            return Json(db.Product.Take(5),JsonRequestBehavior.AllowGet);
        }
    }
}