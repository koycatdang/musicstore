using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;
using System.Collections;

namespace MusicStore.Controllers
{
    public class SongManagerController : Controller
    {
        db_MusicStoreEntities StoreDB = new db_MusicStoreEntities();
        //
        // GET: /SongManager/
        public ActionResult Index()
        {
            var _song = StoreDB.BAIHATs.ToList();
            return View(_song);
        }

        //
        // GET: /SongManager/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangBaiHat = StoreDB.TINHTRANGBAIHATs.OrderBy(ttbh => ttbh.TenTinhTrangBaiHat).ToList();
            ViewBag.TheLoai = StoreDB.THELOAIs.OrderBy(tl => tl.TenTheLoai).ToList();
            ViewBag.Album = StoreDB.ALBUMs.OrderBy(a => a.TenAlbum).ToList();
            ViewBag.ChatLuongAmThanh = StoreDB.CHATLUONGAMTHANHs.OrderBy(clat => clat.MaChatLuongAmThanh).ToList();
            ViewBag.CaSi = StoreDB.CASIs.OrderBy(cs => cs.MaCaSi).ToList();
            ViewBag.NhacSi = StoreDB.NHACSIs.OrderBy(ns => ns.MaNhacSi).ToList();

            var _song = new BAIHAT();

            return View(_song);
        }

        //
        // POST: /SongManager/Create
        [HttpPost]
        public ActionResult Create(BAIHAT _song)
        {
            if (ModelState.IsValid)
            {
                _song.NgayTiepNhan = DateTime.Now;
                _song.SoLuongNghe = _song.SoLuotDownload = 0;
                _song.Diem = 0;
                StoreDB.BAIHATs.AddObject(_song);
                StoreDB.SaveChanges();

                return RedirectToAction("Index");
            }
            //Invalid
            ViewBag.TinhTrangBaiHat = StoreDB.TINHTRANGBAIHATs.OrderBy(ttbh => ttbh.TenTinhTrangBaiHat).ToList();
            ViewBag.TheLoai = StoreDB.THELOAIs.OrderBy(tl => tl.TenTheLoai).ToList();
            ViewBag.Album = StoreDB.ALBUMs.OrderBy(a => a.TenAlbum).ToList();
            ViewBag.ChatLuongAmThanh = StoreDB.CHATLUONGAMTHANHs.OrderBy(clat => clat.MaChatLuongAmThanh).ToList();
            ViewBag.CaSi = StoreDB.CASIs.OrderBy(cs => cs.MaCaSi).ToList();
            ViewBag.NhacSi = StoreDB.NHACSIs.OrderBy(ns => ns.MaNhacSi).ToList();

            return View(_song);
        }

        //
        // GET: /SongManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangBaiHat = StoreDB.TINHTRANGBAIHATs.OrderBy(ttbh => ttbh.TenTinhTrangBaiHat).ToList();
            ViewBag.TheLoai = StoreDB.THELOAIs.OrderBy(tl => tl.TenTheLoai).ToList();
            ViewBag.Album = StoreDB.ALBUMs.OrderBy(a => a.TenAlbum).ToList();
            ViewBag.ChatLuongAmThanh = StoreDB.CHATLUONGAMTHANHs.OrderBy(clat => clat.MaChatLuongAmThanh).ToList();
            ViewBag.CaSi = StoreDB.CASIs.OrderBy(cs => cs.MaCaSi).ToList();
            ViewBag.NhacSi = StoreDB.NHACSIs.OrderBy(ns => ns.MaNhacSi).ToList();

            var _song = StoreDB.BAIHATs.Single(bh => bh.MaBaiHat == id);

            return View(_song);
        }

        //
        // POST: /SongManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _song = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);

            if (TryUpdateModel(_song))
            {
                if (_song.MaTinhTrangBaiHat == 3)
                    upDeleted(id);
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(_song);
        }

        //
        // GET: /SongManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _song = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);
            return View(_song);
        }

        //
        // POST: /SongManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            var _song = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);
            _song.MaTinhTrangBaiHat = 3;

            // Xóa BAIHAT <Cập nhật trạng thái thành Delete)
            if (TryUpdateModel(_song))
            {
                upDeleted(id);
                StoreDB.SaveChanges();
                return View("Deleted");
            }
            else
                return View("Index");
        }

        //
        // GET: /SongManager/Waiting
        public ActionResult Waiting()
        {
            var _song = (from bh in StoreDB.BAIHATs
                         where bh.MaTinhTrangBaiHat == 2
                         select bh).ToList();
            List<BAIHAT> lstbh = new List<BAIHAT>();
            foreach (var bh in _song)
                lstbh.Add(bh);

            return View(lstbh);
        }

        // GET: /SongManager/Accept
        public ActionResult Accept(int id)
        {
            var _song = StoreDB.BAIHATs.First(bh => bh.MaTinhTrangBaiHat == id);
            return View(_song);
        }

        // POST: /SongManager/Accept
        [HttpPost]
        public ActionResult Accept(int id, FormCollection collection)
        {
            var _song = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);
            _song.MaTinhTrangBaiHat = 1;

            if (TryUpdateModel(_song))
            {
                StoreDB.SaveChanges();
                return RedirectToAction("Waiting");
            }
            else
                return View(_song);
        }

        // Trạng thái DELETED của BAIHAT
        private void upDeleted(int id)
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
        }
    }
}
;