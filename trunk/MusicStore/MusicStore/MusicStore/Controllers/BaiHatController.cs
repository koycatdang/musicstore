using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class BaiHatController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();

        //
        // GET: /BaiHat/
        public ActionResult Index()
        {
            return View();
        }

        //
        // Get: /BaiHat/PlaylistBaiHat/2
        public ActionResult PlaylistBaiHat(int id)
        {
            var lstBaiHat = dbEntity.BAIHATs.Where(bh => bh.MaCaSiTrinhBay == id).Where(bh => bh.MaTinhTrangBaiHat != 3).ToList();
            return View(lstBaiHat);
        }

        //
        // Get: /BaiHat/Detail/
        public ActionResult Detail(int id)
        {
            var baihat = dbEntity.BAIHATs.Single(bh => bh.MaBaiHat == id);
            return View(baihat);
        }

        [ChildActionOnly]
        public ActionResult BaiHatYeuThich()
        {
            // Lấy số lượng bài hát yêu thích được kiệt kê tối đa
            var sl = dbEntity.THAMSOes.First();

            var _bhYeuThich = dbEntity.BAIHATs.Where(bh => bh.MaTinhTrangBaiHat != 3).OrderByDescending(bh => bh.Diem).Take(int.Parse(sl.SoLuongBaiHatCoDiemTrungBinhCaoNhatDuocLietKeToiDa.ToString())).ToList();
  
            return PartialView(_bhYeuThich);
        }

        [ChildActionOnly]
        public ActionResult BaiHatMoiNhat()
        {
            var sl = dbEntity.THAMSOes.First();
            var _bhMoiNhat = dbEntity.BAIHATs.Where(bh => bh.MaTinhTrangBaiHat != 3).OrderByDescending(bh => bh.NgayTiepNhan).Take(int.Parse(sl.SoLuongBaiHatMoiNhatDuocLietKeToiDa.ToString())).ToList();
            return PartialView(_bhMoiNhat);
        }

        // Bài hát lấy ra từ menu TheLoai
        public ActionResult BaiHatTheoTheLoai(int id)
        {
            var sl = dbEntity.THAMSOes.First();

            var _bhTheoTheLoai = dbEntity.BAIHATs.Where(bh => bh.MaTheLoai == id).Where(bh => bh.MaTinhTrangBaiHat != 3).OrderByDescending(bh => bh.NgayTiepNhan).Take(int.Parse(sl.SoLuongBaiHatLietKeMoiTrangToiDa.ToString())).ToList();

            return View(_bhTheoTheLoai);
        }

        // Bài hát lấy ra từ menu CaSi
        public ActionResult BaiHatTheoCaSi(int id)
        {
            var sl = dbEntity.THAMSOes.First();

            var _bhTheoCaSi = dbEntity.BAIHATs.Where(bh => bh.MaTinhTrangBaiHat != 3).Where(bh => bh.MaCaSiTrinhBay == id).OrderByDescending(bh => bh.NgayTiepNhan).Take(int.Parse(sl.SoLuongBaiHatLietKeMoiTrangToiDa.ToString())).ToList();

            return View(_bhTheoCaSi);
        }

        // Bài hát cùng ca sĩ trong trang Detail của Bài hát
        [ChildActionOnly]
        public ActionResult BaiHatCungCaSi(int id)
        {
            var sl = dbEntity.THAMSOes.First();
            return PartialView();
        }
    }
}
