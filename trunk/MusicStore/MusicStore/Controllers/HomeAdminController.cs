using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class HomeAdminController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /HomeAdmin/
        public ActionResult Index()
        {
            // Tổng số bài hát
            ViewBag.slbh = (from bh in dbEntity.BAIHATs select bh).Count();
            
            // Tổng số bài hát đang chờ
            ViewBag.slbhc = (from bh in dbEntity.BAIHATs where bh.MaTinhTrangBaiHat == 2 select bh).Count();
            
            // Bài hát mới nhất
            var _temp1 = (from bh in dbEntity.BAIHATs orderby bh.NgayTiepNhan descending select new { bh.TenBaiHat }).Take(1).ToList();
            String _newSong = "";
            foreach (var item in _temp1)
                 _newSong = item.TenBaiHat;
            ViewBag.bhm = _newSong;

            // Tổng thể loại
            ViewBag.sltl = (from tl in dbEntity.THELOAIs select tl).Count();
            
            // Tổng người dùng
            ViewBag.slnd = (from nd in dbEntity.NGUOIDUNGs select nd).Count();
            
            // Tổng tài khoản bị ban
            ViewBag.ban = (from b in dbEntity.BANNICKs select b).Count();
            
            // Tổng số lượng ca sĩ
            ViewBag.slcs = (from cs in dbEntity.CASIs select cs).Count();
            
            // Tổng số lượng nhạc sĩ
            ViewBag.slns = (from ns in dbEntity.NHACSIs select ns).Count();
            
            // Tổng số lượng Alum
            ViewBag.sla = (from a in dbEntity.ALBUMs select a).Count();
            
            // Tổng số lượng playlist
            ViewBag.slpl = (from pl in dbEntity.PLAYLISTs select pl).Count();               

            return View();
        }

    }
}
