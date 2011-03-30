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
        Models.db_MusicStoreEntities dbEntity = new Models.db_MusicStoreEntities();

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
    }
}
