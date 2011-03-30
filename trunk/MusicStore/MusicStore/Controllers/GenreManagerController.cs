using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class GenreManagerController : Controller
    {
        db_MusicStoreEntities StoreDB = new db_MusicStoreEntities();
        //
        // GET: /GenreManager/
        public ActionResult Index()
        {
            var _genre = StoreDB.THELOAIs.ToList();
            return View(_genre);
        }

        //
        // GET: /GenreManager/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangTheLoai = StoreDB.TINHTRANGTHELOAIs.OrderBy(tttl => tttl.MaTinhTrangTheLoai).ToList();

            var _genre = new THELOAI();
            return View(_genre);
        } 

        //
        // POST: /GenreManager/Create
        [HttpPost]
        public ActionResult Create(THELOAI _genre)
        {
            if (ModelState.IsValid)
            {
                StoreDB.THELOAIs.AddObject(_genre);
                StoreDB.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangTheLoai = StoreDB.TINHTRANGTHELOAIs.OrderBy(tttl => tttl.MaTinhTrangTheLoai).ToList();
            return View(_genre);
        }
        
        //
        // GET: /GenreManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangTheLoai = StoreDB.TINHTRANGTHELOAIs.OrderBy(tttl => tttl.MaTinhTrangTheLoai).ToList();
            var _genre = new THELOAI();
            return View(_genre);
        }

        //
        // POST: /GenreManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _genre = StoreDB.THELOAIs.First(tl => tl.MaTheLoai == id);
            if (TryUpdateModel(_genre))
            {
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(_genre);
            }
        }

        //
        // GET: /GenreManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _genre = StoreDB.THELOAIs.First(tl => tl.MaTheLoai == id);
            return View(_genre);
        }

        //
        // POST: /GenreManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _genre = StoreDB.THELOAIs.First(tl => tl.MaTheLoai == id);

            StoreDB.THELOAIs.DeleteObject(_genre);
            StoreDB.SaveChanges();
            return View("Deleted");
        }
    }
}
