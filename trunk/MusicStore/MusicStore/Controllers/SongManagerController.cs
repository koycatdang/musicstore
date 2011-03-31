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
            //var _song = StoreDB.BAIHATs.ToList();
            var list = (from bh in StoreDB.BAIHATs
                       join cs in StoreDB.CASIs
                       on bh.MaCaSiTrinhBay equals cs.MaCaSi
                       select new
                       {
                           TenBaiHat = bh.TenBaiHat,
                           CaSiTrinhBay = cs.TenCaSi,
                           SoLuotDownLoad = bh.SoLuotDownload
                       }).ToList();
            IList<BAIHAT> lstbh =  new List<BAIHAT>();
            foreach (var item in list)
            {
                BAIHAT ibh = new BAIHAT();
                ibh.TenBaiHat = item.TenBaiHat;
                ibh.SoLuotDownload = item.SoLuotDownLoad;
            }
            IList<CASI> lstcs = new List<CASI>();
            foreach (var item in list)
            {
                CASI ics = new CASI();
                ics.TenCaSi = item.CaSiTrinhBay;
            }

            return View();
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
        public ActionResult Create(BAIHAT _baiHat)
        {
            if (ModelState.IsValid)
            {
                _baiHat.NgayTiepNhan = DateTime.Now;
                StoreDB.BAIHATs.AddObject(_baiHat);
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

            return View(_baiHat);
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

            var _baiHat = StoreDB.BAIHATs.Single(bh => bh.MaBaiHat == id);

            return View(_baiHat);
        }

        //
        // POST: /SongManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _baiHat = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);

            if (TryUpdateModel(_baiHat))
            {
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(_baiHat);
        }

        //
        // GET: /SongManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _baiHat = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);
            return View(_baiHat);
        }

        //
        // POST: /SongManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _baiHat = StoreDB.BAIHATs.First(bh => bh.MaBaiHat == id);
            _baiHat.MaTinhTrangBaiHat = 3;

            if (TryUpdateModel(_baiHat))
            {
                StoreDB.SaveChanges();
                return View("Deleted");
            }
            else
                return View("Index");
        }
    }
}
;