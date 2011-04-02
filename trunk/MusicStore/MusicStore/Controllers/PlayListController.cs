using System;
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
        public ActionResult PlayList_NgheNhieuNhat()
        {
            var dt = dbEntity.PLAYLISTs.ToList();
            return PartialView(dt);
        }

        public ActionResult Detail(int Playlist)
        {
            var lst = (from bh in dbEntity.BAIHATs
                             join ctPlayList in dbEntity.CHITIETPLAYLISTs on bh.MaBaiHat equals ctPlayList.MaBaiHat
                             where ctPlayList.MaPlaylist == Playlist
                             select new { bh.MaBaiHat, bh.TenBaiHat, bh.LinkDownload}).ToList();

            List<BAIHAT> lstBaiHat = new List<BAIHAT>();
            foreach (var item in lst)
            {
                BAIHAT baiHat = new BAIHAT();
                baiHat.LinkDownload = item.LinkDownload;
                baiHat.MaBaiHat = item.MaBaiHat;
                baiHat.TenBaiHat = item.TenBaiHat;
                lstBaiHat.Add(baiHat);
            }
            return PartialView(lstBaiHat);
        }
    }
}
