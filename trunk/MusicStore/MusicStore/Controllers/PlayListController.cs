﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace DoAn.Controllers
{
    public class PlayListController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /PlayList/

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult PlayListNgheNhieuNhat()
        {
            var sl = dbEntity.THAMSOes.First();
            var _playList = dbEntity.PLAYLISTs.OrderByDescending(pl => pl.SoLuotNghe).Take(int.Parse(sl.SoLuongPlaylistXemNhieuNhatTrenMenu.ToString())).ToList();
            return PartialView(_playList);
        }

        public ActionResult Detail(int id)
        {
            var _playlist = dbEntity.CHITIETPLAYLISTs.Where(pl => pl.MaPlaylist == id).ToList(); 

            return PartialView(_playlist);
        }
    }
}
