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
            var _song = (from bh in StoreDB.BAIHATs
                         where bh.MaCaSiTrinhBay == _singer.MaCaSi
                         select bh).ToList();
            foreach (var bh in _song)
                deleteAll(bh.MaBaiHat);
            StoreDB.CASIs.DeleteObject(_singer);
            StoreDB.SaveChanges();
            return View("Deleted");
        }

        // Xóa toàn bộ thông tin liên quan của nhạc sĩ tương ứng
        private void deleteAll(int id)
        {
            // DELETE COMMENT
            var _comment = (from cm in StoreDB.COMMENTs
                            where cm.MaBaiHat == id
                            select cm).ToList();
            foreach (var cm in _comment)
                StoreDB.COMMENTs.DeleteObject(cm);


            //DELETE DIEM
            var _diem = (from d in StoreDB.DIEMs
                         where d.MaBaiHat == id
                         select d).ToList();
            foreach (var d in _diem)
                StoreDB.DIEMs.DeleteObject(d);

            // DELETE CHITIETALBUM
            var _chiTietAlbum = (from cta in StoreDB.CHITIETALBUMs
                                 where cta.MaBaiHat == id
                                 select cta).ToList();
            foreach (var cta in _chiTietAlbum)
                StoreDB.DeleteObject(cta);

            // DELETE CHITIETPLAYLIST & Cập nhật số lượng bài hát ở PLAYLIST
            var _chiTietPlaylist = (from ctpl in StoreDB.CHITIETPLAYLISTs
                                    where ctpl.MaBaiHat == id
                                    select ctpl).ToList();
            foreach (var item in _chiTietPlaylist)
            {
                // Cập số lượng bài hát trong Playlist
                var _playList = StoreDB.PLAYLISTs.First(pl => pl.MaPlaylist == item.MaPlaylist);
                _playList.SoLuongBaiHat--;
                TryUpdateModel(_playList);

                // DELETE CHITIETPLAYLIST
                StoreDB.DeleteObject(item);
            }

            //DELETE BAIHAT
            var _baiHat = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);
            StoreDB.BAIHATs.DeleteObject(_baiHat);
        }
    }
}
