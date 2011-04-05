using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MusicStore.Models;

namespace MusicStore.Controllers
{

    public class AccountController : Controller
    {
        db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(NGUOIDUNG _nguoiDung, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _user = dbEntity.NGUOIDUNGs.Where(nd => nd.UserName == _nguoiDung.UserName).Where(nd => nd.PassWord == _nguoiDung.PassWord).ToList();
                    if (_user.Count != 0)
                    {
                        Session["UserName"] = _nguoiDung.UserName;
                        Session["MaNguoiDung"] = _nguoiDung.MaNguoiDung;
                        Session["MaLoaiNguoiDung"] = _nguoiDung.MaLoaiNguoiDung;
                        if (_user[0].MaLoaiNguoiDung == 1)
                            return RedirectToAction("Index", "HomeGuest");
                        else           
                            return RedirectToAction("Index", "HomeAdmin");
                    }
                }
                catch (Exception)
                {
                    return View("ErrorLogOn");
                }
            }
            Session["UserName"] = Session["MaNguoiDung"] = Session["MaLoaiNguoiDung"] = null;
            return View("ErrorLogOn");
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();
            Session["UserName"] = "";
            Session["MaNguoiDung"] = "";
            return RedirectToAction("Index", "HomeUser");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(NGUOIDUNG model)
        {
            if (ModelState.IsValid)
            {
                db_MusicStoreEntities dbEntity = new db_MusicStoreEntities();
                model.MaLoaiNguoiDung = 1;
                model.MaTinhTrangNguoiDung = 1;
                dbEntity.NGUOIDUNGs.AddObject(model);
                dbEntity.SaveChanges();
                FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                return RedirectToAction("Index", "HomeUser");
            }
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
