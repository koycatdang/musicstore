using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class PageManagerController : Controller
    {
        db_MusicStoreEntities StoreDB = new db_MusicStoreEntities();
        //
        // GET: /PageManager/
        public ActionResult Index()
        {
            var _page = StoreDB.THAMSOes.ToList();
            return View(_page);
        }

        //
        // GET: /PageManager/Edit/1
        public ActionResult Edit(int id)
        {
            var _page = StoreDB.THAMSOes.First(ts => ts.id == id);
            return View(_page);
        }

        //
        // POST: /PageManager/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _page = StoreDB.THAMSOes.First(ts => ts.id == id);

            if (TryUpdateModel(_page))
            {
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(_page);
        }
    }
}
