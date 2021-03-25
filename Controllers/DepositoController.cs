using Plataforma.Models;
using Plataforma.Models.Deposito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class DepositoController : Controller
    {
        // GET: Deposito
        public ActionResult VisualizarDepositos()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearDeposito(string NombreDeposito, int IdCiudad, int Estado, string Latitud = "", string Longitud = "", string Radio = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = DepositoModelo.CrearDeposito(NombreDeposito, IdCiudad, Estado, Latitud, Longitud, Radio);
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
        public string GetDepositos()
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetDepositos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }

        [HttpPost]
        public string GetDepositosSP()
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetDepositosSP();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }

        [HttpPost]
        public string GetDepositoCiudad(int IdCiudad)
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetDepositoCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }

        [HttpPost]
        public string GetDepositoPais(int IdPais)
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetDepositoPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }

        [HttpPost]
        public string GetDepositoContinente(int IdContinente)
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetDepositoContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }

        [HttpPost]
        public string EditarDeposito(string NombreDeposito, int IdCiudad, int Estado, int IdDeposito, string Latitud = "", string Longitud = "", string Radio = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = DepositoModelo.EditarDeposito(NombreDeposito, IdCiudad, Estado, IdDeposito, Latitud, Longitud, Radio);
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
        public string GetDepositosActivos()
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetDepositosActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }

        [HttpPost]
        public string GetInfoDeposito(int IdDeposito)
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetInfoDeposito(IdDeposito);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }

        public string GetLugares()
        {
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            Depositos = DepositoModelo.GetLugares();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Depositos);
            return datos;
        }
    }
}
