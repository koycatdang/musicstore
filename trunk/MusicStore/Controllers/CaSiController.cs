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

        [ChildActionOnly]
        public ActionResult List()
        {
            var lstCaSi = dbEntity.CASIs.ToList();
            return PartialView(lstCaSi);
        }
    }
}
