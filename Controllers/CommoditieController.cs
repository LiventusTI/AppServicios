using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plataforma.Models;
using Plataforma.Models.Commodity;

namespace Plataforma.Controllers
{
    public class CommoditieController : Controller
    {
        // GET: Commoditie
        public ActionResult VisualizarCommodities()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public string CrearCommodity(string NombreCommodity, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = CommodityModelo.CrearCommodity(NombreCommodity, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Commodity Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetCommodity()
        {
            List<Clases.Commodity> Commodity = new List<Clases.Commodity>();
            Commodity = CommodityModelo.GetCommodity();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Commodity);
            return datos;
        }

        [HttpPost]
        public string GetCommodityActivo()
        {
            List<Clases.Commodity> Commodity = new List<Clases.Commodity>();
            Commodity = CommodityModelo.GetCommodityActivo();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Commodity);
            return datos;
        }

        [HttpPost]
        public string EditarCommodity(string NombreCommodity, int Estado, int IdCommodity)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = CommodityModelo.EditarCommodity(NombreCommodity, Estado, IdCommodity);
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

                aux.Mensaje = "Operación no se Realizó, Commodity Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
