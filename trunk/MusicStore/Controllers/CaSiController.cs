﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class CaSiController : Controller
    {
        Models.db_MusicStoreEntities dbEntity = new Models.db_MusicStoreEntities();

        //
        // GET: /CaSi/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET:
        [ChildActionOnly]
        public ActionResult ListCaSiVietNam()
        {
            var sl = dbEntity.THAMSOes.First();
            
            var temp = dbEntity.CASIs.Where(cs => cs.MaKhuVuc == 1).OrderByDescending(cs => cs.BAIHATs.Count()).Take(int.Parse(sl.SoLuongCaSiLietKeTrenMenu.ToString())).ToList();
            List<CASI> _lstCaSiVietNam = new List<CASI>();
            foreach (var item in temp)
                _lstCaSiVietNam.Add(item);
            
            return PartialView(_lstCaSiVietNam);
        }

        //
        // GET:
        [ChildActionOnly]
        public ActionResult ListCaSiChauA()
        {
            var sl = dbEntity.THAMSOes.First();

            var temp = dbEntity.CASIs.Where(cs => cs.MaKhuVuc == 2).OrderByDescending(cs => cs.BAIHATs.Count()).Take(int.Parse(sl.SoLuongCaSiLietKeTrenMenu.ToString())).ToList();
            List<CASI> _lstCaSiChauA = new List<CASI>();
            foreach (var item in temp)
                _lstCaSiChauA.Add(item);
            return PartialView(_lstCaSiChauA);
        }

        //
        // GET:
        [ChildActionOnly]
        public ActionResult ListCaSiAuMi()
        {
            var sl = dbEntity.THAMSOes.First();

            var temp = dbEntity.CASIs.Where(cs => cs.MaKhuVuc == 3).OrderByDescending(cs => cs.BAIHATs.Count()).Take(int.Parse(sl.SoLuongCaSiLietKeTrenMenu.ToString())).ToList();
            List<CASI> _lstCaSiAuMy = new List<CASI>();
            foreach (var item in temp)
                _lstCaSiAuMy.Add(item);
            return PartialView(_lstCaSiAuMy);
        }
    }
}