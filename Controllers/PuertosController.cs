using Plataforma.Models;
using Plataforma.Models.Puertos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class PuertosController : Controller
    {
        // GET: Puertos
        public ActionResult VisualizarPuertos()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult VisualizarPuertosOrigen()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearPuertos(string NombrePuerto, int IdCiudad, int Estado, string Latitud = "", string Longitud = "", string Radio = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = PuertoModelo.CrearPuertos(NombrePuerto, IdCiudad, Estado, Latitud, Longitud, Radio);
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

                aux.Mensaje = "Operación no se Realizó, Puerto Destino Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetPuertos()
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            PuertoDestino = PuertoModelo.GetPuertos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }

        [HttpPost]
        public string GetPuertosSP()
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            PuertoDestino = PuertoModelo.GetPuertosSP();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }
        public string GetPuertosOrigenCiudad(int IdCiudad)
        {
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            PuertoOrigen = PuertoModelo.GetPuertoOrigenCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoOrigen);
            return datos;
        }

        public string GetPuertosOrigenPais(int IdPais)
        {
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            PuertoOrigen = PuertoModelo.GetPuertosOrigenPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoOrigen);
            return datos;
        }

        public string GetPuertosOrigenContinente(int IdContinente)
        {
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            PuertoOrigen = PuertoModelo.GetPuertosOrigenContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoOrigen);
            return datos;
        }

        public string GetPuertosDestinoCiudad(int IdCiudad)
        {
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            PuertoOrigen = PuertoModelo.GetPuertoDestinoCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoOrigen);
            return datos;
        }

        public string GetPuertosDestinoPais(int IdPais)
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            PuertoDestino = PuertoModelo.GetPuertosDestinoPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }

        public string GetPuertosDestinoContinente(int IdContinente)
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            PuertoDestino = PuertoModelo.GetPuertosDestinoContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }

        public string GetInfoPuertoOrigen(int IdPuertoOrigen)
        {
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            PuertoOrigen = PuertoModelo.GetInfoPuertoOrigen(IdPuertoOrigen);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoOrigen);
            return datos;
        }

        public string GetInfoPuertoDestino(int IdPuertoDestino)
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            PuertoDestino = PuertoModelo.GetInfoPuertoDestino(IdPuertoDestino);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }

        public string EditarPuerto(string NombrePuerto, int IdCiudad, int Estado, int IdPuerto, string Latitud = "", string Longitud = "", string Radio = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = PuertoModelo.EditarPuerto(NombrePuerto, IdCiudad, Estado, IdPuerto, Latitud, Longitud, Radio);
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

                aux.Mensaje = "Operación no se Realizó, Puerto Destino Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        public string GetDestinosViaje(int IdPuertoOrigen)
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            PuertoDestino = PuertoModelo.GetDestinosViaje(IdPuertoOrigen);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }

    }
}
