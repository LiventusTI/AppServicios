using Plataforma.Models;
using Plataforma.Models.Sensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class SensorController : Controller
    {

        public ActionResult GestionarLogisticaSensores()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public JsonResult GetSensores()
        {
            List<Clases.Sensor> Sensores = new List<Clases.Sensor>();
            Sensores = SensorModel.GetSensores();
            var resultados = Json(Sensores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult FiltrarSensores(int Pais=0, int Ciudad=0, int TipoNodo=0, int Nodo=0, int Marca=0, int Modelo=0)
        {
            List<Clases.Sensor> Sensores = new List<Clases.Sensor>();
            Sensores = SensorModel.FiltrarSensores(Pais, Ciudad, TipoNodo, Nodo, Marca, Modelo);
            var resultados = Json(Sensores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public string GetMarcaSensor()
        {
            List<Clases.MarcaSensor> Marcas= new List<Clases.MarcaSensor>();
            Marcas = SensorModel.GetMarcaSensor();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Marcas);
            return datos;
        }

        public string GetModeloSensor()
        {
            List<Clases.ModeloSensor> Modelos = new List<Clases.ModeloSensor>();
            Modelos = SensorModel.GetModeloSensor();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Modelos);
            return datos;
        }

        [HttpPost]
        public string AgregarSensor(string NumeroSerie = "", int Marca=0, int Modelo=0)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = SensorModel.AgregarSensor(NumeroSerie, Marca, Modelo);
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
        public string AgregarMovSensor(string NumeroSerie = "", int TipoNodoDestino = 0, int NodoDestino = 0)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = SensorModel.AgregarMovSensor(NumeroSerie, TipoNodoDestino, NodoDestino);
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
    }
}