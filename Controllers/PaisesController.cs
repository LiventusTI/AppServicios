using Plataforma.Models;
using Plataforma.Models.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class PaisesController : Controller
    {
        // GET: Paises
        public ActionResult VisualizarPaises()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }
        [HttpPost]
        public string CrearPais(string NombrePais, int IdContinente, int Estado)
        {
            
            if (Session["User"] == null)
            {
                Login();
            }

            Clases.Validar aux = new Clases.Validar();
            int Flag = PaisModelo.CrearPais(NombrePais, IdContinente, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Pais Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetPaises()
        {
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            Paises = PaisModelo.GetPaises();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Paises);
            return datos;
        }

        [HttpPost]
        public string GetPaisesSP()
        {
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            Paises = PaisModelo.GetPaisesSP();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Paises);
            return datos;
        }

        [HttpPost]
        public string GetPaisesContinente(int IdContinente)
        {
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            Paises = PaisModelo.GetPaisesContinente(IdContinente);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Paises);
            return datos;
        }

        [HttpPost]
        public string GetPaisesActivos()
        {
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            Paises = PaisModelo.GetPaisesActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Paises);
            return datos;
        }

        [HttpPost]
        public string EditarPais(string NombrePais, int IdContinente, int Estado, int IdPais)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = PaisModelo.EditarPais(NombrePais, IdContinente, Estado, IdPais);
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

                aux.Mensaje = "Operación no se Realizó, Pais Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        public ActionResult VisualizarContinentes()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public string CrearContinente(string NombreContinente, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = PaisModelo.CrearContinente(NombreContinente, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Continente Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetContinentes()
        {
            List<Clases.Continente> Continente = new List<Clases.Continente>();
            Continente = PaisModelo.GetContinentes();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Continente);
            return datos;
        }

        [HttpPost]
        public string GetContinentesActivos()
        {
            List<Clases.Continente> Continente = new List<Clases.Continente>();
            Continente = PaisModelo.GetContinentesActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Continente);
            return datos;
        }

        [HttpPost]
        public string EditarContinente(string NombreContinente, int Estado, int IdContinente)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = PaisModelo.EditarContinente(NombreContinente, Estado, IdContinente);
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

                aux.Mensaje = "Operación no se Realizó, Continente Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetInfoPais(int IdPais)
        {
            List<Clases.Pais> Pais = new List<Clases.Pais>();
            Pais = PaisModelo.GetInfoPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Pais);
            return datos;
        }

        [HttpPost]
        public string GetNombreContienenteByPuerto(int Puerto)
        {
            Clases.Continente Continente = new Clases.Continente();
            Continente = PaisModelo.GetNombreContienenteByPuerto(Puerto);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Continente);
            return datos;
        }

        [HttpPost]
        public string GetNombrePais(int TipoLugar, int Lugar)
        {
            Clases.Pais Pais = new Clases.Pais();
            Pais = PaisModelo.GetNombrePais(TipoLugar, TipoLugar);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Pais);
            return datos;
        }
    }
}
