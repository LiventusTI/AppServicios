using CrystalDecisions.CrystalReports.Engine;
using GemBox.Spreadsheet;
using Newtonsoft.Json;
using Plataforma.Models;
using Plataforma.Models.Contenedor;
using Plataforma.Models.Controlador;
using Plataforma.Models.Retrieval;
using Plataforma.Models.Usuario;
using Plataforma.Models.TipoLugar;
using Plataforma.Models.Nave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Plataforma.Models.Bateria;

namespace Plataforma.Controllers
{
    public class ControladorController : Controller
    {
        [HttpPost]
        public string EnviarControladoresServicio(string[] ControladoresValidados, string[] ControladoresNoValidados)
        {
            string datos = "";
            Clases.Validar aux = new Clases.Validar();
            List<Clases.MovimientoLogistico> ListaControladores = new List<Clases.MovimientoLogistico>();
            List<Clases.MovimientoLogistico> MovimientoLogistico = new List<Clases.MovimientoLogistico>();
            Clases.Usuario Usuario = new Clases.Usuario();
            Clases.Controlador Controlador = new Clases.Controlador();
            string correos = "";
            string correscopias = "";
            Usuario = UsuarioModelo.GetPerfilByUser(Session["user"].ToString().ToUpper());
            string Correo = "";
            if (ControladoresValidados != null)
            {
                for (int i = 0; i < ControladoresValidados.Count(); i++)
                {
                    Correo +=
                        "<tr>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ControladoresValidados[i] + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>SI</td>" +
                        "</tr>";
                }
            }

            if (ControladoresNoValidados != null)
            {
                for (int i = 0; i < ControladoresNoValidados.Count(); i++)
                {
                    Correo +=
                        "<tr>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ControladoresNoValidados[i] + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>NO</td>" +
                        "</tr>";
                }
            }


            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(Usuario.Correo);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Controladores con/sin servicio asociado";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            if (Usuario.Correo != "")
            {
                mmsg.CC.Add(Usuario.Correo);
            }


            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //if (correscopias != "")
            //{
            //    mmsg.Bcc.Add(correscopias); //Opcional
            //}

            //Cuerpo del Mensaje

            int sumaControllers = 0;

            if (ControladoresValidados != null)
            {
                sumaControllers += ControladoresValidados.Count();
            }

            if (ControladoresNoValidados != null)
            {
                sumaControllers += ControladoresNoValidados.Count();
            }

            if (sumaControllers > 30)
            {
                // If using Professional version, put your serial key below.
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                ExcelFile ef = new ExcelFile();
                ExcelWorksheet ws = ef.Worksheets.Add("Tables");

                // Add some data
                object[,] data = new object[1, 2]
                {
                        { "CONTROLLER (ESN)", "TIENE SERVICIO ASOCIADO" },
                };

                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        ws.Cells[i, j].Value = data[i, j];
                    }
                }
                if (ControladoresValidados != null)
                {
                    for (int i = 0; i < ControladoresValidados.Count(); i++)
                    {
                        ws.Cells[i + 1, 0].Value = ControladoresValidados[i];
                        ws.Cells[i + 1, 1].Value = "SI";

                    }
                }

                if (ControladoresNoValidados != null)
                {
                    for (int i = 0; i < ControladoresNoValidados.Count(); i++)
                    {
                        ws.Cells[i + 1, 0].Value = ControladoresNoValidados[i];
                        ws.Cells[i + 1, 1].Value = "NO";

                    }
                }

                mmsg.Body = "<p>Adjunto se encuentra el detalle de los " + sumaControllers + " subidos mediante carga masiva.</p>";

                Path.GetFullPath("Controladores.xlsx");
                ef.Save(Path.Combine(Server.MapPath("~/RetrievalDocumentos"), "Controladores.xlsx"));

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(Path.Combine(Server.MapPath("~/RetrievalDocumentos"), "Controladores.xlsx"));
                mmsg.Attachments.Add(attachment);
            }
            else
            {
                mmsg.Body = "<p>A continuación los controladores cargados mediante Excel para salida a nodo especifico por courier.</p>" +
                            "<p>Si un controlador no tiene asociado servicio, no se podrá cargar mediante excel como salida por courier a Liventus Lab.</p>" +
                            "<table style='border: 1px solid black; border-collapse: collapse; width:100%'>" +
                                "<thead>" +
                                    "<tr>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTROLLER (ESN)</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TIENE SERVICIO ASOCIADO</th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" + Correo +
                                "</tbody>" +
                            "</table>";
            }




            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(Usuario.Correo);


            /*-------------------------CLIENTE DE CORREO----------------------*/
            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            string host = Request.Url.Host;
            if (host == "localhost")
            {
                //cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Liventus2018");
                //cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Office365coke123");
                cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Marzo2017");
            }
            else
            {
                cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Liventus2019");
            }

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail

            cliente.Port = 587;
            cliente.EnableSsl = true;


