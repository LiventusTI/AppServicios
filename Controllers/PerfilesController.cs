using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class PerfilesController : Controller
    {
        // GET: Perfiles
        public ActionResult VisualizarPerfiles()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: Perfiles/Details/5
        public ActionResult Details(int id)
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: Perfiles/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // POST: Perfiles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Perfiles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Perfiles/Edit/5
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

        // GET: Perfiles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Perfiles/Delete/5
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
    }
}
