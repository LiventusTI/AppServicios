using Plataforma.Models;
using Plataforma.Models.Deadline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class DeadlineController : Controller
    {
        // GET: Deadline
        public ActionResult VisualizarDeadline()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearDeadLine(string Descripcion, int IdPais, int DiasLimite, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = DeadLineModelo.CrearDeadLine(Descripcion, IdPais, DiasLimite, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Deadline Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetDeadLine()
        {
            List<Clases.DeadLine> DeadLine = new List<Clases.DeadLine>();
            DeadLine = DeadLineModelo.GetDeadLine();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(DeadLine);
            return datos;
        }

        [HttpPost]
        public string EditarDeadLine(string Descripcion, int IdPais, int DiasLimite, int Estado, int IdDeadLine)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = DeadLineModelo.EditarDeadLine(Descripcion, IdPais, DiasLimite, Estado, IdDeadLine);
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

                aux.Mensaje = "Operación no se Realizó, Deadline Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
