using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class TheLoaiController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /TheLoai/
        public ActionResult Index()
        {
            return View();
        }

        //
        [ChildActionOnly]
        public ActionResult ListTheLoai()
        {
            var sl = dbEntity.THAMSOes.First();

            var temp = dbEntity.THELOAIs.OrderByDescending(cs => cs.BAIHATs.Count()).Take(int.Parse(sl.SoLuongTheLoaiDuocLietKetTrenMenu.ToString())).ToList();
            List<THELOAI> _lstTheLoai = new List<THELOAI>();
            foreach (var tl in temp)
                _lstTheLoai.Add(tl);
            return PartialView(_lstTheLoai);
        }
    }
}
