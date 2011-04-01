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
        // Get: /BaiHat/BrowCaSi/2
        public ActionResult BrowCaSi(int id)
        {

            var lstBaiHat = dbEntity.BAIHATs.Where(bh => bh.CASI.MaCaSi == id).ToList();
            return View(lstBaiHat);
        }

        //
        // Get: /BaiHat/BrowCaSi/Detail
        public ActionResult Detail(int id)
        {
            var baihat = dbEntity.BAIHATs.Single(bh => bh.MaBaiHat == id);
            return View(baihat);
        }

        [ChildActionOnly]
        public ActionResult BaiHatYeuThich()
        {
            // Lấy số lượng bài hát yêu thích được kiệt kê tối đa
            var _soLuongYeuThichDuocLietKe = from ts in dbEntity.THAMSOes
                                             select new { soLuong = ts.SoLuongBaiHatCoDiemTrungBinhCaoNhatDuocLietKeToiDa };

            List<BAIHAT> lstBaiHat = new List<BAIHAT>();
            foreach (var item in _soLuongYeuThichDuocLietKe)
            {
                var _bhYeuThich = (from bh in dbEntity.BAIHATs
                                   orderby bh.Diem
                                   select new { bh.MaBaiHat, bh.TenBaiHat }).Take(int.Parse(item.soLuong.ToString())).ToList();

                foreach (var i in _bhYeuThich)
                {
                    BAIHAT _baiHat = new Models.BAIHAT();
                    _baiHat.MaBaiHat = i.MaBaiHat;
                    _baiHat.TenBaiHat = i.TenBaiHat;
                    lstBaiHat.Add(_baiHat);
                }
            }
            return PartialView(lstBaiHat);
        }
    }
}
