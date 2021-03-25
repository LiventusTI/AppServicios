using Plataforma.Models;
using Plataforma.Models.Contenedor;
using Plataforma.Models.Leaktest;
using Plataforma.Models.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class ContenedoresController : Controller
    {
        private static Dictionary<char, int> _Alphabet;

        private static Dictionary<char, int> Alphabet
        {
            get
            {
                if (_Alphabet == null)
                {
                    //If _Alphabed is null Initialise new dictionary and fill it
                    _Alphabet = new Dictionary<char, int>();

                    //Add Letters
                    _Alphabet.Add('A', 10);
                    _Alphabet.Add('B', 12);
                    _Alphabet.Add('C', 13);
                    _Alphabet.Add('D', 14);
                    _Alphabet.Add('E', 15);
                    _Alphabet.Add('F', 16);
                    _Alphabet.Add('G', 17);
                    _Alphabet.Add('H', 18);
                    _Alphabet.Add('I', 19);
                    _Alphabet.Add('J', 20);
                    _Alphabet.Add('K', 21);
                    _Alphabet.Add('L', 23);
                    _Alphabet.Add('M', 24);
                    _Alphabet.Add('N', 25);
                    _Alphabet.Add('O', 26);
                    _Alphabet.Add('P', 27);
                    _Alphabet.Add('Q', 28);
                    _Alphabet.Add('R', 29);
                    _Alphabet.Add('S', 30);
                    _Alphabet.Add('T', 31);
                    _Alphabet.Add('U', 32);
                    _Alphabet.Add('V', 34);
                    _Alphabet.Add('W', 35);
                    _Alphabet.Add('X', 36);
                    _Alphabet.Add('Y', 37);
                    _Alphabet.Add('Z', 38);

                    //Add Numbers
                    _Alphabet.Add('0', 0);
                    _Alphabet.Add('1', 1);
                    _Alphabet.Add('2', 2);
                    _Alphabet.Add('3', 3);
                    _Alphabet.Add('4', 4);
                    _Alphabet.Add('5', 5);
                    _Alphabet.Add('6', 6);
                    _Alphabet.Add('7', 7);
                    _Alphabet.Add('8', 8);
                    _Alphabet.Add('9', 9);
                }

                return _Alphabet;
            }
        }
        // GET: Contenedores
        public ActionResult VisualizarResumenContenedores()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult VisualizarHistoricoCancelados()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: Contenedores/Details/5
        public ActionResult VerHistoricosCancelados()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: Contenedores/Create
        public ActionResult VisualizarAprobadosDisponibles()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string GetContenedoresByPais(int IdPais)
        {
            Clases.InvetarioContenedor Resultado = ContenedorModelo.GetContenedoresByPais(IdPais);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultado);
            return datos;
        }

        [HttpPost]
        public string GetContenedoresCancelados()
        {
            List<Clases.Contenedor> Resultado = ContenedorModelo.GetContenedoresCancelados();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultado);
            return datos;
        }

        [HttpPost]
        public string GetEstadoContenedores()
        {
            List<Clases.EstadoContenedor> Estados = ContenedorModelo.GetEstadoContenedores();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Estados);
            return datos;
        }

        [HttpPost]
        public string GetContenedoresByCiudad(int IdCiudad)
        {
            Clases.InvetarioContenedor Resultado = ContenedorModelo.GetContenedoresByCiudad(IdCiudad);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultado);
            return datos;
        }

        [HttpPost]
        public string GetContenedoresByDeposito(int IdDeposito)
        {
            Clases.InvetarioContenedor Resultado = ContenedorModelo.GetContenedoresByDeposito(IdDeposito);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultado);
            return datos;
        }

        [HttpPost]
        public string GetContenedores(int Estado)
        {
            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            Contenedores = ContenedorModelo.GetContenedores(Estado);
            Contenedores = ContenedorModelo.GetAlertasContenedores(Contenedores);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }

        [HttpPost]
        public string GetContenedoresAprobados()
        {
            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            Contenedores = ContenedorModelo.GetContenedoresAprobados();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }

        [HttpPost]
        public string GetContenedoresAprobadosSP(string Usuario)
        {
            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            Contenedores = ContenedorModelo.GetContenedoresAprobadosSP(Usuario);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }

        [HttpPost]
        public string CancelarContenedor(int IdContenedor)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ContenedorModelo.CancelarContenedor(IdContenedor);
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
        public string GuardarGestion(int IdContenedor, string Gestion ="", DateTime? FechaGestion=null)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ContenedorModelo.GuardarGestion(IdContenedor, Gestion, FechaGestion);
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
        public string DisponibilizarContenedor(int IdContenedor)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ContenedorModelo.DisponibilizarContenedor(IdContenedor);
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
        public string GetContenedoresByReserva(int IdReserva)
        {
            List<Clases.DetalleContenedor> Contenedores = new List<Clases.DetalleContenedor>();
            Contenedores = ContenedorModelo.GetContenedoresByReserva(IdReserva);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }

        [HttpPost]
        public string GetInfoFiltro(int IdContenedor)
        {
            List<Clases.ResultadoLeaktest> Contenedores = new List<Clases.ResultadoLeaktest>();
            Clases.ReservaLeaktestCompleta Reserva = new Clases.ReservaLeaktestCompleta();
            Contenedores = ContenedorModelo.GetInfoFiltro(IdContenedor);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }

        [HttpPost]
        public string GetInfoFiltroSP(int IdContenedor)
        {
            List<Clases.ResultadoLeaktest> Contenedores = new List<Clases.ResultadoLeaktest>();
            Clases.ReservaLeaktestCompleta Reserva = new Clases.ReservaLeaktestCompleta();
            Contenedores = ContenedorModelo.GetInfoFiltroSP(IdContenedor);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }

        public bool ValidarNumeroContenedor(string containerNumberToCheck)
        {
            //Clean the input string from Chars that are not in the Alphabed
            string containerNumber = CleanConNumberString(containerNumberToCheck);

            //Return true if the input string is empty
            //Used mostly for DataGridView to set the False validation only on false Container Numbers
            //and not empty ones
            if (containerNumber == string.Empty) return true;

            //Return False if the input string has not enough Characters
            if (containerNumber.Length != 11) return false;

            //Get the Sum of the ISO Formula
            double summ = GetSumm(containerNumber);

            //Calculate the Check number with the ISO Formula
            double tempCheckNumber = summ - (Math.Floor(summ / 11) * 11);

            //Set temCheckNumber 0 if it is 10 - In somme cases this is needed
            if (tempCheckNumber == 10) tempCheckNumber = 0;

            //Return true if the calculated check number matches with the input check number
            if (tempCheckNumber == GetCheckNumber(containerNumber))
                return true;

            //If no match return false
            return false;
        }

        private static string CleanConNumberString(string inputString)
        {
            //Set all Chars to Upper
            string resultString = inputString.ToUpper();

            //Loop Trough all chars
            foreach (char c in inputString)
            {
                //Remove Char if its not in the ISO Alphabet
                if (!Alphabet.Keys.Contains(c))
                    resultString = resultString.Replace(c.ToString(), string.Empty); //Remove chars with the String.Replace Method
            }

            //Return the cleaned String
            return resultString;
        }

        private static int GetCheckNumber(string inputString)
        {
            //Loop if string is longer than 1
            if (inputString.Length > 1)
            {
                //Get the last char of the string
                char checkChar = inputString[inputString.Length - 1];

                //Initialise a integer
                int CheckNumber = 0;

                //Parse the last char to a integer
                if (Int32.TryParse(checkChar.ToString(), out CheckNumber))
                    return CheckNumber; //Return the integer if the parsing can be done

            }

            //If parsing can´t be done and the string has just 1 char or is empty
            //Return 11 (A number that can´t be a check number!!!)
            return 11;
        }

        private static double GetSumm(string inputString)
        {
            //Set summ to 0
            double summ = 0;

            //Calculate only if the container string is not empty
            if (inputString.Length > 1)
            {
                //Loop through all chars in the container string
                //EXCEPT the last char!!!
                for (int i = 0; i < inputString.Length - 1; i++)
                {
                    //Get the current char
                    char temChar = inputString[i];

                    //Initialise a integer to represent the char number in the ISO Alphabet
                    //Set it to 0
                    int charNumber = 0;

                    //If Char exists in the Table get it´s number
                    if (Alphabet.Keys.Contains(temChar))
                        charNumber = Alphabet[temChar];

                    //Add the char number to the sum using the ISO Formula
                    summ += charNumber * (Math.Pow(2, i));
                }
            }

            //Return the calculated summ
            return summ;
        }

        [HttpPost]
        public string GetContenedoresEditar(int IdServicio)
        {
            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            List<Clases.Contenedor> ContenedoresValidados = new List<Clases.Contenedor>();
            List<Clases.Servicio> Servicio = new List<Clases.Servicio>();
            Servicio = ReservaModelo.GetServicioById(IdServicio);
            Contenedores = ContenedorModelo.GetContenedoresEditar();

            for (var i=0; i< Contenedores.Count(); i++) {
                if (Contenedores[i].EstadoContenedor == 1)
                {
                    ContenedoresValidados.Add(Contenedores[i]);
                }
                else if(Contenedores[i].EstadoContenedor == 4 && Contenedores[i].IdContenedor == Servicio[0].IdContenedor) {
                    ContenedoresValidados.Add(Contenedores[i]);
                }

            }
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(ContenedoresValidados);
            return datos;
        }

        [HttpPost]
        public string FiltroPorFechasCancelados(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            List<Clases.Contenedor> Resultado = ContenedorModelo.FiltroPorFechasCancelados(Tipo, FechaIni, FechaFin);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Resultado);
            return datos;
        }

        [HttpPost]
        public string GetInfoContenedor(string Contenedor)
        {
            Clases.Contenedor Contenedores = new Clases.Contenedor();
            Contenedores = ContenedorModelo.GetInfoContenedor(Contenedor);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }

        [HttpPost]
        public string QuitarScrubber(int IdContenedor)
        {
            Clases.Validar aux = new Clases.Validar();
            int Flag = ContenedorModelo.QuitarScrubber(IdContenedor);
            if (Flag == 0)
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
    }
}
