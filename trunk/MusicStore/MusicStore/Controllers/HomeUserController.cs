using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class HomeUserController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        
        public ActionResult Index()
        {
            var sl = dbEntity.THAMSOes.First();

            ViewBag.ListBaiHatDiemCaoNhat = dbEntity.BAIHATs.OrderByDescending(bh => bh.Diem).Take(int.Parse(sl.SoLuongBaiHatCoDiemTrungBinhCaoNhatDuocLietKeToiDa.ToString())).ToList();
            ViewBag.ListBaiHatMoiNhat = dbEntity.BAIHATs.OrderByDescending(bh => bh.NgayTiepNhan).Take(int.Parse(sl.SoLuongBaiHatMoiNhatDuocLietKeToiDa.ToString())).ToList();
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
