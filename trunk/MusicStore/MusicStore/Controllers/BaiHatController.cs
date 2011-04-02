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
        // Get: /BaiHat/ListBaiHat/2
        public ActionResult ListBaiHat(int id)
        {
            var lstBaiHat = dbEntity.BAIHATs.Where(bh => bh.MaCaSiTrinhBay == id).ToList();
            return View(lstBaiHat);
        }

        //
        // Get: /BaiHat/ListBaiHat/Detail
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

            var _bhYeuThich = dbEntity.BAIHATs.OrderByDescending(bh => bh.Diem).Take(int.Parse(sl.SoLuongBaiHatCoDiemTrungBinhCaoNhatDuocLietKeToiDa.ToString())).ToList();
  
            return PartialView(_bhYeuThich);
        }
    }
}
