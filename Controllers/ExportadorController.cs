using Plataforma.Models;
using Plataforma.Models.Exportador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ExportadorController : Controller
    {
        // GET: Ciudad
        public ActionResult VisualizarExportador()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            //int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            return View();
        }

        [HttpPost]
        public string CrearExportador(string NombreExportador, int Estado, int IdPais)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ExportadorModel.CrearExportador(NombreExportador, Estado, IdPais);
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

                aux.Mensaje = "Operación no se Realizó, Exportador Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }


        [HttpPost]
        public string GetExportador()
        {
            List<Clases.Exportador> Exportador = new List<Clases.Exportador>();
            Exportador = ExportadorModel.GetExportador();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Exportador);
            return datos;
        }

        public string GetInfoExportador(int IdExportador)
        {
            List<Clases.Exportador> Exportador = new List<Clases.Exportador>();
            Exportador = ExportadorModel.GetInfoExportador(IdExportador);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Exportador);
            return datos;
        }

        [HttpPost]
        public string GetExportadorActivos()
        {
            List<Clases.Exportador> Exportador = new List<Clases.Exportador>();
            Exportador = ExportadorModel.GetExportadorActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Exportador);
            return datos;
        }

        [HttpPost]
        public string GetExportadorPais(int IdPais)
        {
            List<Clases.Exportador> Exportador = new List<Clases.Exportador>();
            Exportador = ExportadorModel.GetExportadorPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Exportador);
            return datos;
        }

        [HttpPost]
        public string EditarExportador(string NombreExportador, int Estado, int IdExportador, int IdPais)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ExportadorModel.EditarExportador(NombreExportador, Estado, IdExportador, IdPais);
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

                aux.Mensaje = "Operación no se Realizó, Exportador Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
