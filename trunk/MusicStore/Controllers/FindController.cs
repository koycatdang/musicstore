using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class FindController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /Find/
        public ActionResult Find()
        {
            ViewBag.TheLoai = dbEntity.THELOAIs.ToList();
            ViewBag.ChatLuong = dbEntity.CHATLUONGAMTHANHs.ToList();
            return View();
        }
        //
        // GET: /Find/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Find(String baihat, String casi, String theloai, String nguoidang)
        {
            var sl = dbEntity.THAMSOes.First();
            return View("FindResult");
        }
    }
}
