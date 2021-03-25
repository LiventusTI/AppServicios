using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado Leaktest
        public ActionResult VisualizarEstadosLeaktest()
        {
            return View();
        }

        // GET: Estado Servicio
        public ActionResult VisualizarEstadosServicio()
        {
            return View();
        }

        // GET: Estado Claims
        public ActionResult VisualizarEstadosClaims()
        {
            return View();
        }

        // POST: Estado/Create
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

        // GET: Estado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estado/Edit/5
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

        // GET: Estado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estado/Delete/5
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
