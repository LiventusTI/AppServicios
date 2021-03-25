using CrystalDecisions.CrystalReports.Engine;
using GemBox.Spreadsheet;
using Newtonsoft.Json;
using Plataforma.Models;
using Plataforma.Models.Controlador;
using Plataforma.Models.Deadline;
using Plataforma.Models.Pais;
using Plataforma.Models.Reservas;
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
using Plataforma.Models.Naviera;
using Plataforma.Models.Exportador;
using Plataforma.Models.Puertos;
using Plataforma.Models.Nave;
using Plataforma.Models.Leaktest;
using Plataforma.Models.Contenedor;

namespace Plataforma.Controllers
{
    public class ReservasController : Controller
    {
        // GET: Reservas

        public ActionResult CrearReservas()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult CrearReservasSP()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult CargaMasiva()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarServicios()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarServiciosSP()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult VisualizarHistorico()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult VisualizarHistoricoPorSemanas()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult VisualizarHistoricoSP()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarDepositos()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarDepositos2()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult Facturacion()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult FacturacionHistorica()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarServiciosLegal()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GraficoMediciones()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public JsonResult GetServiciosLegal()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetServiciosLegal();
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public string AprobarServicio(int Servicio)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            int guardo = ReservaModelo.CrearExtension(Servicio);
            string extension = ReservaModelo.GetExtension(Servicio);
            List<string> CorreoCliente = UsuarioModelo.GetInfoClienteByServicio(Servicio);
            string CorreoComercial = UsuarioModelo.GetInfoComercialByServicio(Servicio);
            int Notificado = ReservaModelo.GetNotificado(Servicio);

