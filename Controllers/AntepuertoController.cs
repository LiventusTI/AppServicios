using Plataforma.Models;
using Plataforma.Models.AntePuerto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class AntepuertoController : Controller
    {
        public ActionResult VisualizarAntepuertos()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearAntepuerto(string NombreAntepuerto, int IdCiudad, int Estado, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = AntepuertoModelo.CrearAntepuerto(NombreAntepuerto, IdCiudad, Estado, Latitud, Longitud, Radio);
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

                aux.Mensaje = "Operación no se Realizó, Deposito Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetAntepuertos()
        {
            List<Clases.AntePuerto> AntePuerto = new List<Clases.AntePuerto>();
            AntePuerto = AntepuertoModelo.GetAntepuertos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AntePuerto);
            return datos;
        }
        [HttpPost]
        public string GetAntepuertoCiudad(int IdCiudad)
        {
            List<Clases.AntePuerto> AntePuerto = new List<Clases.AntePuerto>();
            AntePuerto = AntepuertoModelo.GetAntepuertoCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AntePuerto);
            return datos;
        }

        [HttpPost]
        public string GetAntepuertoPais(int IdPais)
        {
            List<Clases.AntePuerto> AntePuerto = new List<Clases.AntePuerto>();
            AntePuerto = AntepuertoModelo.GetAntepuertoPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AntePuerto);
            return datos;
        }

        [HttpPost]
        public string GetAntepuertoContinente(int IdContinente)
        {
            List<Clases.AntePuerto> AntePuerto = new List<Clases.AntePuerto>();
            AntePuerto = AntepuertoModelo.GetAntepuertoContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AntePuerto);
            return datos;
        }

        [HttpPost]
        public string EditarAntepuerto(string NombreAntepuerto, int IdCiudad, int Estado, int IdAntepuerto, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = AntepuertoModelo.EditarAntePuerto(NombreAntepuerto, IdCiudad, Estado, IdAntepuerto, Latitud, Longitud, Radio);
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

                aux.Mensaje = "Operación no se Realizó, Deposito Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetInfoAntepuerto(int IdAntepuerto)
        {
            List<Clases.AntePuerto> Antepuertos = new List<Clases.AntePuerto>();
            Antepuertos = AntepuertoModelo.GetInfoAntepuerto(IdAntepuerto);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Antepuertos);
            return datos;
        }

        [HttpPost]
        public string GetAntePuertosActivos()
        {
            List<Clases.AntePuerto> AntePuerto = new List<Clases.AntePuerto>();
            AntePuerto = AntepuertoModelo.GetAntePuertosActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AntePuerto);
            return datos;
        }
    }   
}

