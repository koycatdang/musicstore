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
        db_MusicStoreEntities StoreDB = new db_MusicStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult About()
        {
            return View();
        }
    }
}
