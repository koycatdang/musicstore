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
        db_MusicStoreEntities StoreDB = new db_MusicStoreEntities();
        //
        // GET: /NguoiDungManager/
        public ActionResult Index()
        {
            var _nguoiDung = StoreDB.NGUOIDUNGs.ToList();
            return View(_nguoiDung);
        }

        //
        // GET: /NguoiDungManager/Create
        public ActionResult Create()
        {
            ViewBag.LoaiNguoiDung = StoreDB.LOAINGUOIDUNGs.OrderBy(lnd => lnd.TenLoaiNguoiDung).ToList();
            ViewBag.TinhTrangNguoiDung = StoreDB.TINHTRANGNGUOIDUNGs.OrderBy(ttnd => ttnd.TenTinhTrangNguoiDung).ToList();

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
                StoreDB.NGUOIDUNGs.AddObject(_nguoiDung);
                if (_nguoiDung.MaTinhTrangNguoiDung == 2)
                {
                    BANNICK _banNick = new BANNICK();
                    _banNick.MaNguoiDung = _nguoiDung.MaNguoiDung;

                    // Lấy số lượng ngày bị bannick
                    var _soNgayBanNick = (from ts in StoreDB.THAMSOes
                                          select new { ts.QuyDinhSoNgayBanNickToiDa });

                    foreach (var item in _soNgayBanNick)
                    {
                        DateTime dtHetHan = (DateTime.Now).AddDays(item.QuyDinhSoNgayBanNickToiDa);
                        _banNick.NgayHetHan = dtHetHan;
                    }

                    // Add BANNICK
                    StoreDB.BANNICKs.AddObject(_banNick);
                }
                StoreDB.SaveChanges();
                return RedirectToAction("Index");
            }

            // Invalid – redisplay with errors
            ViewBag.LoaiNguoiDung = StoreDB.LOAINGUOIDUNGs.OrderBy(lnd => lnd.TenLoaiNguoiDung).ToList();
            ViewBag.TinhTrangNguoiDung = StoreDB.TINHTRANGNGUOIDUNGs.OrderBy(ttnd => ttnd.TenTinhTrangNguoiDung).ToList();

            return View(_nguoiDung);
        }

        //
        // GET: /NguoiDungManager/Edit/
        public ActionResult Edit(int id)
        {
            ViewBag.LoaiNguoiDung = StoreDB.LOAINGUOIDUNGs.OrderBy(lnd => lnd.TenLoaiNguoiDung).ToList();
            ViewBag.TinhTrangNguoiDung = StoreDB.TINHTRANGNGUOIDUNGs.OrderBy(ttnd => ttnd.TenTinhTrangNguoiDung).ToList();

            var _nguoiDung = StoreDB.NGUOIDUNGs.Single(nd => nd.MaNguoiDung == id);

            return View(_nguoiDung);
        }

        //
        // POST: /NguoiDungManager/Edit/
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var _nguoiDung = StoreDB.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);

            if (TryUpdateModel(_nguoiDung))
            {
                var _bn = StoreDB.BANNICKs.Where(bn => bn.MaNguoiDung == id).ToList();
                if (_bn.Count == 0)
                {
                    if (_nguoiDung.MaTinhTrangNguoiDung == 2)
                    {
                        BANNICK _banNick = new BANNICK();
                        _banNick.MaNguoiDung = _nguoiDung.MaNguoiDung;

                        // Lấy số lượng ngày bị bannick
                        var _soNgayBanNick = from ts in StoreDB.THAMSOes
                                             select new { ts.QuyDinhSoNgayBanNickToiDa };

                        foreach (var item in _soNgayBanNick)
                        {
                            DateTime dtHetHan = (DateTime.Now).AddDays(item.QuyDinhSoNgayBanNickToiDa);
                            _banNick.NgayHetHan = dtHetHan;
                        }
                        // Add BANNICK
                        StoreDB.BANNICKs.AddObject(_banNick);
                    }
                }
                else
                {
                    if (_nguoiDung.MaTinhTrangNguoiDung != 2)
                    {
                        var _banNick = StoreDB.BANNICKs.First(bn => bn.MaNguoiDung == id);
                        StoreDB.BANNICKs.DeleteObject(_banNick);
                    }
                }
                StoreDB.SaveChanges();
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
            var _nguoiDung = StoreDB.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);

            return View(_nguoiDung);
        }

        //
        // POST: /NguoiDungManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var _nguoiDung = StoreDB.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);


            // DELETE PLAYLIST
            var _playList = (from pl in StoreDB.PLAYLISTs
                             where pl.MaNguoiDung == id
                             select pl).ToList();
            foreach (var pl in _playList)
            {
                // DELETE CHITIETPLAYLIST
                var _chiTietPlayList = (from ctpl in StoreDB.CHITIETPLAYLISTs
                                        where ctpl.MaPlaylist == pl.MaPlaylist
                                        select ctpl).ToList();
                foreach (var ctpl in _chiTietPlayList)
                    StoreDB.CHITIETPLAYLISTs.DeleteObject(ctpl);
                StoreDB.PLAYLISTs.DeleteObject(pl);
            }

            // DELETE COMMENT
            var _comment = (from cm in StoreDB.COMMENTs
                            where cm.MaNguoiDung == id
                            select cm).ToList();
            foreach (var cm in _comment)
                StoreDB.COMMENTs.DeleteObject(cm);

            //DELETE DIEM
            var _diem = (from d in StoreDB.DIEMs
                         where d.MaNguoiDung == id
                         select d).ToList();
            foreach (var d in _diem)
                StoreDB.DIEMs.DeleteObject(d);

            // DELETE BANNICK
            if (_nguoiDung.MaTinhTrangNguoiDung == 2)
            {
                var _banNick = StoreDB.BANNICKs.First(bn => bn.MaNguoiDung == id);
                StoreDB.BANNICKs.DeleteObject(_banNick);
            }

            // DELETE NGUOIDUNG
            StoreDB.NGUOIDUNGs.DeleteObject(_nguoiDung);
            StoreDB.SaveChanges();
            return View("Deleted");
        }
    }
}
