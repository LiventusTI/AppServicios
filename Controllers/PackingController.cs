using Plataforma.Models.Packing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Models
{
    public class PackingController : Controller
    {
        public ActionResult VisualizarPacking()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearPacking(string NombrePacking, int IdCiudad, int Estado, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = PackingModelo.CrearPacking(NombrePacking, IdCiudad, Estado, Latitud, Longitud, Radio);
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
        public string GetPacking()
        {
            List<Clases.Packing> Packing = new List<Clases.Packing>();
            Packing = PackingModelo.GetPacking();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Packing);
            return datos;
        }

        [HttpPost]
        public string GetPackingCiudad(int IdCiudad)
        {
            List<Clases.Packing> Packings = new List<Clases.Packing>();
            Packings = PackingModelo.GetPackingCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Packings);
            return datos;
        }

        [HttpPost]
        public string GetPackingPais(int IdPais)
        {
            List<Clases.Packing> Packings = new List<Clases.Packing>();
            Packings = PackingModelo.GetPackingPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Packings);
            return datos;
        }

        [HttpPost]
        public string EditarPacking(string NombrePacking, int IdCiudad, int Estado, int IdPacking, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = PackingModelo.EditarPacking(NombrePacking, IdCiudad, Estado, IdPacking, Latitud, Longitud, Radio);
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
        public string GetInfoPacking(int IdPacking)
        {
            List<Clases.Packing> Packings = new List<Clases.Packing>();
            Packings = PackingModelo.GetInfoPacking(IdPacking);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Packings);
            return datos;
        }

        [HttpPost]
        public string GetPackingActivos()
        {
            List<Clases.Packing> Packing = new List<Clases.Packing>();
            Packing = PackingModelo.GetPackingActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Packing);
            return datos;
        }
    }
}
