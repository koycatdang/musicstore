using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;
using System.IO;

namespace MusicStore.Controllers
{
    public class PlayListController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /Playlist/
        public ActionResult Index()
        {
            int id = int.Parse(Session["MaNguoiDung"].ToString());
            var _pl = dbEntity.PLAYLISTs.Where(ctpl => ctpl.MaNguoiDung == id).ToList();
            if (_pl.Count == 0)
                return View("ErrorPlayList");
            return View();
        }

        public ActionResult Create()
        {
            PLAYLIST pl = new PLAYLIST();
            return View(pl);
        }

        [HttpPost]
        public ActionResult Create(PLAYLIST pl)
        {
            int id = int.Parse(Session["MaNguoiDung"].ToString());
            pl.MaNguoiDung = id;
            pl.NgayTao = DateTime.Now;
            pl.SoLuongBaiHat = pl.SoLuotDownload = pl.SoLuotNghe = 0;
            dbEntity.PLAYLISTs.AddObject(pl);
            dbEntity.SaveChanges();
            return RedirectToAction("Index");
        }
        //
        // GET: /GenreManager/ThemBaiHat
        public ActionResult ThemBaiHat()
        {
            ViewBag.TinhTrangBaiHat = dbEntity.TINHTRANGBAIHATs.ToList();
            ViewBag.TheLoai = dbEntity.THELOAIs.ToList();
            ViewBag.ChatLuongAmThanh = dbEntity.CHATLUONGAMTHANHs.ToList();
            ViewBag.CaSi = dbEntity.CASIs.ToList();
            ViewBag.NhacSi = dbEntity.NHACSIs.ToList();
            var _ctpl = new BAIHAT();
            return View(_ctpl);
        }

        //
        // POST: /GenreManager/ThemBaihat
        [HttpPost]
        public ActionResult ThemBaiHat(BAIHAT _baiHat, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.FileName != "")
                {
                    _baiHat.LinkDownload = Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("~/MusicFiles/") + _baiHat.LinkDownload);
                }
                // Thêm bài hát
                _baiHat.SoLuongNghe = _baiHat.SoLuotDownload = 0;
                _baiHat.NgayTiepNhan = DateTime.Now;
                _baiHat.MaTinhTrangBaiHat = 2;
                dbEntity.BAIHATs.AddObject(_baiHat);

                // Thêm chi tiết playlist
                CHITIETPLAYLIST _pl = new CHITIETPLAYLIST();
                _pl.MaBaiHat = _baiHat.MaBaiHat;
                //_pl.MaPlaylist = 

                dbEntity.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.TinhTrangBaiHat = dbEntity.TINHTRANGBAIHATs.ToList();
            ViewBag.TheLoai = dbEntity.THELOAIs.ToList();
            ViewBag.ChatLuongAmThanh = dbEntity.CHATLUONGAMTHANHs.ToList();
            ViewBag.CaSi = dbEntity.CASIs.ToList();
            ViewBag.NhacSi = dbEntity.NHACSIs.ToList();
            return View(_baiHat);
        }

        [ChildActionOnly]
        public ActionResult PlayListNgheNhieuNhat()
        {
            var sl = dbEntity.THAMSOes.First();
            var _playList = dbEntity.PLAYLISTs.OrderByDescending(pl => pl.SoLuotNghe).Take(int.Parse(sl.SoLuongPlaylistXemNhieuNhatTrenMenu.ToString())).ToList();
            return PartialView(_playList);
        }

        public ActionResult Detail(int id)
        {
            var _playlist = dbEntity.CHITIETPLAYLISTs.Where(pl => pl.MaPlaylist == id).ToList(); 

            return PartialView(_playlist);
        }
    }
}
