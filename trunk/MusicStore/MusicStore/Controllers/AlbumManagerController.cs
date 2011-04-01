using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class AlbumManagerController : Controller
    {

        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /AlbumManager/
        public ActionResult Index()
        {
            var _album = dbEntity.ALBUMs.ToList();
            return View(_album);
        }

        //
        // GET: /AlbumManager/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangAlbum = dbEntity.TINHTRANGALBUMs.OrderBy(tta => tta.MaTinhTrangAlbum).ToList();
            var _album = new ALBUM();
            return View(_album);
        }

        //
        // POST: /AlbumManager/Create
        [HttpPost]
        public ActionResult Create(ALBUM _album)
        {
            if (ModelState.IsValid)
            {
                dbEntity.ALBUMs.AddObject(_album);
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            //Invalid
            ViewBag.TinhTrangAlbum = dbEntity.TINHTRANGALBUMs.OrderBy(tta => tta.MaTinhTrangAlbum).ToList();
            return View(_album);
        }

        //
        // GET: /AlbumManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangAlbum = dbEntity.TINHTRANGALBUMs.OrderBy(tta => tta.MaTinhTrangAlbum).ToList();
            var _album = dbEntity.ALBUMs.First(a => a.MaAlbum == id);
            return View(_album);
        }

        //
        // POST: /AlbumManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _album = dbEntity.ALBUMs.First(a => a.MaAlbum == id);
            if (TryUpdateModel(_album))
            {
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangAlbum = dbEntity.TINHTRANGALBUMs.OrderBy(tta => tta.MaTinhTrangAlbum).ToList();
            return View(_album);
        }

        //
        // GET: /AlbumManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _album = dbEntity.ALBUMs.First(a => a.MaAlbum == id);
            return View(_album);
        }

        //
        // POST: /AlbumManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _album = dbEntity.ALBUMs.First(a => a.MaAlbum == id);
            var _chiTietAlbum = (from cta in dbEntity.CHITIETALBUMs
                                 where cta.MaAlbum == id
                                 select cta).ToList();
            foreach (var cta in _chiTietAlbum)
                dbEntity.CHITIETALBUMs.DeleteObject(cta);
            dbEntity.ALBUMs.DeleteObject(_album);
            dbEntity.SaveChanges();
            return View("Deleted");
        }

        //
        // GET: /AlbumManager/Detail/3
        public ActionResult Details(int id)
        {
            var _chiTietAlbum = dbEntity.CHITIETALBUMs.Where(cta => cta.MaAlbum == id).ToList();

            return View(_chiTietAlbum);
        }

        //
        // GET: /AlbumManager/AddSong/5
        public ActionResult AddSong()
        {
            ViewBag.BaiHat = dbEntity.BAIHATs.OrderBy(bh => bh.MaBaiHat).ToList();
            ViewBag.Album = dbEntity.ALBUMs.OrderBy(a => a.MaAlbum).ToList();

            var _chiTietAlbum = new CHITIETALBUM();
            return View(_chiTietAlbum);
        }

        //
        // GET: /AlbumManager/AddSong/5
        [HttpPost]
        public ActionResult AddSong(CHITIETALBUM _chiTietAlbum)
        {
            if (ModelState.IsValid)
            {
                var _cta = (from cta in dbEntity.CHITIETALBUMs
                            where cta.MaAlbum == _chiTietAlbum.MaAlbum
                            where cta.MaBaiHat == _chiTietAlbum.MaBaiHat
                            select cta).ToList();
                if (_cta.Count() == 0)
                {
                    dbEntity.CHITIETALBUMs.AddObject(_chiTietAlbum);
                    dbEntity.SaveChanges();
                    return RedirectToAction("Detail/", new { id = _chiTietAlbum.MaAlbum});
                }
                else
                    return View("ErrorAddSong");
            }
            //Invalid
            ViewBag.BaiHat = dbEntity.BAIHATs.OrderBy(bh => bh.MaBaiHat).ToList();
            ViewBag.Album = dbEntity.ALBUMs.OrderBy(a => a.MaAlbum).ToList();

            return View(_chiTietAlbum);
        }

        //
        // GET: /AlbumManager/Remove/5
        public ActionResult Remove(int id)
        {
            var _chiTietAlbum = dbEntity.CHITIETALBUMs.First(cta => cta.MaChiTietAlbum == id);
            return View(_chiTietAlbum);
        }

        //
        // POST: /AlbumManager/Remove/5
        [HttpPost]
        public ActionResult Remove(int id, FormCollection collection)
        {
            var _chiTietAlbum = dbEntity.CHITIETALBUMs.First(cta => cta.MaChiTietAlbum == id);
            dbEntity.CHITIETALBUMs.DeleteObject(_chiTietAlbum);
            dbEntity.SaveChanges();

            return View("Removed");
        }
    }
}