            if (Notificado == 0)
            {
                Flag = ReservaModelo.AprobarServicio(Servicio, 1);

                string URL = "http://132.255.70.203/GraficoCliente/TestingData.php?Info=" + extension;
                string Correo = "<p>Estimado, </p>" +
                                "<p>A continuación podrá visualizar el gráfico asociado a su servicio: </p>" +
                                "<p>http://132.255.70.203/GraficoCliente/TestingData.php?Info=" + extension + " </p>" +
                                "<p>" + "Este correo se ha generado automáticamente, favor no responder." + "</p>" +
                                "<p style='font-size:75%;'>Confidentiality Notice:</p>" +
                                "<p style='font-size:75%;'>The information contained in this message may be privileged and confidential. If the reader is not the intended recipient, or the agent of the intended recipient, any unauthorized use, disclosure, copying distribution or dissemination is strictly prohibited. If you have received this communication in error, please notify the sender immediately by telephone and return this message to the above address.</p>" +
                                "<p style='font-size:75%;'>This message shall not be construed as any admission of liability, amount, expenses or any issues by LIVENTUS S.A., and is given without prejudice to any rights or defenses available to the owners of LIVENTUS S.A., including the right to limit or reject liability.</p>" +
                                "<p style='font-size:75%;'>In no case may this information be used to establish and/or present a claim against Liventus S.A., its agencies, and / or the Shipping Lines associated with the shipments that are reported.</p>";

                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.Bcc.Add(CorreoComercial);
                mmsg.Bcc.Add("vrios@liventusglobal.com");
                mmsg.Bcc.Add("rcontreras@liventusglobal.com");

                for (int i = 0; i < CorreoCliente.Count(); i++)
                {
                    mmsg.To.Add(CorreoCliente[i]);
                }

                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Información de Servicio - Liventus S.A.";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje
                mmsg.Body = Correo;

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
                    aux.Mensaje = "Operación Realizada Correctamente";
                    aux.validador = Flag;

                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                    aux.validador = Flag;
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
            else
            {
                Flag = ReservaModelo.AprobarServicio(Servicio, Notificado);
                if (Flag == 0)
                {
                    aux.Mensaje = "Operación Realizada Correctamente, ya se había notificado al cliente anteriomente, por lo tanto no se volverá a notificar.";
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

        public string EnviarCorreoAlertaH1(string Booking, int IdNaviera = 0, int IdExportador = 0, int IdCommodity = 0, int IdSetpoint = 0, int IdPuertoOrigen = 0, int IdPuertoDestino = 0, int IdNave = 0, int CantServicios = 0)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            string Naviera = NavieraModelo.GetNombreNaviera(IdNaviera);
            string Exportador = ExportadorModel.GetNombreExportador(IdExportador);
            string Commodity = CommodityModelo.GetNombreCommodity(IdCommodity);
            string Setpoint = SetpointModelo.GetNombreSetpoint(IdSetpoint);
            string PuertoOrigen = PuertoModelo.GetNombrePuerto(IdPuertoOrigen);
            string PuertoDestino = PuertoModelo.GetNombrePuerto(IdPuertoDestino);
            string Nave = NaveModelo.GetNombreNave(IdNave);
            string Tabla = "";
            Tabla += "<tr>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Booking + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Naviera + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Exportador + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Commodity + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Setpoint + "</td>" +
                                         "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + PuertoOrigen + "</td>" +
                                          "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + PuertoDestino + "</td>" +
                                            "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Nave + "</td>" +
                                            "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + CantServicios + "</td>" +
                                    "</tr>";

            string Correo = "<p>Estimados, </p>" +
                                "<p>Se han ingresado las siguientes reservas con commodity distinto a Avocado Hass 1 asociadas a Neptunia: </p>" +
                                "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                    "<thead >" +
                                        "<tr>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BOOKING</ th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>NAVIERA</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>EXPORTADOR</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>COMMODITY</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SETPOINT</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>PUERTO ORIGEN</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>PUERTO DESTINO</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>NAVE</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CANT. SERVICIOS</th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>" + Tabla +
                                    "</tbody>" +
                                "</table>" +
                                "<p>" + "Favor gestionar re-programación del controller asociado." + "</p>";

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add("mvaras@liventusglobal.com");
            mmsg.To.Add("mgalvez@liventusglobal.com");
            mmsg.To.Add("castorga@liventusglobal.com");
            mmsg.To.Add("jruiz@liventusglobal.com");
            //mmsg.To.Add("jpierola@liventusglobal.com");
            //mmsg.To.Add("baros@liventusglobal.com");
            //mmsg.To.Add("rcontreras@liventusglobal.com");
            mmsg.Bcc.Add("rcontreras@liventusglobal.com");

            //Asunto
            mmsg.Subject = "Alerta automática - Reservas ingresadas distintas a Avocado Hass 1 en Perú - Liventus S.A.";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Cuerpo del Mensaje
            mmsg.Body = Correo;

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
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = Flag;

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;
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


        public string DesaprobarServicio(int Servicio)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();

            string extension = ReservaModelo.GetExtension(Servicio);
            List<string> CorreoCliente = UsuarioModelo.GetInfoClienteByServicio(Servicio);
            string CorreoComercial = UsuarioModelo.GetInfoComercialByServicio(Servicio);
            int Notificado = ReservaModelo.GetNotificado(Servicio);

            if (Notificado == 0)
            {
                Flag = ReservaModelo.DesaprobarServicio(Servicio, 1);

                string URL = "http://132.255.70.203/GraficoCliente/TestingData.php?Info=" + extension;
                string Correo = "<p>Estimado, </p>" +
                                "<p>A continuación podrá visualizar el gráfico asociado a su servicio: </p>" +
                                "<p>http://132.255.70.203/GraficoCliente/TestingData.php?Info=" + extension + "</p>" +
                                "<p>" + "Este correo se ha generado automáticamente, favor no responder." + "</p>" +
                                "<p style='font-size:75%;'>Confidentiality Notice:</p>" +
                                "<p style='font-size:75%;'>The information contained in this message may be privileged and confidential. If the reader is not the intended recipient, or the agent of the intended recipient, any unauthorized use, disclosure, copying distribution or dissemination is strictly prohibited. If you have received this communication in error, please notify the sender immediately by telephone and return this message to the above address.</p>" +
                                "<p style='font-size:75%;'>This message shall not be construed as any admission of liability, amount, expenses or any issues by LIVENTUS S.A., and is given without prejudice to any rights or defenses available to the owners of LIVENTUS S.A., including the right to limit or reject liability.</p>" +
                                "<p style='font-size:75%;'>In no case may this information be used to establish and/or present a claim against Liventus S.A., its agencies, and / or the Shipping Lines associated with the shipments that are reported.</p>";

                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                //mmsg.To.Add("rcontreras@liventusglobal.com");
                mmsg.Bcc.Add(CorreoComercial);
                mmsg.Bcc.Add("vrios@liventusglobal.com");
                mmsg.Bcc.Add("rcontreras@liventusglobal.com");

                for (int i = 0; i < CorreoCliente.Count(); i++)
                {
                    mmsg.To.Add(CorreoCliente[i]);
                }


                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Información Servicio - Liventus S.A.";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje
                mmsg.Body = Correo;

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
                    aux.Mensaje = "Operación Realizada Correctamente";
                    aux.validador = Flag;

                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                    aux.validador = Flag;
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
            else
            {
                Flag = ReservaModelo.DesaprobarServicio(Servicio, Notificado);
                if (Flag == 0)
                {
                    aux.Mensaje = "Operación Realizada Correctamente, ya se había notificado al cliente anteriomente, por lo tanto no se volverá a notificar.";
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

        [HttpPost]
        public string AgregarReserva(string Booking, int IdNaviera, int IdExportador, int IdCommodity, int IdPuertoOrigen, int IdPuertoDestino, int IdNave, int IdSetpoint, string Viaje, int CantServicios, string Temperatura, DateTime Etd, int IdServiceProvider, DateTime? FechaIniStacking = null, DateTime? FechaFinStacking = null, DateTime? EtaNave = null, DateTime? Eta = null, string Consignatario = "", int IdFreightForwarder = 0, int Deposito = 0, int LlevaModem = 0)
        {
            Clases.Reserva ReservaNueva = new Clases.Reserva();
            ReservaNueva.Booking = Booking.Trim();
            ReservaNueva.IdNaviera = IdNaviera;
            ReservaNueva.IdExportador = IdExportador;
            ReservaNueva.IdCommodity = IdCommodity;
            ReservaNueva.IdPuertoDestino = IdPuertoDestino;
            ReservaNueva.IdPuertoOrigen = IdPuertoOrigen;
            ReservaNueva.Eta = HoraFecha(Eta.ToString());
            ReservaNueva.IdNave = IdNave;
            ReservaNueva.IdFreightForwarder = IdFreightForwarder;
            ReservaNueva.Consignatario = Consignatario;
            ReservaNueva.IdSetpoint = IdSetpoint;
            ReservaNueva.FechaIniStacking = HoraFecha(FechaIniStacking.ToString());
            ReservaNueva.FechaFinStacking = HoraFecha(FechaFinStacking.ToString());
            ReservaNueva.Viaje = Viaje;
            ReservaNueva.Etd = HoraFecha(Etd.ToString());
            ReservaNueva.CantidadServicios = CantServicios;
            ReservaNueva.Temperatura = float.Parse(Temperatura, CultureInfo.InvariantCulture); ;
            ReservaNueva.EtaNave = HoraFecha(EtaNave.ToString());
            ReservaNueva.IdServiceProvider = IdServiceProvider;
            ReservaNueva.Deposito = Deposito;
            ReservaNueva.LlevaModem = LlevaModem;

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.IngresarReserva(ReservaNueva);
            if (IdServiceProvider == 10 && IdCommodity != 37)
            {
                EnviarCorreoAlertaH1(Booking.Trim(), IdNaviera, IdExportador, IdCommodity, IdSetpoint, IdPuertoOrigen, IdPuertoDestino, IdNave, CantServicios);
            }
            else if (IdServiceProvider == 10 && IdCommodity == 37 && IdSetpoint != 3)
            {
                EnviarCorreoAlertaH1(Booking.Trim(), IdNaviera, IdExportador, IdCommodity, IdSetpoint, IdPuertoOrigen, IdPuertoDestino, IdNave, CantServicios);
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
        public string AgregarReservaSP(string Booking, int IdNaviera, int IdExportador, int IdCommodity, int IdPuertoOrigen, int IdPuertoDestino, int IdNave, int IdSetpoint, string Viaje, int CantServicios, string Temperatura, DateTime? Etd, int IdServiceProvider, DateTime? FechaIniStacking = null, DateTime? FechaFinStacking = null, DateTime? EtaNave = null, DateTime? Eta = null, string Consignatario = "", int IdFreightForwarder = 0, int LlevaModem = 0,
            string Booking2 = "", int Servicios2 = 0, string Booking3 = "", int Servicios3 = 0, string Booking4 = "", int Servicios4 = 0, string Booking5 = "", int Servicios5 = 0, string Booking6 = "", int Servicios6 = 0, string Booking7 = "", int Servicios7 = 0, string Booking8 = "", int Servicios8 = 0, string Booking9 = "", int Servicios9 = 0, string Booking10 = "", int Servicios10 = 0, string Booking11 = "", int Servicios11 = 0,
            string Booking12 = "", int Servicios12 = 0, string Booking13 = "", int Servicios13 = 0, string Booking14 = "", int Servicios14 = 0, string Booking15 = "", int Servicios15 = 0)
        {
            Clases.Reserva ReservaNueva = new Clases.Reserva();

            ReservaNueva.Booking = Booking.Trim();
            ReservaNueva.IdNaviera = IdNaviera;
            ReservaNueva.IdExportador = IdExportador;
            ReservaNueva.IdCommodity = IdCommodity;
            ReservaNueva.IdPuertoDestino = IdPuertoDestino;
            ReservaNueva.IdPuertoOrigen = IdPuertoOrigen;
            ReservaNueva.Eta = HoraFecha(Eta.ToString());
            ReservaNueva.IdNave = IdNave;
            ReservaNueva.IdFreightForwarder = IdFreightForwarder;
            ReservaNueva.Consignatario = Consignatario;
            ReservaNueva.IdSetpoint = IdSetpoint;
            ReservaNueva.FechaIniStacking = HoraFecha(FechaIniStacking.ToString());
            ReservaNueva.FechaFinStacking = HoraFecha(FechaFinStacking.ToString());
            ReservaNueva.Viaje = Viaje;
            ReservaNueva.Etd = HoraFecha(Etd.ToString());
            ReservaNueva.CantidadServicios = CantServicios;
            if (Temperatura == "")
            {
                ReservaNueva.Temperatura = float.Parse("0", CultureInfo.InvariantCulture);
            }
            else
            {
                ReservaNueva.Temperatura = float.Parse(Temperatura, CultureInfo.InvariantCulture);
            }
            ReservaNueva.EtaNave = HoraFecha(EtaNave.ToString());
            ReservaNueva.IdServiceProvider = IdServiceProvider;
            ReservaNueva.LlevaModem = LlevaModem;
            ReservaNueva.Booking2 = Booking2;
            ReservaNueva.Booking3 = Booking3;
            ReservaNueva.Booking4 = Booking4;
            ReservaNueva.Booking5 = Booking5;
            ReservaNueva.Booking6 = Booking6;
            ReservaNueva.Booking7 = Booking7;
            ReservaNueva.Booking8 = Booking8;
            ReservaNueva.Booking9 = Booking9;
            ReservaNueva.Booking10 = Booking10;
            ReservaNueva.Booking11 = Booking11;
            ReservaNueva.Booking12 = Booking12;
            ReservaNueva.Booking13 = Booking13;
            ReservaNueva.Booking14 = Booking14;
            ReservaNueva.Booking15 = Booking15;
            ReservaNueva.Servicios2 = Servicios2;
            ReservaNueva.Servicios3 = Servicios3;
            ReservaNueva.Servicios4 = Servicios4;
            ReservaNueva.Servicios5 = Servicios5;
            ReservaNueva.Servicios6 = Servicios6;
            ReservaNueva.Servicios7 = Servicios7;
            ReservaNueva.Servicios8 = Servicios8;
            ReservaNueva.Servicios9 = Servicios9;
            ReservaNueva.Servicios10 = Servicios10;
            ReservaNueva.Servicios11 = Servicios11;
            ReservaNueva.Servicios12 = Servicios12;
            ReservaNueva.Servicios13 = Servicios13;
            ReservaNueva.Servicios14 = Servicios14;
            ReservaNueva.Servicios15 = Servicios15;

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.IngresarReservaSP(ReservaNueva);

            if (IdServiceProvider == 10 && IdCommodity != 37)
            {
                EnviarCorreoAlertaH1(Booking.Trim(), IdNaviera, IdExportador, IdCommodity, IdSetpoint, IdPuertoOrigen, IdPuertoDestino, IdNave, CantServicios);
            }
            else if (IdServiceProvider == 10 && IdCommodity == 37 && IdSetpoint != 3)
            {
                EnviarCorreoAlertaH1(Booking.Trim(), IdNaviera, IdExportador, IdCommodity, IdSetpoint, IdPuertoOrigen, IdPuertoDestino, IdNave, CantServicios);
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
        public string AgregarReservasMasiva(string Booking, string Naviera, string Exportador, string Commodity, string PuertoOrigen, string PuertoDestino, DateTime Eta, float CO2, float O2, int CantidadServicios, string Nave = "", string Viaje = "", string FreightForwarder = "", string Consignatario = "", DateTime? FechaIniStacking = null, DateTime? FechaFinStacking = null, DateTime? Etd = null, float Temperatura = 0)
        {
            Clases.Reserva ReservaNueva = new Clases.Reserva();

            ReservaNueva.Booking = Booking.Trim();
            ReservaNueva.Naviera = Naviera;
            ReservaNueva.Exportador = Exportador;
            ReservaNueva.Commodity = Commodity;
            ReservaNueva.PuertoDestino = PuertoDestino;
            ReservaNueva.PuertoOrigen = PuertoOrigen;
            ReservaNueva.Eta = HoraFecha(Eta.ToString());
            ReservaNueva.Nave = Nave;
            ReservaNueva.FreightForwarder = FreightForwarder;
            ReservaNueva.Consignatario = Consignatario;
            ReservaNueva.O2Setpoint = O2;
            ReservaNueva.CO2Setpoint = CO2;
            ReservaNueva.FechaIniStacking = HoraFecha(FechaIniStacking.ToString());
            ReservaNueva.FechaFinStacking = HoraFecha(FechaFinStacking.ToString());
            ReservaNueva.Viaje = Viaje;
            ReservaNueva.Etd = HoraFecha(Etd.ToString());
            ReservaNueva.CantidadServicios = CantidadServicios;
            ReservaNueva.Temperatura = Temperatura;

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.IngresarReservaMasiva(ReservaNueva);
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
            List<Clases.Reserva> Reservas = new List<Clases.Reserva>();
            Reservas = ReservaModelo.GetReservaById(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reservas);
            return datos;
        }

        [HttpPost]
        public string GetServicioById(int IdServicio)
        {
            List<Clases.Servicio> Servicio = new List<Clases.Servicio>();
            List<Clases.Reserva> Reservas = new List<Clases.Reserva>();

            Servicio = ReservaModelo.GetServicioById(IdServicio);
            Reservas = ReservaModelo.GetReservaById(Servicio[0].IdReserva);

            if (Servicio[0].IdNave1 == 0)
            {
                Servicio[0].PuertoOrigen = Reservas[0].IdPuertoOrigen;
            }


            if (Servicio[0].PuertoOrigen == 0)
            {
                Servicio[0].PuertoOrigen = Reservas[0].IdPuertoOrigen;
            }

            if (Servicio[0].PuertoDestino == 0)
            {
                Servicio[0].PuertoDestino = Reservas[0].IdPuertoDestino;
            }

            if (Servicio[0].Consignatario == "")
            {
                Servicio[0].Consignatario = Reservas[0].Consignatario;
            }

            if (Servicio[0].Viaje == "")
            {
                Servicio[0].Viaje = Reservas[0].Viaje;
            }

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Servicio);
            return datos;
        }

        [HttpPost]
        public string GetLugaresServicioById(int IdServicio)
        {
            List<Clases.Servicio> Servicio = new List<Clases.Servicio>();
            Servicio = ReservaModelo.GetLugaresServicioById(IdServicio);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Servicio);
            return datos;
        }

        [HttpPost]
        public string EditarReserva(int IdReserva, int IdNaviera, int IdPuertoDestino, int IdPuertoOrigen, int IdSetpoint, int IdCommodity, int IdFreightForwarder, string Booking, string Viaje, string Consignatario, int CantServicios, DateTime Eta, int IdNave, int IdExportador, DateTime? Etd = null, DateTime? FechaIniStacking = null, DateTime? FechaFinStacking = null, float Temperatura = 0)
        {
            Clases.Reserva ReservaNueva = new Clases.Reserva();

            ReservaNueva.Id = IdReserva;
            ReservaNueva.IdNaviera = IdNaviera;
            ReservaNueva.IdPuertoDestino = IdPuertoDestino;
            ReservaNueva.IdPuertoOrigen = IdPuertoOrigen;
            ReservaNueva.IdSetpoint = IdSetpoint;
            ReservaNueva.IdCommodity = IdCommodity;
            ReservaNueva.IdFreightForwarder = IdFreightForwarder;
            ReservaNueva.Booking = Booking.Trim();
            ReservaNueva.Viaje = Viaje;
            ReservaNueva.Consignatario = Consignatario;
            ReservaNueva.CantidadServicios = CantServicios;
            ReservaNueva.Eta = HoraFecha(Eta.ToString());
            ReservaNueva.IdNave = IdNave;
            ReservaNueva.Etd = HoraFecha(Etd.ToString());
            ReservaNueva.FechaIniStacking = HoraFecha(FechaIniStacking.ToString());
            ReservaNueva.FechaFinStacking = HoraFecha(FechaFinStacking.ToString());
            ReservaNueva.IdExportador = IdExportador;
            ReservaNueva.Temperatura = Temperatura;

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.EditarReserva(ReservaNueva);
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
        public string CancelarReserva(int IdReserva, string Motivo)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.CancelarReserva(IdReserva, Motivo);
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

        [HttpGet]
        public ActionResult EditarReservas(int IdReserva)
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            ViewBag.Id = IdReserva;

            return View();
        }

        [HttpPost]
        public string GetReservas()
        {
            List<Clases.Reserva> Reservas = new List<Clases.Reserva>();
            Reservas = ReservaModelo.GetReservas();

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reservas);
            return datos;
        }

        [HttpPost]
        public string GetReservasEDI()
        {
            List<Clases.ReservaEDI> Reservas = new List<Clases.ReservaEDI>();
            Reservas = ReservaModelo.GetReservasEDI();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reservas);
            return datos;
        }

        [HttpPost]
        public int GetEstadoReserva(int IdReserva)
        {
            int Estado = 0;
            Estado = ReservaModelo.GetEstadoReserva(IdReserva);
            return Estado;
        }

        public ActionResult DetalleServicio()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            Clases.Reserva nuevo = new Clases.Reserva();
            return View();
        }

        public ActionResult VisualizarReservas()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult VisualizarReservasEDI()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult VisualizarHistoricoServicios()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult IngresarServicio()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult RendimientoServicio()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult ExportarServiceReport(string setpoint = "", string cortina = "", int co2Real = 0, int o2Real = 0, string bateria = "", string contenedor = "", string controlador = "", string modem = "", string exportador = "", string consignatario = "", string puertoDestino = "", string packing = "", string puertoOrigen = "", string horaInicioServicio = "00:00", string horaTerminoServicio = "00:00", string tecnicoServicio = "", string commodity = "", string booking = "", string naviera = "", string nave = "", string viaje = "", string candado = "", string scrubber_si = "", string scrubber_no = "", string tecnico_leaktest = "", string tiempo_estanquedad = "", string sello_perno1 = "", string sello_perno2 = "", string sello_tapa = "", string sello_panel1 = "", string sello_panel2 = "", string sello_security = "", string temperatura = "", int id_servicio = 0, string etd = "", string fechaServicio = "", int cantidad_O2 = 0, int cantidad_CO2 = 0, string commodity_tecnica = "", string comentario_sello = "", string temperatura_reserva = "", string eta = "")
        {
            ResultadoLeaktest resultado = new ResultadoLeaktest();
            resultado = ReservaModelo.GetResultadoLeaktestByContenedor(contenedor);
            string fechaServicio2 = "";
            string etd2 = "";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Informes"), "ServiceReport.rpt"));
            rd.SetParameterValue("setpoint", setpoint);
            rd.SetParameterValue("cortina", cortina);
            rd.SetParameterValue("bateria", bateria);
            rd.SetParameterValue("contenedor", contenedor);
            rd.SetParameterValue("controlador", controlador);
            rd.SetParameterValue("modem", modem);
            rd.SetParameterValue("exportador", exportador);
            rd.SetParameterValue("consignatario", consignatario);
            rd.SetParameterValue("puerto_destino", puertoDestino);
            rd.SetParameterValue("packing", packing);
            rd.SetParameterValue("puerto_origen", puertoOrigen);
            rd.SetParameterValue("hora_inicio_servicio", horaInicioServicio);
            rd.SetParameterValue("hora_termino_servicio", horaTerminoServicio);
            rd.SetParameterValue("tecnico_Servicio", tecnicoServicio);
            rd.SetParameterValue("commodity", commodity);
            rd.SetParameterValue("etd", etd);
            rd.SetParameterValue("eta", eta);
            rd.SetParameterValue("booking", booking.Trim());
            rd.SetParameterValue("naviera", naviera);
            rd.SetParameterValue("nave", nave);
            rd.SetParameterValue("viaje", viaje);
            rd.SetParameterValue("candado", candado);
            rd.SetParameterValue("scrubber_si", scrubber_si);
            rd.SetParameterValue("scrubber_no", scrubber_no);
            //if(fechaServicio!="")
            //{
            //    DateTime fechaAuxServicio = Convert.ToDateTime(fechaServicio);
            //    fechaServicio2 = fechaAuxServicio.ToString("dd/MM/yyyy");
            //}else
            //{
            //    fechaServicio2 = "";
            //}
            //string fechaServicio2 = fechaServicio.ToString("MM-dd-yyyy");
            //rd.SetParameterValue("fecha_servicio", fechaServicio2);
            rd.SetParameterValue("fecha_servicio", fechaServicio);
            rd.SetParameterValue("tecnico_leaktest", tecnico_leaktest);
            rd.SetParameterValue("tiempo_estanquedad", tiempo_estanquedad);
            rd.SetParameterValue("sello_perno1", sello_perno1);
            rd.SetParameterValue("sello_perno2", sello_perno2);
            rd.SetParameterValue("sello_tapa", sello_tapa);
            rd.SetParameterValue("sello_panel1", sello_panel1);
            rd.SetParameterValue("sello_panel2", sello_panel2);
            rd.SetParameterValue("sello_security", sello_security);
            rd.SetParameterValue("temperatura", temperatura);
            rd.SetParameterValue("id_servicio", id_servicio);
            rd.SetParameterValue("co2_real", cantidad_CO2);
            rd.SetParameterValue("o2_real", cantidad_O2);
            rd.SetParameterValue("commodity_tecnica", commodity_tecnica);
            rd.SetParameterValue("comentario_sellos", comentario_sello);
            rd.SetParameterValue("temperatura_reserva", temperatura_reserva);
            rd.SetParameterValue("sp_co2", obtenerCO2_SP(setpoint));
            rd.SetParameterValue("sp_o2", obtenerO2_SP(setpoint));
            if (resultado.Tiempo != null)
            {
                rd.SetParameterValue("tiempo_estanquedad", resultado.Tiempo);
            }
            else
            {
                rd.SetParameterValue("tiempo_estanquedad", "");
            }
            if (resultado.FechaRealizacion != null && resultado.FechaRealizacion.ToShortDateString() != "01/01/0001")
            {
                rd.SetParameterValue("fecha_leaktest", resultado.FechaRealizacion.ToShortDateString());
            }
            else
            {
                rd.SetParameterValue("fecha_leaktest", "");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ServiceReport_" + id_servicio + ".pdf");
        }

        public ActionResult ExportarServiceReportSP(string setpoint = "", string cortina = "", int co2Real = 0, int o2Real = 0, string bateria = "", string contenedor = "", string controlador = "", string modem = "", string exportador = "", string consignatario = "", string puertoDestino = "", string packing = "", string puertoOrigen = "", string horaInicioServicio = "00:00", string horaTerminoServicio = "00:00", string tecnicoServicio = "", string commodity = "", string booking = "", string naviera = "", string nave = "", string viaje = "", string candado = "", string scrubber_si = "", string scrubber_no = "", string tecnico_leaktest = "", string tiempo_estanquedad = "", string sello_perno1 = "", string sello_perno2 = "", string sello_tapa = "", string sello_panel1 = "", string sello_panel2 = "", string sello_security = "", string temperatura = "", int id_servicio = 0, string etd = "", string fechaServicio = "", int cantidad_O2 = 0, int cantidad_CO2 = 0, string commodity_tecnica = "", string comentario_sello = "", string temperatura_reserva = "", string eta = "")
        {
            ResultadoLeaktest resultado = new ResultadoLeaktest();
            resultado = ReservaModelo.GetResultadoLeaktestByContenedorSP(contenedor);
            string fechaServicio2 = "";
            string etd2 = "";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Informes"), "ServiceReportSP.rpt"));
            rd.SetParameterValue("setpoint", setpoint);
            rd.SetParameterValue("cortina", cortina);
            rd.SetParameterValue("bateria", bateria);
            rd.SetParameterValue("contenedor", contenedor);
            rd.SetParameterValue("controlador", controlador);
            rd.SetParameterValue("modem", modem);
            rd.SetParameterValue("exportador", exportador);
            rd.SetParameterValue("consignatario", consignatario);
            rd.SetParameterValue("puerto_destino", puertoDestino);
            rd.SetParameterValue("packing", packing);
            rd.SetParameterValue("puerto_origen", puertoOrigen);
            rd.SetParameterValue("hora_inicio_servicio", horaInicioServicio);
            rd.SetParameterValue("hora_termino_servicio", horaTerminoServicio);
            rd.SetParameterValue("tecnico_Servicio", tecnicoServicio);
            rd.SetParameterValue("commodity", commodity);
            rd.SetParameterValue("etd", etd);
            rd.SetParameterValue("eta", eta);
            rd.SetParameterValue("booking", booking.Trim());
            rd.SetParameterValue("naviera", naviera);
            rd.SetParameterValue("nave", nave);
            rd.SetParameterValue("viaje", viaje);
            rd.SetParameterValue("candado", candado);
            rd.SetParameterValue("scrubber_si", scrubber_si);
            rd.SetParameterValue("scrubber_no", scrubber_no);
            rd.SetParameterValue("fecha_servicio", fechaServicio);
            rd.SetParameterValue("tecnico_leaktest", tecnico_leaktest);
            rd.SetParameterValue("tiempo_estanquedad", tiempo_estanquedad);
            rd.SetParameterValue("sello_perno1", sello_perno1);
            rd.SetParameterValue("sello_perno2", sello_perno2);
            rd.SetParameterValue("sello_tapa", sello_tapa);
            rd.SetParameterValue("sello_panel1", sello_panel1);
            rd.SetParameterValue("sello_panel2", sello_panel2);
            rd.SetParameterValue("sello_security", sello_security);
            rd.SetParameterValue("temperatura", temperatura);
            rd.SetParameterValue("id_servicio", id_servicio);
            rd.SetParameterValue("co2_real", cantidad_CO2);
            rd.SetParameterValue("o2_real", cantidad_O2);
            rd.SetParameterValue("commodity_tecnica", commodity_tecnica);
            rd.SetParameterValue("comentario_sellos", comentario_sello);
            rd.SetParameterValue("temperatura_reserva", temperatura_reserva);
            rd.SetParameterValue("sp_co2", obtenerCO2_SP(setpoint));
            rd.SetParameterValue("sp_o2", obtenerO2_SP(setpoint));
            if (resultado.Tiempo != null)
            {
                rd.SetParameterValue("tiempo_estanquedad", resultado.Tiempo);
            }
            else
            {
                rd.SetParameterValue("tiempo_estanquedad", "");
            }

            if (resultado.FechaRealizacion != null && resultado.FechaRealizacion.ToShortDateString() != "01/01/0001")
            {
                rd.SetParameterValue("fecha_leaktest", resultado.FechaRealizacion.ToShortDateString());
            }
            else
            {
                rd.SetParameterValue("fecha_leaktest", "");
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ServiceReport_" + id_servicio + ".pdf");
        }

        [HttpPost]
        public string CrearServicio(int IdReserva, DateTime Eta, string Consignatario = "", string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0, int Nave1 = 0, int Nave2 = 0, int Nave3 = 0, int Contenedor = 0,
            int TipoLugarCortina = 0, int LugarCortina = 0, int Purafil = 0, string Controlador = "", string Bateria = "", string HoraLlegada = "", string HoraSalida = "", int TipoLugarInstContr = 0, int LugarInstContr = 0,
            int TecnicoInstalador = 0, float TemperaturaContenedor = 0, int Folio = 0, int Precinto = 0, int Candado = 0, int TipoLugarGasificacion = 0, int LugarGasificacion = 0, int TecnicoGasificador = 0,
            int TipoTratamientoCO2 = 0, int Scrubber = 0, int Cal = 0, int CortinaInstalada = 0, int Gasificado = 0, int EstadoServicio = 0, string UltimaNave = "", string UltimoControlador = "", int Habilitado = 0,
            int InstaladorCortina = 0, string NotaServicio = "", string NotaLogistica = "", string Selloperno1 = "", string Selloperno2 = "", string Sellotapa = "", string SelloPanel1 = "",
            string SelloPanel2 = "", string SelloSecurity = "", DateTime? FechaInstContr = null, DateTime? FechaCortina = null, DateTime? FechaGasificacion = null, string ObservacionSellos = "")
        {
            Clases.Servicio Servicio = new Clases.Servicio();
            Clases.Controlador Controladores = ControladorModel.GetIdControlador(Controlador);

            if (Controlador != "0")
            {
                Servicio.IdReserva = IdReserva; //Este dato se obtendra de la consulta a partir del numero de booking, viaje, exportador y naviera.
                Servicio.Consignatario = Consignatario;
                Servicio.Viaje = Viaje;
                Servicio.PuertoOrigen = PuertoOrigen;
                Servicio.PuertoDestino = PuertoDestino;
                Servicio.IdNave1 = Nave1;
                Servicio.IdNave2 = Nave2;
                Servicio.IdNave3 = Nave3;
                Servicio.IdContenedor = Contenedor;
                Servicio.Eta = HoraFecha(Eta.ToString());
                Servicio.FechaCortina = FechaCortina;
                Servicio.IdTipoLugarCortina = TipoLugarCortina;
                Servicio.IdLugarCortina = LugarCortina;
                Servicio.CantidadPurafil = Purafil;
                Servicio.IdControlador = Controladores.Id;
                Servicio.Bateria = Bateria;
                Servicio.HoraLlegada = HoraLlegada;
                Servicio.HoraSalida = HoraSalida;
                Servicio.FechaInstControlador = HoraFecha(FechaInstContr.ToString());
                Servicio.IdTipoLugarControlador = TipoLugarInstContr;
                Servicio.IdLugarInstControlador = LugarInstContr;
                Servicio.IdTecnicoInstalador = TecnicoInstalador;
                Servicio.TemperaturaContenedor = TemperaturaContenedor;
                Servicio.FolioServiceReport = Folio;
                Servicio.PrecintoSecurity = Precinto;
                Servicio.Candado = Candado;
                Servicio.FechaGasificacion = HoraFecha(FechaGasificacion.ToString());
                Servicio.IdTipoLugarGasificacion = TipoLugarGasificacion;
                Servicio.IdLugarGasificacion = LugarGasificacion;
                Servicio.IdTecnicoGasificador = TecnicoGasificador;
                Servicio.CO2 = 0;
                Servicio.N2 = 0;
                Servicio.IdTratamientoCO2 = TipoTratamientoCO2;
                Servicio.Scrubber = Scrubber;
                Servicio.Cal = Cal;
                Servicio.CortinaInstalada = CortinaInstalada;
                Servicio.Gasificado = Gasificado;
                Servicio.IdEstadoServicio = EstadoServicio;
                Servicio.UltimaNave = UltimaNave;
                Servicio.UltimoControlador = UltimoControlador;
                Servicio.Habilitado = Habilitado;
                Servicio.InstaladorCortina = InstaladorCortina;
                Servicio.NotaServicio = NotaServicio;
                Servicio.NotaLogistica = NotaLogistica;
                Servicio.Selloperno1 = Selloperno1;
                Servicio.Selloperno2 = Selloperno2;
                Servicio.Sellotapa = Sellotapa;
                Servicio.SelloPanel1 = SelloPanel1;
                Servicio.SelloPanel2 = SelloPanel2;
                Servicio.SelloSecurity = SelloSecurity;
                Servicio.ObservacionSellos = ObservacionSellos;

                Clases.Validar aux = new Clases.Validar();
                int Flag = ReservaModelo.CrearServicio(Servicio);
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
            else
            {
                Clases.Validar aux = new Clases.Validar();
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = 0;
                string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                return datos;
            }
        }

        [HttpPost]
        public string CrearServicio2(int IdReserva, DateTime Eta, string Consignatario = "", string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0, int Nave1 = 0, int Nave2 = 0, int Nave3 = 0, int Contenedor = 0,
            int TipoLugarCortina = 0, int LugarCortina = 0, int Purafil = 0, string Controlador = "", string Bateria = "", string HoraLlegada = "", string HoraSalida = "", int TipoLugarInstContr = 0, int LugarInstContr = 0,
            int TecnicoInstalador = 0, float TemperaturaContenedor = 0, int Folio = 0, int Precinto = 0, int Candado = 0, int TipoLugarGasificacion = 0, int LugarGasificacion = 0, int TecnicoGasificador = 0,
            int TipoTratamientoCO2 = 0, int Scrubber = 0, int Cal = 0, int CortinaInstalada = 0, int Gasificado = 0, int EstadoServicio = 0, string UltimaNave = "", string UltimoControlador = "", int Habilitado = 0,
            int InstaladorCortina = 0, string NotaServicio = "", string NotaLogistica = "", string Selloperno1 = "", string Selloperno2 = "", string Sellotapa = "", string SelloPanel1 = "",
            string SelloPanel2 = "", string SelloSecurity = "", DateTime? FechaInstContr = null, DateTime? FechaCortina = null, DateTime? FechaGasificacion = null, string ObservacionSellos = "")
        {
            Clases.Servicio Servicio = new Clases.Servicio();
            if (Controlador != "0")
            {
                Clases.Controlador Controladores = ControladorModel.GetIdControlador(Controlador);
                Servicio.IdReserva = IdReserva; //Este dato se obtendra de la consulta a partir del numero de booking, viaje, exportador y naviera.
                Servicio.Consignatario = Consignatario;
                Servicio.Viaje = Viaje;
                Servicio.PuertoOrigen = PuertoOrigen;
                Servicio.PuertoDestino = PuertoDestino;
                Servicio.IdNave1 = Nave1;
                Servicio.IdNave2 = Nave2;
                Servicio.IdNave3 = Nave3;
                Servicio.IdContenedor = Contenedor;
                Servicio.Eta = HoraFecha(Eta.ToString());
                Servicio.FechaCortina = HoraFecha(FechaCortina.ToString());
                Servicio.IdTipoLugarCortina = TipoLugarCortina;
                Servicio.IdLugarCortina = LugarCortina;
                Servicio.CantidadPurafil = Purafil;
                Servicio.IdControlador = Controladores.Id;
                Servicio.Bateria = Bateria;
                Servicio.HoraLlegada = HoraLlegada;
                Servicio.HoraSalida = HoraSalida;
                Servicio.FechaInstControlador = HoraFecha(FechaInstContr.ToString());
                Servicio.IdTipoLugarControlador = TipoLugarInstContr;
                Servicio.IdLugarInstControlador = LugarInstContr;
                Servicio.IdTecnicoInstalador = TecnicoInstalador;
                Servicio.TemperaturaContenedor = TemperaturaContenedor;
                Servicio.FolioServiceReport = Folio;
                Servicio.PrecintoSecurity = Precinto;
                Servicio.Candado = Candado;
                Servicio.FechaGasificacion = HoraFecha(FechaGasificacion.ToString());
                Servicio.IdTipoLugarGasificacion = TipoLugarGasificacion;
                Servicio.IdLugarGasificacion = LugarGasificacion;
                Servicio.IdTecnicoGasificador = TecnicoGasificador;
                Servicio.CO2 = 0;
                Servicio.N2 = 0;
                Servicio.IdTratamientoCO2 = TipoTratamientoCO2;
                Servicio.Scrubber = Scrubber;
                Servicio.Cal = Cal;
                Servicio.CortinaInstalada = CortinaInstalada;
                Servicio.Gasificado = Gasificado;
                Servicio.IdEstadoServicio = EstadoServicio;
                Servicio.UltimaNave = UltimaNave;
                Servicio.UltimoControlador = UltimoControlador;
                Servicio.Habilitado = Habilitado;
                Servicio.InstaladorCortina = InstaladorCortina;
                Servicio.NotaServicio = NotaServicio;
                Servicio.NotaLogistica = NotaLogistica;
                Servicio.Selloperno1 = Selloperno1;
                Servicio.Selloperno2 = Selloperno2;
                Servicio.Sellotapa = Sellotapa;
                Servicio.SelloPanel1 = SelloPanel1;
                Servicio.SelloPanel2 = SelloPanel2;
                Servicio.SelloSecurity = SelloSecurity;
                Servicio.ObservacionSellos = ObservacionSellos;

                List<Clases.Servicio> Servicio2 = new List<Clases.Servicio>();

                Servicio2 = ReservaModelo.CrearServicio2(Servicio);

                string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Servicio2);
                return datos;
            }
            else
            {
                Clases.Validar aux = new Clases.Validar();
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = 0;
                string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                return datos;
            }
        }

        [HttpPost]
        public string GetServicios(int IdReserva)
        {
            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            Servicios = ReservaModelo.GetServicios(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Servicios);
            return datos;
        }

        [HttpPost]
        public string GetHistoricoServicios()
        {
            Clases.ReservayServicio ReservasyServicios = new Clases.ReservayServicio();
            ReservasyServicios = ReservaModelo.GetHistoricoServicios();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ReservasyServicios);
            return datos;
        }

        [HttpPost]
        public string GetTodosServicios()
        {
            Clases.ReservayServicio ReservasyServicios = new Clases.ReservayServicio();
            ReservasyServicios = ReservaModelo.GetTodosServicios();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ReservasyServicios);
            return datos;
        }

        public ActionResult EditarPrestacionServicio(int IdServicio)
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            ViewBag.ID = IdServicio;

            return View();
        }


        //public ActionResult IngresarPrestacionServicio(string booking, string naviera, string nave, string viaje, string exportador, string fechaRegistro, string inicioStacking, string eta, string iniciostacking, string commodity, string porigen, string pdestino, string co2, string o2, string temperatura, string servicios)
        public ActionResult IngresarPrestacionServicio(int IdReserva)
        {


            ViewBag.IdReserva = IdReserva;

            return View();
        }

        [HttpGet]
        public ActionResult DetalleReserva(int id, string booking)
        {
            ViewBag.Id = id;
            ViewBag.Booking = booking.Trim();

            return View();
        }

        public ActionResult ConsultarServicios()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult ValidarServicios()
        {
            return View();
        }

        public ActionResult ContabilizarServicios()
        {
            return View();
        }

        [HttpPost]
        public string DetalleReservaServicio(int IdReserva)
        {
            Clases.CantidadReservas Reservas = new Clases.CantidadReservas();
            Reservas = ReservaModelo.DetalleReservaServicio(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reservas);
            return datos;
        }

        [HttpPost]
        public string EditarReservaServicio(int IdReserva)
        {

            return "";
        }

        [HttpPost]
        public string EditarServicio(int IdServicio, DateTime Eta, string Consignatario = "", string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0, int Nave1 = 0, int Nave2 = 0,
            int Nave3 = 0, int Contenedor = 0, int TipoLugarCortina = 0, int LugarCortina = 0, int Purafil = 0, string Controlador = "", string Bateria = "", int TipoLugarInstContr = 0,
            int LugarInstContr = 0, int TecnicoInstalador = 0, float TemperaturaContenedor = 0, int Folio = 0, int Precinto = 0, int Candado = 0, int TipoLugarGasificacion = 0,
            int LugarGasificacion = 0, int TecnicoGasificador = 0, int TipoTratamientoCO2 = 0, int CortinaInstalada = 0, int Gasificado = 0, int EstadoServicio = 0,
            string UltimaNave = "", string UltimoControlador = "", int Habilitado = 0, int InstaladorCortina = 0, string NotaServicio = "", string NotaLogistica = "",
            string Selloperno1 = "", string Selloperno2 = "", string Sellotapa = "", string HoraLlegada = "", string HoraSalida = "", int Scrubber = 0, int Cal = 0,
            string SelloPanel1 = "", string SelloPanel2 = "", string SelloSecurity = "", DateTime? FechaInstContr = null, DateTime? FechaCortina = null,
            DateTime? FechaGasificacion = null, string ObservacionSellosEditar = "", string Booking = "")
        {
            Clases.Servicio Servicio = new Clases.Servicio();
            Clases.Controlador Controladores = ControladorModel.GetIdControlador(Controlador);
            if (Controlador != "0")
            {
                Servicio.Id = IdServicio;
                Servicio.Consignatario = Consignatario;
                Servicio.Viaje = Viaje;
                Servicio.PuertoOrigen = PuertoOrigen;
                Servicio.PuertoDestino = PuertoDestino;
                Servicio.IdNave1 = Nave1;
                Servicio.IdNave2 = Nave2;
                Servicio.IdNave3 = Nave3;
                Servicio.IdContenedor = Contenedor;
                Servicio.Eta = HoraFecha(Eta.ToString());
                Servicio.FechaCortina = HoraFecha(FechaCortina.ToString());
                Servicio.IdTipoLugarCortina = TipoLugarCortina;
                Servicio.IdLugarCortina = LugarCortina;
                Servicio.CantidadPurafil = Purafil;
                Servicio.IdControlador = Controladores.Id;
                Servicio.Bateria = Bateria;
                Servicio.HoraLlegada = HoraLlegada;
                Servicio.HoraSalida = HoraSalida;
                Servicio.FechaInstControlador = HoraFecha(FechaInstContr.ToString());
                Servicio.IdTipoLugarControlador = TipoLugarInstContr;
                Servicio.IdLugarInstControlador = LugarInstContr;
                Servicio.IdTecnicoInstalador = TecnicoInstalador;
                Servicio.TemperaturaContenedor = TemperaturaContenedor;
                Servicio.FolioServiceReport = Folio;
                Servicio.PrecintoSecurity = Precinto;
                Servicio.Candado = Candado;
                Servicio.FechaGasificacion = HoraFecha(FechaGasificacion.ToString());
                Servicio.IdTipoLugarGasificacion = TipoLugarGasificacion;
                Servicio.IdLugarGasificacion = LugarGasificacion;
                Servicio.IdTecnicoGasificador = TecnicoGasificador;
                Servicio.CO2 = 0;
                Servicio.N2 = 0;
                Servicio.IdTratamientoCO2 = TipoTratamientoCO2;
                Servicio.Scrubber = Scrubber;
                Servicio.Cal = Cal;
                Servicio.CortinaInstalada = CortinaInstalada;
                Servicio.Gasificado = Gasificado;
                Servicio.IdEstadoServicio = EstadoServicio;
                Servicio.UltimaNave = UltimaNave;
                Servicio.UltimoControlador = UltimoControlador;
                Servicio.Habilitado = Habilitado;
                Servicio.InstaladorCortina = InstaladorCortina;
                Servicio.NotaServicio = NotaServicio;
                Servicio.NotaLogistica = NotaLogistica;
                Servicio.Selloperno1 = Selloperno1;
                Servicio.Selloperno2 = Selloperno2;
                Servicio.Sellotapa = Sellotapa;
                Servicio.SelloPanel1 = SelloPanel1;
                Servicio.SelloPanel2 = SelloPanel2;
                Servicio.SelloSecurity = SelloSecurity;
                Servicio.ObservacionSellos = ObservacionSellosEditar;
                Servicio.Booking = Booking.Trim();

                Clases.Validar aux = new Clases.Validar();
                int Flag = ReservaModelo.EditarServicio(Servicio);
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
            else
            {
                Clases.Validar aux = new Clases.Validar();
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = 0;
                string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                return datos;
            }
        }

        [HttpPost]
        public string AgregarServicio()
        {

            return "";
        }

        [HttpPost]
        public string UltimasModificaciones()
        {

            return "";
        }

        [HttpPost]
        public string GetHistoricoControladores(int IdServicio)
        {
            List<Clases.DetalleControlador> Controladores = new List<Clases.DetalleControlador>();
            Controladores = ReservaModelo.GetHistoricoControladores(IdServicio);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Controladores);
            return datos;
        }

        [HttpPost]
        public string AgregarPuertoDestino(int IdServicio)
        {

            return "";
        }

        [HttpPost]
        public string DetalleResultadoLeaktest(int IdServicio)
        {

            return "";
        }

        [HttpPost]
        public string RecuperacionControlador(int IdServicio)
        {

            return "";
        }

        [HttpPost]
        public string CancelarServicio(int IdServicio, string Motivo)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.CancelarServicio(IdServicio, Motivo);
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
        public string ValidarNumServicios(int IdReserva)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.ValidarNumServicios(IdReserva);
            if (Flag == 0)
            {
                aux.Mensaje = "Faltan Servicios por agregar a la Reserva";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Reserva está completa, ¿igualmente desea agregar un servicio?";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }


        [HttpPost]
        public string FiltroPorFechas(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            List<Clases.Reserva> Reservas = new List<Clases.Reserva>();
            Reservas = ReservaModelo.FiltroPorFechas(Tipo, FechaIni, FechaFin);

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reservas);
            return datos;
        }

        [HttpPost]
        public string FiltroPorFechasServicio(int IdReserva, int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            Servicios = ReservaModelo.FiltroPorFechasServicio(IdReserva, Tipo, FechaIni, FechaFin);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Servicios);
            return datos;
        }

        [HttpPost]
        public string FiltroPorFechasServicioHistorico(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            Clases.ReservayServicio ReservasyServicios = new Clases.ReservayServicio();
            ReservasyServicios = ReservaModelo.FiltroPorFechasServicioHistorico(Tipo, FechaIni, FechaFin);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ReservasyServicios);
            return datos;
        }

        [HttpPost]
        public string FiltroPorFechasServicioHistorico1(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.FiltroPorFechasServicioHistorico1(Tipo, FechaIni, FechaFin);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ReservasyServicios);
            return datos;
        }

        [HttpPost]
        public string GetModificaciones(int IdReserva)
        {
            List<Clases.AuditoriaReserva> Reservas = new List<Clases.AuditoriaReserva>();
            Reservas = ReservaModelo.GetModificaciones(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reservas);
            return datos;
        }

        [HttpPost]
        public int ValidarBooking(string Booking)
        {
            Clases.Validar aux = new Clases.Validar();
            int Estado = ReservaModelo.ValidarBooking(Booking.Trim());
            return Estado;

        }

        [HttpPost]
        public string ValidarNumeroBooking(string Booking)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.ValidarNumeroBooking(Booking.Trim());
            if (Flag == 0)
            {
                aux.Mensaje = "Booking Validado";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Booking ya existe en los registros actuales.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;

        }

        [HttpPost]
        public string ValidarNumServiciosBooking(string Booking)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.ValidarNumServiciosBooking(Booking.Trim());
            if (Flag == 0)
            {
                aux.Mensaje = "Booking Validado";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Booking validado pero ya tiene la cantidad de servicios reservada, Desea Continuar?";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string ValidarMantenedoresMasiva(string Naviera, string Exportador, string PuertoOrigen, string PuertoDestino, string Commodity)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.ValidarMantenedoresMasiva(Naviera, Exportador, PuertoOrigen, PuertoDestino, Commodity);
            if (Flag != 0)
            {
                aux.Mensaje = "Mantenedores Coinciden";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Existen datos que no coinciden con los que posee la Aplicación actualmente, favor verificar datos.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string LeerCargaMasiva()
        {

            List<Clases.Reserva> ReservasCargadas = new List<Clases.Reserva>();
            //List<string> ControladoresError = new List<string>();
            //Clases.Controlador Controlador = new Clases.Controlador();
            //Clases.Contenedor Contenedor = new Clases.Contenedor();
            bool flag = false;
            string datos = "";
            if (Request.Files["Controladores"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["Controladores"].FileName).ToLower();

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["Controladores"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["Controladores"].SaveAs(path1);

                    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                    ExcelFile ef = ExcelFile.Load(path1);

                    StringBuilder sb = new StringBuilder();
                    int rows = ef.Worksheets.ActiveWorksheet.Rows.Count();
                    int number1 = ef.Worksheets.ActiveWorksheet.SelectedRanges.Count();
                    foreach (ExcelWorksheet sheet in ef.Worksheets)
                    {
                        // Iterate through all rows in an Excel worksheet.
                        foreach (ExcelRow row in sheet.Rows)
                        {
                            Clases.Reserva Reservas = new Clases.Reserva();
                            // Iterate through all allocated cells in an Excel row.
                            foreach (ExcelCell cell in row.AllocatedCells)
                            {

                                if (cell.Row.ToString() != "1")
                                {
                                    if (cell.Row.ToString() != "1" && cell.Column.ToString() == "A")
                                    {
                                        if (cell.ValueType != CellValueType.Null)
                                        {
                                            if (cell.Value.ToString() == "fin")
                                            {
                                                datos = JsonConvert.SerializeObject(ReservasCargadas);
                                                return datos;
                                            }
                                            Reservas.Nave = cell.Value.ToString();
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "B")
                                    {
                                        Reservas.Viaje = cell.Value.ToString();
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "C")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.Eta = Convert.ToDateTime(cell.Value.ToString());
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "D")
                                    {
                                        Reservas.FechaIniStacking = Convert.ToDateTime(cell.Value.ToString());
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "E")
                                    {
                                        Reservas.FechaFinStacking = Convert.ToDateTime(cell.Value.ToString());
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "F")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.Etd = Convert.ToDateTime(cell.Value.ToString());
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "G")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.Naviera = cell.Value.ToString();
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "H")
                                    {
                                        Reservas.FreightForwarder = cell.Value.ToString();
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "I")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.Exportador = cell.Value.ToString();
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "J")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.PuertoOrigen = cell.Value.ToString();
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "K")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.PuertoDestino = cell.Value.ToString();
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "L")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.Booking = cell.Value.ToString().Trim();
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "M")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.CantidadServicios = Convert.ToInt32(cell.Value.ToString());
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "N")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.Commodity = cell.Value.ToString();
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "O")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.CO2Setpoint = float.Parse(cell.Value.ToString());
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "P")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.O2Setpoint = float.Parse(cell.Value.ToString());
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "Q")
                                    {
                                        if (cell.ValueType == CellValueType.Null)
                                        {
                                            var aux = "Error de Datos";
                                            return JsonConvert.SerializeObject(aux);
                                        }
                                        else
                                        {
                                            Reservas.Temperatura = float.Parse(cell.Value.ToString());
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "R")
                                    {
                                        Reservas.Consignatario = cell.Value.ToString();
                                    }
                                    flag = true;
                                }
                                else if (cell.Row.ToString() == "1")
                                {
                                    flag = false;
                                    break;
                                }
                            }

