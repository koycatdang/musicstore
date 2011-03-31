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
        
        // Hàm ban nick
        private void fnBanNick(NGUOIDUNG _nguoiDung)
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

        //
        // POST: /NguoiDungManager/Create
        [HttpPost]
        public ActionResult Create(NGUOIDUNG _nguoiDung)
        {
            if (ModelState.IsValid)
            {
                //Save NGUOIDUNG
                StoreDB.NGUOIDUNGs.AddObject(_nguoiDung);
                if (_nguoiDung.MaTinhTrangNguoiDung == 2) fnBanNick(_nguoiDung);
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
                if (_nguoiDung.MaTinhTrangNguoiDung == 2) fnBanNick(_nguoiDung);
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
            var _banNick = StoreDB.BANNICKs.First(bn => bn.MaNguoiDung == id);
            var _nguoiDung = StoreDB.NGUOIDUNGs.First(nd => nd.MaNguoiDung == id);
            var _chiTietPlayList = StoreDB.CHITIETPLAYLISTs.First(ctpl => ctpl.PLAYLIST.MaNguoiDung == id);
            var _playList = StoreDB.PLAYLISTs.First(pl => pl.MaNguoiDung == id);

            StoreDB.BANNICKs.DeleteObject(_banNick);
            StoreDB.CHITIETPLAYLISTs.DeleteObject(_chiTietPlayList);
            StoreDB.PLAYLISTs.DeleteObject(_playList);
            StoreDB.NGUOIDUNGs.DeleteObject(_nguoiDung);
            StoreDB.SaveChanges();
            return View("Deleted");
        }
    }
}
