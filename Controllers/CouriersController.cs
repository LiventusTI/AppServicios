using Plataforma.Models;
using Plataforma.Models.Courier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class CouriersController : Controller
    {
        // GET: Ciudad
        public ActionResult VisualizarCouriers()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearCourier(string NombreCourier, int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = CourierModel.CrearCourier(NombreCourier, Estado);
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

                aux.Mensaje = "Operación no se Realizó, Commodity Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetCourier()
        {
            List<Clases.Courier> Courier = new List<Clases.Courier>();
            Courier = CourierModel.GetCourier();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Courier);
            return datos;
        }

        [HttpPost]
        public string GetCourierActivos()
        {
            List<Clases.Courier> Courier = new List<Clases.Courier>();
            Courier = CourierModel.GetCourierActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Courier);
            return datos;
        }

        [HttpPost]
        public string EditarCourier(string NombreCourier, int Estado, int IdCourier)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = CourierModel.EditarCourier(NombreCourier, Estado, IdCourier);
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

                aux.Mensaje = "Operación no se Realizó, Commodity Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
