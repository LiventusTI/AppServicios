using Plataforma.Models;
using Plataforma.Models.Nave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class NaveController : Controller
    {
        // GET: Commoditie
        public ActionResult VisualizarNaves()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public string CrearNave(string NombreNave, int Estado, int IdNaviera)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = NaveModelo.CrearNave(NombreNave, Estado, IdNaviera);
            if (Flag == 0)
            {

                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            else if (Flag == 1)
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;
            } else if (Flag == 2) {

                aux.Mensaje = "Operación no se Realizó, Nave Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetNaves()
        {
            List<Clases.Nave> Nave = new List<Clases.Nave>();
            Nave = NaveModelo.GetNaves();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Nave);
            return datos;
        }

        [HttpPost]
        public string GetNavesActivas()
        {
            List<Clases.Nave> Nave = new List<Clases.Nave>();
            Nave = NaveModelo.GetNavesActivas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Nave);
            return datos;
        }

        // GET: Commoditie/Edit/5
        [HttpPost]
        public string EditarNave(string NombreNave, int Estado, int IdNave, int IdNaviera)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = NaveModelo.EditarNave(NombreNave, Estado, IdNave, IdNaviera);
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

                aux.Mensaje = "Operación no se Realizó, Nave Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
