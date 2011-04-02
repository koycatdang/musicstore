using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        Models.db_MusicStoreEntities dbEntity = new Models.db_MusicStoreEntities();
        //
        // GET: /Store/

        public ActionResult Index()
        {
            var genres = dbEntity.THELOAIs.ToList();
            return View(genres);
        }

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id)
        {
            var baihat = dbEntity.BAIHATs.Single(bh => bh.MaBaiHat == id);
            return View(baihat);
        }

        
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = dbEntity.THELOAIs.ToList();
            return PartialView(genres);
        }


        //
        // GET: /Store/Browse?genre=?Disco
        public ActionResult Browse(int MaTheLoai)
        {
            var genreModel = dbEntity.THELOAIs.Include("BAIHATs").Single(g => g.MaTheLoai == MaTheLoai);
            return View(genreModel);

        }
    }
}
