using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class UserManagerController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        //
        // GET: /NguoiDungManager/
        public ActionResult Index()
        {
            var _nguoiDung = dbEntity.NGUOIDUNGs.ToList();
            return View(_nguoiDung);
        }

        //
        // GET: /NguoiDungManager/Create
        public ActionResult Create()
        {
            ViewBag.LoaiNguoiDung = dbEntity.LOAINGUOIDUNGs.ToList();
            ViewBag.TinhTrangNguoiDung = dbEntity.TINHTRANGNGUOIDUNGs.ToList();

            var _nguoiDung = new NGUOIDUNG();

            return View(_nguoiDung);
        }

        //
        // POST: /NguoiDungManager/Create
        [HttpPost]
        public ActionResult Create(NGUOIDUNG _nguoiDung)
        {
            var _check = dbEntity.NGUOIDUNGs.Where(nd => nd.UserName == _nguoiDung.UserName).ToList();
            if (_check.Count() != 0)
                return View("ErrorUserName");
            if (ModelState.IsValid)
            {
                //Save NGUOIDUNG
                dbEntity.NGUOIDUNGs.AddObject(_nguoiDung);
                if (_nguoiDung.MaTinhTrangNguoiDung == 2)
                {
                    BANNICK _banNick = new BANNICK();
                    _banNick.MaNguoiDung = _nguoiDung.MaNguoiDung;

                    // Lấy số lượng ngày bị bannick
                    var _ts = dbEntity.THAMSOes.First(); 
                       
                    DateTime dtHetHan = (DateTime.Now).AddDays(int.Parse(_ts.QuyDinhSoNgayBanNickToiDa.ToString()));
                    _banNick.NgayHetHan = dtHetHan;

                    // Add BANNICK
                    dbEntity.BANNICKs.AddObject(_banNick);
                }
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }

            // Invalid – redisplay with errors
            ViewBag.LoaiNguoiDung = dbEntity.LOAINGUOIDUNGs.ToList();
            ViewBag.TinhTrangNguoiDung = dbEntity.TINHTRANGNGUOIDUNGs.ToList();

            return View(_nguoiDung);
        }

        //
        // GET: /NguoiDungManager/Edit/
        public ActionResult Edit(int id)
        {
            ViewBag.LoaiNguoiDung = dbEntity.LOAINGUOIDUNGs.ToList();
            ViewBag.TinhTrangNguoiDung = dbEntity.TINHTRANGNGUOIDUNGs.ToList();

            var _nguoiDung = dbEntity.NGUOIDUNGs.Single(nd => nd.MaNguoiDung == id);

            return View(_nguoiDung);
        }

        //
        // POST: /NguoiDungManager/Edit/
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _nguoiDung = dbEntity.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);
            //var _check = dbEntity.NGUOIDUNGs.Where(nd => nd.UserName == _nguoiDung.UserName).ToList();
            //if (_check.Count() != 0)
            //    foreach (var nd in _check)
            //        if (nd.MaNguoiDung != _nguoiDung.MaNguoiDung)
            //            return View("ErrorUserName");
            
            if (TryUpdateModel(_nguoiDung))
            {
                var _bn = dbEntity.BANNICKs.Where(bn => bn.MaNguoiDung == id).ToList();
                if (_bn.Count == 0)
                {
                    if (_nguoiDung.MaTinhTrangNguoiDung == 2)
                    {
                        BANNICK _banNick = new BANNICK();
                        _banNick.MaNguoiDung = _nguoiDung.MaNguoiDung;

                        // Lấy số lượng ngày bị bannick
                        var _ts = dbEntity.THAMSOes.First();

                        DateTime dtHetHan = (DateTime.Now).AddDays(int.Parse(_ts.QuyDinhSoNgayBanNickToiDa.ToString()));
                        _banNick.NgayHetHan = dtHetHan;

                        // Add BANNICK
                        dbEntity.BANNICKs.AddObject(_banNick);
                    }
                }
                else
                {
                    if (_nguoiDung.MaTinhTrangNguoiDung != 2)
                    {
                        var _banNick = dbEntity.BANNICKs.First(bn => bn.MaNguoiDung == id);
                        dbEntity.BANNICKs.DeleteObject(_banNick);
                    }
                }
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(_nguoiDung);
            }
        }

        //
        // GET: /NguoiDungManager/Delete/5
        public ActionResult Delete(int id)
        {
            var _nguoiDung = dbEntity.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);

            return View(_nguoiDung);
        }

        //
        // POST: /NguoiDungManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _nguoiDung = dbEntity.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);


            // DELETE PLAYLIST
            var _playList = dbEntity.PLAYLISTs.Where(pl => pl.MaNguoiDung == id).ToList();
            foreach (var pl in _playList)
            {
                // DELETE CHITIETPLAYLIST
                var _chiTietPlayList = dbEntity.CHITIETPLAYLISTs.Where(ctpl => ctpl.MaPlaylist == pl.MaPlaylist).ToList();
                foreach (var ctpl in _chiTietPlayList)
                    dbEntity.CHITIETPLAYLISTs.DeleteObject(ctpl);
                dbEntity.PLAYLISTs.DeleteObject(pl);
            }

            // DELETE COMMENT
            var _comment = dbEntity.COMMENTs.Where(cm => cm.MaBaiHat == id).ToList();
            foreach (var cm in _comment)
                dbEntity.COMMENTs.DeleteObject(cm);

            //DELETE DIEM
            var _diem = dbEntity.DIEMs.Where(d => d.MaBaiHat == id).ToList();
            foreach (var d in _diem)
                dbEntity.DIEMs.DeleteObject(d);

            // DELETE BANNICK
            if (_nguoiDung.MaTinhTrangNguoiDung == 2)
            {
                var _banNick = dbEntity.BANNICKs.First(bn => bn.MaNguoiDung == id);
                dbEntity.BANNICKs.DeleteObject(_banNick);
            }

            // DELETE NGUOIDUNG
            dbEntity.NGUOIDUNGs.DeleteObject(_nguoiDung);
            dbEntity.SaveChanges();
            return View("Deleted");
        }

        //
        // GET: /NguoiDungManager/Ban
        public ActionResult Ban()
        {
            var _bn = dbEntity.BANNICKs.ToList();
            return View(_bn);
        }

        //
        // GET: /NguoiDungManager/EndBan
        public ActionResult EndBan(int id)
        {
            var _bn = dbEntity.BANNICKs.First(bn => bn.MaNguoiDung == id);
            return View(_bn);
        }

        //
        // POST: /NguoiDungManager/EndBan
        [HttpPost]
        public ActionResult EndBan(int id, FormCollection collection)
        {
            var _bn = dbEntity.BANNICKs.First(bn => bn.MaNguoiDung == id);
            
            // DELETE BANNICK
            dbEntity.BANNICKs.DeleteObject(_bn);

            // UPDATE TrangThai NGUOIDUNGS
            var _nguoiDung = dbEntity.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);
            _nguoiDung.MaTinhTrangNguoiDung = 1;
            TryUpdateModel(_nguoiDung);

            dbEntity.SaveChanges();
            return View("EndedBan");
        }
    }
}
