using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plataforma.Models;
using Plataforma.Models.Viaje;

namespace Plataforma.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizarViajes()
        {
            return View();
        }

        [HttpPost]
        public string CrearViaje(string Viaje = "", int PuertoOrigen = 0, int PuertoDestino=0, int Nave=0, DateTime? EtaNave=null, DateTime? InicioStacking=null, DateTime? TerminoStacking=null, DateTime? Etd=null, DateTime? EtaPOD=null, int Estado=0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag2 = ViajeModelo.ValidarViajeCreado(Viaje, PuertoOrigen, PuertoDestino, Nave);
            if (Flag2 == 0)
            {
                int Flag = ViajeModelo.CrearViaje(Viaje, PuertoOrigen, PuertoDestino, Nave, EtaNave, InicioStacking, TerminoStacking, Etd, EtaPOD, Estado);
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
            }
            else
            {
                aux.Mensaje = "Error al crear viaje, viaje ya existe.";
                aux.validador = Flag2;
            }

            
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string EditarViaje(int IdViaje, string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0, int Nave = 0, DateTime? EtaNave = null, DateTime? InicioStacking = null, DateTime? TerminoStacking = null, DateTime? Etd = null, DateTime? EtaPOD = null, int Estado = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ViajeModelo.EditarViaje(IdViaje, Viaje, PuertoOrigen, PuertoDestino, Nave, EtaNave, InicioStacking, TerminoStacking, Etd, EtaPOD, Estado);
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
        public string ValidarViaje(int IdViaje)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ViajeModelo.ValidarViaje(IdViaje);
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
        public string ValidarViajeConServicios(int IdViaje)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ViajeModelo.ValidarViajeConServicios(IdViaje);
            if (Flag == 0)
            {
                aux.Mensaje = "Viaje sin servicios, se puede eliminar";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Viaje con servicios asociados, no se puede eliminar.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string EliminarViaje(int IdViaje)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ViajeModelo.EliminarViaje(IdViaje);
            if (Flag == 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente.";
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
        public string GetViajes()
        {
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            Viaje = ViajeModelo.GetViajes();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }

        [HttpPost]
        public string GetNumViajesFromViajes()
        {
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            Viaje = ViajeModelo.GetNumViajesFromViajes();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }
        public string GetPOFromViajes()
        {
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            Viaje = ViajeModelo.GetPOFromViajes();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }
        public string GetPDFromViajes()
        {
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            Viaje = ViajeModelo.GetPDFromViajes();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }


        [HttpPost]
        public string GetPuertosOrigenViaje(string NumViaje="")
        {
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            Viaje = ViajeModelo.GetPuertosOrigenViaje(NumViaje);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }

        [HttpPost]
        public string GetPuertosDestinoViaje(string NumViaje="", int IdPuertoOrigen=0)
        {
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            Viaje = ViajeModelo.GetPuertosDestinoViaje(NumViaje, IdPuertoOrigen);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }

        [HttpPost]
        public string GetInfoViaje(int IdViaje)
        {
            Clases.Viaje Viaje = new Clases.Viaje();
            Viaje = ViajeModelo.GetInfoViaje(IdViaje);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }

        [HttpPost]
        public string GetViajeServicio(string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0)
        {
            Clases.Viaje Viajes = new Clases.Viaje();
            Viajes = ViajeModelo.GetViajeServicio(Viaje, PuertoOrigen, PuertoDestino);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viajes);
            return datos;
        }

        [HttpPost]
        public string GetViajesPuertos(int IdPuertoOrigen = 0, int IdPuertoDestino = 0)
        {
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            Viaje = ViajeModelo.GetViajesPuertos( IdPuertoOrigen, IdPuertoDestino);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Viaje);
            return datos;
        }
    }
}