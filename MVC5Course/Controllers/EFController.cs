using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        // GET: EF
        FabricsEntities db = new FabricsEntities();
        public ActionResult Index()
        {
            //取得全部資料
            var all = db.Product.AsQueryable();

            var data = all.Where(p => p.Active == true && p.ProductName.Contains("Black"))
                .OrderByDescending(p => p.ProductId);

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product data)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id,Product data)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = data.ProductName;
                item.Price = data.Price;
                item.Stock = data.Stock;
                item.Active = data.Active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                /*foreach(var Order in item.OrderLine.ToList())
                {
                    db.OrderLine.Remove(Order);
                }*/
                //等於上面的foreach
                db.OrderLine.RemoveRange(item.OrderLine);
                db.Product.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}