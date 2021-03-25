using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class DamageReportController : Controller
    {
        // GET: DamageReport
        public ActionResult VisualizarDamageReport()
        {
            return View();
        }

        // GET: DamageReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DamageReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DamageReport/Create
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

        // GET: DamageReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DamageReport/Edit/5
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

        // GET: DamageReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DamageReport/Delete/5
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
