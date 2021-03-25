using Plataforma.Models;
using Plataforma.Models.FreightForwarder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class FreightForwarderController : Controller
    {
        // GET: Ciudad
        public ActionResult VisualizarFreightForwarder()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            //int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            return View();
        }

        [HttpPost]
        public string CrearFreightForwarder(string NombreFreightForwarder, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = FreightForwarderModel.CrearFreightForwarder(NombreFreightForwarder, Estado);
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

                aux.Mensaje = "Operación no se Realizó, FreightForwarder Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }


        [HttpPost]
        public string GetFreightForwarder()
        {
            List<Clases.FreightForwarder> FreightForwarder = new List<Clases.FreightForwarder>();
            FreightForwarder = FreightForwarderModel.GetFreightForwarder();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(FreightForwarder);
            return datos;
        }

        [HttpPost]
        public string GetFreightForwarderActivos()
        {
            List<Clases.FreightForwarder> FreightForwarder = new List<Clases.FreightForwarder>();
            FreightForwarder = FreightForwarderModel.GetFreightForwarderActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(FreightForwarder);
            return datos;
        }

        [HttpPost]
        public string EditarFreightForwarder(string NombreFreightForwarder, int Estado, int IdFreightForwarder)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = FreightForwarderModel.EditarFreightForwarder(NombreFreightForwarder, Estado, IdFreightForwarder);
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

                aux.Mensaje = "Operación no se Realizó, FreightForwarder Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
