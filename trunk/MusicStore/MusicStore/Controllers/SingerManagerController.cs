using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class SingerManagerController : Controller
    {
        db_MusicStoreEntities StoreDB = new db_MusicStoreEntities();
        //
        // GET: /SingerManager/
        public ActionResult Index()
        {
            var _singer = StoreDB.CASIs.ToList();
            return View(_singer);
        }

        //
        // GET: /SingerManager/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangCaSi = StoreDB.TINHTRANGCASIs.OrderBy(ttcs => ttcs.MaTinhTrangCaSi).ToList();
            ViewBag.KhuVuc = StoreDB.KHUVUCs.OrderBy(kv => kv.MaKhuVuc).ToList();

            var _singer = new CASI();
            return View(_singer);
        }

        //
        // POST: /SingerManager/Create
        [HttpPost]
        public ActionResult Create(CASI _singer)
        {
            if (ModelState.IsValid)
            {
                StoreDB.CASIs.AddObject(_singer);
                StoreDB.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangCaSi = StoreDB.TINHTRANGCASIs.OrderBy(ttcs => ttcs.MaTinhTrangCaSi).ToList();
            ViewBag.KhuVuc = StoreDB.KHUVUCs.OrderBy(kv => kv.MaKhuVuc).ToList();
            return View(_singer);
        }

        //
        // GET: /SingerManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangCaSi = StoreDB.TINHTRANGCASIs.OrderBy(ttcs => ttcs.MaTinhTrangCaSi).ToList();
            ViewBag.KhuVuc = StoreDB.KHUVUCs.OrderBy(kv => kv.MaKhuVuc).ToList();

            var _singer = StoreDB.CASIs.First(cs => cs.MaCaSi == id);
            return View(_singer);
        }

        //
        // POST: /SingerManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _singer = StoreDB.CASIs.First(cs => cs.MaCaSi == id);

            if (TryUpdateModel(_singer))
            {
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(_singer);
        }

        //
        // GET: /SingerManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _singer = StoreDB.CASIs.First(cs => cs.MaCaSi == id);
            return View(_singer);
        }

        //
        // POST: /SingerManager/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _singer = StoreDB.CASIs.First(cs => cs.MaCaSi == id);
            StoreDB.CASIs.DeleteObject(_singer);
            StoreDB.SaveChanges();
            return View("Deleted");
        }
    }
}
