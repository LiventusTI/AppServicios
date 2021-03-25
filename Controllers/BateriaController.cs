using CrystalDecisions.CrystalReports.Engine;
using GemBox.Spreadsheet;
using Newtonsoft.Json;
using Plataforma.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EdiWeave.Core;
using EdiWeave.Framework;
using EdiWeave.Framework.Readers;
using EdiWeave.Core.Model.Edi.X12;
using EdiWeave.Core.Model.Edi.Edifact;
using EdiWeave.Core.Model.Edi;
//using EdiWeave.Rules.X12_002040;
using static Plataforma.Models.Clases;
using Plataforma.Models.Commodity;
using System.Globalization;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Net;
using Plataforma.Models.Usuario;
using Plataforma.Models.Bateria;

using ExcelDataReader;

namespace Plataforma.Controllers
{
    public class BateriaController : Controller
    {
        public ActionResult VisualizarBaterias()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }
        public ActionResult CargarBaterias()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }
        public JsonResult GetBaterias()
        {
            List<Clases.Bateria> Baterias = new List<Clases.Bateria>();
            Baterias = BateriaModel.GetBaterias();
            Baterias = BateriaModel.GetAlertasBaterias(Baterias);
            var resultados = Json(Baterias, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public string GetBateriaByControlador(string Controlador)
        {
            string Bateria = "";
            Bateria = BateriaModel.GetBateriaByControlador(Controlador);
            return Bateria;
        }

        public string GetBateriaByModem(string Modem)
        {
            string Bateria = "";
            Bateria = BateriaModel.GetBateriaByModem(Modem);
            return Bateria;
        }

        public string GetBateriasCargadas()
        {
            List<Clases.Bateria> Baterias = new List<Clases.Bateria>();
            Baterias = BateriaModel.GetBateriasCargadas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Baterias);
            return datos;
        }

        public string GetEquipoAsociado(string NumBateria)
        {
            Clases.Bateria Baterias = new Clases.Bateria();
            Baterias = BateriaModel.GetEquipoAsociado(NumBateria);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Baterias);
            return datos;
        }

        public string GetEquipoAsociadoById(int IdBateria)
        {
            Clases.Bateria Baterias = new Clases.Bateria();
            Baterias = BateriaModel.GetEquipoAsociadoById(IdBateria);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Baterias);
            return datos;
        }

        public string GetEquipoAsociadoByNumBateria(string NumBateria = "")
        {
            Clases.Bateria Baterias = new Clases.Bateria();
            Baterias = BateriaModel.GetEquipoAsociadoByNumBateria(NumBateria);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Baterias);
            return datos;
        }
        public int DescartarBateria(string NumBateria)
        {
            int retorno = 0;

            retorno = BateriaModel.DescartarBateria(NumBateria);
            return retorno;
        }

        public string ValidarBateria(string NumBateria = "")
        {
            int Estado = 0;
            Clases.Validar aux = new Clases.Validar();
            Estado = BateriaModel.ValidarBateria(NumBateria);
            if (Estado == 1)
            {
                aux.Mensaje = "Bateria Validada.";
                aux.validador = Estado;
            }
            else if (Estado == 2)
            {
                aux.Mensaje = "Bateria Rechazada. Desea igualmente asignarla a un equipo?";
                aux.validador = Estado;
            }
            else if (Estado == 3)
            {
                aux.Mensaje = "Bateria descartada, no se puede asociar a un equipo.";
                aux.validador = Estado;
            }
            else
            {
                aux.Mensaje = "Bateria no existe en los registros de la aplicación.";
                aux.validador = Estado; 
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
        

        [HttpPost]
        public string AsociarBateria(string NumBateria = "", string ESN = "", string TipoESN = "", DateTime? FechaAsociacion = null)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            Flag = BateriaModel.AsociarBateria(NumBateria, ESN, TipoESN, FechaAsociacion);
            if (Flag == 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
                BateriaModel.VerificarEstadoBateriaAlerta(NumBateria);
                EnviarCorreoAlertaBateria(NumBateria, ESN, TipoESN);
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
        public string DesasociarBateria(string NumBateria = "", string ESN = "", string TipoESN = "")
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            Flag = BateriaModel.DesasociarBateria(NumBateria, ESN, TipoESN);
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
        public string AsociarBateriaPorId(int IdBateria = 0, string ESN = "", string TipoESN = "", DateTime? FechaAsociacion = null)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            Flag = BateriaModel.AsociarBateriaPorId(IdBateria, ESN, TipoESN, FechaAsociacion);
            if (Flag == 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;
                BateriaModel.VerificarEstadoBateriaAlertaPorId(IdBateria);
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
        public string LeerExcelBaterias()
        {
            List<Clases.Bateria> Baterias = new List<Clases.Bateria>();
            List<string> BateriasError = new List<string>();
            string datos = "";
            if (Request.Files["resultadoPruebaBateria"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["resultadoPruebaBateria"].FileName).ToLower();

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["resultadoPruebaBateria"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["resultadoPruebaBateria"].SaveAs(path1);
                    try
                    {
                        using (var stream = System.IO.File.Open(path1, FileMode.Open, FileAccess.Read))
                        {
                            // Auto-detect format, supports:
                            //  - Binary Excel files (2.0-2003 format; *.xls)
                            //  - OpenXml Excel files (2007 format; *.xlsx)
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                // Choose one of either 1 or 2:
                                int cont = 0;
                                // 1. Use the reader methods
                                do
                                {
                                    while (reader.Read())
                                    {
                                        // reader.GetDouble(0);
                                        Bateria Bateria = new Bateria();
                                        Bateria.UsuarioPrueba = reader[0].ToString();
                                        Bateria.NumBateria = reader[1].ToString();
                                        Bateria.TensionVacio = float.Parse(reader[2].ToString());
                                        Bateria.Valor = Convert.ToInt32(reader[3].ToString());
                                        Bateria.TensionCarga = float.Parse(reader[4].ToString());
                                        Bateria.DiferenciaVoltaje = float.Parse(reader[5].ToString());
                                        Bateria.Resultado = reader[6].ToString();

                                        if (Convert.ToInt32(Bateria.NumBateria) > 500000)
                                        {
                                            Bateria.Modelo = "HOWELL";
                                        }
                                        else
                                        {
                                            Bateria.Modelo = "TENERGY";
                                        }

                                        if (Bateria.Modelo == "HOWELL")
                                        {
                                            if (Bateria.TensionVacio >= 10.2 && Bateria.DiferenciaVoltaje <= 3.5)
                                            {
                                                Bateria.Estado = "APROBADA";
                                            }
                                            else
                                            {
                                                Bateria.Estado = "RECHAZADA";
                                            }
                                        }
                                        else
                                        {
                                            if (Bateria.TensionVacio >= 10.5 && Bateria.DiferenciaVoltaje < 1.2)
                                            {
                                                Bateria.Estado = "APROBADA";
                                            }
                                            else
                                            {
                                                Bateria.Estado = "RECHAZADA";
                                            }
                                        }

                                        Baterias.Add(Bateria);
                                    }
                                } while (reader.NextResult());

                                // 2. Use the AsDataSet extension method
                                var result = reader.AsDataSet();

                                // The result of each spreadsheet is in result.Tables
                            }
                        }

                        int retorno = BateriaModel.GuardarBaterias(Baterias);

                        //ViewBag.ControladoresError = ControladoresError;
                        datos = Newtonsoft.Json.JsonConvert.SerializeObject(Baterias);
                        return datos;
                    }
                    catch (Exception ex)
                    {
                        return Newtonsoft.Json.JsonConvert.SerializeObject("Error en lectura de archivo");
                    }

                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject("Error de formato");
                }
            }
            //ViewBag.ControladoresError = ControladoresError;
            datos = Newtonsoft.Json.JsonConvert.SerializeObject(Baterias);
            return datos;
        }

        public static void EnviarCorreoAlertaBateria(string NumBateria, string TipoESN = "", string ESN = "")
        {
            string Correo = "";
            Clases.Bateria bateria = BateriaModel.ObtenerEstadoBateria(NumBateria);

            if (bateria.Modelo == "HOWELL" && TipoESN == "CONTROLADOR")
            {
                Correo += "<tr>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.NumBateria + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.Estado + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.TipoESN + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.ESN + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.FechaAsociacion + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.UsuarioAsociacion + "</td>" +
                       "</tr>";

                /*-------------------------GENERACIÓN MENSAJE DEL CORREO----------------------*/
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Destinatario
                //mmsg.To.Add("castorga@liventusglobal.com");
                //mmsg.To.Add("mvaras@liventusglobal.com");
                ////mmsg.To.Add("ymontecinos@liventusglobal.com");
                //mmsg.To.Add("mgalvez@liventusglobal.com");
                //mmsg.To.Add("jruiz@liventusglobal.com");
                mmsg.Bcc.Add("rcontreras@liventusglobal.com");
                //mmsg.Bcc.Add("jfuentealba@liventusglobal.com");
                ////mmsg.Bcc.Add("rcoulon@liventusglobal.com");
                //mmsg.Bcc.Add("baros@liventusglobal.com");
                //mmsg.Bcc.Add("cazocar@liventusglobal.com");
                //Asunto del Correo
                mmsg.Subject = "Alerta automática - Batería Howell asociada a Controlador - Liventus S.A.";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje Alerta

                mmsg.Body = "<p>Estimados:<p/>" +
                            "<p>Se ha asignado una batería de modelo Howell a un controlador, la información es la siguiente:</p>" +
                            "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                "<thead >" +
                                    "<tr>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BATERÍA</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ESTADO</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TIPO EQUIPO ASOCIADO</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ESN EQUIPO ASOCIADO</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>FECHA ASOCIACIÓN</th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>USUARIO ASOCIACIÓN</th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" + Correo +
                                "</tbody>" +
                            "</table>" +
                            "<p>Favor verificar la información enviada y gestionar los procedimientos correspondientes. Gracias. </p>";

                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress("appservicios@liventusglobal.com");

                /*-------------------------CLIENTE DE CORREO----------------------*/

                //Creamos un objeto de cliente de correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Hay que crear las credenciales del correo emisor
                cliente.Credentials = new System.Net.NetworkCredential("appservicios@liventusglobal.com", "Huc01455");

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
                cliente.Port = 587;
                cliente.EnableSsl = true;

                //Para Gmail "smtp.gmail.com";
                cliente.Host = "outlook.office365.com";


                /*-------------------------ENVIO DE CORREO----------------------*/

                try
                {
                    //Enviamos el mensaje      
                    cliente.Send(mmsg);
                    //aux.Mensaje = "Operación Realizada Correctamente";
                    //aux.validador = 0;
                    //datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                    //return datos;
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    //Aquí gestionamos los errores al intentar enviar el correo
                    //aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                    //aux.validador = 1;
                    //datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                    //return datos;
                }
            }

            if (bateria.Estado == "RECHAZADA")
            {
                Correo += "<tr>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.NumBateria + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.Estado + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.TipoESN + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.ESN + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.FechaAsociacion + "</td>" +
                           "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + bateria.UsuarioAsociacion + "</td>" +
                       "</tr>";

                /*-------------------------GENERACIÓN MENSAJE DEL CORREO----------------------*/
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Destinatario
                //mmsg.To.Add("castorga@liventusglobal.com");
                //mmsg.To.Add("mvaras@liventusglobal.com");
                ////mmsg.To.Add("ymontecinos@liventusglobal.com");
                //mmsg.To.Add("mgalvez@liventusglobal.com");
                //mmsg.To.Add("jruiz@liventusglobal.com");
                mmsg.Bcc.Add("rcontreras@liventusglobal.com");
                //mmsg.Bcc.Add("jfuentealba@liventusglobal.com");
                ////mmsg.Bcc.Add("rcoulon@liventusglobal.com");
                //mmsg.Bcc.Add("baros@liventusglobal.com");
                //mmsg.Bcc.Add("cazocar@liventusglobal.com");
                //Asunto del Correo
                mmsg.Subject = "Alerta automática - Batería Rechaza Asociada a Equipo - Liventus S.A.";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje Alerta

                mmsg.Body = "<p>Estimados:<p/>" +
                            "<p>Las baterias siguientes baterías están con estado rechazada y fueron asociadas a equipos:</p>" +
                            "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                "<thead >" +
                                    "<tr>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BATERÍA</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ESTADO</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TIPO EQUIPO ASOCIADO</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ESN EQUIPO ASOCIADO</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>FECHA ASOCIACIÓN</th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>USUARIO ASOCIACIÓN</th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" + Correo +
                                "</tbody>" +
                            "</table>" +
                            "<p>Favor verificar la información enviada y gestionar los procedimientos correspondientes. Gracias. </p>";

                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress("appservicios@liventusglobal.com");

                /*-------------------------CLIENTE DE CORREO----------------------*/

                //Creamos un objeto de cliente de correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Hay que crear las credenciales del correo emisor
                cliente.Credentials = new System.Net.NetworkCredential("appservicios@liventusglobal.com", "Huc01455");

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
                cliente.Port = 587;
                cliente.EnableSsl = true;

                //Para Gmail "smtp.gmail.com";
                cliente.Host = "outlook.office365.com";


                /*-------------------------ENVIO DE CORREO----------------------*/

                try
                {
                    //Enviamos el mensaje      
                    cliente.Send(mmsg);
                    //aux.Mensaje = "Operación Realizada Correctamente";
                    //aux.validador = 0;
                    //datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                    //return datos;
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    //Aquí gestionamos los errores al intentar enviar el correo
                    //aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                    //aux.validador = 1;
                    //datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                    //return datos;
                }
            }

        }

    }
}