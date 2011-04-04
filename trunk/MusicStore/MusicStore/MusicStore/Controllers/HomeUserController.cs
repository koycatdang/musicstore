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

            ViewBag.ListBaiHatDiemCaoNhat = dbEntity.BAIHATs.Where(bh => bh.MaTinhTrangBaiHat != 3).OrderByDescending(bh => bh.Diem).Take(int.Parse(sl.SoLuongBaiHatCoDiemTrungBinhCaoNhatDuocLietKeToiDa.ToString())).ToList();
            ViewBag.ListBaiHatMoiNhat = dbEntity.BAIHATs.Where(bh => bh.MaTinhTrangBaiHat != 3).OrderByDescending(bh => bh.NgayTiepNhan).Take(int.Parse(sl.SoLuongBaiHatMoiNhatDuocLietKeToiDa.ToString())).ToList();

            var lstTheLoai = dbEntity.THELOAIs.ToList();
            List<BAIHAT> lstBaiHat = new List<BAIHAT>();
            foreach (var tl in lstTheLoai)
            {
                List<BAIHAT> _lstBaiHatTheoTheLoai = new List<BAIHAT>();
                _lstBaiHatTheoTheLoai = dbEntity.BAIHATs.Where(bh => bh.MaTinhTrangBaiHat != 3).Where(bh => bh.MaTheLoai == tl.MaTheLoai).OrderByDescending(bh => bh.SoLuongNghe).Take(int.Parse(sl.SoLuongBaiHatNgheNhieuNhatTheoTheLoaiDuocLietKeToiDa.ToString())).ToList();
                foreach (var bh in _lstBaiHatTheoTheLoai)
                    lstBaiHat.Add(bh);
            }

            ViewBag.ListBaiHatTheoTheLoai = lstBaiHat;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
