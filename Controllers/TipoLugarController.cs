using Plataforma.Models;
using Plataforma.Models.TipoLugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class TipoLugarController : Controller
    {
        // GET: TipoLugar

        [HttpPost]
        public string GetTipoLugares()
        {
            List<Clases.TipoLugar> TipoLugares = new List<Clases.TipoLugar>();
            TipoLugares = TipoLugarModelo.GetTipoLugares();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(TipoLugares);
            return datos;
        }
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: TipoLugar/Details/5
        public ActionResult Details(int id)
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: TipoLugar/Create
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // POST: TipoLugar/Create
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

        // GET: TipoLugar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoLugar/Edit/5
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

        // GET: TipoLugar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoLugar/Delete/5
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
