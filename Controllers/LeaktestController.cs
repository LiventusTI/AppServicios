using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Plataforma.Models;
using Plataforma.Models.Bateria;
using Plataforma.Models.Commodity;
using Plataforma.Models.Contenedor;
using Plataforma.Models.Leaktest;
using Plataforma.Models.Maquinaria;
using Plataforma.Models.Reservas;
using Plataforma.Models.ServiceProvider;
using Plataforma.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class LeaktestController : Controller
    {
        // GET: Leaktest
        public ActionResult AgregarReservaLeaktest()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarLeaktest()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult ConsultarResultadosLeaktest(int IdReserva, string TipoReserva, int Estado)
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            ViewBag.TipoReserva = TipoReserva.Replace(" ", "");
            ViewBag.IdReserva = IdReserva;
            ViewBag.Estado = Estado;
            return View();
        }

        public ActionResult VerResultadosHistoricos()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GenerarLeaktestReport(int Solicitud)
        {

            Clases.ReservaLeaktestCompleta ReservasLeaktest = new Clases.ReservaLeaktestCompleta();
            ReservasLeaktest = LeaktestModelo.GetInfoSolicitudLeaktest(Solicitud);
            Clases.ResultadosLeaktest DetalleReservasLeaktest = new Clases.ResultadosLeaktest();
            ReportDocument rd = new ReportDocument();
            ConnectionInfo crConnectionInfo;
            Database crDatabase;
            Tables crTables;
            TableLogOnInfo crTableLogOnInfo;
            DetalleReservasLeaktest = LeaktestModelo.GetResultadosLeaktest(Solicitud);
            int cuentaresultados = DetalleReservasLeaktest.Resultados.Count();
            int j = 1;
            rd.Load(Path.Combine(Server.MapPath("~/Informes"), "CrystalReport1.rpt"));
            rd.SetParameterValue("SOLICITUD", Solicitud);
            rd.SetParameterValue("NAVIERA", ReservasLeaktest.Reservas[0].NombreNaviera);
            rd.SetParameterValue("DEPOSITO", ReservasLeaktest.Reservas[0].NombreDeposito);

            crDatabase = rd.Database;
            crTables = crDatabase.Tables;
            crConnectionInfo = new ConnectionInfo();
            crConnectionInfo.DatabaseName = "AplicacionWebServicios";
            //crConnectionInfo.DatabaseName = "CapacitacionSP";
            //crConnectionInfo.DatabaseName = "PruebaLiventusAplicacionServicio";
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "SuperUsuario716";
            foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
            {
                crTableLogOnInfo = aTable.LogOnInfo;
                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                aTable.ApplyLogOnInfo(crTableLogOnInfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
           
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "LeakTestReport_" + Solicitud + ".pdf");
        }

        public ActionResult GenerarLeaktestReportSP(int Solicitud)
        {

            Clases.ReservaLeaktestCompleta ReservasLeaktest = new Clases.ReservaLeaktestCompleta();
            ReservasLeaktest = LeaktestModelo.GetInfoSolicitudLeaktestSP(Solicitud);
            ReportDocument rd = new ReportDocument();
            ConnectionInfo crConnectionInfo;
            Database crDatabase;
            Tables crTables;
            TableLogOnInfo crTableLogOnInfo;
            int j = 1;
            rd.Load(Path.Combine(Server.MapPath("~/Informes"), "CrystalReport2.rpt"));
            rd.SetParameterValue("SOLICITUD", Solicitud);
            rd.SetParameterValue("NAVIERA", ReservasLeaktest.Reservas[0].NombreNaviera);
            rd.SetParameterValue("DEPOSITO", ReservasLeaktest.Reservas[0].NombreDeposito);
            rd.SetParameterValue("SERVICEPROVIDER", ReservasLeaktest.Reservas[0].NombreServiceProvider);

            crDatabase = rd.Database;
            crTables = crDatabase.Tables;
            crConnectionInfo = new ConnectionInfo();
            crConnectionInfo.DatabaseName = "AplicacionWebServicios";
            //crConnectionInfo.DatabaseName = "AplicacionWebServicios7";
            //crConnectionInfo.DatabaseName = "CapacitacionSP";
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "SuperUsuario716";
            foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
            {
                crTableLogOnInfo = aTable.LogOnInfo;
                crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                aTable.ApplyLogOnInfo(crTableLogOnInfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "LeakTestReport_" + Solicitud + ".pdf");
        }

        public ActionResult GenerarLeaktestReportVacio(string Naviera, string Deposito, string Solicitud)
        {
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Informes"), "LeaktestReport.rpt"));
            rd.SetParameterValue("deposito", Deposito);
            rd.SetParameterValue("naviera", Naviera);
            rd.SetParameterValue("Solicitud", Solicitud);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "LeakTestReport_" + Solicitud + ".pdf");
        }

        public ActionResult IngresarResultadoLeaktest(int IdReserva, string Maquinaria, int Cantidad)
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            ViewBag.Cantidad = Cantidad;
            return View();
        }

        public ActionResult VisualizarReservasLeaktest()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearLeaktest(int Naviera, int Deposito, DateTime Eta, int Estado, int Maquinaria, int Cantidad, int tiporeserva, string Hora = "", int CantidadScrubber = 0, int Maquinaria1 = 0, int Cantidad1 = 0, int Maquinaria2 = 0, int Cantidad2 = 0, int Maquinaria3 = 0, int Cantidad3 = 0, string Comentario = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.CrearLeaktest(Naviera, Deposito, HoraFecha(Eta.ToString()), Estado, Maquinaria, Cantidad, Maquinaria1, Cantidad1, Maquinaria2, Cantidad2, Maquinaria3, Cantidad3, tiporeserva, Hora, CantidadScrubber, Comentario);
            if (Flag == -1)
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;

            }
            else
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string EditarCelda(int IdResultado, string Campo, string Valor, string Columna, string BateriaControlador = "", DateTime? FechaAsociacionBateriaControlador = null, string BateriaModem = "", DateTime? FechaAsociacionBateriaModem = null)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.EditarCelda(IdResultado, Campo, Valor, Columna, BateriaControlador, FechaAsociacionBateriaControlador, BateriaModem, FechaAsociacionBateriaModem);
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
        public string GetLeaktest()
        {
            Clases.ReservaLeaktestCompleta ReservasLeaktest = new Clases.ReservaLeaktestCompleta();
            ReservasLeaktest = LeaktestModelo.GetLeaktest();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ReservasLeaktest);
            return datos;
        }

        // POST: Leaktest/Delete/5
        [HttpPost]
        public string CancelarReservaLeaktest(int IdReserva, string Motivo, string TipoReserva)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.CancelarReservaLeaktest(IdReserva, Motivo, TipoReserva);
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
        public string DetalleReservaLeaktest(int IdReserva)
        {
            Clases.DetalleReservaLeaktest DetalleReservasLeaktest = new Clases.DetalleReservaLeaktest();
            DetalleReservasLeaktest = LeaktestModelo.GetDetalleReservaLeaktest(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(DetalleReservasLeaktest);
            return datos;
        }

        [HttpPost]
        public string GetDetalleReservasLeaktest()
        {
            Clases.DetalleReservaLeaktest DetalleReservasLeaktest = new Clases.DetalleReservaLeaktest();
            DetalleReservasLeaktest = LeaktestModelo.GetDetalleReservasLeaktest();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(DetalleReservasLeaktest);
            return datos;
        }

        [HttpPost]
        public string GetResultadosLeaktest(int IdReserva)
        {
            Clases.ResultadosLeaktest DetalleReservasLeaktest = new Clases.ResultadosLeaktest();
            DetalleReservasLeaktest = LeaktestModelo.GetResultadosLeaktest(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(DetalleReservasLeaktest);
            ViewBag.IdReserva = IdReserva;
            return datos;
        }

        [HttpPost]
        public string InsertarResultadoLeaktest(string contenedor, int cajacontenedor, int anocontenedor, int serviceprovider, int tecnico, string presion, DateTime fechaejecucion, string controlador, string bateria = "", int maquinaria = 0, int idRerserva = 0, int TipoReserva = 0, int CantidadScrubber = 0, string Selloperno1 = "", string Selloperno2 = "", string Sellotapa = "", string Comentario = "", int KitCortina = 0, string Modem = "", string BateriaModem = ""/*, string modem = ""*/)
        {
            Clases.Validar aux = new Clases.Validar();
            int estado = -1;

            float presion_float = float.Parse(presion, CultureInfo.InvariantCulture.NumberFormat);
            presion_float = (float)Math.Round(presion_float * 100f) / 100f;

            if (presion_float < 3)
            {
                estado = 1;
            }
            else
            {
                estado = 0; // Leaktest Validado
            }

            if (presion_float == -1)
            {
                estado = 3;
            }

            int Flag = LeaktestModelo.CrearResultadoLeaktest(contenedor, cajacontenedor, anocontenedor, serviceprovider, tecnico, presion_float, HoraFecha(fechaejecucion.ToString()), controlador, bateria/*, modem*/, maquinaria, idRerserva, estado, TipoReserva, CantidadScrubber, Selloperno1, Selloperno2, Sellotapa, Comentario, KitCortina, Modem, BateriaModem);
            if (Flag == 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            else if (Flag == 7)
            {
                aux.Mensaje = "Operación no se Realizó, Contenedor Ya está Reservado.";
                aux.validador = Flag;
            }
            else if (Flag == 4)
            {
                aux.Mensaje = "Operación no se Realizó, Contenedor Esta en Viaje.";
                aux.validador = Flag;
            }
            else if (Flag == 3)
            {
                aux.Mensaje = "Operación no se Realizó, Contenedor Esta Con Leaktest Aprobado.";
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
        public string ValidarMaquinaria(int IdReserva, int Maquinaria)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.ValidarMaquinaria(IdReserva, Maquinaria);
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
        public string ValidarCantidadResultados(int IdReserva, int Maquinaria)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.ValidarCantidadResultados(IdReserva, Maquinaria);
            if (Flag != 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = 0;
            }
            else
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = 1;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string ValidarCantidadResultadosPorAprobar(int IdReserva, int Maquinaria)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.ValidarCantidadResultadosPorAprobar(IdReserva, Maquinaria);
            if (Flag != 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = 0;
            }
            else
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = 1;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetComentarios(int IdResultado)
        {
            Clases.ListaComentarios Comentarios = new Clases.ListaComentarios();
            Comentarios = LeaktestModelo.GetComentarios(IdResultado);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Comentarios);
            return datos;
        }

        [HttpPost]
        public string GetArchivos(int IdResultado)
        {
            Clases.ListaArchivos Archivos = new Clases.ListaArchivos();
            Archivos = LeaktestModelo.GetArchivos(IdResultado);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Archivos);
            return datos;
        }

        [HttpPost]
        public string AgregarNotaResultado(int IdResultado, string Comentario)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.AgregarNotaResultado(IdResultado, Comentario);
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
        public string AgregarLeaktestSP(string Estado="",int AnoContenedor=0, string Tiempo="", DateTime? FechaEjecucion=null, int ServiceProvider=0, int Deposito=0, int Naviera=0, int Commodity=0, DateTime? FechaEstimada=null, string HoraEstimada="", int Tecnico=0, int Maquinaria=0, int Cantidad=0, int Maquinaria1=0, int Cantidad1=0, int Maquinaria2=0, int Cantidad2=0, int Maquinaria3=0, int Cantidad3=0)
        {
            Clases.Validar aux = new Clases.Validar();

            if (FechaEstimada != null)
            {
                FechaEstimada = HoraFecha(FechaEstimada.ToString());
            }
            else {
                FechaEstimada = null;
            }

            if (FechaEjecucion != null)
            {
                FechaEjecucion = HoraFecha(FechaEjecucion.ToString());
            }
            else
            {
                FechaEjecucion = null;
            }

            int Flag = LeaktestModelo.AgregarLeaktestSP(Estado,AnoContenedor, Tiempo, FechaEjecucion, ServiceProvider, Deposito, Naviera, Commodity, FechaEstimada, HoraEstimada, Tecnico, Maquinaria, Cantidad, Maquinaria1, Cantidad1, Maquinaria2, Cantidad2, Maquinaria3, Cantidad3);
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
        public string EliminarNotaLeaktest(int IdNota)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.EliminarNotaLeaktest(IdNota);
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
        public string GetResultadoById(int IdResultado)
        {
            Clases.ResultadoLeaktest ResultadoLeaktest = new Clases.ResultadoLeaktest();
            ResultadoLeaktest = LeaktestModelo.GetResultadoById(IdResultado);
            ResultadoLeaktest.CajaContenedor = ContenedorModelo.GetIdCajaContenedor(ResultadoLeaktest.CajaContenedor).ToString();
            ResultadoLeaktest.IdServiceProvider = ServiceProviderModel.GetIdServiceProvider2(ResultadoLeaktest.IdServiceProvider).ToString();
            ResultadoLeaktest.IdTecnico = UsuarioModelo.GetIdTecnico(ResultadoLeaktest.IdTecnico).ToString();
            ResultadoLeaktest.IdMaquinaria = MaquinariaModelo.GetIdMaquinaria2(ResultadoLeaktest.IdMaquinaria).ToString();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ResultadoLeaktest);
            return datos;
        }

        [HttpPost]
        public string GetResultadoLeaktestByContenedor(int IdServicio)
        {
            List<Clases.ReservaResultadoLeaktest> Resultados = new List<Clases.ReservaResultadoLeaktest>();
            Resultados = LeaktestModelo.GetResultadoLeaktestByContenedor(IdServicio);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string GetResultadoLeaktestByIdContenedor(int IdContenedor)
        {
            List<Clases.ReservaResultadoLeaktest> Resultados = new List<Clases.ReservaResultadoLeaktest>();
            Resultados = LeaktestModelo.GetResultadoLeaktestByIdContenedor(IdContenedor);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string EditarResultadoLeaktest(string contenedor, int cajacontenedor, int anocontenedor, int serviceprovider, int tecnico, string presion, DateTime fechaejecucion, string controlador, string bateria = "", int maquinaria = 0, int idRerserva = 0, int IdResultado = 0, int TipoReserva = 0, int CantidadScrubber = 0, string Selloperno1 = "", string Selloperno2 = "", string Sellotapa = "", string Comentario = "", int KitCortina = 0, string Modem = "", string BateriaModem = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int estado = -1;

            float presion_float = float.Parse(presion, CultureInfo.InvariantCulture.NumberFormat);
            presion_float = (float)Math.Round(presion_float * 100f) / 100f;

            if (presion_float < 3)
            {
                estado = 1;
            }
            else
            {
                estado = 0; // Leaktest Validado
            }

            if (presion_float == -1)
            {
                estado = 3;
            }

            int Flag = LeaktestModelo.EditarResultadoLeaktest(contenedor, cajacontenedor, anocontenedor, serviceprovider, tecnico, presion_float, HoraFecha(fechaejecucion.ToString()), controlador, bateria, maquinaria, idRerserva, estado, IdResultado, TipoReserva, CantidadScrubber, Selloperno1, Selloperno2, Sellotapa, Comentario, KitCortina, Modem, BateriaModem);
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
        public string GetReservaById(int IdReserva)
        {
            List<Clases.DetalleReservaLeaktestEditar> Detalles = new List<Clases.DetalleReservaLeaktestEditar>();
            Detalles = LeaktestModelo.GetReservaById(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Detalles);
            return datos;
        }

        [HttpPost]
        public string EditarReservaLeaktest(int IdReserva, int Naviera, int Deposito, DateTime Eta, int Maquinaria = 0, string Hora = "", int Cantidad = 0, int CantidadScrubber = 0, int Maquinaria1 = 0, int Cantidad1 = 0, int Maquinaria2 = 0, int Cantidad2 = 0, int Maquinaria3 = 0, int Cantidad3 = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.EditarReservaLeaktest(IdReserva, Naviera, Deposito, Eta, Maquinaria, Cantidad, Maquinaria1, Cantidad1, Maquinaria2, Cantidad2, Maquinaria3, Cantidad3, Hora, CantidadScrubber);
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
        public string GetAllComentarios(int IdResultado)
        {
            Clases.ListaComentarios Comentarios = new Clases.ListaComentarios();
            Comentarios = LeaktestModelo.GetComentarios(IdResultado);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Comentarios);
            return datos;
        }

        [HttpPost]
        public string GetAllResultados()
        {
            List<Clases.ResultadoLeaktestAll> Resultados = new List<Clases.ResultadoLeaktestAll>();
            Resultados = LeaktestModelo.GetAllResultados();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }
        public JsonResult GetAllResultadosSP()
        {
            List<Clases.ResultadoLeaktestAllSP> Resultados = new List<Clases.ResultadoLeaktestAllSP>();
            Resultados = LeaktestModelo.GetAllResultadosSP();
            var datos = Json(Resultados, JsonRequestBehavior.AllowGet);
            datos.MaxJsonLength = Int32.MaxValue;
            return datos;
        }

        public string GetAllHistoricoResultadosSP(int ResultadosSP)
        {
            List<Clases.ResultadoLeaktestAllSP> Resultados = new List<Clases.ResultadoLeaktestAllSP>();
            Resultados = LeaktestModelo.GetAllHistoricoResultadosSP(ResultadosSP);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string AgregarArchivoResultado()
        {
            Clases.Validar aux = new Clases.Validar();
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var archivo = System.Web.HttpContext.Current.Request.Files["archivo"];
                    var IdResultado = System.Web.HttpContext.Current.Request["IdResultado"];
                    HttpPostedFileBase filebase = new HttpPostedFileWrapper(archivo);
                    var fileName = Path.GetFileName(filebase.FileName);
                    if (!Directory.Exists(Server.MapPath("~/Resultados/" + IdResultado)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Resultados/" + IdResultado));
                    }
                    var path = Path.Combine(Server.MapPath("~/Resultados/" + IdResultado), fileName);
                    if (!Directory.Exists(path))
                    {
                        int Flag = LeaktestModelo.AgregarArchivoLeaktest(Convert.ToInt32(IdResultado), fileName);
                        if (Flag == 0)
                        {
                            aux.Mensaje = "Operación Realizada Correctamente";
                            aux.validador = Flag;
                            filebase.SaveAs(path);
                        }
                        else
                        {
                            aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                            aux.validador = Flag;
                        }

                    }
                    else {
                        aux.Mensaje = "Operación no se Realizó, Archivo Ya Existente.";
                        aux.validador = 1;
                    }
                }
                else {
                    aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                    aux.validador = 1;
                }
            }
            catch (Exception ex) {
                aux.Mensaje = ex.ToString();
                aux.validador = 1;
            }

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string EliminarArchivoLeaktest(int IdArchivo, int IdResultado, string NombreArchivo)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.EliminarArchivoLeaktest(IdArchivo);
            if (Flag == 0)
            {
                var path = Path.Combine(Server.MapPath("~/Resultados/" + IdResultado), NombreArchivo);
                if (!Directory.Exists(path))
                {
                    System.IO.File.Delete(path);
                    aux.Mensaje = "Operación Realizada Correctamente";
                    aux.validador = Flag;
                }
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
        public string GetEstadosReserva()
        {
            List<Clases.EstadoReservaLeaktest> Estados = new List<Clases.EstadoReservaLeaktest>();
            Estados = LeaktestModelo.GetEstadosReserva();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Estados);
            return datos;
        }

        [HttpPost]
        public string EliminarResultado(int IdResultado, int TipoReserva, int IdReserva)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.EliminarResultado(IdResultado, TipoReserva, IdReserva);
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
        public string GetReservasByFechaCreacion(DateTime FechaInicial, DateTime FechaFin)
        {
            Clases.ReservaLeaktestCompleta ReservasLeaktest = new Clases.ReservaLeaktestCompleta();
            ReservasLeaktest = LeaktestModelo.GetReservasByFechaCreacion(FechaInicial, FechaFin);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ReservasLeaktest);
            return datos;
        }

        [HttpPost]
        public string GetReservasByFechaEstimada(DateTime FechaInicial, DateTime FechaFin)
        {
            Clases.ReservaLeaktestCompleta ReservasLeaktest = new Clases.ReservaLeaktestCompleta();
            ReservasLeaktest = LeaktestModelo.GetReservasByFechaEstimada(FechaInicial, FechaFin);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ReservasLeaktest);
            return datos;
        }

        [HttpPost]
        public string GetResultadosByFecha(DateTime FechaInicial, DateTime FechaFin, int IdReserva)
        {
            Clases.ResultadosLeaktest DetalleReservasLeaktest = new Clases.ResultadosLeaktest();
            DetalleReservasLeaktest = LeaktestModelo.GetResultadosByFecha(FechaInicial, FechaFin, IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(DetalleReservasLeaktest);
            ViewBag.IdReserva = IdReserva;
            return datos;
        }

        [HttpPost]
        public string GetAllResultadosByFecha(DateTime FechaInicial, DateTime FechaFin)
        {
            List<Clases.ResultadoLeaktestAll> Resultados = new List<Clases.ResultadoLeaktestAll>();
            Resultados = LeaktestModelo.GetAllResultadosByFecha(FechaInicial, FechaFin);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string CrearLeaktestContenedores(List<string> Contenedores, List<int> Maquinarias , int Naviera, int Deposito, DateTime Eta, int Estado, int tiporeserva, string Hora = "", int CantidadScrubber = 0, string Comentario = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.CrearLeaktestContenedores(Contenedores, Maquinarias, Naviera, Deposito, HoraFecha(Eta.ToString()), Estado, tiporeserva, Hora, CantidadScrubber, Comentario);
            if (Flag != -1)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
            }
            else if (Flag == -1)
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con Administrador.";
                aux.validador = Flag;
            }
           
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string DetalleReservaLeaktestContenedores(int IdReserva)
        {
            List<Clases.DetalleContenedor> Resultados = new List<Clases.DetalleContenedor>();
            Resultados = LeaktestModelo.DetalleReservaLeaktestContenedores(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string GetTipoSolicitud()
        {
            List<Clases.TipoSolicitudLeaktest> Tipos = new List<Clases.TipoSolicitudLeaktest>();
            Tipos  = LeaktestModelo.GetTipoSolicitud();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Tipos);
            return datos;
        }

        [HttpPost]
        public string EditarReservaLeaktestContenedores(int IdReserva, int IdNaviera, int IdDeposito, DateTime Fecha, List<string> Contenedor, List<string> Maquinaria, string Hora = "", int CantidadScrubber = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            List<Clases.DetalleContenedor> Resultados = new List<Clases.DetalleContenedor>();
            Resultados = LeaktestModelo.GetDetalleReservaContenedores(IdReserva);
            int Flag = LeaktestModelo.EditarReservaLeaktestContenedores(Resultados, IdReserva, Contenedor, Maquinaria, IdNaviera, IdDeposito, HoraFecha(Fecha.ToString()), Hora, CantidadScrubber);
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
        public string GetDetalleReservaContenedores(int IdReserva)
        {
            List<Clases.DetalleContenedor> Resultados = new List<Clases.DetalleContenedor>();
            Resultados = LeaktestModelo.GetDetalleReservaContenedores(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string GetSolicitudes()
        {
            List<Clases.ResultadoLeaktestAll> Resultados = new List<Clases.ResultadoLeaktestAll>();
            Resultados = LeaktestModelo.GetSolicitudes();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultados);
            return datos;
        }

        [HttpPost]
        public string TraspasarResultados(string [] Resultados, int IdAntiguo, int IdNuevo, string [] Estado, string [] Maquinaria, string [] Contenedor)
        {
            Clases.Validar aux = new Clases.Validar();
            List<Clases.Validacion> Validados = new List<Clases.Validacion>();
            int validado = 0;
            int Flag = 1;
            for (int i = 0; i < Resultados.Length; i++) {
                validado = LeaktestModelo.ValidarMaquinaria(IdNuevo, Maquinaria[i]);
                if (validado != 0)
                {
                    Flag = LeaktestModelo.TraspasarResultados(Resultados[i], IdAntiguo, IdNuevo, Estado[i], Maquinaria[i], Contenedor[i]);
                    Validados.Add(new Clases.Validacion
                    {
                        IdServicio = Convert.ToInt32(Resultados[i]),
                        Validado = 0
                    });
                }
                else {
                    Validados.Add(new Clases.Validacion
                    {
                        IdServicio = Convert.ToInt32(Resultados[i]),
                        Validado = 1
                    });
                }
            }

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Validados);
            return datos;
        }

        [HttpPost]
        public string CancelarLeaktestSP(int[] Leaktest)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (var i = 0; i < Leaktest.Count(); i++)
            {
                Flag = LeaktestModelo.CancelarLeaktestSP(Leaktest[i]);
            }

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
        public string DescancelarLeaktestSP(int[] Leaktest)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (var i = 0; i < Leaktest.Count(); i++)
            {
                Flag = LeaktestModelo.DescancelarLeaktestSP(Leaktest[i]);
            }

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
        public string EliminarLeaktestSP(int[] Leaktest)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (var i = 0; i < Leaktest.Count(); i++)
            {
                Flag = LeaktestModelo.EliminarLeaktestSP(Leaktest[i]);
            }

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
        public string ValidarLeaktestSP(int[] Leaktest, string SP, string Deposito, string Naviera)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            int Solicitud = 0;

            Solicitud = LeaktestModelo.IngresarSolicitudLeaktestSP(SP, Deposito, Naviera, Leaktest.Length);
            for (var i = 0; i < Leaktest.Count(); i++)
            {
                Flag = LeaktestModelo.ValidarLeaktestSP(Leaktest[i], Solicitud);
            }

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
        public string EdicionMasivaLeaktest(int[] Leaktest, int ServiceProvider = 0, int Naviera = 0, int Deposito=0, DateTime? FechaEstimada=null, string HoraEstimada="", int Commodity = 0, int Maquinaria=0,
                                   int AnoContenedor=0, int Tecnico =0, string Tiempo="", DateTime? FechaEjecucion=null, int Estado = 0, string Comentario="", int PanelReutilizado=0)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            for (int i = 0; i < Leaktest.Count(); i++)
            {
                Flag = LeaktestModelo.EdicionMasivaLeaktest(Leaktest[i], ServiceProvider, Naviera, Deposito, HoraFecha(FechaEstimada.ToString()), HoraEstimada, Commodity, Maquinaria,
                                   AnoContenedor, Tecnico, Tiempo, HoraFecha(FechaEjecucion.ToString()), Estado, Comentario, PanelReutilizado);
            }
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

        public static DateTime? HoraFecha(string Fecha)
        {
            if (Fecha != "")
            {
                var src = DateTime.Now;
                var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
                var hola = hm.ToString();
                hola = hola.Substring(11);
                var hola2 = Fecha.ToString().Substring(0, 11);
                var fecha = hola2 + hola;
                return Convert.ToDateTime(fecha);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public string QuitarControlador(string Controlador, int IdResultado)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = LeaktestModelo.QuitarControlador(Controlador.Trim(), IdResultado);
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
        public string ValidacionPreTecnica(string[] Resultados, string[] Controladores, string[] Contenedores)
        {
            List<Clases.Validacion> ServiciosValidar = new List<Clases.Validacion>();
            List<Clases.Validacion> Validados = new List<Clases.Validacion>();
            int i, j = 0;
            DateTime? diasmas = null;
            DateTime? diasmenos = null;
            bool valaux = false;
            string bookingaux = "";
            string contenedoraux = "";
            string controladoraux = "";
            string commodityaux = "";
            string fechaaux = "";
            ServiciosValidar = ReservaModelo.ValidarServicio();

            for (j = 0; j < Controladores.Length; j++)
            {
                bookingaux = "";
                contenedoraux = "";
                controladoraux = "";
                commodityaux = "";
                fechaaux = "";

                var Servicio = ServiciosValidar.Where(x => x.controlador.Trim() == Controladores[j].Trim()).FirstOrDefault();
                if (Servicio != null)
                {
                    var commodity = CommodityModelo.GetIdTecnica("AVOCADO HASS 1");

                    if (Servicio.controlador.Trim() == Controladores[j].Trim() && Servicio.Contenedor.Trim() == Contenedores[j].Trim() && Servicio.IdCommodity == commodity)
                    {

                        Validados.Add(new Clases.Validacion
                        {
                            IdResultado = Convert.ToInt32(Resultados[j]),
                            Validado = 0,
                            Controladoraux = "Correcto",
                            Contenedoraux = "Correcto",
                            Commodityaux = "Correcto",
                        });
                        LeaktestModelo.ActualizarPreValidacionTecnica(Convert.ToInt32(Resultados[j]), 0);
                        valaux = true;
                    }
                    else
                    {
                        if (Servicio.controlador.Trim() != Controladores[j].Trim())
                        {
                            controladoraux = "Incorrecto";
                        }
                        else
                        {
                            controladoraux = "Correcto";
                        }
                        if (Servicio.Contenedor.Trim() != Contenedores[j].Trim())
                        {
                            contenedoraux = "Incorrecto";
                        }
                        else
                        {
                            contenedoraux = "Correcto";
                        }
                        if (Servicio.IdCommodity != commodity)
                        {
                            commodityaux = "Incorrecto";
                        }
                        else
                        {
                            commodityaux = "Correcto";
                        }

                        LeaktestModelo.ActualizarPreValidacionTecnica(Convert.ToInt32(Resultados[j]), 1);
                        Validados.Add(new Clases.Validacion
                        {
                            IdResultado = Convert.ToInt32(Resultados[j]),
                            Validado = 1,
                            Controladoraux = controladoraux,
                            Contenedoraux = contenedoraux,
                            Commodityaux = commodityaux,
                        });

                    }
                }
                else
                {
                    LeaktestModelo.ActualizarPreValidacionTecnica(Convert.ToInt32(Resultados[j]), 1);
                    Validados.Add(new Clases.Validacion
                    {
                        IdResultado = Convert.ToInt32(Resultados[j]),
                        Validado = 1,
                        Controladoraux = "Incorrecto",
                        Contenedoraux = "Incorrecto",
                        Commodityaux = "Incorrecto",
                    });
                }
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Validados);
            return datos;
        }

    }

}
