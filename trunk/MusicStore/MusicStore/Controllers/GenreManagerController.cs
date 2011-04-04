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
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /GenreManager/
        public ActionResult Index()
        {
            var _genre = dbEntity.THELOAIs.ToList();
            return View(_genre);
        }

        //
        // GET: /GenreManager/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangTheLoai = dbEntity.TINHTRANGTHELOAIs.ToList();

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
                dbEntity.THELOAIs.AddObject(_genre);
                dbEntity.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangTheLoai = dbEntity.TINHTRANGTHELOAIs.ToList();
            return View(_genre);
        }
        
        //
        // GET: /GenreManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangTheLoai = dbEntity.TINHTRANGTHELOAIs.ToList();
            var _genre = dbEntity.THELOAIs.Single(tl => tl.MaTheLoai == id);
            return View(_genre);
        }

        //
        // POST: /GenreManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _genre = dbEntity.THELOAIs.First(tl => tl.MaTheLoai == id);
            if (TryUpdateModel(_genre))
            {
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            ViewBag.TinhTrangTheLoai = dbEntity.TINHTRANGTHELOAIs.ToList();
            return View(_genre);
        }

        //
        // GET: /GenreManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _genre = dbEntity.THELOAIs.First(tl => tl.MaTheLoai == id);
            return View(_genre);
        }

        //
        // POST: /GenreManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _genre = dbEntity.THELOAIs.First(tl => tl.MaTheLoai == id);
            _genre.MaTinhTrangTheLoai = 3;
            if (TryUpdateModel(_genre))
            {
                dbEntity.SaveChanges();
                return RedirectToAction("Deleted");
            }
            else
            {
                return View("Index");
            }
        }
    }
}
