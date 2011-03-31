﻿using System;
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

            // Xóa BAIHAT <Cập nhật trạng thái thành Delete)
            if (TryUpdateModel(_baiHat))
            {
                // DELETE COMMENT
                var _comment = StoreDB.COMMENTs.Select(cm => cm.MaBaiHat == id).ToList();
                for (int i = 0; i < _comment.Count(); i++)
                    StoreDB.DeleteObject(_comment[i]);
                

                //DELETE DIEM
                var _diem = StoreDB.DIEMs.Select(d => d.MaBaiHat == id).ToList();
                for (int i = 0; i < _diem.Count(); i++)
                    StoreDB.DeleteObject(_diem[i]);
                
                // DELETE CHITIETALBUM
                var _chiTietAlbum = StoreDB.CHITIETALBUMs.Select(cta => cta.MaBaiHat == id).ToList();
                for (int i = 0; i < _chiTietAlbum.Count(); i++)
                    StoreDB.DeleteObject(_chiTietAlbum[i]);
                
                // DELETE CHITIETPLAYLIST & Cập nhật số lượng bài hát ở PLAYLIST
                var _chiTietPlatlist = from ctpl in StoreDB.CHITIETPLAYLISTs
                                       where ctpl.MaBaiHat.Equals(id)
                                       select ctpl;
                foreach (var item in _chiTietPlatlist)
                {
                    // Cập số lượng bài hát trong Playlist
                    var _playList = StoreDB.PLAYLISTs.First(pl => pl.MaPlaylist == item.MaPlaylist);
                    _playList.SoLuongBaiHat--;
                    TryUpdateModel(_playList);

                    // DELETE CHITIETPLAYLIST
                    StoreDB.DeleteObject(item);
                }
                
               

                StoreDB.SaveChanges();
                return View("Deleted");
            }
            else
                return View("Index");  
        }
    }
}
;