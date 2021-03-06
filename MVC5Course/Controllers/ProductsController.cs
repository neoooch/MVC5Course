﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            var data = repo.FindByAll(Active, ShowAll: false, ShowCnt: 10);
            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.FindByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.FindByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        ////模型繫結預先驗證
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repo.Update(product);
        //        repo.UnitOfWork.Commit();
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}

        //模型繫結延遲驗證
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var UpdData = repo.FindByProductId(id);

            if (TryUpdateModel(UpdData,new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" }))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(UpdData);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.FindByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repo.FindByProductId(id);
            //關閉驗證
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            repo.Delete(product);
            //可共用UnitOfWork，因此只要commit一次就好，若要刪除OrderLine的資料的話
            //var repoOrderLine = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);

            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult ProductList(SearchProductListVM ListData)
        {
            if (ListData == null)
            {
                ViewBag.Listdata = new SearchProductListVM()
                {
                    MinStock = 0,
                    MaxStock = 999
                };
            }

            FindProductListBySearch(ListData);
            return View();

        }

        [HttpPost]
        public ActionResult ProductList(SearchProductListVM ListData, ProductBatchUpdateVM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var prod = db.Product.Find(item.ProductId);
                    prod.Price = item.Price;
                    prod.Stock = item.Stock;
                }

                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();

                return RedirectToAction("ProductList",ListData);
            }
            FindProductListBySearch(ListData);

            return View();
        }

        private void FindProductListBySearch(SearchProductListVM ListData)
        {
            //查詢所有資料
            var data = repo.FindByAll(true, ShowAll: false, ShowCnt: 10);

            //判斷是否有搜尋條件
            if (!String.IsNullOrEmpty(ListData.qStr))
            {
                data = data.Where(p => p.ProductName.Contains(ListData.qStr));
            }
            data = data.Where(p => p.Stock >= ListData.MinStock && p.Stock <= ListData.MaxStock);


            //選擇顯示欄位並用ProductId倒序
            ViewData.Model = data
                .Select(p => new ProductListVM()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                }).OrderByDescending(p => p.ProductId);
            ViewBag.ListData = ListData;
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(
            [Bind(Include = "ProductName,Price,Stock")]
        ProductListVM data)
        {
            if (ModelState.IsValid)
            {
                //儲存資料進資料庫
                TempData["SuccessResult"] = "新增商品成功";
                return RedirectToAction("ProductList");
            }
            //驗證失敗，繼續顯示原本的表單
            return View();
        }
    }
}
