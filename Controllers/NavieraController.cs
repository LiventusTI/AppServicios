using Plataforma.Models.Naviera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plataforma.Models;

namespace Plataforma.Controllers
{
    public class NavieraController : Controller
    {
        // GET: Naviera
        public ActionResult VisualizarNavieras()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearNaviera(string NombreNaviera, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = NavieraModelo.CrearNaviera(NombreNaviera, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Naviera Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetNavieras()
        {
            List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            Naviera = NavieraModelo.GetNavieras();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Naviera);
            return datos;
        }

        [HttpPost]
        public string GetNavierasActivas()
        {
            List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            Naviera = NavieraModelo.GetNavierasActivas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Naviera);
            return datos;
        }

        // POST: Naviera/Create
        [HttpPost]
        public string EditarNaviera(string NombreNaviera, int Estado, int IdNaviera)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = NavieraModelo.EditarNaviera(NombreNaviera, Estado, IdNaviera);
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

                aux.Mensaje = "Operación no se Realizó, Naviera Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

    }
}
