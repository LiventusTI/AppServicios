using Plataforma.Models;
using Plataforma.Models.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class MaquinariaController : Controller
    {
        // GET: Maquinaria
        public ActionResult VisualizarMaquinarias()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearMaquinaria(string NombreMaquinaria, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = MaquinariaModelo.CrearMaquinaria(NombreMaquinaria, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Maquinaria Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetMaquinarias()
        {
            List<Clases.Maquinaria> Paises = new List<Clases.Maquinaria>();
            Paises = MaquinariaModelo.GetMaquinarias();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Paises);
            return datos;
        }

        // POST: Maquinaria/Create
        [HttpPost]
        public string EditarMaquinaria(string NombreMaquinaria, int Estado, int IdMaquinaria)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = MaquinariaModelo.EditarMaquinaria(NombreMaquinaria, Estado, IdMaquinaria);
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

                aux.Mensaje = "Operación no se Realizó, Maquinaria Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetMaquinariasActivas()
        {
            List<Clases.Maquinaria> Paises = new List<Clases.Maquinaria>();
            Paises = MaquinariaModelo.GetMaquinariasActivas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Paises);
            return datos;
        }
    }
}
