using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plataforma.Models;
using Plataforma.Models.Alerta;

namespace Plataforma.Controllers
{
    public class AlertasController : Controller
    {
        // GET: Alertas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AlertasEnTransito()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public JsonResult GetAlertasEnTransito()
        {
            List<Clases.Alerta> Alertas = new List<Clases.Alerta>();
            Alertas = AlertaModelo.ConsultarAlertasEnTransito();
            var resultados = Json(Alertas, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public string ActivarDesactivarAlertas(int[] lista_alertas)
        {
            Clases.Validar aux = new Clases.Validar();
            List<int> respuestas = new List<int>();
            int respuesta = 0;
            int contador = 0;
            foreach (var id_registro_alerta in lista_alertas)
            {
                respuesta = 0;
                respuesta = AlertaModelo.ActivarDesactivarAlerta(id_registro_alerta);
                respuestas.Add(respuesta);
                contador++;
            }

            int respuesta_final = 0;
            string mensaje_final = "";
            string tipo_mensaje = "";
            string titulo_mensaje = "";

            bool todos_correctos = true;
            foreach (var item in respuestas)
            {
                if (item == 0) todos_correctos = false;
            }

            if (todos_correctos)
            {
                respuesta_final = 1;
                mensaje_final = "La alerta se ha modificado correctamente";
                tipo_mensaje = "success";
                titulo_mensaje = "¡Muy Bien!";
            }
            else
            {
                respuesta_final = 0;
                mensaje_final = "La alerta no logró ser modificada";
                tipo_mensaje = "warning";
                titulo_mensaje = "¡Se produjo un error!";
            }

            aux.validador = respuesta_final;
            aux.Mensaje = mensaje_final;
            aux.tipo_mensaje = tipo_mensaje;
            aux.titulo_mensaje = titulo_mensaje;
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
    }
}