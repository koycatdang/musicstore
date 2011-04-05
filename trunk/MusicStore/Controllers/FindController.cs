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
        public ActionResult Index()
        {
            ViewBag.TheLoai = dbEntity.THELOAIs.ToList();
            ViewBag.ChatLuong = dbEntity.THELOAIs.ToList();
            return View();
        }

    }
}
