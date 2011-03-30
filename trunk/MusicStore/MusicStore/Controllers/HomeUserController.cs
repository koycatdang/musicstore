using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    public class HomeUserController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Nhạc người lớn";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
