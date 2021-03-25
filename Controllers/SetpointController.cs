using Plataforma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class SetpointController : Controller
    {
        public ActionResult VisualizarSetpoints()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public string CrearSetpoint(float CO2, float O2,int Estado)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = SetpointModelo.CrearSetpoint(CO2, O2, Estado);
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

                aux.Mensaje = "Operación no se Realizó, SetPoint Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetSetpoint()
        {
            List<Clases.Setpoint> Setpoint = new List<Clases.Setpoint>();
            Setpoint = SetpointModelo.GetSetpoint();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Setpoint);
            return datos;
        }

        [HttpPost]
        public string GetSetpointCommodity(int Commodity)
        {
            List<Clases.Setpoint> Setpoint = new List<Clases.Setpoint>();
            List<Clases.Setpoint> Nuevasetpoint = new List<Clases.Setpoint>();
            Setpoint = SetpointModelo.GetSetpointCommodity(Commodity);

            for (var i = 0; i< Setpoint.Count(); i++)
            {
                if (Setpoint[i].IdaplicacionServicio == Commodity)
                {
                    Nuevasetpoint.Insert(0, Setpoint[i]);
                }
                else {
                    Nuevasetpoint.Add(Setpoint[i]);
                }

            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Nuevasetpoint);
            return datos;
        }
        [HttpPost]
        public string GetSetpointActivos()
        {
            List<Clases.Setpoint> Setpoint = new List<Clases.Setpoint>();
            Setpoint = SetpointModelo.GetSetpointActivos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Setpoint);
            return datos;
        }

        [HttpPost]
        public string EditarSetpoint(int CO2, int O2, int Estado, int IdSetpoint)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = SetpointModelo.EditarCommodity(CO2, O2, Estado, IdSetpoint);
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

                aux.Mensaje = "Operación no se Realizó, SetPoint Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}
