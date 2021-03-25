using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class EstadisticaController : Controller
    {
        // GET: Estadistica

        // GET: Estadistica/Create
        public ActionResult TotalesServicios()
        {
            return View();
        }

        public ActionResult TotalesLeaktest()
        {
            return View();
        }

        public ActionResult TotalesControladores()
        {
            return View();
        }

        public ActionResult TotalesClaims()
        {
            return View();
        }

        public ActionResult TotalesRecuperacionControladores()
        {
            return View();
        }

        // POST: Estadistica/Create
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

        // GET: Estadistica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estadistica/Edit/5
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

        // GET: Estadistica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estadistica/Delete/5
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
