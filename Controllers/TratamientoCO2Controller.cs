using Plataforma.Models;
using Plataforma.Models.TratamientoCO2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class TratamientoCO2Controller : Controller
    {

        [HttpPost]
        public string GetTratamientoCO2()
        {
            List<Clases.TratamientoCO2> TratamientosCO2 = new List<Clases.TratamientoCO2>();
            TratamientosCO2 = TratamientoCO2Modelo.GetTratamientoCO2();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(TratamientosCO2);
            return datos;
        }
        // GET: TratamientoCO2
        public ActionResult Index()
        {
            return View();
        }

        // GET: TratamientoCO2/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TratamientoCO2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TratamientoCO2/Create
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

        // GET: TratamientoCO2/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TratamientoCO2/Edit/5
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

        // GET: TratamientoCO2/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TratamientoCO2/Delete/5
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
