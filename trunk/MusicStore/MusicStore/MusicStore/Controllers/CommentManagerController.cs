using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class CommentManagerController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /CommentManager/
        public ActionResult Index()
        {
            var _comment = dbEntity.COMMENTs.ToList();
            return View(_comment);
        }
        
        //
        // GET: /CommentManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            var _comment = dbEntity.COMMENTs.First(cm => cm.MaComment == id);
            return View(_comment);
        }

        //
        // POST: /CommentManager/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _comment = dbEntity.COMMENTs.First(cm => cm.MaComment == id);
            if (TryUpdateModel(_comment))
            {
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_comment);
        }

        //
        // GET: /CommentManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            var _comment = dbEntity.COMMENTs.First(cm => cm.MaComment == id);
            return View(_comment);
        }

        //
        // POST: /CommentManager/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _comment = dbEntity.COMMENTs.First(cm => cm.MaComment == id);
            dbEntity.COMMENTs.DeleteObject(_comment);
            dbEntity.SaveChanges();
            return View("Deleted");
        }
    }
}
