using Plataforma.Models;
using Plataforma.Models.Ciudad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class CiudadController : Controller
    {
        public ActionResult VisualizarCiudad()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearCiudad(string NombreCiudad, int IdPais, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = CiudadModelo.CrearCiudad(NombreCiudad, IdPais, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Ciudad Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetCiudades()
        {
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            Ciudades = CiudadModelo.GetCiudades();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Ciudades);
            return datos;
        }


        [HttpPost]
        public string GetCiudadesSP()
        {
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            Ciudades = CiudadModelo.GetCiudadesSP();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Ciudades);
            return datos;
        }


        [HttpPost]
        public string GetCiudadesActivas()
        {
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            Ciudades = CiudadModelo.GetCiudadesActivas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Ciudades);
            return datos;
        }

        [HttpPost]
        public string GetInfoCiudad(int IdCiudad)
        {
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            Ciudades = CiudadModelo.GetInfoCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Ciudades);
            return datos;
        }

        [HttpPost]
        public string GetCiudadesPais(int IdPais)
        {
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            Ciudades = CiudadModelo.GetCiudadesPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Ciudades);
            return datos;
        }

        [HttpPost]
        public string GetCiudadesContinente(int IdContinente)
        {
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            Ciudades = CiudadModelo.GetCiudadesContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Ciudades);
            return datos;
        }

        [HttpPost]
        public string EditarCiudad(string NombreCiudad, int IdPais, int Estado, int IdCiudad)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = CiudadModelo.EditarCiudad(NombreCiudad, IdPais, Estado, IdCiudad);
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

                aux.Mensaje = "Operación no se Realizó, Ciudad Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
