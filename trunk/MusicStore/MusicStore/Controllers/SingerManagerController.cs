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
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /SingerManager/
        public ActionResult Index()
        {
            var _singer = dbEntity.CASIs.ToList();
            return View(_singer);
        }

        //
        // GET: /SingerManager/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangCaSi = dbEntity.TINHTRANGCASIs.ToList();
            ViewBag.KhuVuc = dbEntity.KHUVUCs.ToList();

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
                dbEntity.CASIs.AddObject(_singer);
                dbEntity.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangCaSi = dbEntity.TINHTRANGCASIs.ToList();
            ViewBag.KhuVuc = dbEntity.KHUVUCs.ToList();
            return View(_singer);
        }

        //
        // GET: /SingerManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangCaSi = dbEntity.TINHTRANGCASIs.ToList();
            ViewBag.KhuVuc = dbEntity.KHUVUCs.ToList();

            var _singer = dbEntity.CASIs.First(cs => cs.MaCaSi == id);
            return View(_singer);
        }

        //
        // POST: /SingerManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _singer = dbEntity.CASIs.First(cs => cs.MaCaSi == id);

            if (TryUpdateModel(_singer))
            {
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.TinhTrangCaSi = dbEntity.TINHTRANGCASIs.ToList();
            ViewBag.KhuVuc = dbEntity.KHUVUCs.ToList();
            return View(_singer);
        }

        //
        // GET: /SingerManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _singer = dbEntity.CASIs.First(cs => cs.MaCaSi == id);
            return View(_singer);
        }

        //
        // POST: /SingerManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _singer = dbEntity.CASIs.First(cs => cs.MaCaSi == id);
            var _song = dbEntity.BAIHATs.Where(bh => bh.MaCaSiTrinhBay == _singer.MaCaSi).ToList();
            foreach (var bh in _song)
                deleteAll(bh.MaBaiHat);
            dbEntity.CASIs.DeleteObject(_singer);
            dbEntity.SaveChanges();
            return View("Deleted");
        }

        // Xóa toàn bộ thông tin liên quan của ca sĩ sĩ tương ứng
        private void deleteAll(int id)
        {
            // DELETE COMMENT
            var _comment = dbEntity.COMMENTs.Where(cm => cm.MaBaiHat == id).ToList();
            foreach (var cm in _comment)
                dbEntity.COMMENTs.DeleteObject(cm);


            //DELETE DIEM
            var _diem = dbEntity.DIEMs.Where(d => d.MaBaiHat == id).ToList();
            foreach (var d in _diem)
                dbEntity.DIEMs.DeleteObject(d);

            // DELETE CHITIETALBUM
            var _chiTietAlbum = dbEntity.CHITIETALBUMs.Where(cta => cta.MaBaiHat == id).ToList();
            foreach (var cta in _chiTietAlbum)
                dbEntity.DeleteObject(cta);

            // DELETE CHITIETPLAYLIST & Cập nhật số lượng bài hát ở PLAYLIST
            var _chiTietPlaylist = dbEntity.CHITIETPLAYLISTs.Where(ctpl => ctpl.MaBaiHat == id).ToList();
            foreach (var ctpl in _chiTietPlaylist)
            {
                // Cập số lượng bài hát trong Playlist
                var _playList = dbEntity.PLAYLISTs.First(pl => pl.MaPlaylist == ctpl.MaPlaylist);
                _playList.SoLuongBaiHat--;
                TryUpdateModel(_playList);

                // DELETE CHITIETPLAYLIST
                dbEntity.DeleteObject(ctpl);
            }

            //DELETE BAIHAT
            var _baiHat = dbEntity.BAIHATs.First(bh => bh.MaBaiHat == id);
            dbEntity.BAIHATs.DeleteObject(_baiHat);
        }
    }
}