            //cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";
            cliente.Host = "outlook.office365.com";



            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
                aux.Mensaje = "Operación Realizada Correctamente";
                aux.validador = 0;
                datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                return datos;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = 1;
                datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                return datos;
            }

        }

        [HttpPost]
        public string EnviarCorreo(string[] Controladores, string[] Modems, int Retrieval, int Involucrados = 0)
        {
            string datos = "";
            Clases.Validar aux = new Clases.Validar();
            List<Clases.MovimientoLogistico> ListaControladores = new List<Clases.MovimientoLogistico>();
            List<Clases.MovimientoLogistico> MovimientoLogistico = new List<Clases.MovimientoLogistico>();
            Clases.RetrievalProvider RetrievalProvider = new Clases.RetrievalProvider();
            Clases.Usuario Usuario = new Clases.Usuario();
            //Clases.Controlador Controlador = new Clases.Controlador();
            Clases.ESN ESN = new Clases.ESN();
            RetrievalProvider = RetrievalModelo.GetRetrievalProviderById(Retrieval);
            string suplier = "LIVENTUS";
            int CantControladores = 0;

            if (RetrievalProvider == null)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                aux.validador = 1;
                datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                return datos;
            }
            else
            {
                bool correo_especial = false; //Correo especial para RP con Nodo: ROTTERDAM y Naviera: MSC

                //PRUEBAS: if ((RetrievalProvider.IdNodo == 4 || RetrievalProvider.IdNodo == 1011) && RetrievalProvider.IdNaviera == 52)
                if (RetrievalProvider.IdNodo == 1011 && RetrievalProvider.IdNaviera == 52)
                {
                    correo_especial = true;
                }

                string correos = "";
                string correscopias = "";
                string correosCopiasOcultas = "ssepulveda@liventusglobal.com";

                if (RetrievalProvider.Correo != "W/O DATA")
                {
                    correos = correos + RetrievalProvider.Correo;

                }
               
                Usuario = UsuarioModelo.GetPerfilByUser(Session["user"].ToString().ToUpper());
                string Correo = "";

                if (Involucrados == 1)
                {
                    //Notificacion de controlador y modem
                    for (int i = 0; i < Controladores.Count(); i++)
                    {
                        if (Controladores[i].ToString() != "")
                        {
                            ESN = ControladorModel.GetIdESN(Controladores[i].ToString());
                            MovimientoLogistico = ControladorModel.GetESNById(ESN.Id, ESN.TipoESN);
                            string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                            AgregarNotificacion(ESN.Id, ESN.TipoESN, Retrieval, Session["user"].ToString().ToUpper(), Involucrados);
                            ListaControladores.Add(MovimientoLogistico[0]);
                            string eta = "";
                            if (ListaControladores[i].EtaString != "" && ListaControladores[i].EtaString != null)
                            {
                                eta = ListaControladores[i].EtaString.Substring(0, 10);
                            }

                            if (correo_especial)
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + booking + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + suplier + "</td>" +
                                "</tr>";
                            }
                            else
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].NodoDestino + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Controlador + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Modem + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + booking + "</td>" +
                                "</tr>";
                            }
                            CantControladores++;
                        }
                        else
                        {
                            ESN = ControladorModel.GetIdESN(Modems[i].ToString());
                            MovimientoLogistico = ControladorModel.GetESNById(ESN.Id, ESN.TipoESN);
                            string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                            AgregarNotificacion(ESN.Id, ESN.TipoESN, Retrieval, Session["user"].ToString().ToUpper(), Involucrados);
                            ListaControladores.Add(MovimientoLogistico[0]);
                            string eta = "";
                            if (ListaControladores[i].EtaString != "" && ListaControladores[i].EtaString != null)
                            {
                                eta = ListaControladores[i].EtaString.Substring(0, 10);
                            }

                            if (correo_especial)
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + booking + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + suplier + "</td>" +
                                "</tr>";
                            }
                            else
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].NodoDestino + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Controlador + "</td>" +
                                     "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Modem + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + booking + "</td>" +
                                "</tr>";
                            }
                            CantControladores++;
                        }
                    }

                }
                else if (Involucrados == 2)
                {
                    //Notificacion solo de controlador
                    int cont = 0;
                    for (int i = 0; i < Controladores.Count(); i++)
                    {
                        if (Controladores[i].ToString() != "")
                        {
                            ESN = ControladorModel.GetIdESN(Controladores[i].ToString());
                            MovimientoLogistico = ControladorModel.GetESNById(ESN.Id, ESN.TipoESN);
                            string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                            AgregarNotificacion(ESN.Id, ESN.TipoESN, Retrieval, Session["user"].ToString().ToUpper(), Involucrados);
                            ListaControladores.Add(MovimientoLogistico[0]);
                            string eta = "";
                            if (ListaControladores[cont].EtaString != "" && ListaControladores[cont].EtaString != null)
                            {
                                eta = ListaControladores[cont].EtaString.Substring(0, 10);
                            }

                            if (correo_especial)
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + booking + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + suplier + "</td>" +
                                "</tr>";
                            }
                            else
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].NodoDestino + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Controlador + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Booking + "</td>" +
                                "</tr>";
                            }
                            cont++;
                            CantControladores++;
                        }
                    }
                }
                else if (Involucrados == 3)
                {
                    //Notificacion solo de modem
                    int cont = 0;
                    for (int i = 0; i < Modems.Count(); i++)
                    {
                        if (Modems[i].ToString() != "")
                        {
                            ESN = ControladorModel.GetIdESN(Modems[i].ToString());
                            MovimientoLogistico = ControladorModel.GetESNById(ESN.Id, ESN.TipoESN);
                            string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                            AgregarNotificacion(ESN.Id, ESN.TipoESN, Retrieval, Session["user"].ToString().ToUpper(), Involucrados);
                            ListaControladores.Add(MovimientoLogistico[0]);
                            string eta = "";
                            if (ListaControladores[cont].EtaString != "" && ListaControladores[cont].EtaString != null)
                            {
                                eta = ListaControladores[cont].EtaString.Substring(0, 10);
                            }

                            if (correo_especial)
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[i].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + booking + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + suplier + "</td>" +
                                "</tr>";
                            }
                            else
                            {
                                Correo +=
                                "<tr>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].NodoDestino + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Modem + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Contenedor + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Naviera + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Nave + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + ListaControladores[cont].Viaje + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + eta + "</td>" +
                                    "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + booking + "</td>" +
                                "</tr>";
                            }
                            cont++;
                            CantControladores++;
                        }
                    }
                }

                /*-------------------------MENSAJE DE CORREO----------------------*/

                //Creamos un nuevo Objeto de mensaje
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                string host = Request.Url.Host;

                if (host == "localhost")
                {
                    //correos = "rcontreras@liventusglobal.com";
                    //correos = "rcoulon@liventusglobal.com";
                    correos = "jfuentealba@liventusglobal.com";
                    correosCopiasOcultas = "jfuentealba@liventusglobal.com";
                }
                mmsg.To.Add(correos);
                if (RetrievalProvider.Correo1 != "")
                {
                    mmsg.CC.Add(RetrievalProvider.Correo1);

                }
                if (RetrievalProvider.Correo2 != "")
                {
                    mmsg.CC.Add(RetrievalProvider.Correo2);
                }
                
                mmsg.Bcc.Add(correosCopiasOcultas);

                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Liventus Retrieval Assistance";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
                if (Usuario.Correo != "")
                {
                    mmsg.CC.Add(Usuario.Correo);
                    mmsg.Bcc.Add(Usuario.Correo);
                }
                else
                {
                    mmsg.Bcc.Add("rcontreras@liventusglobal.com");
                }

                //Direccion de correo electronico que queremos que reciba una copia del mensaje
                if (correscopias != "" && correscopias != "W/O DATA;W/O DATA" && correscopias != "W/O DATA")
                {
                    //mmsg.Bcc.Add(correscopias);
                    mmsg.Bcc.Add("rcoulon@liventusglobal.com");//Opcional
                }

                //Cuerpo del Mensaje
                if (Controladores.Count() > 30 || Modems.Count() > 30)
                {
                    CantControladores = 0;
                    // If using Professional version, put your serial key below.
                    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                    ExcelFile ef = new ExcelFile();
                    ExcelWorksheet ws = ef.Worksheets.Add("Tables");

                    if (Involucrados == 1)
                    {
                        // Add some data
                        object[,] data = new object[1, 9]
                        {
                            { "POD", "CONTROLLER (ESN)", "MODEM (ESN)", "CONTAINER", "SHIPPING LINE", "VESSEL", "TRAVEL", "ETA", "BOOKING" },
                        };

                        for (int i = 0; i < 1; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                ws.Cells[i, j].Value = data[i, j];
                            }
                        }

                        for (int i = 0; i < Controladores.Count(); i++)
                        {
                            if (Controladores[i].ToString() != "")
                            {
                                string eta = "";
                                if (ListaControladores[i].EtaString != "" && ListaControladores[i].EtaString != null)
                                {
                                    eta = ListaControladores[i].EtaString.Substring(0, 10);
                                }
                                ESN = ControladorModel.GetIdESN(Controladores[i].ToString());
                                MovimientoLogistico = ControladorModel.GetESNById(ESN.Id, ESN.TipoESN);
                                string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                                ws.Cells[i + 1, 0].Value = ListaControladores[i].NodoDestino;
                                ws.Cells[i + 1, 1].Value = ListaControladores[i].Controlador;
                                ws.Cells[i + 1, 2].Value = ListaControladores[i].Modem;
                                ws.Cells[i + 1, 3].Value = ListaControladores[i].Contenedor;
                                ws.Cells[i + 1, 4].Value = ListaControladores[i].Naviera;
                                ws.Cells[i + 1, 5].Value = ListaControladores[i].Nave;
                                ws.Cells[i + 1, 6].Value = ListaControladores[i].Viaje;
                                ws.Cells[i + 1, 7].Value = eta;
                                ws.Cells[i + 1, 8].Value = booking;

                                CantControladores++;
                            }
                            else
                            {
                                string eta = "";
                                if (ListaControladores[i].EtaString != "" && ListaControladores[i].EtaString != null)
                                {
                                    eta = ListaControladores[i].EtaString.Substring(0, 10);
                                }
                                ESN = ControladorModel.GetIdESN(Modems[i].ToString());
                                MovimientoLogistico = ControladorModel.GetControladorById(ESN.Id);
                                string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                                ws.Cells[i + 1, 0].Value = ListaControladores[i].NodoDestino;
                                ws.Cells[i + 1, 1].Value = ListaControladores[i].Controlador;
                                ws.Cells[i + 1, 2].Value = ListaControladores[i].Modem;
                                ws.Cells[i + 1, 3].Value = ListaControladores[i].Contenedor;
                                ws.Cells[i + 1, 4].Value = ListaControladores[i].Naviera;
                                ws.Cells[i + 1, 5].Value = ListaControladores[i].Nave;
                                ws.Cells[i + 1, 6].Value = ListaControladores[i].Viaje;
                                ws.Cells[i + 1, 7].Value = eta;
                                ws.Cells[i + 1, 8].Value = booking;

                                CantControladores++;
                            }
                        }

                    }
                    else if (Involucrados == 2)
                    {
                        object[,] data = new object[1, 8]
                        {
                            { "POD", "CONTROLLER (ESN)", "CONTAINER", "SHIPPING LINE", "VESSEL", "TRAVEL", "ETA", "BOOKING" },
                        };

                        for (int i = 0; i < 1; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                ws.Cells[i, j].Value = data[i, j];
                            }
                        }
                        for (int i = 0; i < Controladores.Count(); i++)
                        {
                            if (Controladores[i].ToString() != "")
                            {

                                string eta = "";
                                if (ListaControladores[i].EtaString != "" && ListaControladores[i].EtaString != null)
                                {
                                    eta = ListaControladores[i].EtaString.Substring(0, 10);
                                }

                                ESN = ControladorModel.GetIdESN(Controladores[i].ToString());
                                MovimientoLogistico = ControladorModel.GetESNById(ESN.Id, ESN.TipoESN);
                                string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                                ws.Cells[i + 1, 0].Value = ListaControladores[i].NodoDestino;
                                ws.Cells[i + 1, 1].Value = ListaControladores[i].Controlador;
                                ws.Cells[i + 1, 2].Value = ListaControladores[i].Contenedor;
                                ws.Cells[i + 1, 3].Value = ListaControladores[i].Naviera;
                                ws.Cells[i + 1, 4].Value = ListaControladores[i].Nave;
                                ws.Cells[i + 1, 5].Value = ListaControladores[i].Viaje;
                                ws.Cells[i + 1, 6].Value = eta;
                                ws.Cells[i + 1, 7].Value = booking;

                                CantControladores++;
                            }
                        }
                    }
                    else if (Involucrados == 3)
                    {
                        object[,] data = new object[1, 8]
                        {
                            { "POD", "MODEM (ESN)", "CONTAINER", "SHIPPING LINE", "VESSEL", "TRAVEL", "ETA", "BOOKING" },
                        };

                        for (int i = 0; i < 1; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                ws.Cells[i, j].Value = data[i, j];
                            }
                        }

                        for (int i = 0; i < Modems.Count(); i++)
                        {
                            if (Modems[i].ToString() != "")
                            {
                                ESN = ControladorModel.GetIdESN(Modems[i].ToString());
                                MovimientoLogistico = ControladorModel.GetControladorById(ESN.Id);
                                string booking = ControladorModel.GetBookingESN(ESN.Id, ESN.TipoESN);
                                ws.Cells[i + 1, 0].Value = ListaControladores[i].NodoDestino;
                                ws.Cells[i + 1, 1].Value = ListaControladores[i].Modem;
                                ws.Cells[i + 1, 2].Value = ListaControladores[i].Contenedor;
                                ws.Cells[i + 1, 3].Value = ListaControladores[i].Naviera;
                                ws.Cells[i + 1, 4].Value = ListaControladores[i].Nave;
                                ws.Cells[i + 1, 5].Value = ListaControladores[i].Viaje;
                                ws.Cells[i + 1, 6].Value = ListaControladores[i].Eta;
                                ws.Cells[i + 1, 7].Value = booking;

                                CantControladores++;
                            }
                        }
                    }

                    mmsg.Body = "<p>Dear, " + RetrievalProvider.NombreRetrievalProvider + "<p/>" +
                                "<p>Good Day, </p>" +
                                "<p>Attached is the Excel file with the new " + ListaControladores.Count() + " services coming to " + ListaControladores[0].NodoDestino + ".</p>" +
                                "<p>Please advise once you have retrieved our controllers and let me know the AWB number once you send a parcel to us.</p>" +
                                "<p>If you have any inquiry, do not hesitate to contact me.</p>" +
                                "<p>Thanks and best regards.</p>" +
                                "<p>" + Usuario.Nombre + Usuario.Apellido + ".</p>" +
                                "<p>Retrieval Team.</p>";

                    Path.GetFullPath("Controladores.xlsx");
                    ef.Save(Path.Combine(Server.MapPath("~/RetrievalDocumentos"), "Controladores.xlsx"));

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(Path.Combine(Server.MapPath("~/RetrievalDocumentos"), "Controladores.xlsx"));
                    mmsg.Attachments.Add(attachment);
                }
                else
                {
                    if (Involucrados == 1)
                    {
                        if (correo_especial)
                        {
                            mmsg.Body = "<p>Dear, " + RetrievalProvider.NombreRetrievalProvider + "<p/>" +
                                "<p>Please note new units below.</p>" +
                                "<table style='border: 1px solid black; border-collapse: collapse; width:100%'>" +
                                    "<thead>" +
                                        "<tr>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTAINER</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SHIPPING LINE</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>VESSEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TRAVEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ETA</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BOOKING</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SUPLIER</th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>" + Correo +
                                    "</tbody>" +
                                "</table>" +
                                "<p>Please advise once you have retrieved our controllers and let me know the AWB number once you send a parcel to us.</p>" +
                                "<p>If you have any inquiry, do not hesitate to contact me.</p>" +
                                "<p>Thanks and best regards.</p>" +
                                "<p>" + Usuario.Nombre + " " + Usuario.Apellido + ".</p>" +
                                "<p>Retrieval Team.</p>";
                        }
                        else
                        {
                            mmsg.Body = "<p>Dear, " + RetrievalProvider.NombreRetrievalProvider + "<p/>" +
                                "<p>Please note new units below.</p>" +
                                "<table style='border: 1px solid black; border-collapse: collapse; width:100%'>" +
                                    "<thead>" +
                                        "<tr>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>POD</ th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTROLLER (ESN)</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>MODEM (ESN)</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTAINER</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SHIPPING LINE</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>VESSEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TRAVEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ETA</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BOOKING</th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>" + Correo +
                                    "</tbody>" +
                                "</table>" +
                                "<p>Please advise once you have retrieved our controllers and let me know the AWB number once you send a parcel to us.</p>" +
                                "<p>If you have any inquiry, do not hesitate to contact me.</p>" +
                                "<p>Thanks and best regards.</p>" +
                                "<p>" + Usuario.Nombre + " " + Usuario.Apellido + ".</p>" +
                                "<p>Retrieval Team.</p>";
                        }
                    }
                    else if (Involucrados == 2)
                    {
                        if (correo_especial)
                        {
                            mmsg.Body = "<p>Dear, " + RetrievalProvider.NombreRetrievalProvider + "<p/>" +
                                "<p>Please note new units below.</p>" +
                                "<table style='border: 1px solid black; border-collapse: collapse; width:100%'>" +
                                    "<thead>" +
                                        "<tr>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTAINER</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SHIPPING LINE</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>VESSEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TRAVEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ETA</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BOOKING</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SUPLIER</th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>" + Correo +
                                    "</tbody>" +
                                "</table>" +
                                "<p>Please advise once you have retrieved our controllers and let me know the AWB number once you send a parcel to us.</p>" +
                                "<p>If you have any inquiry, do not hesitate to contact me.</p>" +
                                "<p>Thanks and best regards.</p>" +
                                "<p>" + Usuario.Nombre + " " + Usuario.Apellido + ".</p>" +
                                "<p>Retrieval Team.</p>";
                        }
                        else
                        {
                            mmsg.Body = "<p>Dear, " + RetrievalProvider.NombreRetrievalProvider + "<p/>" +
                                "<p>Please note new units below.</p>" +
                                "<table style='border: 1px solid black; border-collapse: collapse; width:100%'>" +
                                    "<thead>" +
                                        "<tr>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>POD</ th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTROLLER (ESN)</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTAINER</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SHIPPING LINE</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>VESSEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TRAVEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ETA</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BOOKING</th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>" + Correo +
                                    "</tbody>" +
                                "</table>" +
                                "<p>Please advise once you have retrieved our controllers and let me know the AWB number once you send a parcel to us.</p>" +
                                "<p>If you have any inquiry, do not hesitate to contact me.</p>" +
                                "<p>Thanks and best regards.</p>" +
                                "<p>" + Usuario.Nombre + " " + Usuario.Apellido + ".</p>" +
                                "<p>Retrieval Team.</p>";
                        }
                    }
                    else if (Involucrados == 3)
                    {
                        if (correo_especial)
                        {
                            mmsg.Body = "<p>Dear, " + RetrievalProvider.NombreRetrievalProvider + "<p/>" +
                                "<p>Please note new units below.</p>" +
                                "<table style='border: 1px solid black; border-collapse: collapse; width:100%'>" +
                                    "<thead>" +
                                        "<tr>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTAINER</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SHIPPING LINE</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>VESSEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TRAVEL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ETA</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BOOKING</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SUPLIER</th>" +
                                        "</tr>" +
                                    "</thead>" +
                                    "<tbody>" + Correo +
                                    "</tbody>" +
                                "</table>" +
                                "<p>Please advise once you have retrieved our controllers and let me know the AWB number once you send a parcel to us.</p>" +
                                "<p>If you have any inquiry, do not hesitate to contact me.</p>" +
                                "<p>Thanks and best regards.</p>" +
                                "<p>" + Usuario.Nombre + " " + Usuario.Apellido + ".</p>" +
                                "<p>Retrieval Team.</p>";
                        }
                        else
                        {
                            mmsg.Body = "<p>Dear, " + RetrievalProvider.NombreRetrievalProvider + "<p/>" +
                               "<p>Please note new units below.</p>" +
                               "<table style='border: 1px solid black; border-collapse: collapse; width:100%'>" +
                                   "<thead>" +
                                       "<tr>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>POD</ th>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>MODEM (ESN)</th>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTAINER</th>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SHIPPING LINE</th>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>VESSEL</th>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>TRAVEL</th>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ETA</th>" +
                                           "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>BOOKING</th>" +
                                       "</tr>" +
                                   "</thead>" +
                                   "<tbody>" + Correo +
                                   "</tbody>" +
                               "</table>" +
                               "<p>Please advise once you have retrieved our controllers and let me know the AWB number once you send a parcel to us.</p>" +
                               "<p>If you have any inquiry, do not hesitate to contact me.</p>" +
                                "<p>Thanks and best regards.</p>" +
                                "<p>" + Usuario.Nombre + " " + Usuario.Apellido + ".</p>" +
                                "<p>Retrieval Team.</p>";
                        }
                    }
                }

                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress(Usuario.Correo);


                /*-------------------------CLIENTE DE CORREO----------------------*/
                //Creamos un objeto de cliente de correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Hay que crear las credenciales del correo emisor
                if (host == "localhost")
                {
                    //cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Liventus2018");
                    //cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Office365coke123");
                    cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Marzo2017");
                }
                else
                {
                    cliente.Credentials = new System.Net.NetworkCredential(Usuario.Correo, "Liventus2019");
                }


                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail

                cliente.Port = 587;
                cliente.EnableSsl = true;


                //cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";
                cliente.Host = "outlook.office365.com";



                /*-------------------------ENVIO DE CORREO----------------------*/

                try
                {
                    //Enviamos el mensaje      
                    cliente.Send(mmsg);
                    aux.Mensaje = "Operación Realizada Correctamente";
                    aux.validador = 0;
                    int retornoBD = ControladorModel.GuardarNotificacion(Retrieval, Usuario.IdUsuario, CantControladores, 1);
                    datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                    return datos;
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    //Aquí gestionamos los errores al intentar enviar el correo
                    aux.Mensaje = "Operación no se Realizó, Contactarse con el Administrador.";
                    aux.validador = 1;
                    int retornoBD = ControladorModel.GuardarNotificacion(Retrieval, Usuario.IdUsuario, CantControladores, 1);
                    datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
                    return datos;
                }
            }

        }

        [HttpPost]
        public string LeerExcel1()
        {
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            List<Clases.MovimientoLogistico> MovimientoLogistico = new List<Clases.MovimientoLogistico>();
            Clases.ESN esn = new Clases.ESN();
            Clases.Controlador Controlador = new Clases.Controlador();
            Clases.Contenedor Contenedor = new Clases.Contenedor();
            Clases.Bateria Bateria = new Clases.Bateria();
            string datos = "";
            string mensaje_error = "";
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
                    int cont = 0;

                    // Iterate through all worksheets in an Excel workbook.
                    foreach (ExcelWorksheet sheet in ef.Worksheets)
                    {
                        // Iterate through all rows in an Excel worksheet.
                        foreach (ExcelRow row in sheet.Rows)
                        {
                            MovimientoLogistico = new List<Clases.MovimientoLogistico>();
                            mensaje_error = "";

                            // Iterate through all allocated cells in an Excel row.
                            foreach (ExcelCell cell in row.AllocatedCells)
                            {
                                if (cell.Row.ToString() != "1" && cell.Column.ToString() == "A")
                                {
                                    if (cell.ValueType != CellValueType.Null)
                                    {
                                        if (cell.Value.ToString() == "fin")
                                        {
                                            return JsonConvert.SerializeObject(Controladores);
                                        }
                                        else
                                        {
                                            esn = ControladorModel.GetIdESN(cell.Value.ToString());

                                            if (esn.Id != 0)
                                            {
                                                MovimientoLogistico = ControladorModel.GetESNById(esn.Id, esn.TipoESN);
                                            }
                                            else
                                            {
                                                Clases.MovimientoLogistico Mov = new Clases.MovimientoLogistico();
                                                Mov.Controlador = "";
                                                Mov.Modem = "";
                                                MovimientoLogistico.Add(Mov);

                                                mensaje_error = "<p>" + "No existe un controlador/modem de ESN " + cell.Value.ToString() + "</p>";
                                                MovimientoLogistico[0].MensajesErrores = MovimientoLogistico[0].MensajesErrores + mensaje_error;
                                            }
                                        }
                                    }
                                }
                                else if (cell.Row.ToString() != "1" && cell.Column.ToString() == "B")
                                {
                                    if (cell.ValueType != CellValueType.Null)
                                    {
                                        Contenedor = ContenedorModelo.GetIdContedor(cell.Value.ToString());
                                        if (Contenedor.IdContenedor != 0)
                                        {
                                            MovimientoLogistico[0].Contenedor = Contenedor.NumeroContenedor;
                                        }
                                        else
                                        {
                                            MovimientoLogistico[0].Contenedor = "";
                                            mensaje_error = "<p>" + "No existe un contenedor llamado " + cell.Value.ToString() + "</p>";
                                            MovimientoLogistico[0].MensajesErrores = MovimientoLogistico[0].MensajesErrores + mensaje_error;
                                        }
                                    }
                                }
                                else if (cell.Row.ToString() != "1" && cell.Column.ToString() == "C")
                                {
                                    if (cell.ValueType != CellValueType.Null)
                                    {
                                        Bateria = BateriaModel.ObtenerEstadoBateria(cell.Value.ToString());
                                        if (Bateria.IdBateria != 0)
                                        {
                                            MovimientoLogistico[0].Bateria = cell.Value.ToString();
                                        }
                                        else
                                        {
                                            MovimientoLogistico[0].Bateria = "";
                                            mensaje_error = "<p>" + "No existe una batería llamada " + cell.Value.ToString() + "</p>";
                                            MovimientoLogistico[0].MensajesErrores = MovimientoLogistico[0].MensajesErrores + mensaje_error;
                                        }
                                    }
                                }
                                else if (cell.Row.ToString() != "1" && cell.ValueType != CellValueType.Null && cell.Column.ToString() == "I")
                                {
                                    int validacion = ValidarControladorServicio(MovimientoLogistico[0].Controlador);
                                    if (validacion != 0)
                                    {
                                        MovimientoLogistico[0].Validado = 1;
                                    }
                                    else
                                    {
                                        MovimientoLogistico[0].Validado = 0;
                                    }
                                    MovimientoLogistico[0].EmpresaTransporte = cell.Value.ToString();
                                }
                                else if (cell.Row.ToString() != "1" && cell.ValueType != CellValueType.Null && cell.Column.ToString() == "J")
                                {
                                    MovimientoLogistico[0].NumeroEnvio = cell.Value.ToString();
                                }
                                else if (cell.Row.ToString() != "1" && cell.ValueType != CellValueType.Null && cell.Column.ToString() == "K")
                                {
                                    MovimientoLogistico[0].RetrievalProvider = cell.Value.ToString();
                                }
                                else if (cell.Row.ToString() != "1" && cell.ValueType != CellValueType.Null && cell.Column.ToString() == "L")
                                {
                                    MovimientoLogistico[0].FechaRecuperacion = Convert.ToDateTime(cell.Value.ToString());
                                }

                            }

                            if (cont != 0)
                            {
                                Controladores.Add(MovimientoLogistico[0]);
                            }
                            cont++;
                        }
                    }
                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject("Error de formato");
                }
            }

            datos = Newtonsoft.Json.JsonConvert.SerializeObject(Controladores);
            return datos;
        }

        public JsonResult GetHistorialNotificaciones()
        {
            List<Clases.Notificaciones> Notificaciones = new List<Clases.Notificaciones>();
            Notificaciones = ControladorModel.GetHistorialNotificaciones();
            var resultados = Json(Notificaciones, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        [HttpPost]
        public string LeerExcelRecuperacion()
        {
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            List<Clases.MovimientoLogistico> MovimientoLogistico = new List<Clases.MovimientoLogistico>();
            List<string> ControladoresError = new List<string>();
            Clases.Controlador Controlador = new Clases.Controlador();
            Clases.ESN esn = new Clases.ESN();
            Clases.Contenedor Contenedor = new Clases.Contenedor();
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

                    // Iterate through all worksheets in an Excel workbook.
                    foreach (ExcelWorksheet sheet in ef.Worksheets)
                    {
                        // Iterate through all rows in an Excel worksheet.
                        foreach (ExcelRow row in sheet.Rows)
                        {
                            // Iterate through all allocated cells in an Excel row.
                            foreach (ExcelCell cell in row.AllocatedCells)
                            {
                                if (cell.Row.ToString() != "1" && cell.Column.ToString() == "A")
                                {
                                    if (cell.ValueType != CellValueType.Null)
                                    {
                                        if (cell.Value.ToString() == "fin")
                                        {
                                            datos = JsonConvert.SerializeObject(Controladores);
                                            return datos;
                                        }
                                        esn = ControladorModel.GetIdESN(cell.Value.ToString());
                                        MovimientoLogistico = ControladorModel.GetESNById(esn.Id, esn.TipoESN);
                                        if(esn.TipoESN=="CONTROLADOR")
                                        {
                                            int validacion = ValidarControladorServicio(cell.Value.ToString());
                                            if (validacion == 1)
                                            {
                                                //Controladores.Add(MovimientoLogistico[0]);
                                                //break;
                                            }
                                            else
                                            {
                                                return JsonConvert.SerializeObject("Error de datos");
                                            }
                                        }
                                        else
                                        {
                                            int validacion = ValidarModemServicio(cell.Value.ToString());
                                            if (validacion == 1)
                                            {
                                                //Controladores.Add(MovimientoLogistico[0]);
                                                //break;
                                            }
                                            else
                                            {
                                                return JsonConvert.SerializeObject("Error de datos");
                                            }
                                        }
                                    }

                                    //if (cell.ValueType != CellValueType.Null)
                                    //{
                                    //    if (cell.Value.ToString() == "fin")
                                    //    {
                                    //        datos = JsonConvert.SerializeObject(Controladores);
                                    //        return datos;
                                    //    }
                                    //    esn = ControladorModel.GetIdESN(cell.Value.ToString());
                                    //    MovimientoLogistico = ControladorModel.GetESNById(esn.Id, esn.TipoESN);
                                    //    if (MovimientoLogistico[0].Controlador != "")
                                    //    {
                                    //        int validacion = ValidarControladorServicio(cell.Value.ToString());
                                    //        if (validacion == 1)
                                    //        {
                                    //            //Controladores.Add(MovimientoLogistico[0]);
                                    //            //break;
                                    //        }
                                    //        else
                                    //        {
                                    //            return JsonConvert.SerializeObject("Error de datos");
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        int validacion = ValidarModemServicio(cell.Value.ToString());
                                    //        if (validacion == 1)
                                    //        {
                                    //            //Controladores.Add(MovimientoLogistico[0]);
                                    //            //break;
                                    //        }
                                    //        else
                                    //        {
                                    //            return JsonConvert.SerializeObject("Error de datos");
                                    //        }
                                    //        return JsonConvert.SerializeObject("Error de datos");
                                    //    }
                                    //}
                                }
                                else if (cell.Row.ToString() != "1" && cell.Column.ToString() == "B")
                                {
                                    if (cell.ValueType != CellValueType.Null)
                                    {
                                        if (MovimientoLogistico[0].Controlador == "" || MovimientoLogistico[0].Controlador == null)
                                        {
                                            Contenedor = ContenedorModelo.GetIdContedor(cell.Value.ToString());
                                            Controlador = ControladorModel.GetIdControladorByContenedor(Contenedor.IdContenedor);
                                            MovimientoLogistico = ControladorModel.GetControladorById(Controlador.Id);
                                            int validacion = ValidarControladorServicio(cell.Value.ToString());
                                            if (validacion == 1)
                                            {
                                                //Controladores.Add(MovimientoLogistico[0]);
                                                //break;
                                            }
                                            else
                                            {
                                                return JsonConvert.SerializeObject("Error de datos");
                                            }
                                        }
                                        else
                                        {
                                            //return JsonConvert.SerializeObject("Error de datos");
                                        }
                                    }
                                }
                                else if (cell.Row.ToString() != "1" && cell.ValueType != CellValueType.Null && cell.Column.ToString() == "C")
                                {
                                    MovimientoLogistico[0].Modem = (cell.Value.ToString()).ToUpper();
                                }

                            }
                            if (row.ToString() != "1")
                            {
                                Controladores.Add(MovimientoLogistico[0]);
                            }

                        }
                    }
                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject("Error de formato");
                }
            }
            ViewBag.ControladoresError = ControladoresError;
            datos = Newtonsoft.Json.JsonConvert.SerializeObject(Controladores);
            return datos;
        }

        [HttpPost]
        public string LeerExcelNodos()
        {
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            List<Clases.MovimientoLogistico> MovimientoLogistico = new List<Clases.MovimientoLogistico>();
            Clases.MovimientoLogistico Prueba = new Clases.MovimientoLogistico();
            List<string> ControladoresError = new List<string>();
            Clases.Controlador Controlador = new Clases.Controlador();
            Clases.ESN esn = new Clases.ESN();
            Clases.Contenedor Contenedor = new Clases.Contenedor();
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
                                                datos = JsonConvert.SerializeObject(Controladores);
                                                return datos;
                                            }

                                            esn = ControladorModel.GetIdESN(cell.Value.ToString());
                                            MovimientoLogistico = ControladorModel.GetESNById(esn.Id, esn.TipoESN);
                                            //if (MovimientoLogistico[0].Controlador != "")
                                            //{
                                            //    MovimientoLogistico[0].Controlador = cell.Value.ToString();
                                            //}

                                            if (MovimientoLogistico[0].TipoNodoDestino != "")
                                            {
                                                MovimientoLogistico[0].TipoNodoOrigen = MovimientoLogistico[0].TipoNodoDestino;
                                            }

                                            if (MovimientoLogistico[0].NodoDestino != "")
                                            {
                                                MovimientoLogistico[0].NodoOrigen = MovimientoLogistico[0].NodoDestino;
                                            }
                                        }
                                    }
                                    else if (cell.Row.ToString() != "1" && cell.Column.ToString() == "B")
                                    {
                                        if (MovimientoLogistico[0].Controlador == "" && cell.ValueType != CellValueType.Null)
                                        {
                                            Contenedor = ContenedorModelo.GetIdContedor(cell.Value.ToString());
                                            Controlador = ControladorModel.GetIdControladorByContenedor(Contenedor.IdContenedor);
                                            MovimientoLogistico = ControladorModel.GetControladorById(Controlador.Id);
                                            if (MovimientoLogistico[0].Controlador != "")
                                            {
                                                MovimientoLogistico[0].Contenedor = cell.Value.ToString();
                                            }
                                        }
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "C")
                                    {
                                        MovimientoLogistico[0].TipoNodoDestino = (cell.Value.ToString()).ToUpper();
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "D")
                                    {
                                        MovimientoLogistico[0].NodoDestino = (cell.Value.ToString()).ToUpper();
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "E")
                                    {
                                        MovimientoLogistico[0].Eta = Convert.ToDateTime(cell.Value);
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "F")
                                    {
                                        MovimientoLogistico[0].Nave = (cell.Value.ToString()).ToUpper();
                                    }
                                    else if (cell.ValueType != CellValueType.Null && cell.Column.ToString() == "G")
                                    {
                                        MovimientoLogistico[0].Viaje = cell.Value.ToString();
                                    }
                                    MovimientoLogistico[0].FechaEnvio = DateTime.Today;
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
                                Controladores.Add(MovimientoLogistico[0]);
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
            ViewBag.ControladoresError = ControladoresError;
            datos = JsonConvert.SerializeObject(Controladores);
            return datos;
        }
        // GET: Controlador
        public ActionResult ConsultarControlador()
        {
            return View();
        }

        public ActionResult HistorialNotificaciones()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult AgregarEntradaControlador()
        {
            return View();
        }

        public ActionResult AgregarSalidaControlador()
        {
            return View();
        }

        public ActionResult RecuperarControlador()
        {
            return View();
        }

        public ActionResult InventarioControlador()
        {
            return View();
        }

        public ActionResult FallaControlador()
        {
            return View();
        }

        public ActionResult ConsultarEntradaSalidaControlador()
        {
            return View();
        }

        public ActionResult VisualizarResumenControladores()
        {
            return View();
        }

        public ActionResult GestionarLogisticaControlador()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarLogisticaDeposito()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionRetrievalControlador()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult GestionarMovimientoControlador()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult EditarEntradaSalidaControlador(string controlador, string tiporegistro, string estadotecnico, string estadologistico, string booking, string contenedor, string fecha, string origen, string destino, int dias, string courier, string ticket)
        {
            ViewBag.Controlador = controlador;
            ViewBag.TipoRegistro = tiporegistro;
            ViewBag.EstadoTecnico = estadotecnico;
            ViewBag.EstadoLogistico = estadologistico;
            ViewBag.Booking = booking;
            ViewBag.Contenedor = contenedor;
            ViewBag.Fecha = fecha;
            ViewBag.Origen = origen;
            ViewBag.Destino = destino;
            ViewBag.Dias = dias;
            ViewBag.Courier = courier;
            ViewBag.Ticket = ticket;

            return View();
        }

        public ActionResult AgregarRecuperacionControlador(string ncontrolador, string estadoTecnico, string estadoLogistico, string version, string booking, string ncontenedor, string eta, string retrievalProvider, string retrieval)
        {
            ViewBag.Ncontrolador = ncontrolador;
            ViewBag.EstadoTecnico = estadoTecnico;
            ViewBag.EstadoLogistico = estadoLogistico;
            ViewBag.Version = version;
            ViewBag.Booking = booking;
            ViewBag.Ncontenedor = ncontenedor;
            ViewBag.Eta = eta;
            ViewBag.RetrievalProvider = retrievalProvider;
            ViewBag.Retrieval = retrieval;
            return View();
        }

        public ActionResult DetalleEntradaSalidaControlador(string controlador, string estadotecnico, string estadologistico, string booking, string contenedor, string entrada, string origenentrada, string destinoentrada, string salida, string origensalida, string destinosalida)
        {
            ViewBag.Controlador = controlador;
            ViewBag.EstadoTecnico = estadotecnico;
            ViewBag.EstadoLogistico = estadologistico;
            ViewBag.Booking = booking;
            ViewBag.Contenedor = contenedor;
            ViewBag.Entrada = entrada;
            ViewBag.OrigenEntrada = origenentrada;
            ViewBag.DestinoEntrada = destinosalida;
            ViewBag.Salida = salida;
            ViewBag.OrigenSalida = origensalida;
            ViewBag.DestinoSalida = destinosalida;

            return View();
        }

        public ActionResult ExportarDamageReport(string controlador, string contenedor, string bateria, string maquinaria, string naviera, string nave, string viaje, string commodity, string puertoOrigen, string puertoDestino, string puertoExtraccion, string co2, string o2, string temperatura, string ubicacion)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Informes"), "DamageReport.rpt"));
            rd.SetParameterValue("controlador", controlador);
            rd.SetParameterValue("contenedor", contenedor);
            rd.SetParameterValue("bateria", bateria);
            rd.SetParameterValue("maquinaria", maquinaria);
            rd.SetParameterValue("naviera", naviera);
            rd.SetParameterValue("nave", nave);
            rd.SetParameterValue("viaje", viaje);
            rd.SetParameterValue("commodity", commodity);
            rd.SetParameterValue("puerto_origen", puertoOrigen);
            rd.SetParameterValue("puerto_destino", puertoDestino);
            rd.SetParameterValue("puerto_extraccion", puertoExtraccion);
            rd.SetParameterValue("o2", o2);
            rd.SetParameterValue("co2", co2);
            rd.SetParameterValue("temperatura", temperatura);
            rd.SetParameterValue("ubicacion", ubicacion);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "DamageReport.pdf");
        }

        public JsonResult GetHistoricoControlador()
        {
            List<Clases.HistoricoControlador> Controladores = new List<Clases.HistoricoControlador>();
            Controladores = ControladorModel.GetHistoricoControlador();
            var resultados = Json(Controladores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        [HttpPost]
        public string GetControladores()
        {
            List<Clases.Controlador> Controladores = new List<Clases.Controlador>();
            Controladores = ControladorModel.GetControladores();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Controladores);
            return datos;
        }

        [HttpPost]
        public string GetControladores1()
        {
            List<Clases.Controlador> Controladores = new List<Clases.Controlador>();
            Controladores = ControladorModel.GetControladores1();
            string optionsAsString = "<option value=''>Seleccione controlador</option>";
            for (var i = 0; i < Controladores.Count(); i++)
            {
                optionsAsString += "<option value='" + Controladores[i].Id + "'>" + Controladores[i].NumControlador + "</option>";
            }

            return optionsAsString;
        }

        [HttpPost]
        public string GetBateriaByControlador(string Controlador)
        {
            Clases.Bateria AsociacionControladorBateria = new Clases.Bateria();
            AsociacionControladorBateria = ControladorModel.GetBateriaByControlador(Controlador);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AsociacionControladorBateria);
            return datos;
        }

        [HttpPost]
        public string GetBateriaByModem(string Modem)
        {
            Clases.Bateria AsociacionControladorBateria = new Clases.Bateria();
            AsociacionControladorBateria = ControladorModel.GetBateriaByModem(Modem);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(AsociacionControladorBateria);
            return datos;
        }

        [HttpPost]
        public string GetControladorById(int IdControlador)
        {
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            Controladores = ControladorModel.GetControladorById(IdControlador);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Controladores);
            return datos;
        }

        [HttpPost]
        public string GetControladorByNumberControlador(string NumControlador)
        {
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            Clases.Controlador Controlador = ControladorModel.GetIdControlador(NumControlador);
            Controladores = ControladorModel.GetControladorById(Controlador.Id);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Controladores);
            return datos;
        }

        [HttpPost]
        public string GetESNByNumberESN(string NumESN)
        {
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            Clases.ESN ESN = ControladorModel.GetIdESN(NumESN);
            Controladores = ControladorModel.GetESNById(ESN.Id, ESN.TipoESN);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Controladores);
            return datos;
        }

        [HttpPost]
        public string AgregarPerdida(string ESN, int TipoPerdida =1, DateTime? FechaPerdida=null, string ComentarioPerdida = "")
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.ESN Controladores = ControladorModel.GetIdESN(ESN);
            int Flag = ControladorModel.AgregarPerdida(Controladores.Id, TipoPerdida, Controladores.TipoESN, FechaPerdida, ComentarioPerdida);
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
        public string AgregarNoPerdida(string ESN)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.ESN Controladores = ControladorModel.GetIdESN(ESN);
            int Flag = ControladorModel.AgregarNoPerdida(Controladores.Id, Controladores.TipoESN);
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
        public string AgregarPriorizacion(string ESN, int Requerimiento)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.ESN Controladores = ControladorModel.GetIdESN(ESN);
            int Flag = ControladorModel.AgregarPriorizacion(Controladores.Id, Controladores.TipoESN, Requerimiento);
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
        public string AgregarNoPriorizacion(string ESN)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.ESN Controladores = ControladorModel.GetIdESN(ESN);
            int Flag = ControladorModel.AgregarNoPriorizacion(Controladores.Id, Controladores.TipoESN);
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
        public string AgregarIngreso(string Controlador, string Modem, string Bateria, DateTime FechaArribo, int TipoNodoDestino, int NodoDestino, string Nota, int Involucrados = 0)
        {
            Clases.MovimientoLogistico MovLogistico = new Clases.MovimientoLogistico();

            MovLogistico.Controlador = Controlador;
            MovLogistico.Modem = Modem;
            MovLogistico.FechaArribo = FechaArribo;
            MovLogistico.IdTipoNodoDestino = TipoNodoDestino;
            MovLogistico.IdNodoDestino = NodoDestino;
            MovLogistico.Nota = Nota;
            MovLogistico.TipoMovimiento = 1;
            MovLogistico.Involucrados = Involucrados;
            MovLogistico.Bateria = Bateria;

            if (Bateria != null && Bateria != "")
            {
                if (Controlador != null && Controlador != "")
                {
                    int resp = BateriaModel.AsociarBateria(Bateria, Controlador, "CONTROLADOR", null);
                }
                else if (Modem != null && Modem != "")
                {
                    int resp = BateriaModel.AsociarBateria(Bateria, Modem, "MODEM", null);
                }
            }

            Clases.Validar aux = new Clases.Validar();
            int Flag = ControladorModel.AgregarIngreso(MovLogistico);
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
        public string AgregarRecuperacion(string[] Controlador, string[] Modem, int RP = 0, DateTime? FechaRecupera = null, int TipoNodoRecupera = 0, int NodoRecupera = 0, int Involucrados = 0)
        {
            Clases.RecuperacionControlador Recuperacion = new Clases.RecuperacionControlador();
            Clases.Notificacion ValidacionNotificado = new Clases.Notificacion(); ;
            int i = 0;
            string CorreoJulian = "";
            string CorreoValentina = "";
            string CorreoFrancisco = "";
            string CorreoUsuario = "";


            Clases.Validar aux = new Clases.Validar();

            for (i = 0; i < Controlador.Count(); i++)
            {
                ValidacionNotificado = ControladorModel.ValidarNotificacion(Controlador[i], Modem[i], RP);

                if (ValidacionNotificado.Validacion == 0)
                {
                    CorreoUsuario = ValidacionNotificado.Email;

                    if (CorreoUsuario == "jromero@liventusglobal.com")
                    {
                        CorreoJulian +=
                          "<tr>" +
                              "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Controlador[i] + "</td>" +
                          "</tr>";
                    }
                    else if (CorreoUsuario == "vreyes@liventusglobal.com")
                    {
                        CorreoValentina +=
                          "<tr>" +
                              "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Controlador[i] + "</td>" +
                          "</tr>";
                    }
                    else if (CorreoUsuario == "fdevivo@liventusglobal.com")
                    {
                        CorreoFrancisco +=
                          "<tr>" +
                              "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + Controlador[i] + "</td>" +
                          "</tr>";
                    }


                }
            }

            if (CorreoJulian != "")
            {
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.To.Add("jromero@liventusglobal.com");


                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Alerta automática - Recuperación Controlador Notificado - Liventus S.A.";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje
                mmsg.Body = "<p>Estimado:<p/>" +
                            "<p>Se han recuperado los siguientes controladores que fueron notificados:</p>" +
                            "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                "<thead >" +
                                    "<tr>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTROLADOR</ th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" + CorreoJulian +
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

                }
                catch (System.Net.Mail.SmtpException ex)
                {

                }
            }

            if (CorreoValentina != "")
            {
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.To.Add("vreyes@liventusglobal.com");


                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Alerta automática - Recuperación Controlador Notificado - Liventus S.A.";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje
                mmsg.Body = "<p>Estimado:<p/>" +
                            "<p>Se han recuperado los siguientes controladores que fueron notificados:</p>" +
                            "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                "<thead >" +
                                    "<tr>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTROLADOR</ th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" + CorreoValentina +
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

                }
                catch (System.Net.Mail.SmtpException ex)
                {

                }
            }

            if (CorreoFrancisco != "")
            {
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.To.Add("fdevivo@liventusglobal.com");


                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = "Alerta automática - Recuperación Controlador Notificado - Liventus S.A.";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje
                mmsg.Body = "<p>Estimado:<p/>" +
                            "<p>Se han recuperado los siguientes controladores que fueron notificados:</p>" +
                            "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                "<thead >" +
                                    "<tr>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTROLADOR</ th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" + CorreoFrancisco +
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

                }
                catch (System.Net.Mail.SmtpException ex)
                {

                }
            }

            for (i = 0; i < Controlador.Count(); i++)
            {
                Recuperacion.Controlador = Controlador[i];
                Recuperacion.IdRetrievalProvider = RP;
                Recuperacion.FechaRecuperacion = FechaRecupera;
                Recuperacion.TipoNodoRecupera = TipoNodoRecupera;
                Recuperacion.NodoRecupera = NodoRecupera;
                Recuperacion.Modem = Modem[i];
                Recuperacion.Involucrados = Involucrados;
                int Flag = ControladorModel.AgregarRecuperacion(Recuperacion);

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
            }


            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;

        }

        [HttpPost]
        public string AgregarSalida(string Controlador, string Modem, string Bateria, DateTime FechaEnvio, DateTime? FechaRecuperacion = null, DateTime? Eta = null, string NumeroEnvio = "", string Courier = "", int TipoNodoDestino = 0, int NodoDestino = 0, string Nota = "", int IdNave = 0, string Viaje = "", string Retrieval = "", /*int Fallas = 0,*/ int Involucrados = 0)
        {
            Clases.MovimientoLogistico MovLogistico = new Clases.MovimientoLogistico();

            MovLogistico.Controlador = Controlador;
            MovLogistico.Modem = Modem;
            MovLogistico.FechaEnvio = FechaEnvio;
            MovLogistico.Eta = Eta;
            MovLogistico.NumeroEnvio = NumeroEnvio;
            MovLogistico.EmpresaTransporte = Courier;
            MovLogistico.IdTipoNodoDestino = TipoNodoDestino;
            MovLogistico.IdNodoDestino = NodoDestino;
            MovLogistico.Nota = Nota;
            MovLogistico.TipoMovimiento = 2;
            MovLogistico.IdNave = IdNave;
            MovLogistico.Viaje = Viaje;
            MovLogistico.RetrievalProvider = Retrieval;
            MovLogistico.FechaRecuperacion = FechaRecuperacion;
            MovLogistico.Involucrados = Involucrados;
            MovLogistico.Bateria = Bateria;


            if (Bateria != null && Bateria != "")
            {
                if (Controlador != null && Controlador != "")
                {
                    int resp = BateriaModel.AsociarBateria(Bateria, Controlador, "CONTROLADOR", null);
                }
                else if (Modem != null && Modem != "")
                {
                    int resp = BateriaModel.AsociarBateria(Bateria, Modem, "MODEM", null);
                }
            }

            Clases.Validar aux = new Clases.Validar();
            int Flag = ControladorModel.AgregarSalida(MovLogistico/*, Fallas*/);
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
        public string AgregarSalidaNodo(string Controlador, string Modem, DateTime Eta, string TipoNodoDestino = "", string NodoDestino = "", string Nave = "", string Viaje = "", string Nota = "", int Involucrados = 0)
        {
            Clases.MovimientoLogistico MovLogistico = new Clases.MovimientoLogistico();

            MovLogistico.Controlador = Controlador;
            MovLogistico.Modem = Modem;
            MovLogistico.Eta = Eta;
            MovLogistico.TipoNodoDestino = TipoNodoDestino;
            MovLogistico.PaisDestino = "";
            MovLogistico.NodoDestino = NodoDestino;
            MovLogistico.TipoMovimiento = 2;
            MovLogistico.Nave = Nave;
            MovLogistico.Viaje = Viaje;
            MovLogistico.Nota = Nota;
            MovLogistico.Involucrados = Involucrados;


            Clases.Validar aux = new Clases.Validar();
            int Flag = ControladorModel.AgregarSalidaNodo(MovLogistico);
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
        public string ValidarContenedor(string Contenedor)
        {
            Clases.Validar aux = new Clases.Validar();
            int Estado = ControladorModel.ValidarContenedor(Contenedor);
            switch (Estado)
            {
                case 0:
                    aux.Mensaje = "Contenedor Validado";
                    aux.validador = Estado;
                    break;
                case 1:
                    aux.Mensaje = "Contenedor esta aprobado";
                    aux.validador = Estado;
                    break;
                case 2:
                    aux.Mensaje = "La solicitud asociada al contenedor ha sido cancelada";
                    aux.validador = Estado;
                    break;
                case 3:
                    aux.Mensaje = "Contenedor ha caducado";
                    aux.validador = Estado;
                    break;
                case 4:
                    aux.Mensaje = "Contenedor está asociado a otro servicio";
                    aux.validador = Estado;
                    break;
                case 5:
                    aux.Mensaje = "Contenedor Ha sido rechazado";
                    aux.validador = Estado;
                    break;
                case 6:
                    aux.Mensaje = "Contenedor recien fue creado en el sistema";
                    aux.validador = Estado;
                    break;
                case 7:
                    aux.Mensaje = "Contenedor ha sido ingresado en una solicitud";
                    aux.validador = Estado;
                    break;
            }


            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string ValidarContenedorById(int IdContenedor)
        {
            Clases.Validar aux = new Clases.Validar();
            int Estado = ControladorModel.ValidarContenedorById(IdContenedor);
            switch (Estado)
            {
                case 0:
                    aux.Mensaje = "Contenedor no se puede utilizar en Servicio";
                    aux.validador = Estado;
                    break;
                case 1:
                    aux.Mensaje = "Contenedor esta aprobado";
                    aux.validador = Estado;
                    break;
                case 2:
                    aux.Mensaje = "La solicitud asociada al contenedor ha sido cancelada";
                    aux.validador = Estado;
                    break;
                case 3:
                    aux.Mensaje = "Contenedor ha caducado";
                    aux.validador = Estado;
                    break;
                case 4:
                    aux.Mensaje = "Contenedor está asociado a otro servicio";
                    aux.validador = Estado;
                    break;
                case 5:
                    aux.Mensaje = "Contenedor Ha sido rechazado";
                    aux.validador = Estado;
                    break;
                case 6:
                    aux.Mensaje = "Contenedor recien fue creado en el sistema";
                    aux.validador = Estado;
                    break;
                case 7:
                    aux.Mensaje = "Contenedor ha sido reservado";
                    aux.validador = Estado;
                    break;
            }


            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string ValidarControladorById(string IdControlador)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.Controlador Controladores = ControladorModel.GetIdControlador(IdControlador);

            int Estado = ControladorModel.ValidarControladorById(Controladores.Id);

            switch (Estado)
            {
                case 0:
                    aux.Mensaje = "Controlador esta recien creado";
                    aux.validador = Estado;
                    break;
                case 1:
                    aux.Mensaje = "Controlador se encuentra asociado a otro Servicio";
                    aux.validador = Estado;
                    break;
                case 2:
                    aux.Mensaje = "Controlador no se ha recuperado en Logistica";
                    aux.validador = Estado;
                    break;
                case 3:
                    aux.Mensaje = "Controlador se recuperó";
                    aux.validador = Estado;
                    break;
                case 4:
                    aux.Mensaje = "Controlador fue retornado";
                    aux.validador = Estado;
                    break;
                case 9:
                    aux.Mensaje = "Controlador esta perdido";
                    aux.validador = Estado;
                    break;
                case 10:
                    aux.Mensaje = "Controlador no se puede utilizar en Servicio";
                    aux.validador = Estado;
                    break;
            }

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string ValidarControladorByIdEditar(string IdControlador, int IdServicio)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.Controlador Controladores = ControladorModel.GetIdControlador(IdControlador);
            int Estado = ControladorModel.ValidarControladorById(Controladores.Id);

            switch (Estado)
            {
                case 0:
                    aux.Mensaje = "Controlador esta recien creado";
                    aux.validador = Estado;
                    break;
                case 1:
                    aux.Mensaje = "Controlador se encuentra asociado a otro Servicio";
                    aux.validador = Estado;
                    break;
                case 2:
                    aux.Mensaje = "Controlador no se ha recuperado en Logistica";
                    aux.validador = Estado;
                    break;
                case 3:
                    aux.Mensaje = "Controlador se recuperó";
                    aux.validador = Estado;
                    break;
                case 4:
                    aux.Mensaje = "Controlador fue retornado";
                    aux.validador = Estado;
                    break;
                case 5:
                    aux.Mensaje = "Controlador se encuentra asociado a otro Servicio distinto al actual";
                    aux.validador = Estado;
                    break;
                case 9:
                    aux.Mensaje = "Controlador esta perdido";
                    aux.validador = Estado;
                    break;
                case 10:
                    aux.Mensaje = "Controlador no se puede utilizar en Servicio";
                    aux.validador = Estado;
                    break;
            }

            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public int ValidarControlador(string Controlador)
        {
            Clases.Validar aux = new Clases.Validar();
            int Estado = ControladorModel.ValidarControlador(Controlador);
            return Estado;

        }

        [HttpPost]
        public int ValidarControladorDeposito(string Controlador, int IdServicio)
        {
            Clases.Validar aux = new Clases.Validar();
            int Estado = ControladorModel.ValidarControladorDeposito(Controlador, IdServicio);
            return Estado;

        }

        [HttpPost]
        public int ValidarModem(string Modem)
        {
            Clases.Validar aux = new Clases.Validar();
            int Estado = ControladorModel.ValidarModem(Modem);
            return Estado;

        }

        [HttpPost]
        public int ValidarESN(string ESN)
        {
            Clases.Validar aux = new Clases.Validar();
            int Estado = ControladorModel.ValidarESN(ESN);
            return Estado;

        }

        [HttpPost]
        public string ValidarTipoESN(string ESN)
        {
            Clases.Validar aux = new Clases.Validar();
            string Tipo = ControladorModel.ValidarTipoESN(ESN);
            return Tipo;

        }

        [HttpPost]
        public int ValidarControladorServicio(string Controlador)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.Controlador Controladores = new Clases.Controlador();
            Controladores = ControladorModel.GetIdControlador(Controlador);
            int Estado = ControladorModel.ValidarControladorServicio(Controladores.Id);
            return Estado;

        }

        [HttpPost]
        public int ValidarModemServicio(string Modem)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.ESN Modems = new Clases.ESN();
            Modems = ControladorModel.GetIdESN(Modem);
            int Estado = ControladorModel.ValidarModemServicio(Modems.Id);
            return Estado;

        }

        public JsonResult GetControladoresLogistica1()
        {
            List<Clases.Controladores> LogisticaControladores = new List<Clases.Controladores>();
            LogisticaControladores = ControladorModel.GetControladoresLogistica1();
            //LogisticaControladores = ControladorModel.GetAlertasEquipos(LogisticaControladores);
            var resultados = Json(LogisticaControladores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetControladoresDeposito()
        {
            List<Clases.Controladores> LogisticaControladores = new List<Clases.Controladores>();
            LogisticaControladores = ControladorModel.GetControladoresDeposito();
            var resultados = Json(LogisticaControladores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetControladoresLogisticaRetrieval()
        {
            List<Clases.Controladores> LogisticaControladores = new List<Clases.Controladores>();
            LogisticaControladores = ControladorModel.GetControladoresLogisticaRetrieval();
            //LogisticaControladores = ControladorModel.GetAlertasEquipos(LogisticaControladores);
            var resultados = Json(LogisticaControladores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        [HttpPost]
        public string AgregarNotificacion(int Controlador, int Retrieval, string Usuario)
        {
            Clases.MovimientoLogistico MovLogistico = new Clases.MovimientoLogistico();

            Clases.Validar aux = new Clases.Validar();
            int Flag = ControladorModel.AgregarNotificacion(Controlador, Retrieval, Usuario);
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
        public string AgregarDamageReport(string[] Controlador)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (int i = 0; i < Controlador.Count(); i++)
            {
                Clases.ESN esn = ControladorModel.GetIdESN(Controlador[i]);
                Flag = ControladorModel.AgregarDamageReport(esn.Id, esn.TipoESN);
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
        public string QuitarDamageReport(string[] Controlador)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = 0;
            for (int i = 0; i < Controlador.Count(); i++)
            {
                Clases.Controlador controlador = ControladorModel.GetIdControlador(Controlador[i]);
                Flag = ControladorModel.QuitarDamageReport(controlador.Id);
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
        public string EliminarMovLogistico(string[] ListaESN)
        {
            Clases.Validar aux = new Clases.Validar();
            Clases.ESN ESN = new Clases.ESN();
            int Flag = 0;
            for (int i = 0; i < ListaESN.Count(); i++)
            {
                ESN = ControladorModel.GetIdESN(ListaESN[i]);
                Flag = ControladorModel.EliminarMovLogistico(ESN.Id, ESN.TipoESN);
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
        public string AgregarNotificacion(int IdESN, string TipoESN, int Retrieval, string Usuario, int Involucrados = 0)
        {
            Clases.MovimientoLogistico MovLogistico = new Clases.MovimientoLogistico();

            Clases.Validar aux = new Clases.Validar();
            int Flag = ControladorModel.AgregarNotificacion(IdESN, TipoESN, Retrieval, Usuario, Involucrados);
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
        public void CorreoLegal(string[] ListaControladores, int Accion = 0)
        {
            // Accion: [1: Registro Entrada, 2: Registro Salida, 3: Registro Recuperación]
            if (Accion == 1 || Accion == 2 || Accion == 3)
            {
                string estado_retrieval = "";
                string usuario = "";
                string Correo = "";
                int EnviarCorreo = 0;

                for (int i = 0; i < ListaControladores.Count(); i++)
                {
                    Clases.Controlador modelo_idcontrolador = ControladorModel.GetIdControlador(ListaControladores[i]);
                    List<Clases.MovimientoLogistico> modelo_controlador = ControladorModel.GetControladorById(modelo_idcontrolador.Id);

                    if (modelo_controlador[0].Prioridad == 1)
                    {

                        int controlador_correo = 0;

                        if (Accion == 1 && modelo_controlador[0].TipoMovimiento == 1 && modelo_controlador[0].TipoNodoDestino == "BODEGA" && modelo_controlador[0].NodoDestino == "LIVENTUS LAB")
                        {
                            estado_retrieval = "LAB";
                            usuario = modelo_controlador[0].UsuarioMovimiento;
                            controlador_correo = 1;
                        }
                        else if (Accion == 2 && modelo_controlador[0].TipoMovimiento == 2 && modelo_controlador[0].TipoNodoDestino == "BODEGA" && modelo_controlador[0].NodoDestino == "LIVENTUS LAB")
                        {
                            if (modelo_controlador[0].Nave == "W/O DATA")
                            {
                                estado_retrieval = "COURIER AÉREO";
                            }
                            else
                            {
                                estado_retrieval = "COURIER MARÍTIMO";
                            }
                            usuario = modelo_controlador[0].UsuarioMovimiento;
                            controlador_correo = 1;
                        }
                        else if (Accion == 3)
                        {
                            estado_retrieval = "RECOVERED";
                            usuario = modelo_controlador[0].UsuarioRecuperacion;
                            controlador_correo = 1;
                        }

                        if (controlador_correo == 1)
                        {
                            EnviarCorreo = 1;
                            Correo += "<tr>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + modelo_controlador[0].Controlador + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + modelo_controlador[0].Contenedor + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + modelo_controlador[0].Naviera + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + usuario + "</td>" +
                                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + estado_retrieval + "</td>" +
                                    "</tr>";
                        }
                    }
                }

                if (EnviarCorreo == 1)
                {
                    string correo1 = "";
                    string correo2 = "";
                    string host = Request.Url.Host;

                    if (host == "localhost")
                    {
                        correo1 = "rcontreras@liventusglobal.com";
                        correo2 = ""; //"rcontreras@liventusglobal.com";
                    }
                    else
                    {
                        correo1 = ""; //"nleon@liventusglobal.com";
                        correo2 = "vrios@liventusglobal.com";
                    }


                    /*-------------------------MENSAJE DE CORREO----------------------*/

                    //Creamos un nuevo Objeto de mensaje
                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                    //Direccion de correo electronico a la que queremos enviar el mensaje
                    if (correo1 != "")
                    {
                        mmsg.To.Add(correo1);
                    }

                    if (correo2 != "")
                    {
                        mmsg.To.Add(correo2);
                    }

                    mmsg.Bcc.Add("soporteti@liventusglobal.com");
                    //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                    //Asunto
                    mmsg.Subject = "Alerta automática - Cambio de Estado de Retrieval - Liventus S.A.";
                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                    //Cuerpo del Mensaje
                    mmsg.Body = "<p>Estimados:<p/>" +
                                "<p>Ha cambiado el estado de retrieval de los siguientes controladores:</p>" +
                                "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                    "<thead >" +
                                        "<tr>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTROLADOR</ th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTENEDOR</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>NAVIERA</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>COORDINADOR RETRIEVAL</th>" +
                                            "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ESTADO RETRIEVAL</th>" +
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
                    }
                    catch (System.Net.Mail.SmtpException ex)
                    {

                    }
                }
            }
        }

        public JsonResult GetControladoresAutomaticos()
        {
            List<Clases.Controladores> LogisticaControladores = new List<Clases.Controladores>();
            LogisticaControladores = ControladorModel.GetControladoresAutomaticos();
            var resultados = Json(LogisticaControladores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }


        [HttpPost]
        public string CambiarMovimientoLogistico(string Controlador, string Modem, DateTime FechaRecuperacion, DateTime FechaSalida, string NumeroGuia, int Courier, int RetrievalProvider, int TipoNodo, int Nodo, int Coordinador)
        {
            Clases.MovimientoLogistico MovLogistico = new Clases.MovimientoLogistico();

            Clases.Validar aux = new Clases.Validar();
            int Flag = ControladorModel.CambiarMovimientoLogisticos(Controlador, Modem, FechaRecuperacion, FechaSalida, NumeroGuia, Courier, RetrievalProvider, TipoNodo, Nodo, Coordinador);
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

        public int GetTipoNodoPorNombre(string NombreTipoNodo)
        {
            Clases.TipoLugar TipoLugar = new Clases.TipoLugar();
            TipoLugar = TipoLugarModelo.GetTipoNodoPorNombre(NombreTipoNodo);
            return TipoLugar.Id;
        }

        public int GetNodoPorNombre(string NombreNodo)
        {
            Clases.Nodo Nodo = new Clases.Nodo();
            Nodo = TipoLugarModelo.GetNodoPorNombre(NombreNodo);
            return Nodo.Id;
        }

        public int GetNavePorNombre(string NombreNave)
        {
            Clases.Nave Nave = new Clases.Nave();
            Nave = NaveModelo.GetIdNave(NombreNave);
            return Nave.IdNave;
        }

        public JsonResult GetCountMatriz(int IdPais, int IdCiudad, int IdTipoLocalidad, int IdLocalidad)
        {
            int[] contador = new int[10];
            contador = ControladorModel.GetCountMatriz(IdPais, IdCiudad, IdTipoLocalidad, IdLocalidad);
            var resultados = Json(contador, JsonRequestBehavior.AllowGet);
            return resultados;
            //resultados.MaxJsonLength = Int32.MaxValue;
        }
    }
}
