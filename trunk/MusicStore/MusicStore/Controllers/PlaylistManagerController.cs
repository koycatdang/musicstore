using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class PlaylistManagerController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /Playlist/
        public ActionResult Index()
        {
            var _playList = dbEntity.PLAYLISTs.ToList();
            return View(_playList);
        }

        //
        // GET: /Playlist/Details/5
        public ActionResult Details(int id)
        {
            var _chiTietPlaylist = dbEntity.CHITIETPLAYLISTs.Where(ctpl => ctpl.MaPlaylist == id).ToList();
            return View(_chiTietPlaylist);
        }

        
        //
        // GET: /Playlist/Edit/5
        public ActionResult Edit(int id)
        {
            var _playlist = dbEntity.PLAYLISTs.First(pl => pl.MaPlaylist == id);
            return View(_playlist);
        }

        //
        // POST: /Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _playlist = dbEntity.PLAYLISTs.First(pl => pl.MaPlaylist == id);
            if (TryUpdateModel(_playlist))
            {
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_playlist);
        }

        //
        // GET: /Playlist/Delete/5
        public ActionResult Delete(int id)
        {
            var _playlist = dbEntity.PLAYLISTs.First(pl => pl.MaPlaylist == id);
            return View(_playlist);
        }

        //
        // POST: /Playlist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _playlist = dbEntity.PLAYLISTs.First(pl => pl.MaPlaylist == id);
            var _chiTietPlayList = (from ctpl in dbEntity.CHITIETPLAYLISTs
                                    where ctpl.MaPlaylist == id
                                    select ctpl).ToList();
            foreach (var item in _chiTietPlayList)
                dbEntity.CHITIETPLAYLISTs.DeleteObject(item);
            dbEntity.PLAYLISTs.DeleteObject(_playlist);
            dbEntity.SaveChanges();
            return View("Deleted");
        }

        //
        // GET: /Playlist/Remove/5
        public ActionResult Remove(int id)
        {
            var _chiTietPlaylist = dbEntity.CHITIETPLAYLISTs.First(ctpl => ctpl.MaChiTietPlayList == id);
            return View(_chiTietPlaylist);
        }

          //
        // POST: /Playlist/Delete/5
        [HttpPost]
        public ActionResult Remove(int id, FormCollection collection)
        {
            var _chiTietPlaylist = dbEntity.CHITIETPLAYLISTs.First(ctpl => ctpl.MaChiTietPlayList == id);
            var _playList = dbEntity.PLAYLISTs.First(pl => pl.MaPlaylist == _chiTietPlaylist.MaPlaylist);
            _playList.SoLuongBaiHat--;
            TryUpdateModel(_playList);

            dbEntity.CHITIETPLAYLISTs.DeleteObject(_chiTietPlaylist);
            dbEntity.SaveChanges();
            return View("Removed");
        }
    }
}
