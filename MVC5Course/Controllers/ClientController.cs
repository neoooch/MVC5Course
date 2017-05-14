using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models.ViewModels;
using MVC5Course.Models;
using System.Data.Entity;

namespace MVC5Course.Controllers
{
    public class ClientController : Controller
    {
        private FabricsEntities db = new FabricsEntities();
        // GET: Client
        public ActionResult BatchUpdate()
        {
            GetClient();
            return View();
        }

        private void GetClient()
        {
            //v ar client = db.Client.Include()
            var client = db.Client.Include(c => c.Occupation).Take(10);
            ViewData.Model = client;
        }

        [HttpPost]
        public ActionResult BatchUpdate(ClientBatchUpdateVM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var Upd = db.Client.Find(item.ClientId);
                    Upd.FirstName = item.FirstName;
                    Upd.MiddleName = item.MiddleName;
                    Upd.LastName = item.LastName;
                }
                db.SaveChanges();
                return RedirectToAction("BatchUpdate");
            }
            GetClient();
            return View();
        }
    }
}