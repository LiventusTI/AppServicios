using Plataforma.Models;
using Plataforma.Models.Continente;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ContinenteController : Controller
    {

        [HttpPost]
        public string GetContinentes()
        {
            List<Clases.Continente> Continentes = new List<Clases.Continente>();
            Continentes = ContinenteModel.GetContinentes();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Continentes);
            return datos;
        }

        // GET: Continente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Continente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Continente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Continente/Create
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

        // GET: Continente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Continente/Edit/5
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

        // GET: Continente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Continente/Delete/5
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