                            if (flag != false)
                            {
                                ReservasCargadas.Add(Reservas);
                            }
                        }
                    }
                }
                else
                {
                    var aux = "Error de Formato";
                    return JsonConvert.SerializeObject(aux);
                }


            }
            datos = JsonConvert.SerializeObject(ReservasCargadas);
            return datos;
        }

        [HttpPost]
        public string EdicionMasiva(int[] Servicios, string Booking = "", int Naviera = 0, int Nave = 0, int Exportador = 0, int FreightForwarder = 0, string Consignatario = "", int Commodity = 0, int Setpoint = 0,
                                    int Setpoint1 = 0, DateTime? IniStacking = null, DateTime? FinStacking = null, string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0,
                                    DateTime? Etd = null, DateTime? EtaNave = null, DateTime? EtaPuerto = null, float Temperatura = 0, int ServiceProvider = 0)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            for (int i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.EdicionMasiva(Servicios[i], Booking.Trim(), Naviera, Nave, Exportador, FreightForwarder, Consignatario, Commodity, Setpoint, Setpoint1, HoraFecha(IniStacking.ToString()), HoraFecha(FinStacking.ToString()), Viaje, PuertoOrigen,
                                       PuertoDestino, HoraFecha(Etd.ToString()), HoraFecha(EtaNave.ToString()), HoraFecha(EtaPuerto.ToString()), Temperatura, ServiceProvider);
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
        public string EdicionMasivaNave(int[] Servicios, DateTime? IniStacking = null, DateTime? FinStacking = null, string Viaje = "", DateTime? Etd = null, DateTime? EtaNave = null)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            for (int i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.EdicionMasivaNave(Servicios[i], HoraFecha(IniStacking.ToString()), HoraFecha(FinStacking.ToString()), Viaje, HoraFecha(Etd.ToString()), HoraFecha(EtaNave.ToString()));
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
        public string EdicionMasivaViaje(int[] Servicios, string NumViaje = "", int IdPuertoOrigen = 0, int IdPuertoDestino = 0)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            for (int i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.EdicionMasivaViaje(Servicios[i], NumViaje, IdPuertoOrigen, IdPuertoDestino);
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

        public JsonResult GetHistoricosServicios1()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1();
            //ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1Color(string color = "")
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1Color(color);
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1SP()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1SP();
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1SPColor(string color = "")
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1SPColor(color);
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1Todos()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1Todos();
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1TodosPorSemana(string SemanaInicio = "", string SemanaFin = "")
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1TodosPorSemana(SemanaInicio, SemanaFin);
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1TodosColor(string color = "")
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1TodosColor(color);
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1TodosSP()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1TodosSP();
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricosServicios1TodosSPColor(string color = "")
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricosServicios1TodosSPColor(color);
            ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetServiciosDeposito()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetServiciosDeposito();
            //ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetServiciosDeposito2()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetServiciosDeposito2();
            //ReservasyServicios = ReservaModelo.GetAlertasServicios(ReservasyServicios);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        [HttpPost]
        public string EditarCelda(int IdServicio, string Campo, string Valor, string Columna, int IdSetpoint = 0, int IdLugarControlador = 0,
                                  int IdLugarcortina = 0, int IdLugarGasificacion = 0, int IdTipoNodoControladorOrigen = 0, int IdNodoControladorOrigen = 0, int IdLugarModem = 0)
        {
            Clases.Validar aux = new Clases.Validar();

            int Flag = ReservaModelo.EditarCelda(IdServicio, Campo, Valor, Columna, IdSetpoint, IdLugarControlador, IdLugarcortina, IdLugarGasificacion, IdTipoNodoControladorOrigen, IdNodoControladorOrigen, IdLugarModem);
            if (Flag == 0 || Flag == 2)
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
        public string CancelarServicios(int[] Servicios, int Confirmado = 1)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (var i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.CancelarServicios(Servicios[i], Confirmado);
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
        public string ValidarServiciosSP(int[] Servicios)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (var i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.ValidarServiciosSP(Servicios[i]);
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
        public string DescancelarServicios(int[] Servicios)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (var i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.DescancelarServicios(Servicios[i]);
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
        public string EliminarServicios(int[] Servicios)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.EliminarServicios(Servicios);
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
        public string AgregarServicioCelda(string Booking = "", int CantServicios = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.AgregarServicioCelda(Booking.Trim(), CantServicios);
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
        public string AgregarServicioCeldaSP(string Booking = "", int CantServicios = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.AgregarServicioCeldaSP(Booking.Trim(), CantServicios);
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
        public string ValidarServicio(string[] Bookings, string[] Servicios, string[] Controladores, string[] Commodity, string[] Contenedores, string[] FechasInicio, string[] Modems, string[] LlevaModem, string[] SP)
        {
            List<Clases.Validacion> ServiciosValidar = new List<Clases.Validacion>();
            List<Clases.Validacion> ServiciosValidarConModem = new List<Clases.Validacion>();
            List<Clases.Validacion> Validados = new List<Clases.Validacion>();
            Clases.Validacion DatosProgramados = new Clases.Validacion();
            Clases.Validacion ContenedorProgramado = new Clases.Validacion();
            Clases.Validacion EstadoControlador = new Clases.Validacion();
            Clases.Validacion ModemProgramado = new Clases.Validacion();
            Clases.Validacion EstadoModem = new Clases.Validacion();
            Clases.LogisticModem info_modem = new Clases.LogisticModem();
            Clases.Setpoint respueta_gasificado = new Clases.Setpoint();

            int i, j = 0;
            DateTime? diasmas = null;
            DateTime? diasmenos = null;
            string bookingaux = "";
            string contenedoraux = "";
            string controladoraux = "";
            string commodityaux = "";
            string modemaux = "";
            string bateriaaux = "";
            string estadoBateria = "";
            string alertaaux = "";
            string gasificadoaux = "";
            string damagereportaux = "";
            string asociacionmodemcontroladoraux = "";
            string conexionmodemappaux = "";
            string estadoControladoraux = "";
            string estadoModemaux = "";
            string controladorEnergizadoaux = "";
            string Scrubberaux = "";
            int setpointCO2 = 0;
            int setpointO2 = 0;
            bool tieneAlertaActiva = false;
            bool tieneDamageReport = false;
            bool controladorEnergizado = false;
            bool ValidacionPreZarpe = false;

            for (j = 0; j < Bookings.Length; j++)
            {
                bookingaux = "";
                contenedoraux = "";
                controladoraux = "";
                commodityaux = "";
                modemaux = "";
                alertaaux = "";
                gasificadoaux = "";
                damagereportaux = "";
                estadoControladoraux = "";
                estadoModemaux = "";
                controladorEnergizadoaux = "";
                DatosProgramados = null;
                ContenedorProgramado = null;
                EstadoControlador = null;
                ModemProgramado = null;
                EstadoModem = null;
                diasmas = null;
                diasmenos = null;
                setpointCO2 = 0;
                setpointO2 = 0;


                //obtener info servicio PTL asociado a controlador
                DatosProgramados = ReservaModelo.DatosServicioPTL_Controlador(Controladores[j]);



                /////***** ITEM: BOOKING *****/////
                if (SP[j] == "NEPTUNIA" || SP[j] == "INVERSIONES MARITIMAS UNIVERSALES PERÚ")
                {
                    bookingaux = "N.A.";
                }
                else
                {
                    if (DatosProgramados.Booking != null && DatosProgramados.Booking != "")
                    {
                        if (DatosProgramados.Booking.Trim() == Bookings[j].Trim())
                        {
                            bookingaux = "Correcto";
                        }
                        else
                        {
                            bookingaux = "Incorrecto";
                        }
                    }
                    else
                    {
                        bookingaux = "Incorrecto";
                    }
                }



                /////***** ITEM: CONTROLADOR *****/////
                // existe servicio asociado a controlador y que posea una fecha de inicio servicio en PTL cercana a fecha inst. controlador
                if (DatosProgramados.IdServicio > 0)
                {
                    if (DatosProgramados.controlador.Trim() == Controladores[j].Trim())
                    {
                        if (DatosProgramados.FechaInicioServicio != DateTime.MinValue)
                        {
                            diasmenos = Convert.ToDateTime(DatosProgramados.FechaInicioServicio).AddDays(-32);
                            diasmas = Convert.ToDateTime(DatosProgramados.FechaInicioServicio).AddDays(32);

                            if (Convert.ToDateTime(FechasInicio[j]) > diasmenos
                                && Convert.ToDateTime(FechasInicio[j]) < diasmas
                            )
                            {
                                controladoraux = "Correcto";
                            }
                            else
                            {
                                controladoraux = "Incorrecto";
                            }
                        }
                        else
                        {
                            controladoraux = "Incorrecto";
                        }
                    }
                    else
                    {
                        controladoraux = "Incorrecto";
                    }
                }
                else
                {
                    controladoraux = "Incorrecto";
                }



                /////***** ITEM: CONTROLADOR EN TRAVEL *****/////
                //busca servicio existente asociado a controlador y que posea estado controlador "TRAVELING" y estado logistico "ACTIVE TRANSIT"
                EstadoControlador = ReservaModelo.VerificarEstadoControladorValidacion(Controladores[j]);
                if (EstadoControlador.IdServicio > 0)
                {
                    estadoControladoraux = "Correcto";
                }
                else
                {
                    estadoControladoraux = "Incorrecto";
                }



                Servicio info_servicio = ReservaModelo.ObtenerInfoServicio(Convert.ToInt32(Servicios[j]));

                if (info_servicio.NombrePaisOrigen == "CHILE" || info_servicio.NombrePaisOrigen.Trim() == "PERU")
                {
                    /////***** ITEM: CONTROLADOR ENERGIZADO *****/////
                    bool ControladorEnergizadoServicio = false;
                    ControladorEnergizadoServicio = ReservaModelo.VerificarEstadoControladorEnergizadoServicio(Convert.ToInt32(Servicios[j]));
                    if (ControladorEnergizadoServicio == false)
                    {
                        controladorEnergizado = ReservaModelo.VerificarControladorEnergizado(Controladores[j]);
                    }
                    else
                    {
                        controladorEnergizado = true;
                    }


                    // GENERACION ALERTA PREZARPE
                    ValidacionPreZarpe = ReservaModelo.ObtenerValidacionPreZarpe(Convert.ToInt32(Servicios[j]));
                    if (ValidacionPreZarpe == false)
                    {
                        int alerta_prezarpe = ReservaModelo.ObtenerAlertaServicio(37, Convert.ToInt32(Servicios[j]));
                        if (alerta_prezarpe == 0)
                        {
                            ReservaModelo.GenerarAlertaServicio(37, Convert.ToInt32(Servicios[j]));
                        }
                    }


                    /////***** ITEM: SCRUBBER *****/////
                    int validacionScrubber = ReservaModelo.ValidacionScrubber(Convert.ToInt32(Servicios[j]), Contenedores[j]);
                    if (validacionScrubber == 1) Scrubberaux = "Incorrecto";
                    else Scrubberaux = "Correcto";
                }
                else
                {
                    Scrubberaux = "N.A.";
                    controladorEnergizado = true;
                }


                if (controladorEnergizado == true)
                {
                    int alerta_controladorEnergizado = ReservaModelo.ObtenerAlertaControlador(39, Controladores[j]);
                    if (alerta_controladorEnergizado > 0)
                    {
                        ReservaModelo.DeshabilitarAlertaControlador(39, Controladores[j]);
                    }

                    controladorEnergizadoaux = "Correcto";
                }
                else
                {
                    controladorEnergizadoaux = "Incorrecto";
                }













                if (LlevaModem[j] == "SI")
                {

                    /////***** ITEM: MODEM *****/////
                    if (Modems[j] != "")
                    {
                        modemaux = "Correcto";
                    }
                    else
                    {
                        modemaux = "Incorrecto";
                    }



                    /////***** ITEM: MODEM EN TRAVEL *****/////
                    EstadoModem = ReservaModelo.VerificarEstadoModemValidacion(Controladores[j]);
                    if (EstadoModem.modem != null && EstadoModem.modem != "")
                    {
                        estadoModemaux = "Correcto";
                    }
                    else
                    {
                        estadoModemaux = "Incorrecto";
                    }



                    /////***** ITEM: CONEXIÓN MODEM *****/////

                    // se obtiene fecha ult conxion con PTL del modem desde PLT tabla logisticModem 
                    info_modem = ReservaModelo.ObtenerInfoModem(Modems[j]);


                    // se obtiene fecha inst. modem desde tabla servicio1 y se convierte a UTC
                    DateTime fecha_instalacion_modem = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(info_servicio.FechaInstModem), TimeZoneInfo.Local);

                    if (info_modem.FechaUltConexionModemPTL != null && info_modem.FechaUltConexionModemPTL != DateTime.MinValue)
                    {
                        if (fecha_instalacion_modem != null && fecha_instalacion_modem != DateTime.MinValue)
                        {
                            if (info_modem.FechaUltConexionModemPTL > fecha_instalacion_modem)
                            {
                                conexionmodemappaux = "Correcto";
                            }
                            else
                            {
                                conexionmodemappaux = "Incorrecto";
                            }
                        }
                        else
                        {
                            conexionmodemappaux = "Incorrecto";
                        }
                    }
                    else
                    {
                        conexionmodemappaux = "Incorrecto";
                    }



                    /////***** ITEM: ASOCIACION MODEM CONTROLADOR *****/////

                    // obtiene el modem PTL asociado al controlador según su último registro de la tabla logistic
                    ModemProgramado = ReservaModelo.VerificarModemProgramadoValidacion(Controladores[j]);
                    if (ModemProgramado.modem != null && ModemProgramado.modem != "" && Modems[j] != "")
                    {
                        if (ModemProgramado.modem.Trim() == Modems[j].Trim())
                        {
                            asociacionmodemcontroladoraux = "Correcto";
                        }
                        else
                        {
                            asociacionmodemcontroladoraux = "Incorrecto";
                        }
                    }
                    else
                    {
                        asociacionmodemcontroladoraux = "Incorrecto";
                    }

                }
                else
                {
                    estadoModemaux = "N.A.";
                    asociacionmodemcontroladoraux = "N.A.";
                    conexionmodemappaux = "N.A.";
                    modemaux = "N.A.";
                }



                /////***** ITEM: CONTENEDOR *****/////

                if (DatosProgramados.Contenedor != null && DatosProgramados.Contenedor != "")
                {
                    if (DatosProgramados.Contenedor.Trim() == Contenedores[j].Trim())
                    {
                        contenedoraux = "Correcto";
                    }
                    else
                    {
                        contenedoraux = "Incorrecto";
                    }
                }
                else
                {
                    contenedoraux = "Incorrecto";
                }



                /////***** ITEM: COMMODITY *****/////

                // obtener id_commodity_ptl según tabla de relaciones commodity
                var IdCommodityPTL = CommodityModelo.GetIdTecnica(Commodity[j]);

                if (DatosProgramados.IdCommodity == IdCommodityPTL)
                {
                    commodityaux = "Correcto";
                }
                else
                {
                    commodityaux = "Incorrecto";
                }



                /////***** ITEM: BATERIA *****/////

                estadoBateria = BateriaModel.ObtenerEstadoBateriaByServicio(Convert.ToInt32(Servicios[j]));

                if (estadoBateria == "APROBADA")
                {
                    bateriaaux = "Correcto";
                }
                else
                {
                    bateriaaux = "Incorrecto";
                }


                /////***** ITEM: SIN ALERTA *****/////
                string alertas = "";
                alertas = ReservaModelo.ObtenerStringAlertasServicio(Convert.ToInt32(Servicios[j]));
                if (alertas != "")
                {
                    string[] array_alertas = alertas.Split('-');
                    string alerta = "";
                    foreach (var item in array_alertas)
                    {
                        alerta = item.Trim();
                        if (alerta != "SIN VALIDACION PREZARPE")
                        {
                            if (alerta == "CONTENEDOR SIN SCRUBBER")
                            {
                                int validacionScrubber = ReservaModelo.ValidacionScrubber(Convert.ToInt32(Servicios[j]), Contenedores[j]);
                                if (validacionScrubber == 1) tieneAlertaActiva = true;
                            }
                            else
                            {
                                tieneAlertaActiva = true;
                            }
                        }
                    }
                }

                if (tieneAlertaActiva == true)
                {
                    alertaaux = "Incorrecto";
                }
                else
                {
                    alertaaux = "Correcto";
                }



                /////***** ITEM: GASIFICACION *****/////
                if (Commodity[j].Contains("BLUEBERRY")
                        || Commodity[j].Contains("PEACH")
                        || Commodity[j].Contains("PLUM")
                        || Commodity[j].Contains("KIWI")
                        || Commodity[j].Contains("NECTARINE")
                        || Commodity[j].Contains("ZARZAPARRILLA")
                    )
                {
                    setpointCO2 = ReservaModelo.ObtenerSetpointCO2(Convert.ToInt32(Servicios[j]));
                    setpointO2 = ReservaModelo.ObtenerSetpointO2(Convert.ToInt32(Servicios[j]));
                    respueta_gasificado = ReservaModelo.ValidarGasificado(Convert.ToInt32(Servicios[j]), Controladores[j], FechasInicio[j], setpointCO2, setpointO2);
                }
                else
                {
                    respueta_gasificado.Activo = 2;
                }



                if (respueta_gasificado.Activo == 2)
                {
                    gasificadoaux = "N.A.";
                }
                else
                {
                    if (respueta_gasificado.Activo == 1) gasificadoaux = "<p>Correcto</p>";
                    else gasificadoaux = "<p>Incorrecto</p>";

                    gasificadoaux = gasificadoaux
                        + "<p>"
                        + "CO2: " + respueta_gasificado.CO2.ToString() + "%, "
                        + "O2: " + respueta_gasificado.O2.ToString() + "%"
                        + "</p>";
                }





                /////***** ITEM: DAMEGE REPORT *****/////
                tieneDamageReport = ReservaModelo.ObtenerDamageReportByServicio(Convert.ToInt32(Servicios[j]));
                if (tieneDamageReport == true)
                {
                    damagereportaux = "Incorrecto";
                }
                else
                {
                    damagereportaux = "Correcto";
                }





                int resultado_validacion_tecnica = 1;
                if ((bookingaux == "Correcto" || bookingaux == "N.A.")
                        && (controladoraux == "Correcto" || controladoraux == "N.A.")
                        && (controladorEnergizadoaux == "Correcto" || controladorEnergizadoaux == "N.A.")
                        && (estadoControladoraux == "Correcto" || estadoControladoraux == "N.A.")
                        && (modemaux == "Correcto" || modemaux == "N.A.")
                        && (estadoModemaux == "Correcto" || estadoModemaux == "N.A.")
                        && (conexionmodemappaux == "Correcto" || conexionmodemappaux == "N.A.")
                        && (asociacionmodemcontroladoraux == "Correcto" || asociacionmodemcontroladoraux == "N.A.")
                        && (contenedoraux == "Correcto" || contenedoraux == "N.A.")
                        && (commodityaux == "Correcto" || commodityaux == "N.A.")
                        && (bateriaaux == "Correcto" || bateriaaux == "N.A.")
                        && (alertaaux == "Correcto" || alertaaux == "N.A.")
                        && respueta_gasificado.Activo != 0
                        && (damagereportaux == "Correcto" || damagereportaux == "N.A.")
                        && (Scrubberaux == "Correcto" || Scrubberaux == "N.A.")
                   )
                {
                    resultado_validacion_tecnica = 0;
                }

                Validados.Add(new Clases.Validacion
                {
                    IdServicio = Convert.ToInt32(Servicios[j]),
                    Validado = resultado_validacion_tecnica,
                    Bookingaux = bookingaux,
                    Controladoraux = controladoraux,
                    Contenedoraux = contenedoraux,
                    Modemaux = modemaux,
                    Commodityaux = commodityaux,
                    Bateriaaux = bateriaaux,
                    Alertaaux = alertaaux,
                    Gasificadoaux = gasificadoaux,
                    DamageReportaux = damagereportaux,
                    AsociacionModemControladoraux = asociacionmodemcontroladoraux,
                    ConexionModemAppTecnicaaux = conexionmodemappaux,
                    EstadoControladoraux = estadoControladoraux,
                    EstadoModemaux = estadoModemaux,
                    ControladorEnergizadoaux = controladorEnergizadoaux,
                    Scrubber = Scrubberaux
                });

                if (resultado_validacion_tecnica == 0)
                {
                    //se valida el servicio tecnicamente
                    ReservaModelo.ActualizarValidacionServicio(Convert.ToInt32(Servicios[j]), 0);

                    //se inserta en tabla facturación
                    ReservaModelo.ServicioParaFacturar(Convert.ToInt32(Servicios[j]));
                }
                else
                {
                    ReservaModelo.ActualizarValidacionServicio(Convert.ToInt32(Servicios[j]), 1);
                }

            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Validados);
            return datos;
        }

        [HttpPost]
        public string ValidarPreZarpe(string[] Servicios)
        {
            List<ResultadoValidacionPrezarpe> resultados = new List<ResultadoValidacionPrezarpe>();
            foreach (var servicio in Servicios)
            {
                ResultadoValidacionPrezarpe resultado_prezarpe = new ResultadoValidacionPrezarpe();
                resultado_prezarpe.IdServicio = Convert.ToInt32(servicio);

                Servicio info_servicio = ReservaModelo.ObtenerInfoServicio(resultado_prezarpe.IdServicio);

                // Validar Controlador con Descarga de datos en PTL
                bool validacion_descarga_datos = ValidarDescargaDeDatosControlador(info_servicio.Controlador);

                // Validar Fault Analysis
                bool validacion_fault_analysis = false;
                if (validacion_descarga_datos)
                {
                    validacion_fault_analysis = ValidarFaultAnalysisServicio(info_servicio.Controlador);
                }


                resultado_prezarpe.Resultado = 1;
                if (validacion_descarga_datos && validacion_fault_analysis)
                {
                    resultado_prezarpe.Resultado = 0;
                }
                else
                {
                    if (!validacion_descarga_datos)
                    {
                        resultado_prezarpe.Resultado = 2;
                    }
                    else
                    {
                        resultado_prezarpe.Resultado = 3;
                    }
                }

                resultados.Add(resultado_prezarpe);
                int respuesta_edicion = ReservaModelo.ActualizarValidacionPrezarpeServicio(resultado_prezarpe.Resultado, resultado_prezarpe.IdServicio);
            }

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(resultados);
            return datos;
        }

        [HttpPost]
        public bool ValidarDescargaDeDatosControlador(string Controlador)
        {
            bool respuesta = false;

            if (Controlador != null)
            {
                Clases.InfoServicioPTL infoServicioPTL = ReservaModelo.ObtenerInfoServicioPTL_SegunControlador(Controlador);
                DateTime fecha_inicio = new DateTime();

                if (infoServicioPTL.lastDataDownload > fecha_inicio
                    && infoServicioPTL.faultAnalysisResult != "ERR - 6")
                {
                    respuesta = true;
                }
            }

            return respuesta;
        }

        [HttpPost]
        public bool ValidarFaultAnalysisServicio(string Controlador)
        {
            bool respuesta = false;
            bool falso_positivo = true;
            float falla_sensor_co2 = 0;
            float falla_sensor_o2 = 0;
            float valvula_trabada_cerrada = 0;
            float valvula_trabada_abierta = 0;
            string bateria_bajo_75 = "";
            float gas_fuera_rango = 0;
            float fuga_leve = 0;
            float fuga_media = 0;
            float fuga_alta = 0;
            float contenedor_anaerobico = 0;
            float scrubber_no_absorbe = 0;
            float scrubber_con_fuga = 0;
            float temperatura_fuera_rango = 0;

            if (Controlador != null)
            {
                Clases.InfoServicioPTL infoServicioPTL = ReservaModelo.ObtenerInfoServicioPTL_SegunControlador(Controlador);
                List<Clases.ResultadoFaultAnalysisPTL> lista_resultadoFaultAnalysis = ReservaModelo.ObtenerResultadosFaultAnalysisPTL(infoServicioPTL.id);

                foreach (var resultadoFA in lista_resultadoFaultAnalysis)
                {
                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 104)
                    {
                        falla_sensor_co2 = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 105)
                    {
                        falla_sensor_o2 = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 102)
                    {
                        valvula_trabada_cerrada = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 103)
                    {
                        valvula_trabada_abierta = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 106)
                    {
                        bateria_bajo_75 = resultadoFA.faultResult;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 1)
                    {
                        gas_fuera_rango = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 2)
                    {
                        fuga_leve = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 3)
                    {
                        fuga_media = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 4)
                    {
                        fuga_alta = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 5)
                    {
                        contenedor_anaerobico = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 7)
                    {
                        scrubber_no_absorbe = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 8)
                    {
                        scrubber_con_fuga = resultadoFA.faultTimePercent;
                    }

                    if (resultadoFA.nrStretch == 1000 && resultadoFA.faultCode == 9)
                    {
                        temperatura_fuera_rango = resultadoFA.faultTimePercent;
                    }
                }


                if (infoServicioPTL.faultAnalysisResult != "FAULTY"
                    && infoServicioPTL.faultAnalysisResult != "INDET")
                {
                    falso_positivo = false;
                }

                if (falla_sensor_co2 > 0.01
                    || falla_sensor_o2 > 0.01
                    || valvula_trabada_cerrada > 0
                    || valvula_trabada_abierta > 0
                    || bateria_bajo_75 == "FAULTY"
                    || gas_fuera_rango > 0.3
                    || fuga_leve > 0.2
                    || fuga_media > 0.05
                    || fuga_alta > 0.05
                    || contenedor_anaerobico > 0.02
                    || scrubber_no_absorbe > 0.02
                    || scrubber_con_fuga > 0.02
                    || scrubber_con_fuga > 0.9)
                {
                    falso_positivo = false;
                }


                if (infoServicioPTL.faultAnalysisResult == "OK")
                {
                    respuesta = true;
                }
                else
                {
                    if (falso_positivo)
                    {
                        respuesta = true;
                    }
                }

            }

            return respuesta;
        }

        public JsonResult GetHistoricoServiciosMsc()
        {
            List<Clases.ServicioCompleto> ReservasyServicios = new List<Clases.ServicioCompleto>();
            ReservasyServicios = ReservaModelo.GetHistoricoServiciosMsc();
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        [HttpPost]
        public string EditarDepositoServicio(int IdServicio, string Pais, string Deposito)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.EditarDepositoServicio(IdServicio, Pais, Deposito);
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
        public string HistoricoControladorServicio(int[] Servicios)
        {
            List<Clases.ServicioCompleto> AuditoriaServicio = new List<Clases.ServicioCompleto>();
            AuditoriaServicio = ReservaModelo.GetModificacionesServicio(Servicios[0]);
            AuditoriaServicio = ReservaModelo.GetAlertasServicios(AuditoriaServicio);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AuditoriaServicio);
            return datos;
        }

        [HttpPost]
        public string QuitarContenedor(string NumContenedor, int IdServicio)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.QuitarContenedor(NumContenedor.Trim(), IdServicio);
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
        public string QuitarControlador(string NumControlador, int IdServicio)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.QuitarControlador(NumControlador.Trim(), IdServicio);
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
        public string QuitarModem(string NumModem, int IdServicio)
        {

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.QuitarModem(NumModem.Trim(), IdServicio);
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
        public string GetInfoBooking(string Booking)
        {
            Clases.Reserva Reserva = new Clases.Reserva();
            int IdReserva = ReservaModelo.GetIdReservaByBooking(Booking);
            Reserva = ReservaModelo.GetReservaByIdServicio(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reserva);
            return datos;
        }

        [HttpPost]
        public string GetInfoBookingId(string Booking)
        {
            Clases.Reserva Reserva = new Clases.Reserva();
            int IdReserva = ReservaModelo.GetIdReservaByBooking(Booking);
            Reserva = ReservaModelo.GetInfoBookingId(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reserva);
            return datos;
        }

        [HttpPost]
        public string RolearServicio(string Booking = "", int IdServicio = 0, string BookingAntiguo = "")
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.RolearServicio(Booking, IdServicio, BookingAntiguo);
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
        public string RoleoServicio(string Booking, int IdNaviera, int IdExportador, int IdCommodity, int IdPuertoOrigen, int IdPuertoDestino, int IdNave, int IdSetpoint, string Viaje, int CantServicios, string Temperatura, DateTime Etd, int IdServiceProvider, DateTime? FechaIniStacking = null, DateTime? FechaFinStacking = null, DateTime? EtaNave = null, DateTime? Eta = null, string Consignatario = "", int IdFreightForwarder = 0, int IdServicio = 0)
        {
            Clases.Reserva ReservaNueva = new Clases.Reserva();

            ReservaNueva.Booking = Booking.Trim();
            ReservaNueva.IdNaviera = IdNaviera;
            ReservaNueva.IdExportador = IdExportador;
            ReservaNueva.IdCommodity = IdCommodity;
            ReservaNueva.IdPuertoDestino = IdPuertoDestino;
            ReservaNueva.IdPuertoOrigen = IdPuertoOrigen;
            ReservaNueva.Eta = HoraFecha(Eta.ToString());
            ReservaNueva.IdNave = IdNave;
            ReservaNueva.IdFreightForwarder = IdFreightForwarder;
            ReservaNueva.Consignatario = Consignatario;
            ReservaNueva.IdSetpoint = IdSetpoint;
            ReservaNueva.FechaIniStacking = HoraFecha(FechaIniStacking.ToString());
            ReservaNueva.FechaFinStacking = HoraFecha(FechaFinStacking.ToString());
            ReservaNueva.Viaje = Viaje;
            ReservaNueva.Etd = HoraFecha(Etd.ToString());
            ReservaNueva.CantidadServicios = CantServicios;
            ReservaNueva.Temperatura = float.Parse(Temperatura, CultureInfo.InvariantCulture); ;
            ReservaNueva.EtaNave = HoraFecha(EtaNave.ToString());
            ReservaNueva.IdServiceProvider = IdServiceProvider;

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.RoleoServicio(ReservaNueva, IdServicio);
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
        public string RoleoServicioSP(string Booking, int IdNaviera, int IdExportador, int IdCommodity, int IdPuertoOrigen, int IdPuertoDestino, int IdNave, int IdSetpoint, string Viaje, int CantServicios, string Temperatura, DateTime Etd, DateTime? FechaIniStacking = null, DateTime? FechaFinStacking = null, DateTime? EtaNave = null, DateTime? Eta = null, string Consignatario = "", int IdFreightForwarder = 0, int IdServicio = 0)
        {
            Clases.Reserva ReservaNueva = new Clases.Reserva();

            ReservaNueva.Booking = Booking.Trim();
            ReservaNueva.IdNaviera = IdNaviera;
            ReservaNueva.IdExportador = IdExportador;
            ReservaNueva.IdCommodity = IdCommodity;
            ReservaNueva.IdPuertoDestino = IdPuertoDestino;
            ReservaNueva.IdPuertoOrigen = IdPuertoOrigen;
            ReservaNueva.Eta = HoraFecha(Eta.ToString());
            ReservaNueva.IdNave = IdNave;
            ReservaNueva.IdFreightForwarder = IdFreightForwarder;
            ReservaNueva.Consignatario = Consignatario;
            ReservaNueva.IdSetpoint = IdSetpoint;
            ReservaNueva.FechaIniStacking = HoraFecha(FechaIniStacking.ToString());
            ReservaNueva.FechaFinStacking = HoraFecha(FechaFinStacking.ToString());
            ReservaNueva.Viaje = Viaje;
            ReservaNueva.Etd = HoraFecha(Etd.ToString());
            ReservaNueva.CantidadServicios = CantServicios;
            ReservaNueva.Temperatura = float.Parse(Temperatura, CultureInfo.InvariantCulture); ;
            ReservaNueva.EtaNave = HoraFecha(EtaNave.ToString());

            Clases.Validar aux = new Clases.Validar();
            int Flag = ReservaModelo.RoleoServicioSP(ReservaNueva, IdServicio);
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
        public string GetDatosBookingEdi(string Booking)
        {
            Clases.Reserva Reserva = new Clases.Reserva();
            int IdReserva = ReservaModelo.GetIdReservaByBooking(Booking);
            Reserva = ReservaModelo.GetDatosBookingEdi(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Reserva);
            return datos;
        }

        [HttpPost]
        public string ValidarServicioNoTecnica(int[] Servicios, string[] Contenedores, string[] Controladores, string[] Validado, string[] ValidadoPreZarpe, string[] SelloSecurity, string[] TipoLugarInstalacion, string[] LugarInstalacion, string[] SP, string[] TecnicoControlador, string[] TipoLugarCortina, string[] TecnicoCortina, string[] EtaPOD)
        {
            Clases.Validar aux = new Clases.Validar();
            List<Clases.Validacion> Validados = new List<Clases.Validacion>();
            int Flag = 0;
            bool ValidacionPreZarpe = true;

            for (var i = 0; i < Servicios.Count(); i++)
            {
                ValidacionPreZarpe = true;
                Servicio info_servicio = ReservaModelo.ObtenerInfoServicio(Convert.ToInt32(Servicios[i]));
                if (info_servicio.NombrePaisOrigen == "CHILE" || info_servicio.NombrePaisOrigen.Trim() == "PERU")
                {
                    if (ValidadoPreZarpe[i] != "SI")
                    {
                        ValidacionPreZarpe = false;
                    }
                }


                if (SP[i].Trim() == "LIVENTUS")
                {
                    if (Contenedores[i] != " " && Controladores[i] != " " && Validado[i] == "SI" && ValidacionPreZarpe)
                    {
                        Flag = ReservaModelo.ValidarServicioNoTecnica(Servicios[i], 0);

                        Validados.Add(new Clases.Validacion
                        {
                            IdServicio = Convert.ToInt32(Servicios[i]),
                            Validado = Flag
                        });
                    }
                    else
                    {
                        Flag = ReservaModelo.ValidarServicioNoTecnica(Servicios[i], 1);

                        Validados.Add(new Clases.Validacion
                        {
                            IdServicio = Convert.ToInt32(Servicios[i]),
                            Validado = 1
                        });
                    }
                }
                else
                {
                    if (Contenedores[i] != "" && Controladores[i] != "" && Validado[i] == "SI" && ValidacionPreZarpe)
                    {
                        Flag = ReservaModelo.ValidarServicioNoTecnica(Servicios[i], 0);

                        Validados.Add(new Clases.Validacion
                        {
                            IdServicio = Convert.ToInt32(Servicios[i]),
                            Validado = Flag
                        });
                    }
                    else
                    {
                        Flag = ReservaModelo.ValidarServicioNoTecnica(Servicios[i], 1);

                        Validados.Add(new Clases.Validacion
                        {
                            IdServicio = Convert.ToInt32(Servicios[i]),
                            Validado = 1
                        });
                    }
                }
            }

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Validados);
            return datos;
        }

        [HttpPost]
        public string ValidarServicioNoTecnica2(int[] Servicios)
        {
            Clases.Validar aux = new Clases.Validar();
            List<Clases.Validacion> Validados = new List<Clases.Validacion>();
            int Flag = 0;

            for (var i = 0; i < Servicios.Count(); i++)
            {

                Flag = ReservaModelo.ValidarServicioNoTecnica(Servicios[i], 0);

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
        public string GenerarGraficos1(string[] Servicios)
        {

            Clases.Validar aux = new Clases.Validar();

            int Flag = 0;
            //string data = "\"";
            string data = "";
            for (var i = 0; i < Servicios.Count(); i++)
            {
                Flag = 0;
                data += Servicios[i] + ", ";
            }
            //data += " \"";
            // pathToProgram = Path.GetPathRoot(Environment.CurrentDirectory).Replace("\\","/");
            string pathToProgram = "C:/Users/Administrator/Documents/Visual Studio 2015/Projects/ConsoleApplication4/ConsoleApplication4/bin/Debug/ConsoleApplication4.exe";
            string dia = DateTime.Now.ToString("yyyyMMddHHmm");
            string correo = Session["Correo"].ToString();
            string[] argsString = { data, dia, correo };
            var process = Process.Start(pathToProgram, String.Join(" ", argsString));
            process.WaitForExit();
            //string correo = HttpContext.Current.Session["user"].ToString();
            var exitCode = process.ExitCode;
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
        public string GenerarGraficosVista(string[] Servicios)
        {
            Clases.Validar aux = new Clases.Validar();
            string[] args = { };
            int Flag = 0;
            string data = "";
            for (var i = 0; i < Servicios.Count(); i++)
            {
                Flag = 0;
                data += Servicios[i] + ", ";
            }
            data += " ";
            //string pathToProgram = "C:/Users/Administrator/Documents/Visual Studio 2015/Projects/ConsoleApplication4/ConsoleApplication4/bin/Debug/ConsoleApplication4.exe";
            string dia = DateTime.Now.ToString("yyyyMMddHHmm");
            string correo = Session["Correo"].ToString();
            string[] argsString = { data, dia };

            string comando = "E:/ScriptGraficosPython/main.py";
            string argumentos = argsString[0];
            string timestamp = argsString[1];
            //Console.WriteLine(argumentos);
            string resultado = run_cmd(comando, argumentos, timestamp);

            string contenedor = resultado.Substring(0, resultado.IndexOf("\r\n"));

            if (contenedor != "NOVALUE")
            {
                string abspath = "E:/ScriptGraficosPython/Graficos" + "/" + timestamp + "/" + contenedor + "/CO2_O2/" + contenedor + ".png";
                string Proyecto = System.Web.HttpContext.Current.Server.MapPath("../Graficos/" + contenedor + ".png");

                if (System.IO.File.Exists(Proyecto))
                {
                    System.IO.File.Delete(Proyecto);
                    System.IO.Directory.Move(abspath, Proyecto);
                }
                else
                {
                    System.IO.Directory.Move(abspath, Proyecto);
                }

                bool existe = System.IO.File.Exists("E:/ScriptGraficosPython/Graficos" + "/" + timestamp + ".zip");

                if (existe)
                {
                    Directory.Delete("E:/ScriptGraficosPython/Graficos" + "/" + timestamp, true);
                    System.IO.File.Delete("E:/ScriptGraficosPython/Graficos" + "/" + timestamp + ".zip");
                }
                return contenedor;
            }
            else
            {
                return "NOGRAFICO";
            }

        }

        public static string run_cmd(string cmd, string args, string timestamp)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:/ProgramData/Anaconda3/envs/py34/python.exe";
            start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" ", cmd, args, timestamp);
            Console.WriteLine(start.Arguments);
            //Console.WriteLine(start.Arguments);
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    return result;
                }
            }
        }

        [HttpPost]
        public string BorrarGrafico(string Contenedor)
        {
            string destination = System.Web.HttpContext.Current.Server.MapPath("../Graficos/" + Contenedor + ".png");
            if (System.IO.File.Exists(destination))
            {
                System.IO.File.Delete(destination);
                return "Ok";
            }
            else
            {
                return "No";
            }
        }

        [HttpPost]
        public string GenerarGraficosUnitario(string Bookings, string Servicios, string Controladores, string Commodity, string Contenedores, string FechasInicio)
        {
            List<Clases.Validacion> ServiciosValidar = new List<Clases.Validacion>();
            int i = 0;
            string ServiceData = "";
            DateTime? diasmas = null;
            DateTime? diasmenos = null;
            bool valaux = false;
            FechasInicio = FechasInicio.Substring(0, 10);
            ServiciosValidar = ReservaModelo.GetServiceData();

            var ServicioWData = ServiciosValidar.Where(x => x.controlador.Trim() == Controladores.Trim()).FirstOrDefault();
            if (ServicioWData != null)
            {
                diasmas = Convert.ToDateTime(ServicioWData.FechaInicioServicio).AddDays(32);
                diasmenos = Convert.ToDateTime(ServicioWData.FechaInicioServicio).AddDays(-32);
                var commodity = CommodityModelo.GetIdTecnica(Commodity);
                if (ServicioWData.Booking.Trim() == Bookings.Trim() && ServicioWData.controlador.Trim() == Controladores.Trim() && ServicioWData.Contenedor.Trim() == Contenedores.Trim() && ServicioWData.IdCommodity == commodity && (diasmas > Convert.ToDateTime(FechasInicio) && diasmenos < Convert.ToDateTime(FechasInicio)))
                {
                    ServiceData = ServicioWData.ServiceData;
                }
                else
                {
                    ServiceData = "";
                }
            }
            else
            {
                ServiceData = "";
            }

            return ServiceData;
        }

        [HttpPost]
        public string GetGestion(string idServicio)
        {

            string Servicio = ReservaModelo.GetGestion(Convert.ToInt32(idServicio));
            return Servicio;
        }

        public static DateTime? HoraFecha(string Fecha)
        {
            if (Fecha != "")
            {
                var src = DateTime.Now;
                var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
                var hola = hm.ToString();
                hola = hola.Substring(11);
                var hola2 = Fecha.ToString().Substring(0, 10);
                var fecha = hola2 + " " + hola;
                return Convert.ToDateTime(fecha);
            }
            else
            {
                return null;
            }
        }

        public JsonResult GetServiciosFacturacion()
        {
            List<Clases.Facturacion> ReservasyServicios = new List<Clases.Facturacion>();
            ReservasyServicios = ReservaModelo.GetServiciosFacturacion();
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        [HttpPost]
        public string EditarCeldaFacturacion(int IdServicio, string Campo, string Valor, string Columna, int IdCentro = 0, int Tamano = 0)
        {
            Clases.Validar aux = new Clases.Validar();

            int Flag = ReservaModelo.EditarCeldaFacturacion(IdServicio, Campo, Valor, Columna, IdCentro, Tamano);
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
        public string GetGestionFacturacion(string idServicio)
        {

            string Servicio = ReservaModelo.GetGestionFacturacion(Convert.ToInt32(idServicio));
            return Servicio;
        }

        [HttpPost]
        public string GetCentroCostos()
        {
            List<Clases.CentroCostos> Centros = new List<Clases.CentroCostos>();
            Centros = ReservaModelo.GetCentroCostos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Centros);
            return datos;
        }

        [HttpPost]
        public string EdicionMasivaFactura(int[] Servicios, string Factura = "", string Precio = "", string TipoCambio = "", int Centro = 0, string PrecioNota = "", string TipoCambioNota = "", int Tamano = 0, string Fecha = "", string Gestion = "", string FechaGestion = "")
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            for (int i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.EdicionMasivaFactura(Servicios[i], Factura, Precio, TipoCambio, Centro, PrecioNota, TipoCambioNota, Tamano, Fecha, Gestion, FechaGestion);
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

        public JsonResult GetServiciosFacturacionById(int servicio)
        {
            List<Clases.Facturacion> ReservasyServicios = new List<Clases.Facturacion>();
            ReservasyServicios = ReservaModelo.GetServiciosFacturacionById(servicio);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetServiciosById(int servicio)
        {
            List<Clases.Facturacion> ReservasyServicios = new List<Clases.Facturacion>();
            ReservasyServicios = ReservaModelo.GetServiciosById(servicio);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public string EliminarFacturas(int[] Servicios)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            for (int i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.EliminarFacturas(Servicios[i]);
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

        public JsonResult GetServiciosFacturacionHistorica()
        {
            List<Clases.Facturacion> ReservasyServicios = new List<Clases.Facturacion>();
            ReservasyServicios = ReservaModelo.GetServiciosFacturacionHistorica();
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult FiltrarFacturados(int Inicio, int Fin)
        {
            List<Clases.Facturacion> ReservasyServicios = new List<Clases.Facturacion>();
            ReservasyServicios = ReservaModelo.FiltrarFacturados(Inicio, Fin);
            var resultados = Json(ReservasyServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public string FacturarServicio(int[] Servicios)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();
            for (int i = 0; i < Servicios.Count(); i++)
            {
                Flag = ReservaModelo.FacturarServicio(Servicios[i]);
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

        public string VerCambiosReserva()
        {
            List<Clases.CambioReserva> Cambios = new List<Clases.CambioReserva>();
            Cambios = ReservaModelo.VerCambiosReserva();
            string resultados = Newtonsoft.Json.JsonConvert.SerializeObject(Cambios);
            return resultados;
        }

        public string AccionCambiosReserva(string Accion, string Booking, string Item, string ValorActual, string ValorEdi)
        {
            int Flag = 0;
            Clases.Validar aux = new Clases.Validar();

            Flag = ReservaModelo.AccionCambiosReserva(Accion, Booking, Item, ValorActual, ValorEdi);
            if (Flag == 0)
            {
                aux.Mensaje = "Operación Realizada Correctamente.";
                aux.validador = Flag;
            }
            else
            {
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = Flag;
            }

            string resultados = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return resultados;
        }

        public string obtenerCO2_SP(string setpoint)
        {
            string respuesta = "";
            string[] array = setpoint.Split(' ');
            if (array[1] == "CO2%,")
            {
                respuesta = array[0] + "%";
            }

            return respuesta;
        }
        public string obtenerO2_SP(string setpoint)
        {
            string respuesta = "";
            string[] array = setpoint.Split(' ');
            if (array[3] == "O2%")
            {
                respuesta = array[2] + "%";
            }

            return respuesta;
        }

        /*public string ObtenerMuestrasServicio(int IdServicio)
        {
            List<Muestra> muestras = ReservaModelo.ObtenerMuestrasServicio(IdServicio);
            string resultados = Newtonsoft.Json.JsonConvert.SerializeObject(muestras);
            return resultados;
        }*/

        public string ObtenerInfoServicio(int IdServicio)
        {
            Servicio info_servicio = ReservaModelo.ObtenerInfoServicio(IdServicio);
            string resultados = Newtonsoft.Json.JsonConvert.SerializeObject(info_servicio);
            return resultados;
        }

    }

}
