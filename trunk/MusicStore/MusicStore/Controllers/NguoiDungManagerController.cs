using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class NguoiDungManagerController : Controller
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
            ViewBag.LoaiNguoiDung = dbEntity.LOAINGUOIDUNGs.OrderBy(lnd => lnd.TenLoaiNguoiDung).ToList();
            ViewBag.TinhTrangNguoiDung = dbEntity.TINHTRANGNGUOIDUNGs.OrderBy(ttnd => ttnd.TenTinhTrangNguoiDung).ToList();

            var _nguoiDung = new NGUOIDUNG();

            return View(_nguoiDung);
        }

        //
        // POST: /NguoiDungManager/Create
        [HttpPost]
        public ActionResult Create(NGUOIDUNG _nguoiDung)
        {
            if (ModelState.IsValid)
            {
                //Save NGUOIDUNG
                dbEntity.NGUOIDUNGs.AddObject(_nguoiDung);
                if (_nguoiDung.MaTinhTrangNguoiDung == 2)
                {
                    BANNICK _banNick = new BANNICK();
                    _banNick.MaNguoiDung = _nguoiDung.MaNguoiDung;

                    // Lấy số lượng ngày bị bannick
                    var _soNgayBanNick = (from ts in dbEntity.THAMSOes
                                          select new { ts.QuyDinhSoNgayBanNickToiDa });

                    foreach (var item in _soNgayBanNick)
                    {
                        DateTime dtHetHan = (DateTime.Now).AddDays(item.QuyDinhSoNgayBanNickToiDa);
                        _banNick.NgayHetHan = dtHetHan;
                    }

                    // Add BANNICK
                    dbEntity.BANNICKs.AddObject(_banNick);
                }
                dbEntity.SaveChanges();
                return RedirectToAction("Index");
            }

            // Invalid – redisplay with errors
            ViewBag.LoaiNguoiDung = dbEntity.LOAINGUOIDUNGs.OrderBy(lnd => lnd.TenLoaiNguoiDung).ToList();
            ViewBag.TinhTrangNguoiDung = dbEntity.TINHTRANGNGUOIDUNGs.OrderBy(ttnd => ttnd.TenTinhTrangNguoiDung).ToList();

            return View(_nguoiDung);
        }

        //
        // GET: /NguoiDungManager/Edit/
        public ActionResult Edit(int id)
        {
            ViewBag.LoaiNguoiDung = dbEntity.LOAINGUOIDUNGs.OrderBy(lnd => lnd.TenLoaiNguoiDung).ToList();
            ViewBag.TinhTrangNguoiDung = dbEntity.TINHTRANGNGUOIDUNGs.OrderBy(ttnd => ttnd.TenTinhTrangNguoiDung).ToList();

            var _nguoiDung = dbEntity.NGUOIDUNGs.Single(nd => nd.MaNguoiDung == id);

            return View(_nguoiDung);
        }

        //
        // POST: /NguoiDungManager/Edit/
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _nguoiDung = dbEntity.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);

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
                        var _soNgayBanNick = from ts in dbEntity.THAMSOes
                                             select new { ts.QuyDinhSoNgayBanNickToiDa };

                        foreach (var item in _soNgayBanNick)
                        {
                            DateTime dtHetHan = (DateTime.Now).AddDays(item.QuyDinhSoNgayBanNickToiDa);
                            _banNick.NgayHetHan = dtHetHan;
                        }
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
            var _playList = (from pl in dbEntity.PLAYLISTs
                             where pl.MaNguoiDung == id
                             select pl).ToList();
            foreach (var pl in _playList)
            {
                // DELETE CHITIETPLAYLIST
                var _chiTietPlayList = (from ctpl in dbEntity.CHITIETPLAYLISTs
                                        where ctpl.MaPlaylist == pl.MaPlaylist
                                        select ctpl).ToList();
                foreach (var ctpl in _chiTietPlayList)
                    dbEntity.CHITIETPLAYLISTs.DeleteObject(ctpl);
                dbEntity.PLAYLISTs.DeleteObject(pl);
            }

            // DELETE COMMENT
            var _comment = (from cm in dbEntity.COMMENTs
                            where cm.MaNguoiDung == id
                            select cm).ToList();
            foreach (var cm in _comment)
                dbEntity.COMMENTs.DeleteObject(cm);

            //DELETE DIEM
            var _diem = (from d in dbEntity.DIEMs
                         where d.MaNguoiDung == id
                         select d).ToList();
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
