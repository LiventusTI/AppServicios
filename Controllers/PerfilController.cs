using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: Perfil/Details/5
        public ActionResult Details(int id)
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: Perfil/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // POST: Perfil/Create
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

        // GET: Perfil/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Perfil/Edit/5
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

        // GET: Perfil/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Perfil/Delete/5
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
