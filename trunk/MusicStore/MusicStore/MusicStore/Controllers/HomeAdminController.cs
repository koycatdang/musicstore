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
            ViewBag.slbh = dbEntity.BAIHATs.Count();
            
            // Tổng số bài hát đang chờ
            ViewBag.slbhc = dbEntity.BAIHATs.Where(bh => bh.MaTinhTrangBaiHat == 2).Count();
            
            // Bài hát mới nhất
            var _temp1 = dbEntity.BAIHATs.OrderByDescending(bh => bh.NgayTiepNhan).First();
            String _newSong = _temp1.TenBaiHat;
            ViewBag.bhm = _newSong;

            // Tổng thể loại
            ViewBag.sltl = dbEntity.THAMSOes.Count();
            
            // Tổng người dùng
            ViewBag.slnd = dbEntity.NGUOIDUNGs.Count();
            
            // Tổng tài khoản bị ban
            ViewBag.ban = dbEntity.BANNICKs.Count();
            
            // Tổng số lượng ca sĩ
            ViewBag.slcs = dbEntity.CASIs.Count();
            
            // Tổng số lượng nhạc sĩ
            ViewBag.slns = dbEntity.NHACSIs.Count();
            
            // Tổng số lượng Alum
            ViewBag.sla = dbEntity.ALBUMs.Count();
            
            // Tổng số lượng playlist
            ViewBag.slpl = dbEntity.PLAYLISTs.Count();               

            return View();
        }

    }
}
