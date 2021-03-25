using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ForecastController : Controller
    {
        // GET: Forecast
        public ActionResult Consultar()
        {
            return View();
        }


        public ActionResult CrearForecastVacio()
        {
            return View();
        }

        public ActionResult CrearForecast()
        {
            return View();
        }

        public ActionResult VerReportes()
        {
            return View();
        }

        public ActionResult AgregarDatosExternos()
        {
            return View();
        }

        public ActionResult ModificarDatosExternos()
        {
            return View();
        }

        public ActionResult ConsultarDatosExternos()
        {
            return View();
        }
    }
}
