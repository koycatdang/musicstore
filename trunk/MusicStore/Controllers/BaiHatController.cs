using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        // GET: /BaiHat/BrowCaSi/...
        public ActionResult BrowCaSi(int id)
        {
            var lstBaiHat = dbEntity.BAIHATs.Where(bh => bh.CASI.MaCaSi == id).ToList();

            return View(lstBaiHat);
        }
    }
}
