using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class CaSiController : Controller
    {
        Models.db_MusicStoreEntities dbEntity = new Models.db_MusicStoreEntities();

        //
        // GET: /CaSi/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET:
        [ChildActionOnly]
        public ActionResult ListCaSiVietNam()
        {
            var sl = dbEntity.THAMSOes.First();
            List<CASI> _lstCaSiVietNam = new List<CASI>();

            var temp = dbEntity.CASIs.Where(cs => cs.MaKhuVuc == 1).OrderByDescending(cs => cs.BAIHATs.Count()).Take(int.Parse(sl.SoLuongCaSiLietKeTrenMenu.ToString())).ToList();

            foreach (var item in temp)
                _lstCaSiVietNam.Add(item);
            
            return PartialView(_lstCaSiVietNam);
        }

        //
        // GET:
        [ChildActionOnly]
        public ActionResult ListCaSiChauA()
        {
            var lstCaSiChauA = dbEntity.CASIs.Where(g => g.MaKhuVuc == 2).ToList();
            return PartialView(lstCaSiChauA);
        }

        //
        // GET:
        [ChildActionOnly]
        public ActionResult ListCaSiAuMi()
        {
            var lstCaSiAuMy = dbEntity.CASIs.Where(g => g.MaKhuVuc == 3).ToList();
            return PartialView(lstCaSiAuMy);
        }
    }
}
