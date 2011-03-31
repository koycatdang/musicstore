using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class ArtistManagerController : Controller
    {
        db_MusicStoreEntities StoreDB = new db_MusicStoreEntities();

        //
        // GET: /Artist/
        public ActionResult Index()
        {
            var _artist = StoreDB.NHACSIs.ToList();
            return View(_artist);
        }


        //
        // GET: /Artist/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangNhacSi = StoreDB.TINHTRANGNHACSIs.OrderBy(ttns => ttns.MaTinhTrangNhacSi).ToList();

            var _artist = new NHACSI();
            return View(_artist);
        }

        //
        // POST: /Artist/Create
        [HttpPost]
        public ActionResult Create(NHACSI _artist)
        {
            if (ModelState.IsValid)
            {
                StoreDB.NHACSIs.AddObject(_artist);
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangNhacSi = StoreDB.TINHTRANGNHACSIs.OrderBy(ttns => ttns.MaTinhTrangNhacSi).ToList();
            return View(_artist);
        }

        //
        // GET: /Artist/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangNhacSi = StoreDB.TINHTRANGNHACSIs.OrderBy(ttns => ttns.MaTinhTrangNhacSi).ToList();
            var _artist = StoreDB.NHACSIs.First(ns => ns.MaNhacSi == id);
            return View(_artist);
        }

        //
        // POST: /Artist/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _artist = StoreDB.NHACSIs.First(ns => ns.MaNhacSi == id);
            if (TryUpdateModel(_artist))
            {
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangNhacSi = StoreDB.TINHTRANGNHACSIs.OrderBy(ttns => ttns.MaTinhTrangNhacSi).ToList();
            return View(_artist);
        }

        //
        // GET: /Artist/Delete/5

        public ActionResult Delete(int id)
        {
            var _artist = StoreDB.NHACSIs.First(ns => ns.MaNhacSi == id);
            return View(_artist);
        }

        //
        // POST: /Artist/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _artist = StoreDB.NHACSIs.First(ns => ns.MaNhacSi == id);
            StoreDB.NHACSIs.DeleteObject(_artist);
            StoreDB.SaveChanges();
            return View("Deleted");
        }
    }
}
