using Plataforma.Models;
using Plataforma.Models.Retrieval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Outlook=Microsoft.Office.Interop.Outlook;

namespace Plataforma.Controllers
{
    public class RetrievalController : Controller
    {
        // GET: RetrievalProvider
        public ActionResult VisualizarRetrievalProvider()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string CrearRetrievalProvider(string NombreRetrievalProvider, string Email, string Telefono, int Diasprearribo, int Diaspostarribo, int Estado, string Email1 = null, string Email2 = null, string Telefono1 = null, string Telefono2 = null, int Diasprearribo1 = 0, int Diasprearribo2 = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = RetrievalModelo.CrearServiceProvider(NombreRetrievalProvider, Email, Telefono, Diasprearribo, Diaspostarribo, Estado, Email1, Email2, Telefono1, Telefono2, Diasprearribo1, Diasprearribo2);
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

                aux.Mensaje = "Operación no se Realizó, Retrieval Provider Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string GetRetrievalProvider()
        {
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            RetrievalProvider = RetrievalModelo.GetRetrievalProvider();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(RetrievalProvider);
            return datos;
        }

        [HttpPost]
        public string GetRetrievalProviderActivas()
        {
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            RetrievalProvider = RetrievalModelo.GetRetrievalProviderActivas();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(RetrievalProvider);
            return datos;
        }

        [HttpPost]
        public string GetRetrievalProviderByNaviera(int IdNaviera)
        {
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            RetrievalProvider = RetrievalModelo.GetRetrievalProviderByNaviera(IdNaviera);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(RetrievalProvider);
            return datos;
        }

        [HttpPost]
        public string GetRetrievalProviderByNodo(int TipoNodo, int Nodo)
        {
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            RetrievalProvider = RetrievalModelo.GetRetrievalProviderByNodo(TipoNodo, Nodo);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(RetrievalProvider);
            return datos;
        }

        /*[HttpPost]
        public string EnviarCorreoRP()
        {
            try
            {
                List<string> lstAllRecipients = new List<string>();
                //Below is hardcoded - can be replaced with db data
                lstAllRecipients.Add("rcoulon@liventusglobal.com");

                Outlook.Application outlookApp = new Outlook.Application();
                Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                Outlook.Inspector oInspector = oMailItem.GetInspector;
                // Thread.Sleep(10000);

                // Recipient
                Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                foreach (String recipient in lstAllRecipients)
                {
                    Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(recipient);
                    oRecip.Resolve();
                }

                ////Add CC
                //Outlook.Recipient oCCRecip = oRecips.Add("THIYAGARAJAN.DURAIRAJAN@testmail.com");
                //oCCRecip.Type = (int)Outlook.OlMailRecipientType.olCC;
                //oCCRecip.Resolve();

                //Add Subject
                oMailItem.Subject = "Test Mail";

                // body, bcc etc...

                //Display the mailbox
                oMailItem.Display(true);
            }
            catch (Exception objEx)
            {
                Response.Write(objEx.ToString());
            }
            return "";
        }*/

        [HttpPost]
        public string GetRetrievalProviderById(int IdRetrieval)
        {
            Clases.RetrievalProvider RetrievalProvider = new Clases.RetrievalProvider();
            RetrievalProvider = RetrievalModelo.GetRetrievalProviderById(IdRetrieval);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(RetrievalProvider);
            return datos;
        }

        [HttpPost]
        public string GetNavierasRetrieval(int IdRetrieval)
        {
            List<Clases.Naviera> Navieras = new List<Clases.Naviera>();
            Navieras = RetrievalModelo.GetNavierasRetrieval(IdRetrieval);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Navieras);
            return datos;
        }

        [HttpPost]
        public string GetNodosRetrieval(int IdRetrieval)
        {
            List<Clases.AsociacionNodoRetrieval> Nodos = new List<Clases.AsociacionNodoRetrieval>();
            Nodos = RetrievalModelo.GetNodosRetrieval(IdRetrieval);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Nodos);
            return datos;
        }

        [HttpPost]
        public string EditarRetrievalProvider(string NombreRetrievalProvider, string Email, string Telefono, int Diasprearribo, int Diaspostarribo, int Estado, int idRetrievalProvider, string Email1 = null, string Email2 = null, string Telefono1 = null, string Telefono2 = null, int Diasprearribo1 = 0, int Diasprearribo2 = 0)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = RetrievalModelo.EditarRetrievalProvider(NombreRetrievalProvider, Email, Telefono, Diasprearribo, Diaspostarribo, Estado, idRetrievalProvider, Email1, Email2, Telefono1, Telefono2, Diasprearribo1, Diasprearribo2);
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

                aux.Mensaje = "Operación no se Realizó, Retrieval Provider Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }
        [HttpPost]
        public string AgregarNavieraRetrieval(int IdNaviera, int IdRetrieval)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = RetrievalModelo.AgregarNavieraRetrieval(IdNaviera, IdRetrieval);
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
                aux.Mensaje = "Operación no se Realizó, Asociacion de naviera Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

        [HttpPost]
        public string AgregarNodoRetrieval(int IdTipoLugar, int IdNodo, int Dias, int IdRetrieval)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = RetrievalModelo.AgregarNodoRetrieval(IdTipoLugar, IdNodo, Dias, IdRetrieval);
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
                aux.Mensaje = "Operación no se Realizó, Asociacion de naviera Existente.";
                aux.validador = Flag;
            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(aux);
            return datos;
        }

    }
}
