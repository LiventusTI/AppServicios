using Plataforma.Models;
using Plataforma.Models.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ModemController : Controller
    {
        // GET: Modem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizarResumenModems()
        {
            return View();
        }

        public JsonResult GetCountMatriz(int IdPais, int IdCiudad, int IdTipoLocalidad, int IdLocalidad)
        {
            int[] contador = new int[10];
            contador = ControladorModel.GetCountMatrizModem(IdPais, IdCiudad, IdTipoLocalidad, IdLocalidad);
            var resultados = Json(contador, JsonRequestBehavior.AllowGet);
            return resultados;
        }

        [HttpPost]
        public int ValidarModemServicio(string Modem)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.ESN Modems = new Clases.ESN();
            Modems = ControladorModel.GetIdESN(Modem);
            int Estado = ControladorModel.ValidarModemServicio(Modems.Id);
            return Estado;

        }
    }
}