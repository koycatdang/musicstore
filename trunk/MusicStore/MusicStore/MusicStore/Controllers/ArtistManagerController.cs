﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class ArtistManagerController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();

        //
        // GET: /Artist/
        public ActionResult Index()
        {
            var _artist = dbEntity.NHACSIs.ToList();
            return View(_artist);
        }


        //
        // GET: /Artist/Create
        public ActionResult Create()
        {
            ViewBag.TinhTrangNhacSi = dbEntity.TINHTRANGNHACSIs.ToList();

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
                dbEntity.NHACSIs.AddObject(_artist);
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangNhacSi = dbEntity.TINHTRANGNHACSIs.ToList();
            return View(_artist);
        }

        //
        // GET: /Artist/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrangNhacSi = dbEntity.TINHTRANGNHACSIs.ToList();
            var _artist = dbEntity.NHACSIs.First(ns => ns.MaNhacSi == id);
            return View(_artist);
        }

        //
        // POST: /Artist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _artist = dbEntity.NHACSIs.First(ns => ns.MaNhacSi == id);
            if (TryUpdateModel(_artist))
            {
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangNhacSi = dbEntity.TINHTRANGNHACSIs.ToList();
            return View(_artist);
        }

        //
        // GET: /Artist/Delete/5
        public ActionResult Delete(int id)
        {
            var _artist = dbEntity.NHACSIs.First(ns => ns.MaNhacSi == id);
            return View(_artist);
        }

        //
        // POST: /Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _artist = dbEntity.NHACSIs.First(ns => ns.MaNhacSi == id);
            var _song = dbEntity.BAIHATs.Where(bh => bh.MaNhacSi == _artist.MaNhacSi).ToList();
            foreach (var bh in _song)
                deleteAll(bh.MaBaiHat);
            
            dbEntity.NHACSIs.DeleteObject(_artist);
            dbEntity.SaveChanges();
            return View("Deleted");
        }

        // Xóa toàn bộ thông tin liên quan của nhạc sĩ tương ứng
        private void  deleteAll(int id)
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