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

            StoreDB.NGUOIDUNGs.DeleteObject(_nguoiDung);
            StoreDB.SaveChanges();
            return View("Deleted");
        }
    }
}
