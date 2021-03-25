using Plataforma.Models;
using Plataforma.Models.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ServiciosController : Controller
    {
        // GET: Servicios
        public ActionResult ProgramarServicios()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public string IngresarPosturaCortina(int IdContenedor, DateTime FechaEstimada, string HoraEstimada, int TipoLugar, int Lugar)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = ServicioModelo.IngresarPosturaCortina(IdContenedor,FechaEstimada,HoraEstimada,TipoLugar,Lugar);
            if (Flag == 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;

        }
        public string IngresarInstalacionScrubber(int IdContenedor, DateTime FechaEstimada, string HoraEstimada, int TipoLugar, int Lugar)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = ServicioModelo.IngresarInstalacionScrubber(IdContenedor, FechaEstimada, HoraEstimada, TipoLugar, Lugar);
            if (Flag == 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;

        }

        [HttpPost]
        public string ConsultarProgramacionServicio()
        {
            List<Clases.ProgramacionServicio> Resultados = new List<Clases.ProgramacionServicio>();
            Resultados = ServicioModelo.ConsultarProgramacionServicio();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string GetInformacionProgramarServicio(int IdRegistro)
        {
            List<Clases.ProgramacionServicio> Resultados = new List<Clases.ProgramacionServicio>();
            Resultados = ServicioModelo.GetInformacionProgramarServicio(IdRegistro);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }
    }
}
