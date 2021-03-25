using Plataforma.Models;
using Plataforma.Models.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class TerminalController : Controller
    {
        public ActionResult VisualizarTerminales()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearTerminal(string NombreTerminal, int IdCiudad, int Estado, string Latitud = "", string Longitud = "", string Radio = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = TerminalModelo.CrearTerminal(NombreTerminal, IdCiudad, Estado, Latitud, Longitud, Radio);
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

                aux.Mensaje = "Operación no se Realizó, Terminal Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetTerminales()
        {
            List<Clases.Terminal> Terminal = new List<Clases.Terminal>();
            Terminal = TerminalModelo.GetTerminales();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Terminal);
            return datos;
        }
        [HttpPost]
        public string GetTerminalCiudad(int IdCiudad)
        {
            List<Clases.Terminal> Terminal = new List<Clases.Terminal>();
            Terminal = TerminalModelo.GetTerminalCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Terminal);
            return datos;
        }

        [HttpPost]
        public string GetTerminalPais(int IdPais)
        {
            List<Clases.Terminal> Terminal = new List<Clases.Terminal>();
            Terminal = TerminalModelo.GetTerminalPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Terminal);
            return datos;
        }

        [HttpPost]
        public string GetTerminalContinente(int IdContinente)
        {
            List<Clases.Terminal> Terminal = new List<Clases.Terminal>();
            Terminal = TerminalModelo.GetTerminalContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Terminal);
            return datos;
        }

        [HttpPost]
        public string EditarTerminal(string NombreTerminal, int IdCiudad, int Estado, int IdTerminal, string Latitud = "", string Longitud = "", string Radio = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = TerminalModelo.EditarTerminal(NombreTerminal, IdCiudad, Estado, IdTerminal, Latitud, Longitud, Radio);
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

                aux.Mensaje = "Operación no se Realizó, Terminal Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetInfoTerminal(int IdTerminal)
        {
            List<Clases.Terminal> Terminales = new List<Clases.Terminal>();
            Terminales = TerminalModelo.GetInfoTerminal(IdTerminal);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Terminales);
            return datos;
        }

        [HttpPost]
        public string GetTerminalesActivos()
        {
            List<Clases.Terminal> Terminal = new List<Clases.Terminal>();
            Terminal = TerminalModelo.GetTerminalesActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Terminal);
            return datos;
        }

    }
}