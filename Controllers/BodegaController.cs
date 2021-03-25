using Plataforma.Models;
using Plataforma.Models.Bodega;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Plataforma.Controllers
{
    public class BodegaController : Controller
    {
        public ActionResult VisualizarBodegas()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearBodega(string NombreBodega, int IdCiudad, int Estado, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = BodegaModelo.CrearBodega(NombreBodega, IdCiudad, Estado, Latitud, Longitud, Radio);
            if (Flag == 0)
            {

                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            else if (Flag == 1)
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;
            }
            else if (Flag == 2)
            {

                aux.Mensaje = "Operación no se Realizó, Deposito Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string EditarBodega(string NombreBodega = "", int IdCiudad = 0, int Estado = 0, int IdBodega = 0, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = BodegaModelo.EditarBodega(NombreBodega, IdCiudad, Estado, IdBodega, Latitud, Longitud, Radio);
            if (Flag == 0)
            {

                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            else if (Flag == 1)
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;
            }
            else if (Flag == 2)
            {

                aux.Mensaje = "Operación no se Realizó, Deposito Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetBodegas()
        {
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            Bodegas = BodegaModelo.GetBodegas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Bodegas);
            return datos;
        }

        [HttpPost]
        public string GetBodegasCiudad(int IdCiudad)
        {
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            Bodegas = BodegaModelo.GetBodegasCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Bodegas);
            return datos;
        }

        [HttpPost]
        public string GetBodegasPais(int IdPais)
        {
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            Bodegas = BodegaModelo.GetBodegasPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Bodegas);
            return datos;
        }

        [HttpPost]
        public string GetBodegasContinente(int IdContinente)
        {
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            Bodegas = BodegaModelo.GetBodegasContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Bodegas);
            return datos;
        }

        [HttpPost]
        public string GetInfoBodega(int IdBodega)
        {
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            Bodegas = BodegaModelo.GetInfoBodega(IdBodega);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Bodegas);
            return datos;
        }

        // GET: Bodega
        public ActionResult Index()
        {
            return View();
        }

        // GET: Bodega/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bodega/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bodega/Create
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

        // GET: Bodega/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bodega/Edit/5
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

        // GET: Bodega/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bodega/Delete/5
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
