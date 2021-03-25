using Plataforma.Models;
using Plataforma.Models.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ServiceProviderController : Controller
    {
        // GET: ServiceProvider
        public ActionResult VisualizarServiceProvider()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearServiceProvider(string NombreServiceProvider, int IdPais, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ServiceProviderModel.CrearServiceProvider(NombreServiceProvider, IdPais, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Service Provider Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetServiceProvider()
        {
            List<Clases.ServiceProvider> ServiceProvider = new List<Clases.ServiceProvider>();
            ServiceProvider = ServiceProviderModel.GetServiceProvider();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ServiceProvider);
            return datos;
        }

        [HttpPost]
        public string GetServiceProviderActivas()
        {
            List<Clases.ServiceProvider> ServiceProvider = new List<Clases.ServiceProvider>();
            ServiceProvider = ServiceProviderModel.GetServiceProviderActivas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ServiceProvider);
            return datos;
        }

        [HttpPost]
        public string GetServiceProviderPais(int IdPais)
        {
            List<Clases.ServiceProvider> ServiceProvider = new List<Clases.ServiceProvider>();
            ServiceProvider = ServiceProviderModel.GetServiceProviderPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ServiceProvider);
            return datos;
        }

        [HttpPost]
        public string EditarServiceProvider(string NombreServiceProvider, int IdPais, int Estado, int IdServiceProvider)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ServiceProviderModel.EditarServiceProvider(NombreServiceProvider, IdPais, Estado, IdServiceProvider);
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

                aux.Mensaje = "Operación no se Realizó, Service Provider Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

    }
}
