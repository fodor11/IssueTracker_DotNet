using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTracker.Controllers
{
    public class AccountController : Controller
    {
        ////////////////////////////// LOGIN PAGE //////////////////////////////
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }
        //TODO:post login



        ////////////////////////////// USER DETAILS & EDIT //////////////////////////////

        // GET: Account/Details
        public ActionResult Details()
        {
            UserModel dummy = new UserModel("Dummy User", "justfor@its.sake", null, null, @"issa\path\man", "password");
            return View(dummy);
        }
        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        ////////////////////////////// REGISTER //////////////////////////////

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
