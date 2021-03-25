using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plataforma.Models;
using MySql.Data.MySqlClient;
using Plataforma.Models.Reservas;



namespace Plataforma.Controllers
{
    public class ControlHorarioController : Controller
    {
        // GET: ControlHorario
        public ActionResult IngresarDatos()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GuardarDatos(Clases.Reserva DatosReserva) {
            List<Clases.Reserva> ListaReservas = new List<Clases.Reserva>();
            //ListaReservas = ReservaModelo.GetReservas();
            return Json(DatosReserva);
        }

        public ActionResult GenerarDocumento() {

            return View();
        }
    }
}
