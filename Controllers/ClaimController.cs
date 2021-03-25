using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ClaimController : Controller
    {
        // GET: Claim
        public ActionResult AgregarClaim()
        {
            return View();
        }

        public ActionResult IngresarClaim()
        {
            return View();
        }


        public ActionResult ConsultarClaims()
        {
            return View();
        }

        public ActionResult ClaimsPendientePago()
        {
            return View();
        }

        public ActionResult EditarClaim()
        {
            return View();
        }

        public ActionResult DetalleClaim(int id, string estado, string fechaNotificacion, string booking, string contenedor, string controlador, string csgrafico, string dol, string vigencia, string statusLab, string notificacionJLT, string notificacionOrion, string notificacionGML, string fechaCierre)
        {
            ViewBag.Id = id;
            ViewBag.Estado = estado;
            ViewBag.FechaNotificacion = fechaNotificacion;
            ViewBag.Booking = booking;
            ViewBag.Contenedor = contenedor;
            ViewBag.Controlador = controlador;
            ViewBag.Csgrafico = csgrafico;
            ViewBag.Dol = dol;
            ViewBag.Vigencia = vigencia;
            ViewBag.StatusLab = statusLab;
            ViewBag.NotificacionJLT = notificacionJLT;
            ViewBag.NotificacionGML = notificacionGML;
            ViewBag.NotificacionOrion = notificacionOrion;
            ViewBag.FechaCierre = fechaCierre;

            return View();
        }

        public ActionResult IngresarClaimServicio(int id, string booking, string contenedor, string controlador, string naviera, string nave, string viaje, string exportador, string eta, string iniciostacking, string producto, string porigen, string pdestino, int co2, int o2, string temperatura, string tecnologia)
        {
            ViewBag.Id = id;
            ViewBag.Booking = booking;
            ViewBag.Contenedor = contenedor;
            ViewBag.Controlador = controlador;
            ViewBag.Naviera = naviera;
            ViewBag.Nave = nave;
            ViewBag.Viaje = viaje;
            ViewBag.Exportador = exportador;
            ViewBag.Eta = eta;
            ViewBag.InicioStacking = iniciostacking;
            ViewBag.Producto = producto;
            ViewBag.Porigen = porigen;
            ViewBag.Pdestino = pdestino;
            ViewBag.Co2 = co2;
            ViewBag.O2 = o2;
            ViewBag.Temperatura = temperatura;
            ViewBag.Tecnologia = tecnologia;

            return View();
        }


        // POST: Claim/Create
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

        // GET: Claim/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Claim/Edit/5
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

        // GET: Claim/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Claim/Delete/5
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
