using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plataforma.Models.Contenedor;
using Plataforma.Models.Leaktest;
using Plataforma.Models.Servicio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static Plataforma.Models.Clases;


namespace Plataforma.Models.Reservas
{
    public class ReservaModelo
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo;Connect Timeout=10";

        public static List<Clases.Reserva> GetReservas()
        {
            SqlConnection cnn;
            List<Clases.Reserva> Reservas = new List<Clases.Reserva>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarReservas", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    Reservas.Add(new Clases.Reserva
                    {
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        CantidadServicios = Convert.ToInt32(reader["CANTIDADSERVICIOS"]),
                        Eta = Convert.ToDateTime(reader["ETA"]),
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"], CultureInfo.InvariantCulture),
                        Exportador = reader["NOMBREXPORTADOR"].ToString(),
                        Nave = reader["NOMBRENAVE"].ToString(),
                        Naviera = reader["NOMBRENAVIERA"].ToString(),
                        PuertoDestino = reader["NOMBREPUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["NOMBREPUERTOORIGEN"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Commodity = reader["NOMBRECOMMODITY"].ToString(),
                        CommodityTecnica = reader["NOMBREPLATAFORMATECNICA"].ToString(),
                        FreightForwarder = reader["NOMBREFREIGHTFORWARDER"].ToString(),
                        Estado = Convert.ToInt32(reader["ESTADO"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Reservas;
        }

        public static List<Clases.ReservaEDI> GetReservasEDI()
        {
            SqlConnection cnn;
            List<Clases.ReservaEDI> Reservas = new List<Clases.ReservaEDI>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarReservasEDI", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etd = null;
                    DateTime? eta = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    int ContenedorValido = 2;

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        eta = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    //if (reader["CONTENEDOR_INVALIDO"] != DBNull.Value)
                    //{
                    //    ContenedorValido = Convert.ToInt32(reader["CONTENEDOR_INVALIDO"].ToString());
                    //}


                    // VALIDAR CAMPOS NULOS
                    float o2 = 0;
                    float co2 = 0;
                    float temperatura = 0;
                    int idReserva = 0;
                    string nombreEDI = "";
                    string booking = "";
                    string viaje = "";
                    string consignatario = "";
                    string puertoOrigen = "";
                    string puertoDestino = "";
                    string exportador = "";
                    string nave = "";
                    string naviera = "";
                    string commodity = "";
                    string freightForwarder = "";
                    string contenedor = "";
                    string clausula = "";
                    string tipoEdi = "";
                    string deposito = "";
                    string terminal = "";
                    int cantidadServicios = 0;

                    if (reader["O2"] != DBNull.Value) o2 = float.Parse(reader["O2"].ToString());
                    if (reader["CO2"] != DBNull.Value) co2 = float.Parse(reader["CO2"].ToString());
                    if (reader["TEMPERATURA"] != DBNull.Value) temperatura = float.Parse(reader["TEMPERATURA"].ToString());
                    // if (reader["ID_RESERVA"] != DBNull.Value) idReserva = Convert.ToInt32(reader["ID_RESERVA"]);
                    if (reader["NOMBRE_EDI"] != DBNull.Value) nombreEDI = reader["NOMBRE_EDI"].ToString();
                    if (reader["BOOKING"] != DBNull.Value) booking = reader["BOOKING"].ToString();
                    if (reader["VIAJE"] != DBNull.Value) viaje = reader["VIAJE"].ToString();
                    if (reader["CONSIGNATARIO"] != DBNull.Value) consignatario = reader["CONSIGNATARIO"].ToString();
                    if (reader["PUERTOORIGEN"] != DBNull.Value) puertoOrigen = reader["PUERTOORIGEN"].ToString();
                    if (reader["PUERTODESTINO"] != DBNull.Value) puertoDestino = reader["PUERTODESTINO"].ToString();
                    if (reader["EXPORTADOR"] != DBNull.Value) exportador = reader["EXPORTADOR"].ToString();
                    if (reader["NAVE"] != DBNull.Value) nave = reader["NAVE"].ToString();
                    if (reader["NAVIERA"] != DBNull.Value) naviera = reader["NAVIERA"].ToString();
                    if (reader["COMMODITY"] != DBNull.Value) commodity = reader["COMMODITY"].ToString();
                    if (reader["FREIGHTFORWARDER"] != DBNull.Value) freightForwarder = reader["FREIGHTFORWARDER"].ToString();
                    if (reader["CONTENEDOR"] != DBNull.Value) contenedor = reader["CONTENEDOR"].ToString();
                    if (reader["CLAUSULA"] != DBNull.Value) clausula = reader["CLAUSULA"].ToString();
                    if (reader["TIPO_EDI"] != DBNull.Value) tipoEdi = reader["TIPO_EDI"].ToString();
                    if (reader["DEPOSITO"] != DBNull.Value) deposito = reader["DEPOSITO"].ToString();
                    if (reader["TERMINAL"] != DBNull.Value) terminal = reader["TERMINAL"].ToString();
                    if (reader["CANTIDAD_SERVICIOS"] != DBNull.Value) cantidadServicios = Convert.ToInt32(reader["CANTIDAD_SERVICIOS"]);

                    Reservas.Add(new Clases.ReservaEDI
                    {
                        NombreEDI = nombreEDI,
                        Booking = booking,
                        Viaje = viaje,
                        Consignatario = consignatario,
                        Eta = eta,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        PuertoOrigen = puertoOrigen,
                        PuertoDestino = puertoDestino,
                        Exportador = exportador,
                        Nave = nave,
                        Naviera = naviera,
                        O2 = o2,
                        CO2 = co2,
                        Commodity = commodity,
                        FreightForwarder = freightForwarder,
                        Temperatura = temperatura,
                        Contenedor = contenedor,
                        //ContenedorValido = ContenedorValido,
                        DepositoContenedor = deposito,
                        Terminal = terminal,
                        Clausula = clausula,
                        TipoEdi = tipoEdi,
                        CantidadContenedores = cantidadServicios
                        //IdReserva = idReserva
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Reservas;
        }

        public static int IngresarReserva(Clases.Reserva Reserva)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarReservaServicio", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Reserva.IdNaviera;
            scCommand.Parameters.Add("@IDPUERTODESTINO", SqlDbType.Int, 50).Value = Reserva.IdPuertoDestino;
            scCommand.Parameters.Add("@IDPUERTOORIGEN", SqlDbType.Int, 50).Value = Reserva.IdPuertoOrigen;
            scCommand.Parameters.Add("@IDSETPOINT", SqlDbType.Int, 50).Value = Reserva.IdSetpoint;
            scCommand.Parameters.Add("@IDCOMMODITY", SqlDbType.Int, 50).Value = Reserva.IdCommodity;
            scCommand.Parameters.Add("@IDFREIGHTFORWARDER", SqlDbType.Int, 50).Value = Reserva.IdFreightForwarder;
            scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 100).Value = Reserva.Booking;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 100).Value = Reserva.Viaje;
            scCommand.Parameters.Add("@CONSIGNATARIO", SqlDbType.VarChar, 100).Value = Reserva.Consignatario;
            scCommand.Parameters.Add("@CANTIDADSERVICIOS", SqlDbType.Int, 100).Value = Reserva.CantidadServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@IDNAVE", SqlDbType.Int, 100).Value = Reserva.IdNave;
            scCommand.Parameters.Add("@TEMPERATURA", SqlDbType.Float, 100).Value = Reserva.Temperatura;
            scCommand.Parameters.Add("@SERVICEPROVIDER", SqlDbType.Int, 100).Value = Reserva.IdServiceProvider;
            scCommand.Parameters.Add("@ID_DEPOSITO", SqlDbType.Int, 100).Value = Reserva.Deposito;
            scCommand.Parameters.Add("@LLEVA_MODEM", SqlDbType.Int, 100).Value = Reserva.LlevaModem;

            if (Reserva.Eta == null)
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = Reserva.Eta;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = Reserva.Eta;
            }

            if (Reserva.EtaNave == null)
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = Reserva.EtaNave;
            }

            if (Reserva.Etd == null)
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = Reserva.Etd;
            }
            if (Reserva.FechaIniStacking == null)
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = Reserva.FechaIniStacking;
            }
            if (Reserva.FechaFinStacking == null)
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = Reserva.FechaFinStacking;
            }
            scCommand.Parameters.Add("@IDEXPORTADOR", SqlDbType.Int, 100).Value = Reserva.IdExportador;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int IngresarReservaSP(Clases.Reserva Reserva)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarReservaServicioSP", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Reserva.IdNaviera;
            scCommand.Parameters.Add("@IDPUERTODESTINO", SqlDbType.Int, 50).Value = Reserva.IdPuertoDestino;
            scCommand.Parameters.Add("@IDPUERTOORIGEN", SqlDbType.Int, 50).Value = Reserva.IdPuertoOrigen;
            scCommand.Parameters.Add("@IDSETPOINT", SqlDbType.Int, 50).Value = Reserva.IdSetpoint;
            scCommand.Parameters.Add("@IDCOMMODITY", SqlDbType.Int, 50).Value = Reserva.IdCommodity;
            scCommand.Parameters.Add("@IDFREIGHTFORWARDER", SqlDbType.Int, 50).Value = Reserva.IdFreightForwarder;
            scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 100).Value = Reserva.Booking;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 100).Value = Reserva.Viaje;
            scCommand.Parameters.Add("@CONSIGNATARIO", SqlDbType.VarChar, 100).Value = Reserva.Consignatario;
            scCommand.Parameters.Add("@CANTIDADSERVICIOS", SqlDbType.Int, 100).Value = Reserva.CantidadServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@IDNAVE", SqlDbType.Int, 100).Value = Reserva.IdNave;
            scCommand.Parameters.Add("@TEMPERATURA", SqlDbType.Float, 100).Value = Reserva.Temperatura;
            scCommand.Parameters.Add("@SERVICEPROVIDER", SqlDbType.Int, 100).Value = Reserva.IdServiceProvider;

            if (Reserva.Eta == null)
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = Reserva.Eta;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = Reserva.Eta;
            }

            if (Reserva.EtaNave == null)
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = Reserva.EtaNave;
            }

            if (Reserva.Etd == null)
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = Reserva.Etd;
            }
            if (Reserva.FechaIniStacking == null)
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = Reserva.FechaIniStacking;
            }
            if (Reserva.FechaFinStacking == null)
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = Reserva.FechaFinStacking;
            }
            scCommand.Parameters.Add("@IDEXPORTADOR", SqlDbType.Int, 100).Value = Reserva.IdExportador;
            scCommand.Parameters.Add("@ESSP", SqlDbType.Int, 50).Value = 1;
            scCommand.Parameters.Add("@LLEVA_MODEM", SqlDbType.Int, 50).Value = Reserva.LlevaModem;
            scCommand.Parameters.Add("@BOOKING2", SqlDbType.VarChar, 50).Value = Reserva.Booking2;
            scCommand.Parameters.Add("@BOOKING3", SqlDbType.VarChar, 50).Value = Reserva.Booking3;
            scCommand.Parameters.Add("@BOOKING4", SqlDbType.VarChar, 50).Value = Reserva.Booking4;
            scCommand.Parameters.Add("@BOOKING5", SqlDbType.VarChar, 50).Value = Reserva.Booking5;
            scCommand.Parameters.Add("@BOOKING6", SqlDbType.VarChar, 50).Value = Reserva.Booking6;
            scCommand.Parameters.Add("@BOOKING7", SqlDbType.VarChar, 50).Value = Reserva.Booking7;
            scCommand.Parameters.Add("@BOOKING8", SqlDbType.VarChar, 50).Value = Reserva.Booking8;
            scCommand.Parameters.Add("@BOOKING9", SqlDbType.VarChar, 50).Value = Reserva.Booking9;
            scCommand.Parameters.Add("@BOOKING10", SqlDbType.VarChar, 50).Value = Reserva.Booking10;
            scCommand.Parameters.Add("@BOOKING11", SqlDbType.VarChar, 50).Value = Reserva.Booking11;
            scCommand.Parameters.Add("@BOOKING12", SqlDbType.VarChar, 50).Value = Reserva.Booking12;
            scCommand.Parameters.Add("@BOOKING13", SqlDbType.VarChar, 50).Value = Reserva.Booking13;
            scCommand.Parameters.Add("@BOOKING14", SqlDbType.VarChar, 50).Value = Reserva.Booking14;
            scCommand.Parameters.Add("@BOOKING15", SqlDbType.VarChar, 50).Value = Reserva.Booking15;
            scCommand.Parameters.Add("@SERVICIOS2", SqlDbType.Int, 50).Value = Reserva.Servicios2;
            scCommand.Parameters.Add("@SERVICIOS3", SqlDbType.Int, 50).Value = Reserva.Servicios3;
            scCommand.Parameters.Add("@SERVICIOS4", SqlDbType.Int, 50).Value = Reserva.Servicios4;
            scCommand.Parameters.Add("@SERVICIOS5", SqlDbType.Int, 50).Value = Reserva.Servicios5;
            scCommand.Parameters.Add("@SERVICIOS6", SqlDbType.Int, 50).Value = Reserva.Servicios6;
            scCommand.Parameters.Add("@SERVICIOS7", SqlDbType.Int, 50).Value = Reserva.Servicios7;
            scCommand.Parameters.Add("@SERVICIOS8", SqlDbType.Int, 50).Value = Reserva.Servicios8;
            scCommand.Parameters.Add("@SERVICIOS9", SqlDbType.Int, 50).Value = Reserva.Servicios9;
            scCommand.Parameters.Add("@SERVICIOS10", SqlDbType.Int, 50).Value = Reserva.Servicios10;
            scCommand.Parameters.Add("@SERVICIOS11", SqlDbType.Int, 50).Value = Reserva.Servicios11;
            scCommand.Parameters.Add("@SERVICIOS12", SqlDbType.Int, 50).Value = Reserva.Servicios12;
            scCommand.Parameters.Add("@SERVICIOS13", SqlDbType.Int, 50).Value = Reserva.Servicios13;
            scCommand.Parameters.Add("@SERVICIOS14", SqlDbType.Int, 50).Value = Reserva.Servicios14;
            scCommand.Parameters.Add("@SERVICIOS15", SqlDbType.Int, 50).Value = Reserva.Servicios15;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int IngresarReservasEDI(ReservaEDI Reserva)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarReservaServicio", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            //scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Reserva.IdNaviera;
            //scCommand.Parameters.Add("@IDPUERTODESTINO", SqlDbType.Int, 50).Value = Reserva.IdPuertoDestino;
            //scCommand.Parameters.Add("@IDPUERTOORIGEN", SqlDbType.Int, 50).Value = Reserva.IdPuertoOrigen;
            //scCommand.Parameters.Add("@IDSETPOINT", SqlDbType.Int, 50).Value = Reserva.IdSetpoint;
            //scCommand.Parameters.Add("@IDCOMMODITY", SqlDbType.Int, 50).Value = Reserva.IdCommodity;
            //scCommand.Parameters.Add("@IDFREIGHTFORWARDER", SqlDbType.Int, 50).Value = Reserva.IdFreightForwarder;
            //scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 100).Value = Reserva.Booking;
            //scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 100).Value = Reserva.Viaje;
            //scCommand.Parameters.Add("@CONSIGNATARIO", SqlDbType.VarChar, 100).Value = Reserva.Consignatario;
            //scCommand.Parameters.Add("@CANTIDADSERVICIOS", SqlDbType.Int, 100).Value = Reserva.CantidadServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@ETA", SqlDbType.DateTime, 100).Value = Reserva.Eta;
            scCommand.Parameters.Add("@IDNAVE", SqlDbType.Int, 100).Value = Reserva.IdNave;
            scCommand.Parameters.Add("@TEMPERATURA", SqlDbType.Float, 100).Value = Reserva.Temperatura;

            if (Reserva.Etd == null)
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = Reserva.Etd;
            }
            if (Reserva.FechaIniStacking == null)
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = Reserva.FechaIniStacking;
            }
            if (Reserva.FechaFinStacking == null)
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = Reserva.FechaFinStacking;
            }
            //scCommand.Parameters.Add("@IDEXPORTADOR", SqlDbType.Int, 100).Value = Reserva.IdExportador;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }
        public static int IngresarReservaMasiva(Clases.Reserva Reserva)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarReservaServicioMasiva", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NAVIERA", SqlDbType.VarChar, 50).Value = Reserva.Naviera;
            scCommand.Parameters.Add("@PUERTODESTINO", SqlDbType.VarChar, 50).Value = Reserva.PuertoDestino;
            scCommand.Parameters.Add("@PUERTOORIGEN", SqlDbType.VarChar, 50).Value = Reserva.PuertoOrigen;
            scCommand.Parameters.Add("@CO2", SqlDbType.Float, 50).Value = Reserva.CO2Setpoint;
            scCommand.Parameters.Add("@O2", SqlDbType.Float, 50).Value = Reserva.O2Setpoint;
            scCommand.Parameters.Add("@COMMODITY", SqlDbType.VarChar, 50).Value = Reserva.Commodity;
            scCommand.Parameters.Add("@FREIGHTFORWARDER", SqlDbType.VarChar, 50).Value = Reserva.FreightForwarder;
            scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 100).Value = Reserva.Booking;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 100).Value = Reserva.Viaje;
            scCommand.Parameters.Add("@CONSIGNATARIO", SqlDbType.VarChar, 100).Value = Reserva.Consignatario;
            scCommand.Parameters.Add("@CANTIDADSERVICIOS", SqlDbType.Int, 100).Value = Reserva.CantidadServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@ETA", SqlDbType.DateTime, 100).Value = Reserva.Eta;
            scCommand.Parameters.Add("@NAVE", SqlDbType.VarChar, 50).Value = Reserva.Nave;
            scCommand.Parameters.Add("@TEMPERATURA", SqlDbType.Float, 100).Value = Reserva.Temperatura;

            if (Reserva.Etd == null)
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = Reserva.Etd;
            }
            if (Reserva.FechaIniStacking == null)
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = Reserva.FechaIniStacking;
            }
            if (Reserva.FechaFinStacking == null)
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = Reserva.FechaFinStacking;
            }
            scCommand.Parameters.Add("@EXPORTADOR", SqlDbType.VarChar, 100).Value = Reserva.Exportador;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }
        public static List<Clases.Reserva> GetReservaById(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.Reserva> Reservas = new List<Clases.Reserva>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarReservaById " + IdReserva, cnn);
                //command.Parameters.AddWithValue("@ID_RESERVA", IdReserva);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    Reservas.Add(new Clases.Reserva
                    {
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        CantidadServicios = Convert.ToInt32(reader["CANTIDADSERVICIOS"]),
                        Eta = Convert.ToDateTime(reader["ETA"]),
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        IdExportador = Convert.ToInt32(reader["ID_EXPORTADOR"]),
                        IdNave = Convert.ToInt32(reader["ID_NAVE"]),
                        IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]),
                        IdPuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"]),
                        IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdSetpoint = Convert.ToInt32(reader["ID_SETPOINT"]),
                        IdCommodity = Convert.ToInt32(reader["ID_COMMODITY"]),
                        IdFreightForwarder = Convert.ToInt32(reader["ID_FREIGHTFORWARDER"]),
                        IdPaisDestino = Convert.ToInt32(reader["ID_PAIS_DESTINO"]),
                        IdCiudadDestino = Convert.ToInt32(reader["ID_CIUDAD_DESTINO"]),
                        IdPaisOrigen = Convert.ToInt32(reader["ID_PAIS_ORIGEN"]),
                        IdCiudadOrigen = Convert.ToInt32(reader["ID_CIUDAD_ORIGEN"]),
                        IdPaisExportador = Convert.ToInt32(reader["ID_PAIS_EXPORTADOR"]),
                        Temperatura = float.Parse(reader["TEMPERATURA"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Reservas;
        }
        public static List<Clases.Servicio> GetServicioById(int IdServicio)
        {
            SqlConnection cnn;
            List<Clases.Servicio> Servicio = new List<Clases.Servicio>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServicioById @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? FechaCortina = null;
                    DateTime? FechaInstalacion = null;
                    DateTime? FechaGasificacion = null;

                    if (reader["FECHA_CORTINA"] != DBNull.Value)
                    {
                        FechaCortina = Convert.ToDateTime(reader["FECHA_CORTINA"]);
                    }

                    if (reader["FECHA_INSTALACION_CONTROLADOR"] != DBNull.Value)
                    {
                        FechaInstalacion = Convert.ToDateTime(reader["FECHA_INSTALACION_CONTROLADOR"]);
                    }

                    if (reader["FECHA_GASIFICACION"] != DBNull.Value)
                    {
                        FechaGasificacion = Convert.ToDateTime(reader["FECHA_GASIFICACION"]);
                    }

                    Servicio.Add(new Clases.Servicio
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        PuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        PuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"]),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        Eta = Convert.ToDateTime(reader["ETA"]),
                        IdNave1 = Convert.ToInt32(reader["NAVE1"]),
                        IdNave2 = Convert.ToInt32(reader["NAVE2"]),
                        IdNave3 = Convert.ToInt32(reader["NAVE3"]),
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        InstaladorCortina = Convert.ToInt32(reader["INSTALADOR_CORTINA"]),
                        IdTipoLugarCortina = Convert.ToInt32(reader["ID_TIPO_LUGAR_CORTINA"]),
                        IdPaisCortina = Convert.ToInt32(reader["ID_PAIS_CORTINA"]),
                        IdCiudadCortina = Convert.ToInt32(reader["ID_CIUDAD_CORTINA"]),
                        IdLugarCortina = Convert.ToInt32(reader["LUGAR_CORTINA"]),
                        IdTipoLugarControlador = Convert.ToInt32(reader["ID_TIPO_LUGAR_CONTROLADOR"]),
                        IdPaisControlador = Convert.ToInt32(reader["ID_PAIS_CONTROLADOR"]),
                        IdCiudadControlador = Convert.ToInt32(reader["ID_CIUDAD_CONTROLADOR"]),
                        IdLugarInstControlador = Convert.ToInt32(reader["LUGAR_CONTROLADOR"]),
                        IdTipoLugarGasificacion = Convert.ToInt32(reader["ID_TIPO_LUGAR_GASIFICACION"]),
                        IdPaisGasificacion = Convert.ToInt32(reader["ID_PAIS_GASIFICACION"]),
                        IdCiudadGasificacion = Convert.ToInt32(reader["ID_CIUDAD_GASIFICACION"]),
                        IdLugarGasificacion = Convert.ToInt32(reader["LUGAR_GASIFICACION"]),
                        IdPaisSPControlador = Convert.ToInt32(reader["ID_PAIS_SP_CONTROLADOR"]),
                        IdSPControlador = Convert.ToInt32(reader["ID_SERVICEPROVIDER_CONTROLADOR"]),
                        IdTecnicoInstalador = Convert.ToInt32(reader["ID_TECNICO_CONTROLADOR"]),
                        IdPaisSPGasificacion = Convert.ToInt32(reader["ID_PAIS_SP_GASIFICACION"]),
                        IdSPGasificacion = Convert.ToInt32(reader["ID_SERVICEPROVIDER_GASIFICACION"]),
                        IdTecnicoGasificador = Convert.ToInt32(reader["ID_TECNICO_GASIFICADOR"]),
                        CantidadPurafil = Convert.ToInt32(reader["CANTIDAD_PURAFIL"]),
                        IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        Controlador = reader["NUMCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        HoraLlegada = reader["HORA_LLEGADA"].ToString(),
                        HoraSalida = reader["HORA_SALIDA"].ToString(),
                        TemperaturaContenedor = float.Parse(reader["TEMPERATURA_CONTENEDOR"].ToString()),
                        FolioServiceReport = Convert.ToInt32(reader["SERVICEREPORT"]),
                        PrecintoSecurity = Convert.ToInt32(reader["PRECINTO_SECURITY"]),
                        Candado = Convert.ToInt32(reader["CANDADO_CONTENEDOR"]),
                        CO2 = Convert.ToInt32(reader["CO2_INYECTADO"]),
                        N2 = Convert.ToInt32(reader["N2_INYECTADO"]),
                        IdTratamientoCO2 = Convert.ToInt32(reader["ID_TRATAMIENTO_CO2"]),
                        Scrubber = Convert.ToInt32(reader["CANTIDAD_FILTROS"]),
                        Cal = Convert.ToInt32(reader["CANTIDAD_CAL"]),
                        Habilitado = Convert.ToInt32(reader["HABILITADO"]),
                        NotaServicio = reader["NOTA_SERVICIO"].ToString(),
                        NotaLogistica = reader["NOTA_LOGISTICA"].ToString(),
                        Selloperno1 = reader["SELLOPERNO1"].ToString(),
                        Selloperno2 = reader["SELLOPERNO2"].ToString(),
                        Sellotapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        FechaCortina = FechaCortina,
                        FechaGasificacion = FechaGasificacion,
                        FechaInstControlador = FechaInstalacion
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Servicio;
        }
        public static List<Clases.Servicio> GetLugaresServicioById(int IdServicio)
        {
            SqlConnection cnn;
            List<Clases.Servicio> Servicio = new List<Clases.Servicio>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarLugaresServicioById @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Servicio.Add(new Clases.Servicio
                    {
                        IdTipoLugarCortina = Convert.ToInt32(reader["ID_TIPO_LUGAR_CORTINA"]),
                        IdLugarCortina = Convert.ToInt32(reader["LUGAR_CORTINA"]),
                        IdTipoLugarControlador = Convert.ToInt32(reader["ID_TIPO_LUGAR_CONTROLADOR"]),
                        IdLugarInstControlador = Convert.ToInt32(reader["LUGAR_CONTROLADOR"]),
                        IdTipoLugarGasificacion = Convert.ToInt32(reader["ID_TIPO_LUGAR_GASIFICACION"]),
                        IdLugarGasificacion = Convert.ToInt32(reader["LUGAR_GASIFICACION"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Servicio;
        }
        public static int GetEstadoReserva(int IdReserva)
        {
            SqlConnection cnn;
            int Estado = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ESTADO FROM AplicacionServicio_ReservaServicio WHERE ID_RESERVA=@ID_RESERVA", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", IdReserva);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estado = Convert.ToInt32(reader["ESTADO"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Estado;
        }
        public static Clases.Reserva GetReservaByIdServicio(int IdReserva)
        {
            SqlConnection cnn;
            Clases.Reserva Reservas = new Clases.Reserva();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarReservaByIdServicio @ID_RESERVA", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etd = null;
                    DateTime? etanave = null;
                    DateTime? eta = null;
                    DateTime? InicioStacking = null;
                    DateTime? FinStacking = null;

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etanave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        InicioStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        FinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["ETA"] != DBNull.Value)
                    {
                        eta = Convert.ToDateTime(reader["ETA"]);
                    }

                    Reservas.Naviera = reader["NAVIERA"].ToString();
                    Reservas.Nave = reader["NAVE"].ToString();
                    Reservas.Exportador = reader["EXPORTADOR"].ToString();
                    Reservas.FreightForwarder = reader["FREIGHTFORWARDER"].ToString();
                    Reservas.Viaje = reader["VIAJE"].ToString();
                    Reservas.Eta = eta;
                    Reservas.PuertoOrigen = reader["PUERTOORIGEN"].ToString();
                    Reservas.PuertoDestino = reader["PUERTODESTINO"].ToString();
                    Reservas.Consignatario = reader["CONSIGNATARIO"].ToString();
                    Reservas.Setpoint = reader["SETPOINT"].ToString();
                    Reservas.Commodity = reader["COMMODITY"].ToString();
                    Reservas.Etd = etd;
                    Reservas.Temperatura = float.Parse(reader["TEMPERATURA"].ToString());
                    Reservas.EtaNave = etanave;
                    Reservas.FechaIniStacking = InicioStacking;
                    Reservas.FechaFinStacking = FinStacking;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Reservas;
        }

        public static Clases.Reserva GetInfoBookingId(int IdReserva)
        {
            SqlConnection cnn;
            Clases.Reserva Reservas = new Clases.Reserva();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarReservaByIdServicio @ID_RESERVA", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etd = null;
                    DateTime? etanave = null;
                    DateTime? eta = null;
                    DateTime? InicioStacking = null;
                    DateTime? FinStacking = null;

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etanave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        InicioStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        FinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["ETA"] != DBNull.Value)
                    {
                        eta = Convert.ToDateTime(reader["ETA"]);
                    }

                    Reservas.IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]);
                    Reservas.IdNave = Convert.ToInt32(reader["ID_NAVE"]);
                    Reservas.IdExportador = Convert.ToInt32(reader["ID_EXPORTADOR"]);
                    Reservas.IdFreightForwarder = Convert.ToInt32(reader["ID_FREIGHTFORWARDER"]);
                    Reservas.Viaje = reader["VIAJE"].ToString();
                    Reservas.Eta = eta;
                    Reservas.IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]);
                    Reservas.IdPuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"]);
                    Reservas.Consignatario = reader["CONSIGNATARIO"].ToString();
                    Reservas.IdSetpoint = Convert.ToInt32(reader["ID_SETPOINT"]);
                    Reservas.IdCommodity = Convert.ToInt32(reader["ID_COMMODITY"]);
                    Reservas.Etd = etd;
                    Reservas.Temperatura = float.Parse(reader["TEMPERATURA"].ToString());
                    Reservas.EtaNave = etanave;
                    Reservas.FechaIniStacking = InicioStacking;
                    Reservas.FechaFinStacking = FinStacking;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Reservas;
        }
        public static int EditarReserva(Clases.Reserva Reserva)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EditarReservaServicio", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_RESERVA", SqlDbType.Int, 50).Value = Reserva.Id;
                command.Parameters.Add("@ID_NAVIERA", SqlDbType.Int, 50).Value = Reserva.IdNaviera;
                command.Parameters.Add("@ID_PUERTODESTINO", SqlDbType.Int, 50).Value = Reserva.IdPuertoDestino;
                command.Parameters.Add("@ID_PUERTOORIGEN", SqlDbType.Int, 50).Value = Reserva.IdPuertoOrigen;
                command.Parameters.Add("@ID_SETPOINT", SqlDbType.Int, 50).Value = Reserva.IdSetpoint;
                command.Parameters.Add("@ID_COMMODITY", SqlDbType.Int, 50).Value = Reserva.IdCommodity;
                command.Parameters.Add("@ID_FREIGHTFORWARDER", SqlDbType.Int, 50).Value = Reserva.IdFreightForwarder;
                command.Parameters.Add("@BOOKING", SqlDbType.VarChar, 100).Value = Reserva.Booking;
                command.Parameters.Add("@VIAJE", SqlDbType.VarChar, 50).Value = Reserva.Viaje;
                command.Parameters.Add("@CONSIGNATARIO", SqlDbType.VarChar, 100).Value = Reserva.Consignatario;
                command.Parameters.Add("@CANTIDADSERVICIOS", SqlDbType.Int, 50).Value = Reserva.CantidadServicios;
                command.Parameters.Add("@ETA", SqlDbType.DateTime, 50).Value = Reserva.Eta;
                command.Parameters.Add("@ID_NAVE", SqlDbType.Int, 50).Value = Reserva.IdNave;
                command.Parameters.Add("@ID_EXPORTADOR", SqlDbType.Int, 50).Value = Reserva.IdExportador;
                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
                command.Parameters.Add("@TEMPERATURA", SqlDbType.Float, 50).Value = Reserva.Temperatura;

                if (Reserva.Etd == null)
                {
                    command.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.Add("@ETD", SqlDbType.DateTime).Value = Reserva.Etd;
                }
                if (Reserva.FechaIniStacking == null)
                {
                    command.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = Reserva.FechaIniStacking;
                }
                if (Reserva.FechaFinStacking == null)
                {
                    command.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = Reserva.FechaFinStacking;
                }

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int CancelarServicio(int IdServicio, string Motivo)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Servicio1 SET ID_ESTADO_SERVICIO = 3, MOTIVO = @MOTIVO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@MOTIVO", Motivo);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int ValidarServiciosSP(int IdServicio)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Servicio1 SET VALIDADOSP=0 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int Facturar(int IdMovimiento)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Facturar", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_MOVIMIENTO", IdMovimiento);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static List<Clases.Servicio> GetServicios(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServicioByReserva @ID_RESERVA", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int Purafil = 0;
                    int Filtros = 0;
                    int Cal = 0;
                    int CO2 = 0;
                    int N2 = 0;
                    string instalador = "";
                    DateTime? FechaGasificacion = null;
                    DateTime? FechaCortina = null;
                    DateTime? eta = null;

                    if (reader["ETA"] != DBNull.Value)
                    {
                        eta = Convert.ToDateTime(reader["ETA"]);
                    }

                    if (reader["FECHA_CORTINA"] != DBNull.Value)
                    {
                        FechaCortina = Convert.ToDateTime(reader["FECHA_CORTINA"]);
                    }

                    if (reader["FECHA_GASIFICACION"] != DBNull.Value)
                    {
                        FechaGasificacion = Convert.ToDateTime(reader["FECHA_GASIFICACION"]);
                    }

                    if (reader["CANTIDAD_PURAFIL"] != DBNull.Value)
                    {
                        Purafil = Convert.ToInt32(reader["CANTIDAD_PURAFIL"]);
                    }
                    if (reader["CANTIDAD_FILTROS"] != DBNull.Value)
                    {
                        Filtros = Convert.ToInt32(reader["CANTIDAD_FILTROS"]);
                    }
                    if (reader["CANTIDAD_CAL"] != DBNull.Value)
                    {
                        Cal = Convert.ToInt32(reader["CANTIDAD_CAL"]);
                    }
                    if (reader["CO2_INYECTADO"] != DBNull.Value)
                    {
                        CO2 = Convert.ToInt32(reader["CO2_INYECTADO"]);
                    }

                    if (reader["N2_INYECTADO"] != DBNull.Value)
                    {
                        N2 = Convert.ToInt32(reader["N2_INYECTADO"]);
                    }
                    if (reader["INSTALADOR"] == DBNull.Value || Convert.ToInt32(reader["ID_SERVICIO"]) == 0)
                    {
                        instalador = "W/O DATA";
                    }
                    else if (Convert.ToInt32(reader["INSTALADOR"]) == 1)
                    {
                        instalador = "LIVENTUS";
                    }
                    else
                    {
                        instalador = "TERCEROS";
                    }



                    Servicios.Add(new Clases.Servicio
                    {
                        Id = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Booking = reader["BOOKING"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Eta = eta,
                        UltimaNave = reader["ULTIMA_NAVE"].ToString(),
                        Nave = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        UltimoControlador = reader["ULTIMO_CONTROLADOR"].ToString(),
                        EstadoServicio = reader["ESTADO_SERVICIO"].ToString(),
                        N_CortinaInstalada = reader["CORTINA_INSTALADA"].ToString(),
                        N_Gasificado = reader["GASIFICADO"].ToString(),
                        N_Validado = reader["VALIDADO"].ToString(),
                        FechaCortina = FechaCortina,
                        TipoLugarCortina = reader["TIPO_LUGAR_CORTINA"].ToString(),
                        LugarCortina = reader["LUGAR_CORTINA"].ToString(),
                        CantidadPurafil = Purafil,
                        FechaGasificacion = FechaGasificacion,
                        TecnicoGasificador = reader["TECNICO_GASIFICADOR"].ToString(),
                        TipoLugarGasificacion = reader["TIPO_LUGAR_GASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGAR_GASIFICACION"].ToString(),
                        CO2 = CO2,
                        N2 = N2,
                        TratamientoCO2 = reader["TRATAMIENTO_CO2"].ToString(),
                        Scrubber = Filtros,
                        Cal = Cal,
                        Commodity = reader["COMMODITY"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        Instalador = instalador
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }
        public static Clases.ReservayServicio GetHistoricoServicios()
        {
            SqlConnection cnn;
            Clases.ReservayServicio ReservasyServicios = new Clases.ReservayServicio();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int Purafil = 0;
                    string Filtros = "";
                    int Cal = 0;
                    int CO2 = 0;
                    int N2 = 0;
                    string gasificado = "NO GASIFICADO";
                    string cortina = "CORTINA NO INSTALADA";
                    string Nave = "";
                    DateTime? FechaGasificacion = null;
                    DateTime? FechaCortina = null;
                    DateTime? etd = null;
                    DateTime? eta = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        eta = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        FechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                        cortina = "CORTINA INSTALADA";
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        FechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                        gasificado = "GASIFICADO";
                    }

                    if (reader["PURAFILCORTINA"] != DBNull.Value)
                    {
                        Purafil = Convert.ToInt32(reader["PURAFILCORTINA"]);
                    }
                    if (reader["FILTROSCRUBBER"] != DBNull.Value)
                    {
                        Filtros = reader["FILTROSCRUBBER"].ToString();
                    }
                    if (reader["CANTIDADCALSCRUBBER"] != DBNull.Value)
                    {
                        Cal = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]);
                    }
                    if (reader["CO2GASIFICACION"] != DBNull.Value)
                    {
                        CO2 = Convert.ToInt32(reader["CO2GASIFICACION"]);
                    }

                    if (reader["N2GASIFICACION"] != DBNull.Value)
                    {
                        N2 = Convert.ToInt32(reader["N2GASIFICACION"]);
                    }

                    if (reader["NAVE3"] != DBNull.Value)
                    {
                        Nave = reader["NAVE3"].ToString();
                    }
                    else if (reader["NAVE2"] != DBNull.Value)
                    {
                        Nave = reader["NAVE2"].ToString();
                    }
                    else
                    {
                        Nave = reader["NAVE1"].ToString();
                    }

                    ReservasyServicios.Reserva.Add(new Clases.Reserva
                    {
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"].ToString()),
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Etd = etd,
                        Commodity = reader["COMMODITY"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Temperatura = float.Parse(reader["TEMPERATURA"].ToString())
                    });

                    ReservasyServicios.Servicio.Add(new Clases.Servicio
                    {
                        Id = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Eta = eta,
                        UltimaNave = Nave,
                        UltimoControlador = reader["CONTROLADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        N_CortinaInstalada = cortina,
                        N_Gasificado = gasificado,
                        N_Validado = reader["VALIDADO"].ToString(),
                        FechaCortina = FechaCortina,
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        CantidadPurafil = Purafil,
                        FechaGasificacion = FechaGasificacion,
                        TecnicoGasificador = reader["TECNICOGASIFICACION"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        CO2 = CO2,
                        N2 = N2,
                        TratamientoCO2 = reader["TRATAMIENTOCO2"].ToString(),
                        FiltrosScrubber = Filtros,
                        Cal = Cal,
                        Instalador = reader["TECNICOCORTINA"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return ReservasyServicios;
        }
        public static Clases.ReservayServicio GetTodosServicios()
        {
            SqlConnection cnn;
            Clases.ReservayServicio ReservasyServicios = new Clases.ReservayServicio();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int Purafil = 0;
                    int Filtros = 0;
                    int Cal = 0;
                    int CO2 = 0;
                    int N2 = 0;
                    float Temperatura = 0;
                    DateTime? FechaGasificacion = null;
                    DateTime? FechaCortina = null;
                    DateTime? etd = null;
                    DateTime? eta = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    string instalador = "";

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["ETA"] != DBNull.Value)
                    {
                        eta = Convert.ToDateTime(reader["ETA"]);
                    }


                    if (reader["INICIO_STACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["INICIO_STACKING"]);
                    }

                    if (reader["TERMINO_STACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["TERMINO_STACKING"]);
                    }

                    if (reader["FECHA_CORTINA"] != DBNull.Value)
                    {
                        FechaCortina = Convert.ToDateTime(reader["FECHA_CORTINA"]);
                    }

                    if (reader["FECHA_GASIFICACION"] != DBNull.Value)
                    {
                        FechaGasificacion = Convert.ToDateTime(reader["FECHA_GASIFICACION"]);
                    }

                    if (reader["CANTIDAD_PURAFIL"] != DBNull.Value)
                    {
                        Purafil = Convert.ToInt32(reader["CANTIDAD_PURAFIL"]);
                    }
                    if (reader["CANTIDAD_FILTROS"] != DBNull.Value)
                    {
                        Filtros = Convert.ToInt32(reader["CANTIDAD_FILTROS"]);
                    }
                    if (reader["CANTIDAD_CAL"] != DBNull.Value)
                    {
                        Cal = Convert.ToInt32(reader["CANTIDAD_CAL"]);
                    }
                    if (reader["CO2_INYECTADO"] != DBNull.Value)
                    {
                        CO2 = Convert.ToInt32(reader["CO2_INYECTADO"]);
                    }

                    if (reader["N2_INYECTADO"] != DBNull.Value)
                    {
                        N2 = Convert.ToInt32(reader["N2_INYECTADO"]);
                    }

                    if (reader["TEMPERATURA"] != DBNull.Value)
                    {
                        Temperatura = float.Parse(reader["TEMPERATURA"].ToString());
                    }

                    if (reader["INSTALADOR"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(reader["INSTALADOR"]) == 0)
                        {
                            instalador = "W/O DATA";
                        }
                        else if (Convert.ToInt32(reader["INSTALADOR"]) == 1)
                        {
                            instalador = "LIVENTUS";
                        }
                        else
                        {
                            instalador = "TERCEROS";
                        }
                    }

                    ReservasyServicios.Reserva.Add(new Clases.Reserva
                    {
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"].ToString()),
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Etd = etd,
                        Commodity = reader["COMMODITY"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Temperatura = Temperatura
                    });

                    ReservasyServicios.Servicio.Add(new Clases.Servicio
                    {
                        Id = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Eta = eta,
                        UltimaNave = reader["ULTIMA_NAVE"].ToString(),
                        UltimoControlador = reader["ULTIMO_CONTROLADOR"].ToString(),
                        EstadoServicio = reader["ESTADO_SERVICIO"].ToString(),
                        N_CortinaInstalada = reader["CORTINA_INSTALADA"].ToString(),
                        N_Gasificado = reader["GASIFICADO"].ToString(),
                        N_Validado = reader["VALIDADO"].ToString(),
                        FechaCortina = FechaCortina,
                        TipoLugarCortina = reader["TIPO_LUGAR_CORTINA"].ToString(),
                        LugarCortina = reader["LUGAR_CORTINA"].ToString(),
                        CantidadPurafil = Purafil,
                        FechaGasificacion = FechaGasificacion,
                        TecnicoGasificador = reader["TECNICO_GASIFICADOR"].ToString(),
                        TipoLugarGasificacion = reader["TIPO_LUGAR_GASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGAR_GASIFICACION"].ToString(),
                        CO2 = CO2,
                        N2 = N2,
                        TratamientoCO2 = reader["TRATAMIENTO_CO2"].ToString(),
                        Scrubber = Filtros,
                        Cal = Cal,
                        Instalador = instalador
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return ReservasyServicios;
        }
        public static List<Clases.DetalleControlador> GetHistoricoControladores(int IdServicio)
        {
            SqlConnection cnn;
            List<Clases.DetalleControlador> Controladores = new List<Clases.DetalleControlador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarControladoresByServicio @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? FechaInstalacion = null;
                    if (reader["FECHA_INSTALACION"] != DBNull.Value)
                    {
                        FechaInstalacion = Convert.ToDateTime(reader["FECHA_INSTALACION"]);
                    }

                    Controladores.Add(new Clases.DetalleControlador
                    {
                        Id = Convert.ToInt32(reader["ID_DETALLE_CONTROLADOR"]),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        FechaInstalacion = FechaInstalacion,
                        TipoLugar = reader["TIPO_LUGAR"].ToString(),
                        LugarControlador = reader["LUGAR_CONTROLADOR"].ToString(),
                        Tecnico = reader["TECNICO"].ToString(),
                        Bateria = reader["BATERIA"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Controladores;
        }
        public static int CrearServicio(Clases.Servicio Servicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.AgregarServicio @ID_RESERVA, @CONSIGNATARIO, @VIAJE, @ID_PUERTOORIGEN, @ID_PUERTODESTINO, @ID_NAVE1, @ID_NAVE2, @ID_NAVE3, @ID_CONTENEDOR, @ID_CONTROLADOR, @FECHA_CORTINA, @TIPO_LUGAR_CORTINA, @ID_NAVE, @LUGAR_CORTINA, @CANTIDAD_PURAFIL, @BATERIA, @HORA_LLEGADA, @HORA_SALIDA, @FECHA_INST_CONTROLADOR, @TIPO_LUGAR_CONTROLADOR, @LUGAR_CONTROLADOR, @TECNICO_CONTROLADOR, @TEMPERATURA_CONTENEDOR, @PRECINTO_SECURITY, @CANDADO_CONTENEDOR, @TIPO_LUGAR_GASIFICACION, @LUGAR_GASIFICACION, @FECHA_GASIFICACION, @TECNICO_GASIFICADOR, @CO2, @N2, @TRATAMIENTO_CO2, @CANTIDAD_FILTROS, @CANTIDAD_CAL, @ETA, @CORTINA_INSTALADA, @GASIFICADO, @SERVICEREPORT, @ID_ESTADO_SERVICIO, @ULTIMA_NAVE, @ULTIMO_CONTROLADOR, @USUARIO, @HABILITADO, @INSTALADOR, @NOTA_SERVICIO, @NOTA_LOGISTICA, @SELLOPERNO1, @SELLOPERNO2, @SELLOTAPA, @SELLOPANEL1,@SELLOPANEL2,@SELLOSECURITY,@OBSERVACIONSELLOS", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", Servicio.IdReserva);
                command.Parameters.AddWithValue("@CONSIGNATARIO", Servicio.Consignatario);
                command.Parameters.AddWithValue("@VIAJE", Servicio.Viaje);
                command.Parameters.AddWithValue("@ID_PUERTOORIGEN", Servicio.PuertoOrigen);
                command.Parameters.AddWithValue("@ID_PUERTODESTINO", Servicio.PuertoDestino);
                command.Parameters.AddWithValue("@ID_NAVE1", Servicio.IdNave1);
                command.Parameters.AddWithValue("@ID_NAVE2", Servicio.IdNave2);
                command.Parameters.AddWithValue("@ID_NAVE3", Servicio.IdNave3);
                command.Parameters.AddWithValue("@ID_CONTENEDOR", Servicio.IdContenedor);
                command.Parameters.AddWithValue("@ID_CONTROLADOR", Servicio.IdControlador);
                command.Parameters.AddWithValue("@TIPO_LUGAR_CORTINA", Servicio.IdTipoLugarCortina);
                command.Parameters.AddWithValue("@ID_NAVE", Servicio.IdNave1);
                command.Parameters.AddWithValue("@LUGAR_CORTINA", Servicio.IdLugarCortina);
                command.Parameters.AddWithValue("@CANTIDAD_PURAFIL", Servicio.CantidadPurafil);
                command.Parameters.AddWithValue("@BATERIA", Servicio.Bateria);
                command.Parameters.AddWithValue("@HORA_LLEGADA", Servicio.HoraLlegada);
                command.Parameters.AddWithValue("@HORA_SALIDA", Servicio.HoraSalida);
                command.Parameters.AddWithValue("@TIPO_LUGAR_CONTROLADOR", Servicio.IdTipoLugarControlador);
                command.Parameters.AddWithValue("@LUGAR_CONTROLADOR", Servicio.IdLugarInstControlador);
                command.Parameters.AddWithValue("@TECNICO_CONTROLADOR", Servicio.IdTecnicoInstalador);
                command.Parameters.AddWithValue("@TEMPERATURA_CONTENEDOR", Servicio.TemperaturaContenedor);
                command.Parameters.AddWithValue("@PRECINTO_SECURITY", Servicio.PrecintoSecurity);
                command.Parameters.AddWithValue("@CANDADO_CONTENEDOR", Servicio.Candado);
                command.Parameters.AddWithValue("@TIPO_LUGAR_GASIFICACION", Servicio.IdTipoLugarGasificacion);
                command.Parameters.AddWithValue("@LUGAR_GASIFICACION", Servicio.IdLugarGasificacion);
                command.Parameters.AddWithValue("@TECNICO_GASIFICADOR", Servicio.IdTecnicoGasificador);
                command.Parameters.AddWithValue("@CO2", Servicio.CO2);
                command.Parameters.AddWithValue("@N2", Servicio.N2);
                command.Parameters.AddWithValue("@TRATAMIENTO_CO2", Servicio.IdTratamientoCO2);
                command.Parameters.AddWithValue("@CANTIDAD_FILTROS", Servicio.Scrubber);
                command.Parameters.AddWithValue("@CANTIDAD_CAL", Servicio.Cal);
                command.Parameters.AddWithValue("@ETA", Servicio.Eta);
                command.Parameters.AddWithValue("@CORTINA_INSTALADA", Servicio.CortinaInstalada);
                command.Parameters.AddWithValue("@GASIFICADO", Servicio.Gasificado);
                command.Parameters.AddWithValue("@SERVICEREPORT", Servicio.FolioServiceReport);
                command.Parameters.AddWithValue("@ID_ESTADO_SERVICIO", Servicio.IdEstadoServicio);
                command.Parameters.AddWithValue("@ULTIMA_NAVE", Servicio.UltimaNave);
                command.Parameters.AddWithValue("@ULTIMO_CONTROLADOR", Servicio.UltimoControlador);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@HABILITADO", Servicio.Habilitado);
                command.Parameters.AddWithValue("@INSTALADOR", Servicio.InstaladorCortina);
                command.Parameters.AddWithValue("@NOTA_SERVICIO", Servicio.NotaServicio);
                command.Parameters.AddWithValue("@NOTA_LOGISTICA", Servicio.NotaLogistica);
                command.Parameters.AddWithValue("@SELLOPERNO1", Servicio.Selloperno1);
                command.Parameters.AddWithValue("@SELLOPERNO2", Servicio.Selloperno2);
                command.Parameters.AddWithValue("@SELLOTAPA", Servicio.Sellotapa);
                command.Parameters.AddWithValue("@SELLOPANEL1", Servicio.SelloPanel1);
                command.Parameters.AddWithValue("@SELLOPANEL2", Servicio.SelloPanel2);
                command.Parameters.AddWithValue("@SELLOSECURITY", Servicio.SelloSecurity);
                command.Parameters.AddWithValue("@OBSERVACIONSELLOS", Servicio.ObservacionSellos);

                if (Servicio.FechaInstControlador == null)
                {
                    command.Parameters.Add("@FECHA_INST_CONTROLADOR", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_INST_CONTROLADOR", Servicio.FechaInstControlador);
                }
                if (Servicio.FechaCortina == null)
                {
                    command.Parameters.Add("@FECHA_CORTINA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_CORTINA", Servicio.FechaCortina);
                }
                if (Servicio.FechaGasificacion == null)
                {
                    command.Parameters.Add("@FECHA_GASIFICACION", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_GASIFICACION", Servicio.FechaGasificacion);
                }

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static List<Clases.Servicio> CrearServicio2(Clases.Servicio Servicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.AgregarServicio2 @ID_RESERVA, @CONSIGNATARIO, @VIAJE, @ID_PUERTOORIGEN, @ID_PUERTODESTINO, @ID_NAVE1, @ID_NAVE2, @ID_NAVE3, @ID_CONTENEDOR, @ID_CONTROLADOR, @FECHA_CORTINA, @TIPO_LUGAR_CORTINA, @ID_NAVE, @LUGAR_CORTINA, @CANTIDAD_PURAFIL, @BATERIA, @HORA_LLEGADA, @HORA_SALIDA, @FECHA_INST_CONTROLADOR, @TIPO_LUGAR_CONTROLADOR, @LUGAR_CONTROLADOR, @TECNICO_CONTROLADOR, @TEMPERATURA_CONTENEDOR, @PRECINTO_SECURITY, @CANDADO_CONTENEDOR, @TIPO_LUGAR_GASIFICACION, @LUGAR_GASIFICACION, @FECHA_GASIFICACION, @TECNICO_GASIFICADOR, @CO2, @N2, @TRATAMIENTO_CO2, @CANTIDAD_FILTROS, @CANTIDAD_CAL, @ETA, @CORTINA_INSTALADA, @GASIFICADO, @SERVICEREPORT, @ID_ESTADO_SERVICIO, @ULTIMA_NAVE, @ULTIMO_CONTROLADOR, @USUARIO, @HABILITADO, @INSTALADOR, @NOTA_SERVICIO, @NOTA_LOGISTICA, @SELLOPERNO1, @SELLOPERNO2, @SELLOTAPA, @SELLOPANEL1,@SELLOPANEL2,@SELLOSECURITY,@OBSERVACIONSELLOS", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", Servicio.IdReserva);
                command.Parameters.AddWithValue("@CONSIGNATARIO", Servicio.Consignatario);
                command.Parameters.AddWithValue("@VIAJE", Servicio.Viaje);
                command.Parameters.AddWithValue("@ID_PUERTOORIGEN", Servicio.PuertoOrigen);
                command.Parameters.AddWithValue("@ID_PUERTODESTINO", Servicio.PuertoDestino);
                command.Parameters.AddWithValue("@ID_NAVE1", Servicio.IdNave1);
                command.Parameters.AddWithValue("@ID_NAVE2", Servicio.IdNave2);
                command.Parameters.AddWithValue("@ID_NAVE3", Servicio.IdNave3);
                command.Parameters.AddWithValue("@ID_CONTENEDOR", Servicio.IdContenedor);
                command.Parameters.AddWithValue("@ID_CONTROLADOR", Servicio.IdControlador);
                command.Parameters.AddWithValue("@TIPO_LUGAR_CORTINA", Servicio.IdTipoLugarCortina);
                command.Parameters.AddWithValue("@ID_NAVE", Servicio.IdNave1);
                command.Parameters.AddWithValue("@LUGAR_CORTINA", Servicio.IdLugarCortina);
                command.Parameters.AddWithValue("@CANTIDAD_PURAFIL", Servicio.CantidadPurafil);
                command.Parameters.AddWithValue("@BATERIA", Servicio.Bateria);
                command.Parameters.AddWithValue("@HORA_LLEGADA", Servicio.HoraLlegada);
                command.Parameters.AddWithValue("@HORA_SALIDA", Servicio.HoraSalida);
                command.Parameters.AddWithValue("@TIPO_LUGAR_CONTROLADOR", Servicio.IdTipoLugarControlador);
                command.Parameters.AddWithValue("@LUGAR_CONTROLADOR", Servicio.IdLugarInstControlador);
                command.Parameters.AddWithValue("@TECNICO_CONTROLADOR", Servicio.IdTecnicoInstalador);
                command.Parameters.AddWithValue("@TEMPERATURA_CONTENEDOR", Servicio.TemperaturaContenedor);
                command.Parameters.AddWithValue("@PRECINTO_SECURITY", Servicio.PrecintoSecurity);
                command.Parameters.AddWithValue("@CANDADO_CONTENEDOR", Servicio.Candado);
                command.Parameters.AddWithValue("@TIPO_LUGAR_GASIFICACION", Servicio.IdTipoLugarGasificacion);
                command.Parameters.AddWithValue("@LUGAR_GASIFICACION", Servicio.IdLugarGasificacion);
                command.Parameters.AddWithValue("@TECNICO_GASIFICADOR", Servicio.IdTecnicoGasificador);
                command.Parameters.AddWithValue("@CO2", Servicio.CO2);
                command.Parameters.AddWithValue("@N2", Servicio.N2);
                command.Parameters.AddWithValue("@TRATAMIENTO_CO2", Servicio.IdTratamientoCO2);
                command.Parameters.AddWithValue("@CANTIDAD_FILTROS", Servicio.Scrubber);
                command.Parameters.AddWithValue("@CANTIDAD_CAL", Servicio.Cal);
                command.Parameters.AddWithValue("@ETA", Servicio.Eta);
                command.Parameters.AddWithValue("@CORTINA_INSTALADA", Servicio.CortinaInstalada);
                command.Parameters.AddWithValue("@GASIFICADO", Servicio.Gasificado);
                command.Parameters.AddWithValue("@SERVICEREPORT", Servicio.FolioServiceReport);
                command.Parameters.AddWithValue("@ID_ESTADO_SERVICIO", Servicio.IdEstadoServicio);
                command.Parameters.AddWithValue("@ULTIMA_NAVE", Servicio.UltimaNave);
                command.Parameters.AddWithValue("@ULTIMO_CONTROLADOR", Servicio.UltimoControlador);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@HABILITADO", Servicio.Habilitado);
                command.Parameters.AddWithValue("@INSTALADOR", Servicio.InstaladorCortina);
                command.Parameters.AddWithValue("@NOTA_SERVICIO", Servicio.NotaServicio);
                command.Parameters.AddWithValue("@NOTA_LOGISTICA", Servicio.NotaLogistica);
                command.Parameters.AddWithValue("@SELLOPERNO1", Servicio.Selloperno1);
                command.Parameters.AddWithValue("@SELLOPERNO2", Servicio.Selloperno2);
                command.Parameters.AddWithValue("@SELLOTAPA", Servicio.Sellotapa);
                command.Parameters.AddWithValue("@SELLOPANEL1", Servicio.SelloPanel1);
                command.Parameters.AddWithValue("@SELLOPANEL2", Servicio.SelloPanel2);
                command.Parameters.AddWithValue("@SELLOSECURITY", Servicio.SelloSecurity);
                command.Parameters.AddWithValue("@OBSERVACIONSELLOS", Servicio.SelloSecurity);

                if (Servicio.FechaInstControlador == null)
                {
                    command.Parameters.Add("@FECHA_INST_CONTROLADOR", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_INST_CONTROLADOR", Servicio.FechaInstControlador);
                }
                if (Servicio.FechaCortina == null)
                {
                    command.Parameters.Add("@FECHA_CORTINA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_CORTINA", Servicio.FechaCortina);
                }
                if (Servicio.FechaGasificacion == null)
                {
                    command.Parameters.Add("@FECHA_GASIFICACION", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_GASIFICACION", Servicio.FechaGasificacion);
                }

                List<Clases.Servicio> Servicio2 = new List<Clases.Servicio>();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Servicio2.Add(new Clases.Servicio
                    {
                        Id = Convert.ToInt32(reader["SERVICIO"]),
                    });
                }
                return Servicio2;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int EditarServicio(Clases.Servicio Servicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.EditarServicio @ID_SERVICIO, @CONSIGNATARIO, @VIAJE, @ID_PUERTOORIGEN, @ID_PUERTODESTINO, @ID_NAVE1, @ID_NAVE2, @ID_NAVE3, @ID_CONTENEDOR, @ID_CONTROLADOR, @FECHA_CORTINA, @TIPO_LUGAR_CORTINA, @LUGAR_CORTINA, @CANTIDAD_PURAFIL, @BATERIA, @HORA_LLEGADA, @HORA_SALIDA, @FECHA_INST_CONTROLADOR, @TIPO_LUGAR_CONTROLADOR, @LUGAR_CONTROLADOR, @TECNICO_INSTALADOR, @TEMPERATURA_CONTENEDOR, @PRECINTO_SECURITY, @CANDADO_CONTENEDOR, @TIPO_LUGAR_GASIFICACION, @LUGAR_GASIFICACION, @FECHA_GASIFICACION, @TECNICO_GASIFICADOR, @CO2, @N2, @TRATAMIENTO_CO2, @CANTIDAD_FILTROS, @CANTIDAD_CAL, @ETA, @CORTINA_INSTALADA, @GASIFICADO, @SERVICEREPORT, @USUARIO, @HABILITADO, @INSTALADOR, @NOTA_SERVICIO, @NOTA_LOGISTICA, @SELLOPERNO1, @SELLOPERNO2, @SELLOTAPA, @SELLOPANEL1, @SELLOPANEL2, @SELLOSECURITY, @OBSERVACIONSELLOS, @BOOKING", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", Servicio.Id);
                command.Parameters.AddWithValue("@CONSIGNATARIO", Servicio.Consignatario);
                command.Parameters.AddWithValue("@VIAJE", Servicio.Viaje);
                command.Parameters.AddWithValue("@ID_PUERTOORIGEN", Servicio.PuertoOrigen);
                command.Parameters.AddWithValue("@ID_PUERTODESTINO", Servicio.PuertoDestino);
                command.Parameters.AddWithValue("@ID_NAVE1", Servicio.IdNave1);
                command.Parameters.AddWithValue("@ID_NAVE2", Servicio.IdNave2);
                command.Parameters.AddWithValue("@ID_NAVE3", Servicio.IdNave3);
                command.Parameters.AddWithValue("@ID_CONTENEDOR", Servicio.IdContenedor);
                command.Parameters.AddWithValue("@ID_CONTROLADOR", Servicio.IdControlador);
                command.Parameters.AddWithValue("@TIPO_LUGAR_CORTINA", Servicio.IdTipoLugarCortina);
                command.Parameters.AddWithValue("@LUGAR_CORTINA", Servicio.IdLugarCortina);
                command.Parameters.AddWithValue("@CANTIDAD_PURAFIL", Servicio.CantidadPurafil);
                command.Parameters.AddWithValue("@BATERIA", Servicio.Bateria);
                command.Parameters.AddWithValue("@HORA_LLEGADA", Servicio.HoraLlegada);
                command.Parameters.AddWithValue("@HORA_SALIDA", Servicio.HoraSalida);
                command.Parameters.AddWithValue("@TIPO_LUGAR_CONTROLADOR", Servicio.IdTipoLugarControlador);
                command.Parameters.AddWithValue("@LUGAR_CONTROLADOR", Servicio.IdLugarInstControlador);
                command.Parameters.AddWithValue("@TECNICO_INSTALADOR", Servicio.IdTecnicoInstalador);
                command.Parameters.AddWithValue("@TEMPERATURA_CONTENEDOR", Servicio.TemperaturaContenedor);
                command.Parameters.AddWithValue("@PRECINTO_SECURITY", Servicio.PrecintoSecurity);
                command.Parameters.AddWithValue("@CANDADO_CONTENEDOR", Servicio.Candado);
                command.Parameters.AddWithValue("@TIPO_LUGAR_GASIFICACION", Servicio.IdTipoLugarGasificacion);
                command.Parameters.AddWithValue("@LUGAR_GASIFICACION", Servicio.IdLugarGasificacion);
                command.Parameters.AddWithValue("@TECNICO_GASIFICADOR", Servicio.IdTecnicoGasificador);
                command.Parameters.AddWithValue("@CO2", Servicio.CO2);
                command.Parameters.AddWithValue("@N2", Servicio.N2);
                command.Parameters.AddWithValue("@TRATAMIENTO_CO2", Servicio.IdTratamientoCO2);
                command.Parameters.AddWithValue("@CANTIDAD_FILTROS", Servicio.Scrubber);
                command.Parameters.AddWithValue("@CANTIDAD_CAL", Servicio.Cal);
                command.Parameters.AddWithValue("@ETA", Servicio.Eta);
                command.Parameters.AddWithValue("@CORTINA_INSTALADA", Servicio.CortinaInstalada);
                command.Parameters.AddWithValue("@GASIFICADO", Servicio.Gasificado);
                command.Parameters.AddWithValue("@SERVICEREPORT", Servicio.FolioServiceReport);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@HABILITADO", Servicio.Habilitado);
                command.Parameters.AddWithValue("@INSTALADOR", Servicio.InstaladorCortina);
                command.Parameters.AddWithValue("@NOTA_SERVICIO", Servicio.NotaServicio);
                command.Parameters.AddWithValue("@NOTA_LOGISTICA", Servicio.NotaLogistica);
                command.Parameters.AddWithValue("@SELLOPERNO1", Servicio.Selloperno1);
                command.Parameters.AddWithValue("@SELLOPERNO2", Servicio.Selloperno2);
                command.Parameters.AddWithValue("@SELLOTAPA", Servicio.Sellotapa);
                command.Parameters.AddWithValue("@SELLOPANEL1", Servicio.SelloPanel1);
                command.Parameters.AddWithValue("@SELLOPANEL2", Servicio.SelloPanel2);
                command.Parameters.AddWithValue("@SELLOSECURITY", Servicio.SelloSecurity);
                command.Parameters.AddWithValue("@OBSERVACIONSELLOS", Servicio.ObservacionSellos);
                command.Parameters.AddWithValue("@BOOKING", Servicio.Booking);

                if (Servicio.FechaInstControlador == null)
                {
                    command.Parameters.Add("@FECHA_INST_CONTROLADOR", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_INST_CONTROLADOR", Servicio.FechaInstControlador);
                }
                if (Servicio.FechaCortina == null)
                {
                    command.Parameters.Add("@FECHA_CORTINA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_CORTINA", Servicio.FechaCortina);
                }
                if (Servicio.FechaGasificacion == null)
                {
                    command.Parameters.Add("@FECHA_GASIFICACION", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.AddWithValue("@FECHA_GASIFICACION", Servicio.FechaGasificacion);
                }

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static Clases.CantidadReservas DetalleReservaServicio(int IdReserva)
        {
            SqlConnection cnn;
            Clases.CantidadReservas Detalle = new Clases.CantidadReservas();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarCantidadesReserva @ID_RESERVA", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Detalle.TotalServicios = Convert.ToInt32(reader["TOTALSERVICIOS"]);
                    Detalle.Booking = reader["BOOKING"].ToString();
                    Detalle.Reservados = Convert.ToInt32(reader["RESERVADOS"]);
                    Detalle.EnViaje = Convert.ToInt32(reader["ENVIAJE"]);
                    Detalle.Cancelados = Convert.ToInt32(reader["CANCELADOS"]);
                    Detalle.NoRecuperados = Convert.ToInt32(reader["NORECUPERADO"]);
                    Detalle.Recuperados = Convert.ToInt32(reader["RECUPERADOS"]);
                    Detalle.EnLab = Convert.ToInt32(reader["LAB"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Detalle;
        }
        public static int CancelarReserva(int IdReserva, string Motivo)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_RESERVASERVICIO SET ESTADO = 1, MOTIVO = @MOTIVO WHERE ID_RESERVA = @IDRESERVA", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                command.Parameters.AddWithValue("@MOTIVO", Motivo);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int ValidarNumServicios(int IdReserva)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int ServicioValido = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("ValidarNumServicios", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_RESERVA", SqlDbType.Int, 50).Value = IdReserva;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ServicioValido = Convert.ToInt32(reader["SERVICIO_VALIDO"]);
                }

                return ServicioValido;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static List<Clases.Reserva> FiltroPorFechas(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            SqlConnection cnn;
            List<Clases.Reserva> Reservas = new List<Clases.Reserva>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                if (Tipo == 1)
                {
                    command = new SqlCommand("ConsultarReservasFechaRegistro", cnn);
                }
                else if (Tipo == 2)
                {
                    command = new SqlCommand("ConsultarReservasFechaIniStacking", cnn);
                }
                else if (Tipo == 3)
                {
                    command = new SqlCommand("ConsultarReservasFechaFinStacking", cnn);
                }
                else if (Tipo == 4)
                {
                    command = new SqlCommand("ConsultarReservasEtd", cnn);
                }
                else if (Tipo == 5)
                {
                    command = new SqlCommand("ConsultarReservasEta", cnn);
                }

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FECHAINI", SqlDbType.DateTime, 50).Value = FechaIni;
                command.Parameters.Add("@FECHAFIN", SqlDbType.DateTime, 50).Value = FechaFin;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    Reservas.Add(new Clases.Reserva
                    {
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        CantidadServicios = Convert.ToInt32(reader["CANTIDADSERVICIOS"]),
                        Eta = Convert.ToDateTime(reader["ETA"]),
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Exportador = reader["NOMBREXPORTADOR"].ToString(),
                        Nave = reader["NOMBRENAVE"].ToString(),
                        Naviera = reader["NOMBRENAVIERA"].ToString(),
                        PuertoDestino = reader["NOMBREPUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["NOMBREPUERTOORIGEN"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Commodity = reader["NOMBRECOMMODITY"].ToString(),
                        CommodityTecnica = reader["NOMBREPLATAFORMATECNICA"].ToString(),
                        FreightForwarder = reader["NOMBREFREIGHTFORWARDER"].ToString(),
                        Estado = Convert.ToInt32(reader["ESTADO"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Reservas;
        }
        public static List<Clases.Servicio> FiltroPorFechasServicio(int IdReserva, int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            SqlConnection cnn;
            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                if (Tipo == 1)
                {
                    command = new SqlCommand("ConsultarServiciosFechaCortina", cnn);
                }
                if (Tipo == 2)
                {
                    command = new SqlCommand("ConsultarServiciosFechaGasificacion", cnn);
                }

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_RESERVA", SqlDbType.Int, 50).Value = IdReserva;
                command.Parameters.Add("@FECHAINI", SqlDbType.DateTime, 50).Value = FechaIni;
                command.Parameters.Add("@FECHAFIN", SqlDbType.DateTime, 50).Value = FechaFin;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int Purafil = 0;
                    int Filtros = 0;
                    int Cal = 0;
                    int CO2 = 0;
                    int N2 = 0;
                    string instalador = "";
                    DateTime? FechaCortina = null;
                    DateTime? FechaInstalacion = null;
                    DateTime? FechaGasificacion = null;

                    if (reader["FECHA_CORTINA"] != DBNull.Value)
                    {
                        FechaCortina = Convert.ToDateTime(reader["FECHA_CORTINA"]);
                    }

                    if (reader["FECHA_GASIFICACION"] != DBNull.Value)
                    {
                        FechaGasificacion = Convert.ToDateTime(reader["FECHA_GASIFICACION"]);
                    }

                    if (reader["CANTIDAD_PURAFIL"] != DBNull.Value)
                    {
                        Purafil = Convert.ToInt32(reader["CANTIDAD_PURAFIL"]);
                    }
                    if (reader["CANTIDAD_FILTROS"] != DBNull.Value)
                    {
                        Filtros = Convert.ToInt32(reader["CANTIDAD_FILTROS"]);
                    }
                    if (reader["CANTIDAD_CAL"] != DBNull.Value)
                    {
                        Cal = Convert.ToInt32(reader["CANTIDAD_CAL"]);
                    }
                    if (reader["CO2_INYECTADO"] != DBNull.Value)
                    {
                        CO2 = Convert.ToInt32(reader["CO2_INYECTADO"]);
                    }

                    if (reader["N2_INYECTADO"] != DBNull.Value)
                    {
                        N2 = Convert.ToInt32(reader["N2_INYECTADO"]);
                    }

                    if (Convert.ToInt32(reader["INSTALADOR"]) == 0 || reader["INSTALADOR"] == DBNull.Value)
                    {
                        instalador = "W/O DATA";
                    }
                    else if (Convert.ToInt32(reader["INSTALADOR"]) == 1)
                    {
                        instalador = "LIVENTUS";
                    }
                    else
                    {
                        instalador = "TERCEROS";
                    }

                    Servicios.Add(new Clases.Servicio
                    {
                        Id = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Booking = reader["BOOKING"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Eta = Convert.ToDateTime(reader["ETA"].ToString()),
                        UltimaNave = reader["ULTIMA_NAVE"].ToString(),
                        Nave = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        UltimoControlador = reader["ULTIMO_CONTROLADOR"].ToString(),
                        EstadoServicio = reader["ESTADO_SERVICIO"].ToString(),
                        N_CortinaInstalada = reader["CORTINA_INSTALADA"].ToString(),
                        N_Gasificado = reader["GASIFICADO"].ToString(),
                        N_Validado = reader["VALIDADO"].ToString(),
                        FechaCortina = FechaCortina,
                        TipoLugarCortina = reader["TIPO_LUGAR_CORTINA"].ToString(),
                        LugarCortina = reader["LUGAR_CORTINA"].ToString(),
                        CantidadPurafil = Purafil,
                        FechaGasificacion = FechaGasificacion,
                        TecnicoGasificador = reader["TECNICO_GASIFICADOR"].ToString(),
                        TipoLugarGasificacion = reader["TIPO_LUGAR_GASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGAR_GASIFICACION"].ToString(),
                        CO2 = CO2,
                        N2 = N2,
                        TratamientoCO2 = reader["TRATAMIENTO_CO2"].ToString(),
                        Scrubber = Filtros,
                        Cal = Cal,
                        Commodity = reader["COMMODITY"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        Instalador = instalador

                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Servicios;
        }
        public static Clases.ReservayServicio FiltroPorFechasServicioHistorico(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            SqlConnection cnn;
            Clases.ReservayServicio ReservasyServicios = new Clases.ReservayServicio();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                switch (Tipo)
                {
                    case 1:
                        command = new SqlCommand("ConsultarHistoricoServiciosFechaRegistro", cnn);
                        break;
                    case 2:
                        command = new SqlCommand("ConsultarHistoricoServiciosFechaIniStacking", cnn);
                        break;
                    case 3:
                        command = new SqlCommand("ConsultarHistoricoServiciosFechaFinStacking", cnn);
                        break;
                    case 4:
                        command = new SqlCommand("ConsultarHistoricoServiciosEtd", cnn);
                        break;
                    case 5:
                        command = new SqlCommand("ConsultarHistoricoServiciosEta", cnn);
                        break;
                    case 6:
                        command = new SqlCommand("ConsultarHistoricoServiciosFechaCortina", cnn);
                        break;
                    case 7:
                        command = new SqlCommand("ConsultarHistoricoServiciosFechaGasificacion", cnn);
                        break;
                }

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FECHAINI", SqlDbType.DateTime, 50).Value = FechaIni;
                command.Parameters.Add("@FECHAFIN", SqlDbType.DateTime, 50).Value = FechaFin;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int Purafil = 0;
                    int Filtros = 0;
                    int Cal = 0;
                    int CO2 = 0;
                    int N2 = 0;
                    string instalador = "";
                    DateTime? FechaGasificacion = null;
                    DateTime? FechaCortina = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["INICIO_STACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["INICIO_STACKING"]);
                    }

                    if (reader["TERMINO_STACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["TERMINO_STACKING"]);
                    }

                    if (reader["FECHA_CORTINA"] != DBNull.Value)
                    {
                        FechaCortina = Convert.ToDateTime(reader["FECHA_CORTINA"]);
                    }

                    if (reader["FECHA_GASIFICACION"] != DBNull.Value)
                    {
                        FechaGasificacion = Convert.ToDateTime(reader["FECHA_GASIFICACION"]);
                    }

                    if (reader["CANTIDAD_PURAFIL"] != DBNull.Value)
                    {
                        Purafil = Convert.ToInt32(reader["CANTIDAD_PURAFIL"]);
                    }
                    if (reader["CANTIDAD_FILTROS"] != DBNull.Value)
                    {
                        Filtros = Convert.ToInt32(reader["CANTIDAD_FILTROS"]);
                    }
                    if (reader["CANTIDAD_CAL"] != DBNull.Value)
                    {
                        Cal = Convert.ToInt32(reader["CANTIDAD_CAL"]);
                    }
                    if (reader["CO2_INYECTADO"] != DBNull.Value)
                    {
                        CO2 = Convert.ToInt32(reader["CO2_INYECTADO"]);
                    }

                    if (reader["N2_INYECTADO"] != DBNull.Value)
                    {
                        N2 = Convert.ToInt32(reader["N2_INYECTADO"]);
                    }
                    if (Convert.ToInt32(reader["INSTALADOR"]) == 0)
                    {
                        instalador = "W/O DATA";
                    }
                    else if (Convert.ToInt32(reader["INSTALADOR"]) == 1)
                    {
                        instalador = "LIVENTUS";
                    }
                    else
                    {
                        instalador = "TERCEROS";
                    }

                    ReservasyServicios.Reserva.Add(new Clases.Reserva
                    {
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"].ToString()),
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Etd = etd,
                        Commodity = reader["COMMODITY"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Temperatura = float.Parse(reader["TEMPERATURA"].ToString())
                    });

                    ReservasyServicios.Servicio.Add(new Clases.Servicio
                    {
                        Id = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Eta = Convert.ToDateTime(reader["ETA"].ToString()),
                        UltimaNave = reader["ULTIMA_NAVE"].ToString(),
                        UltimoControlador = reader["ULTIMO_CONTROLADOR"].ToString(),
                        EstadoServicio = reader["ESTADO_SERVICIO"].ToString(),
                        N_CortinaInstalada = reader["CORTINA_INSTALADA"].ToString(),
                        N_Gasificado = reader["GASIFICADO"].ToString(),
                        N_Validado = reader["VALIDADO"].ToString(),
                        FechaCortina = FechaCortina,
                        TipoLugarCortina = reader["TIPO_LUGAR_CORTINA"].ToString(),
                        LugarCortina = reader["LUGAR_CORTINA"].ToString(),
                        CantidadPurafil = Purafil,
                        FechaGasificacion = FechaGasificacion,
                        TecnicoGasificador = reader["TECNICO_GASIFICADOR"].ToString(),
                        TipoLugarGasificacion = reader["TIPO_LUGAR_GASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGAR_GASIFICACION"].ToString(),
                        CO2 = CO2,
                        N2 = N2,
                        TratamientoCO2 = reader["TRATAMIENTO_CO2"].ToString(),
                        Scrubber = Filtros,
                        Cal = Cal,
                        Instalador = instalador
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return ReservasyServicios;
        }
        public static List<Clases.ServicioCompleto> FiltroPorFechasServicioHistorico1(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();

            try
            {
                cnn.Open();
                switch (Tipo)
                {
                    case 1:
                        command = new SqlCommand("ConsultarHistoricoServicios1FechaRegistro", cnn);
                        break;
                    case 2:
                        command = new SqlCommand("ConsultarHistoricoServicios1FechaIniStacking", cnn);
                        break;
                    case 3:
                        command = new SqlCommand("ConsultarHistoricoServicios1FechaFinStacking", cnn);
                        break;
                    case 4:
                        command = new SqlCommand("ConsultarHistoricoServicios1Etd", cnn);
                        break;
                    case 5:
                        command = new SqlCommand("ConsultarHistoricoServicios1Eta", cnn);
                        break;
                    case 6:
                        command = new SqlCommand("ConsultarHistoricoServicios1FechaCortina", cnn);
                        break;
                    case 7:
                        command = new SqlCommand("ConsultarHistoricoServicios1FechaGasificacion", cnn);
                        break;
                }

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FECHAINI", SqlDbType.DateTime, 50).Value = FechaIni;
                command.Parameters.Add("@FECHAFIN", SqlDbType.DateTime, 50).Value = FechaFin;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }



                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]),
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = Convert.ToInt32(reader["HABILITADO"]),
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Servicios;
        }
        public static List<Clases.AuditoriaReserva> GetModificaciones(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.AuditoriaReserva> Auditoria = new List<Clases.AuditoriaReserva>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                command = new SqlCommand("ConsultarModificacionesReservaById", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IDRESERVA", SqlDbType.Int, 50).Value = IdReserva;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Auditoria.Add(new Clases.AuditoriaReserva
                    {
                        Id = Convert.ToInt32(reader["ID_AUDITORIA"]),
                        Operacion = reader["OPERACION"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Cantidadservicios = Convert.ToInt32(reader["CANTIDADSERVICIOS"]),
                        Eta = Convert.ToDateTime(reader["ETA"]),
                        Nave = reader["NAVE"].ToString(),
                        Etd = Convert.ToDateTime(reader["ETD"]),
                        Fechainistacking = Convert.ToDateTime(reader["FECHAINISTACKING"]),
                        Fechafinstacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        Estado = Convert.ToInt32(reader["ESTADO"])

                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Auditoria;
        }

        public static List<Clases.ServicioCompleto> GetModificacionesServicio(int IdServicio)
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> AuditoriaServicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarModificacionesServicioById @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? fechaAccion = null;
                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechaPreZarpe = null;
                    DateTime? fechaModem = null;
                    DateTime? fechaAsociacionControlador = null;
                    DateTime? fechaAsociacionModem = null;

                    if (reader["FECHA_ACCION"] != DBNull.Value)
                    {
                        fechaAccion = Convert.ToDateTime(reader["FECHA_ACCION"]);
                    }

                    if (reader["FECHA_ASOCIACION_CONTROLADOR"] != DBNull.Value)
                    {
                        fechaAsociacionControlador = Convert.ToDateTime(reader["FECHA_ASOCIACION_CONTROLADOR"]);
                    }

                    if (reader["FECHA_ASOCIACION_MODEM"] != DBNull.Value)
                    {
                        fechaAsociacionModem = Convert.ToDateTime(reader["FECHA_ASOCIACION_MODEM"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }
                    if (reader["FECHAMODEM"] != DBNull.Value)
                    {
                        fechaModem = Convert.ToDateTime(reader["FECHAMODEM"]);
                    }

                    AuditoriaServicios.Add(new Clases.ServicioCompleto
                    {
                        Accion = reader["ACCION"].ToString(),
                        FechaAccion = Convert.ToDateTime(fechaAccion),
                        UsuarioAccion = reader["USUARIO_ACCION"].ToString(),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        FreightforwarderEdi = reader["FREIGHTFORWARDER_EDI"].ToString(),
                        PaisExportador = reader["PAIS_EXPORTADOR"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        ExportadorEdi = reader["EXPORTADOR_EDI"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]),
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = Convert.ToInt32(reader["HABILITADO"]),
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        Viaje2 = reader["VIAJE2"].ToString(),
                        Viaje3 = reader["VIAJE3"].ToString(),
                        PaisCortina = reader["PAISCORTINA"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        MovContenedor = reader["MOVCONTENEDOR"].ToString(),
                        Alerta = Convert.ToInt32(reader["ALERTA"]),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = "", //reader["COLORESTADO"].ToString(),
                        GestionServicio = reader["GESTIONSERVICIO"].ToString(),
                        FechaGestion = fechaGestion,
                        Modem = reader["MODEM"].ToString(),
                        ValidadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]),
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]),
                        Semana = 0, //Convert.ToInt32(reader["SEMANA"]),
                        LlevaModem = Convert.ToInt32(reader["LLEVA_MODEM"]),
                        FechaModem = fechaModem,
                        TipoLugarModem = reader["TIPOLUGARMODEM"].ToString(),
                        LugarModem = reader["LUGARMODEM"].ToString(),
                        TecnicoModem = reader["TECNICOMODEM"].ToString(),
                        FechaAsociacionBateriaControlador = fechaAsociacionControlador,
                        FechaAsociacionBateriaModem = fechaAsociacionModem,
                        Sensor = "",
                        TipoEntrega = "",
                        NumeroOrdenCarga = 0,
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return AuditoriaServicios;
        }

        public static int ValidarBooking(string Booking)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select Count(*) as CANTIDAD from AplicacionServicio_Servicio1 where BOOKING = @BOOKING;", cnn);
                command.Parameters.AddWithValue("@BOOKING", Booking);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cantidad = Convert.ToInt32(reader["CANTIDAD"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return cantidad;
        }
        public static int ValidarNumeroBooking(string Booking)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) AS CANTIDAD FROM AplicacionServicio_Servicio1 WHERE BOOKING=@BOOKING;", cnn);
                command.Parameters.AddWithValue("@BOOKING", Booking);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cantidad = Convert.ToInt32(reader["CANTIDAD"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return cantidad;
        }
        public static int ValidarNumServiciosBooking(string IdReserva)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int ServicioValido = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("ValidarNumServiciosBooking", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@BOOKING", SqlDbType.VarChar, 50).Value = IdReserva;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ServicioValido = Convert.ToInt32(reader["SERVICIO_VALIDO"]);
                }

                return ServicioValido;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int ValidarMantenedoresMasiva(string Naviera, string Exportador, string PuertoOrigen, string PuertoDestino, string Commodity)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int MantenedoresValidos = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("ValidarMantenedoresMasiva", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NAVIERA", SqlDbType.VarChar, 50).Value = Naviera;
                command.Parameters.Add("@EXPORTADOR", SqlDbType.VarChar, 50).Value = Exportador;
                command.Parameters.Add("@PUERTOORIGEN", SqlDbType.VarChar, 50).Value = PuertoOrigen;
                command.Parameters.Add("@PUERTODESTINO", SqlDbType.VarChar, 50).Value = PuertoDestino;
                command.Parameters.Add("@COMMODITY", SqlDbType.VarChar, 50).Value = Commodity;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MantenedoresValidos = Convert.ToInt32(reader["MANTENEDORES_VALIDOS"]);
                }

                return MantenedoresValidos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int EdicionMasiva(int Servicios, string Booking = "", int Naviera = 0, int Nave = 0, int Exportador = 0, int FreightForwarder = 0, string Consignatario = "", int Commodity = 0, int Setpoint = 0,
                                     int Setpoint1 = 0, DateTime? IniStacking = null, DateTime? FinStacking = null, string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0,
                                     DateTime? Etd = null, DateTime? EtaNave = null, DateTime? EtaPuerto = null, float Temperatura = 0, int ServiceProvider = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int IDRESERVA = 0;
            try
            {
                bool cambiarEstadoFacturacion = false;

                //IDRESERVA = GetIdReservaByBooking(Booking);
                cnn.Open();
                SqlCommand command = new SqlCommand();
                //CAMBIOS DE DATOS QUE CAMBIAN PARA TODOS LOS SERVICIOS DE UN BOOKING
                if (Naviera != 0)
                {
                    command = new SqlCommand("EXEC dbo.EditarNavieraServicio @NAVIERA, @SERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@NAVIERA", Convert.ToInt32(Naviera));
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Nave != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_NAVE1 = @NAVE, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@NAVE", Nave);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (FreightForwarder != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_FREIGHTFORWARDER = @FREIGHTFORWARDER, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@FREIGHTFORWARDER", FreightForwarder);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Exportador != 0)
                {
                    command = new SqlCommand("EXEC dbo.EditarExportadorServicio @EXPORTADOR, @SERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@EXPORTADOR", Exportador);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Consignatario != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET CONSIGNATARIO = @CONSIGNATARIO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@CONSIGNATARIO", Consignatario);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Commodity != 0)
                {
                    command = new SqlCommand("EXEC dbo.AgregarCommodityServicio @ID_RESERVA, @ID_COMMODITY, @ID_SETPOINT, @SERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ID_RESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@ID_COMMODITY", Commodity);
                    command.Parameters.AddWithValue("@ID_SETPOINT", Setpoint);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Setpoint1 != 0)
                {
                    command = new SqlCommand("EXEC dbo.AgregarSetpointServicio @ID_RESERVA, @ID_SETPOINT, @SERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ID_RESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@ID_SETPOINT", Setpoint1);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (IniStacking != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAINISTACKING = @FECHA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@FECHA", IniStacking);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (FinStacking != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAFINSTACKING = @FECHA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@FECHA", FinStacking);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Viaje != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VIAJE = @VIAJE, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@VIAJE", Viaje);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (PuertoOrigen != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_PUERTOORIGEN = @PUERTOORIGEN, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@PUERTOORIGEN", PuertoOrigen);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (PuertoDestino != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_PUERTODESTINO = @PUERTODESTINO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@PUERTODESTINO", PuertoDestino);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Etd != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ETD = @ETD, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@ETD", Etd);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (EtaNave != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ETANAVE = @ETANAVE, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@ETANAVE", EtaNave);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (EtaPuerto != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ETAPUERTO = @ETAPUERTO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@ETAPUERTO", EtaPuerto);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Temperatura != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET TEMPERATURA = @TEMPERATURA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@TEMPERATURA", Temperatura);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (ServiceProvider != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET IDSERVICEPROVIDER = @IDSERVICEPROVIDER, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@IDSERVICEPROVIDER", ServiceProvider);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }

                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    if (cambiarEstadoFacturacion)
                    {
                        SqlConnection cnn2;
                        cnn2 = new SqlConnection(connectionString);
                        cnn2.Open();
                        SqlCommand command2 = new SqlCommand();
                        command2 = new SqlCommand("EXEC dbo.CambiarEstadoFacturacion @IDSERVICIO, @USUARIO", cnn2);
                        command2.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                        command2.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                        command2.ExecuteNonQuery();
                        cnn2.Close();
                    }
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }


        public static int EdicionMasivaNave(int Servicios, DateTime? IniStacking = null, DateTime? FinStacking = null, string Viaje = "", DateTime? Etd = null, DateTime? EtaNave = null)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int IDRESERVA = 0;
            try
            {

                //IDRESERVA = GetIdReservaByBooking(Booking);
                cnn.Open();
                SqlCommand command = new SqlCommand();
                //CAMBIOS DE DATOS QUE CAMBIAN PARA TODOS LOS SERVICIOS DE UN BOOKING
                if (IniStacking != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAINISTACKING = @FECHA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@FECHA", IniStacking);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (FinStacking != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAFINSTACKING = @FECHA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@FECHA", FinStacking);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Viaje != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VIAJE = @VIAJE, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@VIAJE", Viaje);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Etd != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ETD = @ETD, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@ETD", Etd);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (EtaNave != null)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ETANAVE = @ETANAVE, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @SERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDRESERVA", IDRESERVA);
                    command.Parameters.AddWithValue("@ETANAVE", EtaNave);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }

                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int EdicionMasivaViaje(int Servicios, string NumViaje = "", int IdPuertoOrigen = 0, int IdPuertoDestino = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {

                //IDRESERVA = GetIdReservaByBooking(Booking);
                cnn.Open();
                SqlCommand command = new SqlCommand();
                //CAMBIOS DE DATOS QUE CAMBIAN PARA TODOS LOS SERVICIOS DE UN BOOKING
                if (NumViaje != "")
                {
                    command = new SqlCommand("EXEC dbo.EdicionNumeroViaje @NUMVIAJE, @ID_PUERTOORIGEN, @ID_PUERTODESTINO, @SERVICIO, @USUARIO ", cnn);
                    command.Parameters.AddWithValue("@NUMVIAJE", NumViaje);
                    command.Parameters.AddWithValue("@SERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    command.Parameters.AddWithValue("@ID_PUERTOORIGEN", IdPuertoOrigen);
                    command.Parameters.AddWithValue("@ID_PUERTODESTINO", IdPuertoDestino);
                }

                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int GetIdReservaByBooking(string Booking = "")
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int IDRESERVA = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT MAX(ID_RESERVA) AS IDRESERVA FROM APLICACIONSERVICIO_RESERVASERVICIO WHERE BOOKING = @BOOKING", cnn);
                command.Parameters.AddWithValue("@BOOKING", Booking);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["IDRESERVA"] != DBNull.Value)
                    {
                        IDRESERVA = Convert.ToInt32(reader["IDRESERVA"]);
                    }
                    else
                    {
                        IDRESERVA = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return IDRESERVA;
        }

        public static List<Clases.ServicioCompleto> GetAlertasServicios(List<Clases.ServicioCompleto> Servicios)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            foreach (Clases.ServicioCompleto serv in Servicios)
            {
                try
                {
                    cnn.Open();
                    SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAlertaByServicio @ID_SERVICIO", cnn);
                    command.Parameters.AddWithValue("@ID_SERVICIO", serv.IdServicio);
                    command.CommandTimeout = 999;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        serv.DetalleAlerta = reader["DETALLE_ALERTA"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1 @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.CommandTimeout = 999;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechaPreZarpe = null;
                    DateTime? fechaModem = null;
                    DateTime? fechaAsociacionControlador = null;
                    DateTime? fechaAsociacionModem = null;


                    if (reader["FECHA_ASOCIACION_CONTROLADOR"] != DBNull.Value)
                    {
                        fechaAsociacionControlador = Convert.ToDateTime(reader["FECHA_ASOCIACION_CONTROLADOR"]);
                    }

                    if (reader["FECHA_ASOCIACION_MODEM"] != DBNull.Value)
                    {
                        fechaAsociacionModem = Convert.ToDateTime(reader["FECHA_ASOCIACION_MODEM"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }
                    if (reader["FECHAMODEM"] != DBNull.Value)
                    {
                        fechaModem = Convert.ToDateTime(reader["FECHAMODEM"]);
                    }

                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        FreightforwarderEdi = reader["FREIGHTFORWARDER_EDI"].ToString(),
                        PaisExportador = reader["PAIS_EXPORTADOR"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        ExportadorEdi = reader["EXPORTADOR_EDI"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]),
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = Convert.ToInt32(reader["HABILITADO"]),
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        Viaje2 = reader["VIAJE2"].ToString(),
                        Viaje3 = reader["VIAJE3"].ToString(),
                        PaisCortina = reader["PAISCORTINA"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        MovContenedor = reader["MOVCONTENEDOR"].ToString(),
                        Alerta = Convert.ToInt32(reader["ALERTA"]),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        GestionServicio = reader["GESTIONSERVICIO"].ToString(),
                        FechaGestion = fechaGestion,
                        Modem = reader["MODEM"].ToString(),
                        ValidadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]),
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]),
                        Semana = Convert.ToInt32(reader["SEMANA"]),
                        LlevaModem = Convert.ToInt32(reader["LLEVA_MODEM"]),
                        FechaModem = fechaModem,
                        TipoLugarModem = reader["TIPOLUGARMODEM"].ToString(),
                        LugarModem = reader["LUGARMODEM"].ToString(),
                        TecnicoModem = reader["TECNICOMODEM"].ToString(),
                        FechaAsociacionBateriaControlador = fechaAsociacionControlador,
                        FechaAsociacionBateriaModem = fechaAsociacionModem,
                        Sensor = "",
                        TipoEntrega = "",
                        NumeroOrdenCarga = 0,
                        DetalleAlerta = reader["DETALLE_ALERTA"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1Color(string color = "")
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1Color @COLOR", cnn);
                command.Parameters.AddWithValue("@COLOR", color);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaGestion = null;

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }



                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]),
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = Convert.ToInt32(reader["HABILITADO"]),
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        Viaje2 = reader["VIAJE2"].ToString(),
                        Viaje3 = reader["VIAJE3"].ToString(),
                        PaisCortina = reader["PAISCORTINA"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        MovContenedor = reader["MOVCONTENEDOR"].ToString(),
                        Alerta = Convert.ToInt32(reader["ALERTA"]),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        GestionServicio = reader["GESTIONSERVICIO"].ToString(),
                        FechaGestion = fechaGestion
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1SP()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1SP @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaPreZarpe = null;
                    DateTime? fechaAsociacionControlador = null;
                    DateTime? fechaAsociacionModem = null;


                    if (reader["FECHA_ASOCIACION_CONTROLADOR"] != DBNull.Value)
                    {
                        fechaAsociacionControlador = Convert.ToDateTime(reader["FECHA_ASOCIACION_CONTROLADOR"]);
                    }

                    if (reader["FECHA_ASOCIACION_MODEM"] != DBNull.Value)
                    {
                        fechaAsociacionModem = Convert.ToDateTime(reader["FECHA_ASOCIACION_MODEM"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }


                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        Traspasado = Convert.ToInt32(reader["TRASPASADO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        ValidadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]),
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]),
                        Semana = Convert.ToInt32(reader["SEMANA"]),
                        LlevaModem = Convert.ToInt32(reader["LLEVA_MODEM"]),
                        FechaAsociacionBateriaControlador = fechaAsociacionControlador,
                        FechaAsociacionBateriaModem = fechaAsociacionModem,
                        Sensor = ""
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1SPColor(string color = "")
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1SPColor @COLOR, @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@COLOR", color);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaPreZarpe = null;

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }



                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        Traspasado = Convert.ToInt32(reader["TRASPASADO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        ValidadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]),
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1Todos()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1Todos @USUARIO", cnn);
                command.CommandTimeout = 999;
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechaPreZarpe = null;
                    DateTime? fechaRegistro = null;
                    DateTime? fechaModem = null;
                    DateTime? fechaAsociacionControlador = null;
                    DateTime? fechaAsociacionModem = null;


                    if (reader["FECHA_ASOCIACION_CONTROLADOR"] != DBNull.Value)
                    {
                        fechaAsociacionControlador = Convert.ToDateTime(reader["FECHA_ASOCIACION_CONTROLADOR"]);
                    }

                    if (reader["FECHA_ASOCIACION_MODEM"] != DBNull.Value)
                    {
                        fechaAsociacionModem = Convert.ToDateTime(reader["FECHA_ASOCIACION_MODEM"]);
                    }

                    if (reader["FECHAREGISTRO"] != DBNull.Value)
                    {
                        fechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }

                    if (reader["FECHAMODEM"] != DBNull.Value)
                    {
                        fechaModem = Convert.ToDateTime(reader["FECHAMODEM"]);
                    }


                    // VALIDACION CAMPOS NULOS
                    int idReserva = 0;
                    string booking = "";
                    string viaje = "";
                    string consignatario = "";
                    string temperatura = "";
                    int idServicio = 0;
                    string freightforwarder = "";
                    string exportador = "";
                    string contenedor = "";
                    string controlador = "";
                    string nave1 = "";
                    int purafilCortina = 0;
                    int habilitado = 0;
                    int validado = 0;
                    int cantidadCalScrubber = 0;
                    int alerta = 0;
                    int validacionServicio = 0;
                    int validadoPreZarpe = 0;
                    int posContenedorNave = 0;
                    string depositociudad = "";
                    int semana = 0;


                    if (reader["ID_RESERVA"] != DBNull.Value) idReserva = Convert.ToInt32(reader["ID_RESERVA"]);
                    if (reader["BOOKING"] != DBNull.Value) booking = reader["BOOKING"].ToString();
                    if (reader["VIAJE"] != DBNull.Value) viaje = reader["VIAJE"].ToString();
                    if (reader["CONSIGNATARIO"] != DBNull.Value) consignatario = reader["CONSIGNATARIO"].ToString();
                    if (reader["TEMPERATURA"] != DBNull.Value) temperatura = reader["TEMPERATURA"].ToString();
                    if (reader["ID_SERVICIO"] != DBNull.Value) idServicio = Convert.ToInt32(reader["ID_SERVICIO"]);
                    if (reader["FREIGHTFORWARDER"] != DBNull.Value) freightforwarder = reader["FREIGHTFORWARDER"].ToString();
                    if (reader["EXPORTADOR"] != DBNull.Value) exportador = reader["EXPORTADOR"].ToString();
                    if (reader["CONTENEDOR"] != DBNull.Value) contenedor = reader["CONTENEDOR"].ToString();
                    if (reader["CONTROLADOR"] != DBNull.Value) controlador = reader["CONTROLADOR"].ToString();
                    if (reader["NAVE1"] != DBNull.Value) nave1 = reader["NAVE1"].ToString();

                    if (reader["PURAFILCORTINA"] != DBNull.Value) purafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]);
                    if (reader["HABILITADO"] != DBNull.Value) habilitado = Convert.ToInt32(reader["HABILITADO"]);
                    if (reader["VALIDADO"] != DBNull.Value) validado = Convert.ToInt32(reader["VALIDADO"]);
                    if (reader["CANTIDADCALSCRUBBER"] != DBNull.Value) cantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]);
                    if (reader["ALERTA"] != DBNull.Value) alerta = Convert.ToInt32(reader["ALERTA"]);
                    if (reader["VALIDACIONSERVICIO"] != DBNull.Value) validacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]);
                    if (reader["VALIDACIONPREZARPE"] != DBNull.Value) validadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]);
                    if (reader["POSCONTENEDORNAVE"] != DBNull.Value) posContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]);
                    if (reader["SEMANA"] != DBNull.Value) semana = Convert.ToInt32(reader["SEMANA"]);

                    if (reader["CIUDADDEPOSITO"] != DBNull.Value) depositociudad = reader["CIUDADDEPOSITO"].ToString();

                    if (depositociudad != "")
                    {
                        depositociudad = reader["DEPOSITO"].ToString() + " - " + reader["CIUDADDEPOSITO"].ToString();
                    }
                    else
                    {
                        depositociudad = reader["DEPOSITO"].ToString();
                    }

                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = idReserva,
                        Booking = booking,
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = viaje,
                        Consignatario = consignatario,
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = temperatura,
                        IdServicio = idServicio,
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = freightforwarder,
                        FreightforwarderEdi = reader["FREIGHTFORWARDER_EDI"].ToString(),
                        PaisExportador = reader["PAIS_EXPORTADOR"].ToString(),
                        Exportador = exportador,
                        ExportadorEdi = reader["EXPORTADOR_EDI"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = contenedor,
                        Modem = reader["MODEM"].ToString(),
                        Controlador = controlador,
                        Nave1 = nave1,
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = purafilCortina,
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = habilitado,
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        Validado = validado,
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = cantidadCalScrubber,
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        Viaje2 = reader["VIAJE2"].ToString(),
                        Viaje3 = reader["VIAJE3"].ToString(),
                        PaisCortina = reader["PAISCORTINA"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = depositociudad,
                        Checkbox = reader["CHECKBOX"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        MovContenedor = reader["MOVCONTENEDOR"].ToString(),
                        Alerta = alerta,
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        GestionServicio = reader["GESTIONSERVICIO"].ToString(),
                        FechaGestion = fechaGestion,
                        ValidadoPreZarpe = validadoPreZarpe,
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = posContenedorNave,
                        Semana = semana,
                        LlevaModem = Convert.ToInt32(reader["LLEVA_MODEM"]),
                        FechaModem = fechaModem,
                        TipoLugarModem = reader["TIPOLUGARMODEM"].ToString(),
                        LugarModem = reader["LUGARMODEM"].ToString(),
                        TecnicoModem = reader["TECNICOMODEM"].ToString(),
                        FechaAsociacionBateriaControlador = fechaAsociacionControlador,
                        FechaAsociacionBateriaModem = fechaAsociacionModem,
                        Sensor = "",
                        NumeroOrdenCarga = 0,
                        TipoEntrega = "",
                        DetalleAlerta = reader["DETALLE_ALERTA"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1TodosPorSemana(string SemanaInicio = "", string SemanaFin = "")
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);

            int semInicio = Convert.ToInt32(SemanaInicio.Substring(6, 2));
            int anoInicio = Convert.ToInt32(SemanaInicio.Substring(0, 4));
            int semFin = Convert.ToInt32(SemanaFin.Substring(6, 2));
            int anoFin = Convert.ToInt32(SemanaFin.Substring(0, 4));

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1TodosPorSemana @SEMANAINI, @ANOINI, @SEMANAFIN, @ANOFIN", cnn);
                command.CommandTimeout = 999;
                command.Parameters.AddWithValue("@SEMANAINI", semInicio);
                command.Parameters.AddWithValue("@ANOINI", anoInicio);
                command.Parameters.AddWithValue("@SEMANAFIN", semFin);
                command.Parameters.AddWithValue("@ANOFIN", anoFin);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechaPreZarpe = null;
                    DateTime? fechaRegistro = null;
                    DateTime? fechaModem = null;

                    if (reader["FECHAREGISTRO"] != DBNull.Value)
                    {
                        fechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }

                    if (reader["FECHAMODEM"] != DBNull.Value)
                    {
                        fechaModem = Convert.ToDateTime(reader["FECHAMODEM"]);
                    }


                    // VALIDACION CAMPOS NULOS
                    int idReserva = 0;
                    string booking = "";
                    string viaje = "";
                    string consignatario = "";
                    string temperatura = "";
                    int idServicio = 0;
                    string freightforwarder = "";
                    string exportador = "";
                    string contenedor = "";
                    string controlador = "";
                    string nave1 = "";
                    int purafilCortina = 0;
                    int habilitado = 0;
                    int validado = 0;
                    int cantidadCalScrubber = 0;
                    int alerta = 0;
                    int validacionServicio = 0;
                    int validadoPreZarpe = 0;
                    int posContenedorNave = 0;
                    string depositociudad = "";
                    int semana = 0;


                    if (reader["ID_RESERVA"] != DBNull.Value) idReserva = Convert.ToInt32(reader["ID_RESERVA"]);
                    if (reader["BOOKING"] != DBNull.Value) booking = reader["BOOKING"].ToString();
                    if (reader["VIAJE"] != DBNull.Value) viaje = reader["VIAJE"].ToString();
                    if (reader["CONSIGNATARIO"] != DBNull.Value) consignatario = reader["CONSIGNATARIO"].ToString();
                    if (reader["TEMPERATURA"] != DBNull.Value) temperatura = reader["TEMPERATURA"].ToString();
                    if (reader["ID_SERVICIO"] != DBNull.Value) idServicio = Convert.ToInt32(reader["ID_SERVICIO"]);
                    if (reader["FREIGHTFORWARDER"] != DBNull.Value) freightforwarder = reader["FREIGHTFORWARDER"].ToString();
                    if (reader["EXPORTADOR"] != DBNull.Value) exportador = reader["EXPORTADOR"].ToString();
                    if (reader["CONTENEDOR"] != DBNull.Value) contenedor = reader["CONTENEDOR"].ToString();
                    if (reader["CONTROLADOR"] != DBNull.Value) controlador = reader["CONTROLADOR"].ToString();
                    if (reader["NAVE1"] != DBNull.Value) nave1 = reader["NAVE1"].ToString();

                    if (reader["PURAFILCORTINA"] != DBNull.Value) purafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]);
                    if (reader["HABILITADO"] != DBNull.Value) habilitado = Convert.ToInt32(reader["HABILITADO"]);
                    if (reader["VALIDADO"] != DBNull.Value) validado = Convert.ToInt32(reader["VALIDADO"]);
                    if (reader["CANTIDADCALSCRUBBER"] != DBNull.Value) cantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]);
                    if (reader["ALERTA"] != DBNull.Value) alerta = Convert.ToInt32(reader["ALERTA"]);
                    if (reader["VALIDACIONSERVICIO"] != DBNull.Value) validacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]);
                    if (reader["VALIDACIONPREZARPE"] != DBNull.Value) validadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]);
                    if (reader["POSCONTENEDORNAVE"] != DBNull.Value) posContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]);
                    if (reader["SEMANA"] != DBNull.Value) semana = Convert.ToInt32(reader["SEMANA"]);

                    if (reader["CIUDADDEPOSITO"] != DBNull.Value) depositociudad = reader["CIUDADDEPOSITO"].ToString();

                    if (depositociudad != "")
                    {
                        depositociudad = reader["DEPOSITO"].ToString() + " - " + reader["CIUDADDEPOSITO"].ToString();
                    }
                    else
                    {
                        depositociudad = reader["DEPOSITO"].ToString();
                    }

                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = idReserva,
                        Booking = booking,
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = viaje,
                        Consignatario = consignatario,
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = temperatura,
                        IdServicio = idServicio,
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = freightforwarder,
                        FreightforwarderEdi = reader["FREIGHTFORWARDER_EDI"].ToString(),
                        PaisExportador = reader["PAIS_EXPORTADOR"].ToString(),
                        Exportador = exportador,
                        ExportadorEdi = reader["EXPORTADOR_EDI"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = contenedor,
                        Modem = reader["MODEM"].ToString(),
                        Controlador = controlador,
                        Nave1 = nave1,
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = purafilCortina,
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = habilitado,
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = validado,
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = cantidadCalScrubber,
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        Viaje2 = reader["VIAJE2"].ToString(),
                        Viaje3 = reader["VIAJE3"].ToString(),
                        PaisCortina = reader["PAISCORTINA"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = depositociudad,
                        Checkbox = reader["CHECKBOX"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        MovContenedor = reader["MOVCONTENEDOR"].ToString(),
                        Alerta = alerta,
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        GestionServicio = reader["GESTIONSERVICIO"].ToString(),
                        FechaGestion = fechaGestion,
                        ValidadoPreZarpe = validadoPreZarpe,
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = posContenedorNave,
                        Semana = semana,
                        LlevaModem = Convert.ToInt32(reader["LLEVA_MODEM"]),
                        FechaModem = fechaModem,
                        TipoLugarModem = reader["TIPOLUGARMODEM"].ToString(),
                        LugarModem = reader["LUGARMODEM"].ToString(),
                        TecnicoModem = reader["TECNICOMODEM"].ToString(),
                        DetalleAlerta = reader["DETALLE_ALERTA"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1TodosColor(string color = "")
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1TodosColor @COLOR", cnn);
                command.Parameters.AddWithValue("@COLOR", color);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaGestion = null;

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }



                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]),
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = Convert.ToInt32(reader["HABILITADO"]),
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        Viaje2 = reader["VIAJE2"].ToString(),
                        Viaje3 = reader["VIAJE3"].ToString(),
                        PaisCortina = reader["PAISCORTINA"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        Checkbox = reader["CHECKBOX"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        MovContenedor = reader["MOVCONTENEDOR"].ToString(),
                        Alerta = Convert.ToInt32(reader["ALERTA"]),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        GestionServicio = reader["GESTIONSERVICIO"].ToString(),
                        FechaGestion = fechaGestion
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1TodosSP()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1TodosSP @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();

                string usuario = HttpContext.Current.Session["user"].ToString();
                while (reader.Read())
                {
                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaPreZarpe = null;
                    int semana = 0;
                    DateTime? fechaAsociacionControlador = null;
                    DateTime? fechaAsociacionModem = null;


                    if (reader["FECHA_ASOCIACION_CONTROLADOR"] != DBNull.Value)
                    {
                        fechaAsociacionControlador = Convert.ToDateTime(reader["FECHA_ASOCIACION_CONTROLADOR"]);
                    }

                    if (reader["FECHA_ASOCIACION_MODEM"] != DBNull.Value)
                    {
                        fechaAsociacionModem = Convert.ToDateTime(reader["FECHA_ASOCIACION_MODEM"]);
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }
                    if (reader["SEMANA"] != DBNull.Value) semana = Convert.ToInt32(reader["SEMANA"]);


                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        ValidadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]),
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]),
                        Semana = semana,
                        LlevaModem = Convert.ToInt32(reader["LLEVA_MODEM"]),
                        FechaAsociacionBateriaControlador = fechaAsociacionControlador,
                        FechaAsociacionBateriaModem = fechaAsociacionModem,
                        Traspasado = Convert.ToInt32(reader["TRASPASADO"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetHistoricosServicios1TodosSPColor(string color = "")
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1TodosSPColor @COLOR, @USUARIO", cnn);
                command.Parameters.AddWithValue("@COLOR", color);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    DateTime? fechaPreZarpe = null;

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["FECHAVALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        fechaPreZarpe = Convert.ToDateTime(reader["FECHAVALIDACIONPREZARPE"]);
                    }



                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        PaisPuertoOrigen = reader["NOMBREPAISORIGEN"].ToString(),
                        ValidacionServicio = Convert.ToInt32(reader["VALIDACIONSERVICIO"]),
                        ColorEstado = reader["COLORESTADO"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        ValidadoPreZarpe = Convert.ToInt32(reader["VALIDACIONPREZARPE"]),
                        FechaPreZarpe = fechaPreZarpe,
                        PosContenedorNave = Convert.ToInt32(reader["POSCONTENEDORNAVE"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetServiciosDeposito()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosDeposito @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaControlador = null;
                    string depositoCiudad = "";
                    int scrubber = -1;

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["DEPOSITO"] != DBNull.Value)
                    {
                        depositoCiudad = reader["DEPOSITO"].ToString() + " - " + reader["CIUDADDEPOSITO"].ToString();
                    }
                    else
                    {
                        depositoCiudad = "";
                    }

                    if (reader["SCRUBBER"] != DBNull.Value)
                    {
                        scrubber = Convert.ToInt32(reader["SCRUBBER"]);
                    }

                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        FechaControlador = fechaControlador,
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        TipoEntrega = reader["TIPO_ENTREGA"].ToString(),
                        Semana = Convert.ToInt32(reader["SEMANA"]),
                        Comentario = reader["COMENTARIO_DEPOSITO"].ToString(),
                        ContenedorConLeaktest = Convert.ToInt32(reader["CONTENEDORCONLEAKTEST"]),
                        NumeroOrdenCarga = Convert.ToInt32(reader["NUMEROCARGA"]),
                        ValidacionTecnica = Convert.ToInt32(reader["NUMEROCARGA"]),
                        Checkbox = "",
                        Deposito = depositoCiudad,
                        Scrubber = scrubber
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.ServicioCompleto> GetServiciosDeposito2()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosDeposito2 @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaControlador = null;
                    DateTime? fechaContenedor = null;
                    string depositoCiudad = "";
                    int scrubber = -1;

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["DEPOSITO"] != DBNull.Value)
                    {
                        depositoCiudad = reader["DEPOSITO"].ToString() + " - " + reader["CIUDADDEPOSITO"].ToString();
                    }
                    else
                    {
                        depositoCiudad = "";
                    }

                    if (reader["FECHA_CONTENEDOR"] != DBNull.Value)
                    {
                        fechaContenedor = Convert.ToDateTime(reader["FECHA_CONTENEDOR"]);
                    }

                    if (reader["SCRUBBER"] != DBNull.Value)
                    {
                        scrubber = Convert.ToInt32(reader["SCRUBBER"]);
                    }

                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        FechaControlador = fechaControlador,
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        TipoEntrega = reader["TIPO_ENTREGA"].ToString(),
                        Semana = Convert.ToInt32(reader["SEMANA"]),
                        Comentario = reader["COMENTARIO_DEPOSITO"].ToString(),
                        ContenedorConLeaktest = Convert.ToInt32(reader["CONTENEDORCONLEAKTEST"]),
                        NumeroOrdenCarga = Convert.ToInt32(reader["NUMEROCARGA"]),
                        ValidacionTecnica = Convert.ToInt32(reader["NUMEROCARGA"]),
                        Checkbox = "",
                        Deposito = depositoCiudad,
                        Modem = reader["MODEM"].ToString(),
                        ControladorPorDeposito = reader["CONTENEDOR_POR_DEPOSITO"].ToString(),
                        FechaContenedor = fechaContenedor,
                        Scrubber = scrubber
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }


        public static ServicioCompleto ValidarContenedorControlador(int IdServicio, string Contenedor, string Controlador)
        {
            SqlConnection cnn;
            ServicioCompleto servicioValidado = new ServicioCompleto();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarContenedorControladorModem @IdServicio, @Contenedor, @Controlador;", cnn);
                command.Parameters.AddWithValue("@IdServicio", IdServicio);
                command.Parameters.AddWithValue("@Contenedor", Contenedor);
                command.Parameters.AddWithValue("@Controlador", Controlador);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    servicioValidado.Contenedor = reader["CONTENEDOR"].ToString();
                    servicioValidado.Modem = reader["NUMMODEM"].ToString();
                    servicioValidado.Controlador = reader["NUMCONTROLADOR"].ToString();
                    servicioValidado.IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]);
                    servicioValidado.IdModem = Convert.ToInt32(reader["ID_MODEM"]);
                    servicioValidado.IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return servicioValidado;
        }

        public static ServicioCompleto IdModemIdControlerAPITecnica(ServicioCompleto servicioValidado)
        {
            MySqlConnection cnn;
            ServicioCompleto servicioApiTecnica = new ServicioCompleto();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT M.ID as ID_MODEM FROM modem AS M WHERE M.ESN = @Modem ORDER BY M.ID DESC LIMIT 1", cnn);
                command.CommandTimeout = 10;
                command.Parameters.Add("@Modem", MySqlDbType.VarChar, 50).Value = servicioValidado.Modem;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    servicioApiTecnica.IdModem = Convert.ToInt32(reader["ID_MODEM"]);
                    servicioApiTecnica.Controlador = servicioValidado.Controlador;
                    servicioApiTecnica.Contenedor = servicioValidado.Contenedor;
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return servicioApiTecnica;
        }

        public static int InsertarInstruccionAPITecnica(ServicioCompleto servicioValidadoOK)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO instModem (idModem,parameter,idStatusInst,instruction,creationDate,modificationDate,idModifiedBy)" +
                    "VALUES (@IdModem, @Parametros, 1, 1, now(), now(), @IdUsuario);", cnn);
                command.CommandTimeout = 10;
                command.Parameters.Add("@idModem", MySqlDbType.Int32).Value = servicioValidadoOK.IdModem;
                command.Parameters.Add("@Parametros", MySqlDbType.VarChar, 100).Value = "{\"CN\":\"" + servicioValidadoOK.Contenedor + "\",\"CID\":\"" + servicioValidadoOK.Controlador + "\"}";
                command.Parameters.Add("@IdUsuario", MySqlDbType.Int32).Value = 118; //Usuario Randal

                command.Prepare();
                int val = command.ExecuteNonQuery();

                cnn.Close();

                if (val > 0)
                {
                    //insertado con exito
                    return 1;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return 0;
        }

        public static void ActualizarAlertasInactivas(ServicioCompleto servicioValidadoOK)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("UPDATE AplicacionServicio_HistoricoAlertas SET ACTIVO = 0 WHERE ID_MODEM = @ID_MODEM AND (ID_ALERTA = 26 OR ID_ALERTA = 27) ", cnn);
                command.Parameters.AddWithValue("@ID_MODEM", servicioValidadoOK.IdModem);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int EditarCelda(int IdServicio, string Campo, string Valor, string Columna, int IdSetpoint = 0, int IdLugarControlador = 0,
    int IdLugarcortina = 0, int IdLugarGasificacion = 0, int IdTipoNodoControladorOrigen = 0, int IdNodoControladorOrigen = 0, int IdLugarModem = 0)
        {
            int respuesta = 0;
            bool alerta = false;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                bool cambiarEstadoFacturacion = false;
                ServicioCompleto servicioValidadoOK = new ServicioCompleto();
                ServicioCompleto servicioValidadoAPITecnica = new ServicioCompleto();

                cnn.Open();
                SqlCommand command = new SqlCommand();


                // AFECTAN A VALIDACIÓN TÉCNICA Y DE SERVICIO
                if (Campo == "Commodity")
                {
                    command = new SqlCommand("EXEC dbo.EditarCeldaCommodity @COMMODITY, @IDSETPOINT, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@COMMODITY", Valor);
                    command.Parameters.AddWithValue("@IDSETPOINT", IdSetpoint);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Contenedor")
                {
                    string usuario = HttpContext.Current.Session["user"].ToString();
                    command = new SqlCommand("EXEC dbo.ContenedorEnServicio @CONTENEDOR, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@CONTENEDOR", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;

                    //si se encuentra configurado OK el servicio se procede a iniciar servicio de Modem 
                    servicioValidadoOK = ValidarContenedorControlador(IdServicio, Valor, ""); //CONTENEDOR
                    //validar
                    if (servicioValidadoOK.Modem != "" && servicioValidadoOK.Contenedor != "" && servicioValidadoOK.Controlador != "")
                    {
                        servicioValidadoAPITecnica = IdModemIdControlerAPITecnica(servicioValidadoOK);
                        InsertarInstruccionAPITecnica(servicioValidadoAPITecnica);
                        //quitar alertas para el modem
                        ActualizarAlertasInactivas(servicioValidadoOK);
                    }

                    //// ALERTAR CUANDO SERVICIO DEBE POSEER SCRUBBER Y NO FUE REGISTRADO EN RESULTADO LEAKTEST ////
                    int id_contenedor = Convert.ToInt32(Valor);
                    string contenedor = ContenedorModelo.ObtenerNumContenedor(id_contenedor);

                    int validacionScrubber = ValidacionScrubber(IdServicio, contenedor);
                    if (validacionScrubber == 1) alerta = true;
                }
                else if (Campo == "Controlador")
                {
                    command = new SqlCommand("EXEC dbo.ControladorEnServicio @CONTROLADOR, @IDSERVICIO, @USUARIO, @TIPONODO, @NODO", cnn);
                    command.Parameters.AddWithValue("@CONTROLADOR", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    command.Parameters.AddWithValue("@TIPONODO", IdTipoNodoControladorOrigen);
                    command.Parameters.AddWithValue("@NODO", IdNodoControladorOrigen);
                    cambiarEstadoFacturacion = true;

                    //si se encuentra configurado OK el servicio se procede a iniciar servicio de Modem 
                    servicioValidadoOK = ValidarContenedorControlador(IdServicio, "", Valor); //CONTROLADOR
                    //validar
                    if (servicioValidadoOK.Modem != "" && servicioValidadoOK.Contenedor != "" && servicioValidadoOK.Controlador != "")
                    {
                        servicioValidadoAPITecnica = IdModemIdControlerAPITecnica(servicioValidadoOK);
                        InsertarInstruccionAPITecnica(servicioValidadoAPITecnica);
                        //quitar alertas para el modem
                        ActualizarAlertasInactivas(servicioValidadoOK);
                    }
                }


                // AFECTAN A VALIDACIÓN DE SERVICIO
                else if (Campo == "Nave1") //AFECTA VALIDACION SERVICIO // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("EXEC dbo.EditarCeldaNave  @NAVE, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@NAVE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Nave2") // NO SE UTILIZA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_NAVE2 = @NAVE, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@NAVE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Nave3") // NO SE UTILIZA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_NAVE3 = @NAVE, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@NAVE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Viaje") //AFECTA VALIDACION SERVICIO // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("EXEC dbo.EditarCeldaViaje @VIAJE, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@VIAJE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Viaje2") // NO SE UTILIZA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VIAJE2 = @VIAJE, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@VIAJE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Viaje3") // NO SE UTILIZA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VIAJE3 = @VIAJE, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@VIAJE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Booking") //AFECTA VALIDACION SERVICIO
                {
                    command = new SqlCommand("EXEC dbo.EditarCeldaBooking @BOOKING, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@BOOKING", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }
                else if (Campo == "Exportador") //AFECTA VALIDACION SERVICIO
                {
                    command = new SqlCommand("EXEC dbo.EditarCeldaExportador @ID_EXPORTADOR, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ID_EXPORTADOR", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    cambiarEstadoFacturacion = true;
                }


                // NO AFECTAN A VALIDACIÓN
                else if (Campo == "Freightforwarder")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_FREIGHTFORWARDER = @FREIGHTFORWARDER, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FREIGHTFORWARDER", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Consignatario")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET CONSIGNATARIO = @CONSIGNATARIO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@CONSIGNATARIO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Temperatura")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET TEMPERATURA = @TEMPERATURA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TEMPERATURA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Bateria") // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET BATERIA = @BATERIA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@BATERIA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Precintosecurity")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET PRECINTOSECURITY = @PRECINTOSECURITY, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@PRECINTOSECURITY", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Candado")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET CANDADO = @CANDADO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@CANDADO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "SelloP1Servicio")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET SELLOPANEL1 = @SELLOPANEL1, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@SELLOPANEL1", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "SelloP2Servicio")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET SELLOPANEL2 = @SELLOPANEL2, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@SELLOPANEL2", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "ObservacionSellos")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET OBSERVACIONSELLOS = @OBSERVACIONSELLOS, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@OBSERVACIONSELLOS", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Horallegada")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET HORALLEGADA = @HORALLEGADA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@HORALLEGADA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Horasalida")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET HORASALIDA = @HORASALIDA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@HORASALIDA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FechaCortina")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHACORTINA = @FECHA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Purafil")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET PURAFILCORTINA = @CANTIDAD, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@CANTIDAD", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tecnicocortina")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET TECNICOCORTINA = @TECNICO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TECNICO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Fechagasificacion")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAGASIFICACION = @FECHA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tipolugargasificacion")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_TIPOLUGARGASIFICACION = @TIPOLUGARGASIFICACION, ID_LUGARGASIFICACION = @LUGARGASIFICACION, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TIPOLUGARGASIFICACION", Valor);
                    command.Parameters.AddWithValue("@LUGARGASIFICACION", IdLugarGasificacion);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tecnicogasificacion")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_TECNICOGASIFICACION = @TECNICOGASIFICACION, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TECNICOGASIFICACION", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tratamiento")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_TRATAMIENTOCO2 = @TRATAMIENTO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TRATAMIENTO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Cantidadfiltros")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FILTROSCRUBBER = @FILTROS, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FILTROS", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Cal")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET CANTIDADCALSCRUBBER = @CANTIDAD, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@CANTIDAD", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Notaservicio")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET NOTASERVICIO = @NOTA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@NOTA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Notalogistica")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET NOTALOGISTICA = @NOTA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@NOTA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Validado")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VALIDADO = @VALIDADOS, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@VALIDADOS", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Habilitado")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET HABILITADO = @HABILITADO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@HABILITADO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "GestionServicio")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET GESTIONSERVICIO = @GESTION, FECHAGESTION = SYSDATETIME(), USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@GESTION", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "ValidacionPreZarpe")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VALIDACIONPREZARPE=@VALIDACIONPREZARPE, FECHAVALIDACIONPREZARPE = SYSDATETIME(), USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@VALIDACIONPREZARPE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());

                    if (Valor == "0")
                    {
                        DeshabilitarAlertaServicio(37, IdServicio);
                    }
                }
                else if (Campo == "FechaValidacionPreZarpe")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAVALIDACIONPREZARPE=@FECHAVALIDACIONPREZARPE, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHAVALIDACIONPREZARPE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "PosContenedorNave")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET POSCONTENEDORNAVE=@POSCONTENEDORNAVE, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@POSCONTENEDORNAVE", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "EtaPuerto") //AFECTA VALIDACION SERVICIO
                {
                    command = new SqlCommand("EXEC dbo.EtaEnServicio @FECHA, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Modem") //AFECTA VALIDACION TECNICA Y DE SERVICIO
                {
                    command = new SqlCommand("EXEC dbo.ModemEnServicio @MODEM, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@MODEM", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tipolugarcontrolador")  //NO TIENE ACTUALMENTE ACTUALIZACION DE VALIDACION SERVICIO
                {
                    command = new SqlCommand("EXEC dbo.TipoLugarControlador @IDTIPOLUGACONTROLADOR, @IDLUGARCONTROLADOR, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@IDTIPOLUGACONTROLADOR", Valor);
                    command.Parameters.AddWithValue("@IDLUGARCONTROLADOR", IdLugarControlador);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tecnicocontrolador") //AFECTA VALIDACION DE SERVICIO
                {
                    command = new SqlCommand("EXEC dbo.TecnicoControlador @IDTENICOCONTROLADOR, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@IDTENICOCONTROLADOR", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FechaControlador") //NO TIENE ACTUALMENTE ACTUALIZACION DE VALIDACION SERVICIO NI TECNICA
                {
                    DateTime? horaaa = HoraFecha(Valor);
                    command = new SqlCommand("EXEC dbo.FechaControlador @FECHA, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "EtaNave") // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ETANAVE = @ETANAVE, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@ETANAVE", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "IniStacking") // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAINISTACKING = @FECHA, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FinStacking") // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAFINSTACKING = @FECHA, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Etd") // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ETD = @FECHA, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "PuertoOrigen") // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_PUERTOORIGEN = @PUERTO, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@PUERTO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "PuertoDestino") // NO SE PUEDE EDITAR A NIVEL DE CELDA
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_PUERTODESTINO = @PUERTO, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@PUERTO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "SelloSecurityServicio")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET SELLOSECURITY = @SELLOSECURITY, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@SELLOSECURITY", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tipolugarcortina")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_TIPOLUGARCORTINA = @TIPOLUGARCORTINA, ID_LUGARCORTINA = @LUGARCORTINA, USUARIOACCION = @USUARIO, VALIDACIONSERVICIO = 1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TIPOLUGARCORTINA", Valor);
                    command.Parameters.AddWithValue("@LUGARCORTINA", IdLugarcortina);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "NumeroCarga")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET NUMEROCARGA=@NUMEROCARGA, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@NUMEROCARGA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "ComentarioDeposito")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET COMENTARIO_DEPOSITO=@COMENTARIO_DEPOSITO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@COMENTARIO_DEPOSITO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Deposito2") //Modificacion deposito asociado al booking
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_DEPOSITO = @DEPOSITO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@DEPOSITO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                //Desde aquí, campos que se agregaron por modificación de proceso de instalación del modem
                else if (Campo == "LlevaModem")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET LLEVA_MODEM=@LLEVA_MODEM, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@LLEVA_MODEM", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FechaModem")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET FECHAMODEM=@FECHAMODEM, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHAMODEM", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "TipolugarModem")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_TIPOLUGARMODEM=@ID_TIPOLUGARMODEM, ID_LUGARMODEM=@ID_LUGARMODEM, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@ID_TIPOLUGARMODEM", Valor);
                    command.Parameters.AddWithValue("@ID_LUGARMODEM", IdLugarModem);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "TecnicoModem")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET ID_TECNICOMODEM=@ID_TECNICOMODEM, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@ID_TECNICOMODEM", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }

                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {

                    respuesta = 1;
                }
                else
                {
                    if (cambiarEstadoFacturacion)
                    {
                        SqlConnection cnn2;
                        cnn2 = new SqlConnection(connectionString);
                        cnn2.Open();
                        SqlCommand command2 = new SqlCommand();
                        command2 = new SqlCommand("EXEC dbo.CambiarEstadoFacturacion @IDSERVICIO, @USUARIO", cnn2);
                        command2.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                        command2.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                        command2.ExecuteNonQuery();
                        cnn2.Close();
                    }

                    if (alerta)
                    {
                        respuesta = 2;
                    }
                    else
                    {
                        respuesta = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }


        public static int CancelarServicios(int Servicios, int Confirmado)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.CancelarServicio @IDSERVICIO, @USUARIO, @CONFIRMADO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@CONFIRMADO", Confirmado);
                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int DescancelarServicios(int Servicios)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.DescancelarServicio @IDSERVICIO, @USUARIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int EliminarServicios(int[] Servicios)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand();
                for (var i = 0; i < Servicios.Count(); i++)
                {
                    setearusuario(Servicios[i]);
                    command = new SqlCommand("EXEC dbo.EliminarServicio @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios[i]);
                }


                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static void setearusuario(int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static int AgregarServicioCelda(string Booking = "", int CantServicios = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarServicioCelda", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 50).Value = Booking;
            scCommand.Parameters.Add("@CANTSERVICIOS", SqlDbType.Int, 50).Value = CantServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int AgregarServicioCeldaSP(string Booking = "", int CantServicios = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarServicioCeldaSP", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 50).Value = Booking;
            scCommand.Parameters.Add("@CANTSERVICIOS", SqlDbType.Int, 50).Value = CantServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }
        public static List<Clases.Validacion> ValidarServicio()
        {
            MySqlConnection cnn;
            List<Clases.Validacion> Servicios = new List<Clases.Validacion>();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT IFNULL(c.esn,'') AS esn, b.id, IFNULL(b.bookingNumber,'') AS bookingNumber, IFNULL(b.idComoditie,0) AS idComoditie, CONCAT(d.prefix, b.containerNumber) AS Contenedor, DATE_FORMAT(b.travelStartDate,'%Y-%m-%d') AS travelStartDate FROM prometeo.logistic a INNER JOIN (SELECT id, bookingNumber, idComoditie, travelStartDate, idContainer, containerNumber FROM prometeo.service) b ON b.id = a.idService INNER JOIN (SELECT id, esn FROM prometeo.controller) c ON c.id =  a.idController INNER JOIN (SELECT id, prefix FROM prometeo.container) d ON d.id = b.idContainer WHERE a.idLogisticStatus = 8 AND a.idControllerStatus = 2 AND b.travelStartDate IS NOT NULL ORDER BY b.travelStartDate ASC;", cnn);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Servicios.Add(new Clases.Validacion
                    {
                        controlador = reader["esn"].ToString(),
                        IdServicio = Convert.ToInt32(reader["id"]),
                        Booking = reader["bookingNumber"].ToString(),
                        IdCommodity = Convert.ToInt32(reader["idComoditie"]),
                        Contenedor = reader["Contenedor"].ToString(),
                        FechaInicioServicio = Convert.ToDateTime(reader["travelStartDate"])
                    });
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static bool VerificarControladorEnergizado(string Controlador)
        {
            MySqlConnection cnn;
            Clases.Validacion Servicios = new Clases.Validacion();
            cnn = new MySqlConnection(connectionStringTecnica);
            DateTime fechahoy = DateTime.Now;
            DateTime fechaInicio = fechahoy.AddHours(-24);
            string dataEnergizada = "";
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT data FROM `msgProc` WHERE `data` LIKE '%" + Controlador + "%' AND registerDate >= '" + fechaInicio.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY id DESC LIMIT 1", cnn);
                command.CommandTimeout = 30;
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataEnergizada = reader["data"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            if (dataEnergizada != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static Clases.Validacion DatosServicioPTL_Controlador(string Controlador)
        {
            MySqlConnection cnn;
            Clases.Validacion Servicios = new Clases.Validacion();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT service.id, IFNULL( service.bookingNumber, '' ) AS bookingNumber, IFNULL( service.idComoditie, 0 ) AS idComoditie, DATE_FORMAT(service.travelStartDate, '%Y-%m-%d') AS travelStartDate, CONCAT( (SELECT prefix FROM prometeo.container WHERE id = service.idContainer), service.containerNumber ) AS Contenedor  FROM service WHERE id = (SELECT idService FROM logistic WHERE idController = (SELECT id FROM controller WHERE esn = @CONTROLADOR ORDER BY id DESC LIMIT 1) ORDER BY id DESC LIMIT 1) ORDER BY id DESC LIMIT 1;", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Servicios.controlador = Controlador;
                    if (reader["id"] != DBNull.Value) Servicios.IdServicio = Convert.ToInt32(reader["id"]);
                    if (reader["bookingNumber"] != DBNull.Value) Servicios.Booking = reader["bookingNumber"].ToString();
                    if (reader["idComoditie"] != DBNull.Value) Servicios.IdCommodity = Convert.ToInt32(reader["idComoditie"]);
                    if (reader["travelStartDate"] != DBNull.Value) Servicios.FechaInicioServicio = Convert.ToDateTime(reader["travelStartDate"]);
                    if (reader["Contenedor"] != DBNull.Value) Servicios.Contenedor = reader["Contenedor"].ToString();
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static Clases.Validacion VerificarEstadoControladorValidacion(string Controlador)
        {
            MySqlConnection cnn;
            Clases.Validacion Servicios = new Clases.Validacion();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT idService AS ID_SERVICIO FROM prometeo.logistic WHERE prometeo.logistic.idController = (SELECT id FROM prometeo.controller WHERE esn = @CONTROLADOR ORDER BY id DESC LIMIT 1) AND prometeo.logistic.idLogisticStatus = 8 AND prometeo.logistic.idControllerStatus = 2 ORDER BY id DESC LIMIT 1;", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["ID_SERVICIO"] != DBNull.Value) Servicios.IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static Clases.Validacion VerificarModemProgramadoValidacion(string Controlador)
        {
            MySqlConnection cnn;
            Clases.Validacion Servicios = new Clases.Validacion();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT IFNULL( prometeo.modem.esn, '' ) AS modem FROM prometeo.modem WHERE prometeo.modem.id = (SELECT idModem FROM prometeo.logistic WHERE prometeo.logistic.idController = (SELECT id FROM prometeo.controller WHERE esn = @CONTROLADOR ORDER BY id DESC LIMIT 1)	ORDER BY id DESC LIMIT 1);", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Servicios.controlador = Controlador;
                    Servicios.modem = reader["modem"].ToString();
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static Clases.Validacion VerificarEstadoModemValidacion(string Controlador)
        {
            MySqlConnection cnn;
            Clases.Validacion Servicios = new Clases.Validacion();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT IFNULL( prometeo.modem.esn, '' ) AS modem FROM prometeo.modem WHERE prometeo.modem.id = (SELECT prometeo.logisticModem.idModem FROM prometeo.logisticModem WHERE prometeo.logisticModem.idModem  = (SELECT idModem FROM prometeo.logistic WHERE prometeo.logistic.idController = (SELECT id FROM prometeo.controller WHERE esn = @CONTROLADOR ORDER BY id DESC LIMIT 1) ORDER BY id DESC LIMIT 1) AND prometeo.logisticModem.idModemStatus = 3 ORDER BY id DESC LIMIT 1);", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Servicios.controlador = Controlador;
                    Servicios.modem = reader["modem"].ToString();
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Validacion> ValidarServicioConModem()
        {
            MySqlConnection cnn;
            List<Clases.Validacion> Servicios = new List<Clases.Validacion>();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT IFNULL(d.esn, '') AS esn, IFNULL(e.esn, '') as modem, b.id as id, IFNULL(b.bookingNumber, '') AS bookingNumber, IFNULL(b.idComoditie, 0) AS idComoditie, concat(c.prefix, b.containerNumber) as Contenedor, DATE_FORMAT(b.travelStartDate, '%Y-%m-%d') as travelStartDate FROM prometeo.logistic a INNER JOIN (SELECT id, bookingNumber, idComoditie, idContainer, travelStartDate, containerNumber FROM prometeo.service) b ON a.idService = b.id INNER JOIN(SELECT id, prefix FROM prometeo.container) c ON b.idContainer = c.id INNER JOIN(SELECT id, esn FROM prometeo.controller) d ON a.idController = d.id INNER JOIN(SELECT id, esn FROM prometeo.modem) e ON a.idModem = e.id INNER JOIN(SELECT idModem, idModemStatus FROM prometeo.logisticModem) f ON a.idModem = f.idModem WHERE b.travelStartDate IS NOT NULL AND a.idLogisticStatus = 8 AND a.idControllerStatus = 2 AND f.idModemStatus = 3 ORDER BY b.travelStartDate ASC;", cnn);

                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Servicios.Add(new Clases.Validacion
                    {
                        controlador = reader["esn"].ToString(),
                        modem = reader["modem"].ToString(),
                        IdServicio = Convert.ToInt32(reader["id"]),
                        Booking = reader["bookingNumber"].ToString(),
                        IdCommodity = Convert.ToInt32(reader["idComoditie"]),
                        Contenedor = reader["Contenedor"].ToString(),
                        FechaInicioServicio = Convert.ToDateTime(reader["travelStartDate"])
                    });
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static void ActualizarValidacionServicio(int IdSevicio, int Estado)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VALIDADO = @VALIDADO, USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdSevicio);
                command.Parameters.AddWithValue("@VALIDADO", Estado);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static List<Clases.ServicioCompleto> GetHistoricoServiciosMsc()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServicios1TodosMsc", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string UltimaNave = "";
                    DateTime? etaPuerto = null;
                    DateTime? etaNave = null;
                    DateTime? etd = null;
                    DateTime? fechaIniStacking = null;
                    DateTime? fechaFinStacking = null;
                    DateTime? fechaCortina = null;
                    DateTime? fechaGasificacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaCancelacion = null;
                    int Alerta = 0;

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        fechaIniStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        fechaFinStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["FECHACORTINA"] != DBNull.Value)
                    {
                        fechaCortina = Convert.ToDateTime(reader["FECHACORTINA"]);
                    }

                    if (reader["FECHAGASIFICACION"] != DBNull.Value)
                    {
                        fechaGasificacion = Convert.ToDateTime(reader["FECHAGASIFICACION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHACANCELACION"] != DBNull.Value)
                    {
                        fechaCancelacion = Convert.ToDateTime(reader["FECHACANCELACION"]);
                    }

                    if (reader["NAVE3"] != DBNull.Value)
                    {
                        UltimaNave = reader["NAVE3"].ToString();
                    }
                    else if (reader["NAVE2"] != DBNull.Value)
                    {
                        UltimaNave = reader["NAVE2"].ToString();
                    }
                    else
                    {
                        UltimaNave = reader["NAVE1"].ToString();
                    }

                    if (reader["ALERTA"] != DBNull.Value)
                    {
                        Alerta = Convert.ToInt32(reader["ALERTA"]);
                    }

                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Booking = reader["BOOKING"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Viaje = reader["VIAJE"].ToString(),
                        Consignatario = reader["CONSIGNATARIO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EtaPuerto = etaPuerto,
                        EtaNave = etaNave,
                        Etd = etd,
                        FechaIniStacking = fechaIniStacking,
                        FechaFinStacking = fechaFinStacking,
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        Freightforwarder = reader["FREIGHTFORWARDER"].ToString(),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Nave1 = reader["NAVE1"].ToString(),
                        Nave2 = reader["NAVE2"].ToString(),
                        Nave3 = reader["NAVE3"].ToString(),
                        TratamientoCo2 = reader["TRATAMIENTOCO2"].ToString(),
                        TipoLugarCortina = reader["TIPOLUGARCORTINA"].ToString(),
                        LugarCortina = reader["LUGARCORTINA"].ToString(),
                        PurafilCortina = Convert.ToInt32(reader["PURAFILCORTINA"]),
                        FechaCortina = fechaCortina,
                        TecnicoCortina = reader["TECNICOCORTINA"].ToString(),
                        TipoLugarGasificacion = reader["TIPOLUGARGASIFICACION"].ToString(),
                        LugarGasificacion = reader["LUGARGASIFICACION"].ToString(),
                        TecnicoGasificacion = reader["TECNICOGASIFICACION"].ToString(),
                        FechaGasificacion = fechaGasificacion,
                        Co2Gasificacion = reader["CO2GASIFICACION"].ToString(),
                        N2Gasificacion = reader["N2GASIFICACION"].ToString(),
                        Habilitado = Convert.ToInt32(reader["HABILITADO"]),
                        FechaControlador = fechaControlador,
                        TipoLugarControlador = reader["TIPOLUGARCONTROLADOR"].ToString(),
                        LugarControlador = reader["LUGARCONTROLADOR"].ToString(),
                        TecnicoControlador = reader["TECNICOCONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Validado = Convert.ToInt32(reader["VALIDADO"]),
                        PrecintoSecurity = reader["PRECINTOSECURITY"].ToString(),
                        Candado = reader["CANDADO"].ToString(),
                        FiltroScrubber = reader["FILTROSCRUBBER"].ToString(),
                        CantidadCalScrubber = Convert.ToInt32(reader["CANTIDADCALSCRUBBER"]),
                        Horallegada = reader["HORALLEGADA"].ToString(),
                        HoraSalida = reader["HORASALIDA"].ToString(),
                        NotaServicio = reader["NOTASERVICIO"].ToString(),
                        NotasLogistica = reader["NOTALOGISTICA"].ToString(),
                        SelloPerno1 = reader["SELLOPERNO1"].ToString(),
                        SelloPerno2 = reader["SELLOPERNO2"].ToString(),
                        SelloTapa = reader["SELLOTAPA"].ToString(),
                        SelloPanel1 = reader["SELLOPANEL1"].ToString(),
                        SelloPanel2 = reader["SELLOPANEL2"].ToString(),
                        SelloSecurity = reader["SELLOSECURITY"].ToString(),
                        ObservacionSellos = reader["OBSERVACIONSELLOS"].ToString(),
                        Motivo = reader["MOTIVO"].ToString(),
                        CommodityTecnica = reader["COMMODITYTECNICA"].ToString(),
                        FechaCancelacion = fechaCancelacion,
                        ContinenteDestino = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ContinenteOrigen = reader["CONTINENTEPUERTOORIGEN"].ToString(),
                        Viaje2 = reader["VIAJE2"].ToString(),
                        Viaje3 = reader["VIAJE3"].ToString(),
                        PaisCortina = reader["PAISCORTINA"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Deposito = reader["DEPOSITO"].ToString(),
                        Checkbox = reader["CHECKBOX"].ToString(),
                        UltimaNave = UltimaNave,
                        MovContenedor = reader["MOVCONTENEDOR"].ToString(),
                        Alerta = Alerta
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }
        public static int EditarDepositoServicio(int IdServicio, string Pais, string Deposito)
        {
            SqlConnection cnn;
            int Estado = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET PAISDEPOSITO = @PAIS, DEPOSITO = @DEPOSITO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@PAIS", Pais);
                command.Parameters.AddWithValue("@DEPOSITO", Deposito);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estado = Convert.ToInt32(reader["ESTADO"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Estado;
        }
        public static List<Clases.HistoricoControlador> HistoricoControladorServicio(int[] Servicios)
        {
            SqlConnection cnn;
            List<Clases.HistoricoControlador> Controladores = new List<Clases.HistoricoControlador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.HistoricoControladorServicio @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", Servicios[0]);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Controladores.Add(new Clases.HistoricoControlador
                    {
                        IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
                        Fecha = Convert.ToDateTime(reader["FECHAACCION"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Controladores;
        }
        public static int QuitarContenedor(string NumContenedor, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.QuitarContenedor @IDSERVICIO, @NUMCONTENEDOR", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@NUMCONTENEDOR", NumContenedor);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int QuitarControlador(string NumControlador, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.QuitarControladorServicio @IDSERVICIO, @NUMCONTROLADOR", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@NUMCONTROLADOR", NumControlador);


                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int QuitarModem(string NumModem, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.QuitarModemServicio @IDSERVICIO, @NUMMODEM", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@NUMMODEM", NumModem);


                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int RolearServicio(string Booking, int IdServicio, string BookingAntiguo)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.RolearServicio @IDSERVICIO, @BOOKING, @BOOKINGANTIGUO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@BOOKING", Booking.Trim());
                command.Parameters.AddWithValue("@BOOKINGANTIGUO", BookingAntiguo.Trim());

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    bool cambiarEstadoFacturacion = true;
                    if (cambiarEstadoFacturacion)
                    {
                        SqlConnection cnn2;
                        cnn2 = new SqlConnection(connectionString);
                        cnn2.Open();
                        SqlCommand command2 = new SqlCommand();
                        command2 = new SqlCommand("EXEC dbo.CambiarEstadoFacturacion @IDSERVICIO, @USUARIO", cnn2);
                        command2.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                        command2.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                        command2.ExecuteNonQuery();
                        cnn2.Close();
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }


        public static int RoleoServicio(Clases.Reserva Reserva, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("RoleoServicio", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Reserva.IdNaviera;
            scCommand.Parameters.Add("@IDPUERTODESTINO", SqlDbType.Int, 50).Value = Reserva.IdPuertoDestino;
            scCommand.Parameters.Add("@IDPUERTOORIGEN", SqlDbType.Int, 50).Value = Reserva.IdPuertoOrigen;
            scCommand.Parameters.Add("@IDSETPOINT", SqlDbType.Int, 50).Value = Reserva.IdSetpoint;
            scCommand.Parameters.Add("@IDCOMMODITY", SqlDbType.Int, 50).Value = Reserva.IdCommodity;
            scCommand.Parameters.Add("@IDFREIGHTFORWARDER", SqlDbType.Int, 50).Value = Reserva.IdFreightForwarder;
            scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 100).Value = Reserva.Booking;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 100).Value = Reserva.Viaje;
            scCommand.Parameters.Add("@CONSIGNATARIO", SqlDbType.VarChar, 100).Value = Reserva.Consignatario;
            scCommand.Parameters.Add("@CANTIDADSERVICIOS", SqlDbType.Int, 100).Value = Reserva.CantidadServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@IDNAVE", SqlDbType.Int, 100).Value = Reserva.IdNave;
            scCommand.Parameters.Add("@TEMPERATURA", SqlDbType.Float, 100).Value = Reserva.Temperatura;
            scCommand.Parameters.Add("@SERVICEPROVIDER", SqlDbType.Int, 100).Value = Reserva.IdServiceProvider;
            scCommand.Parameters.Add("@IDSERVICIOEDITAR", SqlDbType.Int, 100).Value = IdServicio;


            if (Reserva.Eta == null)
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = Reserva.Eta;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = Reserva.Eta;
            }

            if (Reserva.EtaNave == null)
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = Reserva.EtaNave;
            }

            if (Reserva.Etd == null)
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = Reserva.Etd;
            }
            if (Reserva.FechaIniStacking == null)
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = Reserva.FechaIniStacking;
            }
            if (Reserva.FechaFinStacking == null)
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = Reserva.FechaFinStacking;
            }
            scCommand.Parameters.Add("@IDEXPORTADOR", SqlDbType.Int, 100).Value = Reserva.IdExportador;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }

                result = scCommand.ExecuteNonQuery();
                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    bool cambiarEstadoFacturacion = true;
                    if (cambiarEstadoFacturacion)
                    {
                        SqlConnection cnn2;
                        cnn2 = new SqlConnection(connectionString);
                        cnn2.Open();
                        SqlCommand command2 = new SqlCommand();
                        command2 = new SqlCommand("EXEC dbo.CambiarEstadoFacturacion @IDSERVICIO, @USUARIO", cnn2);
                        command2.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                        command2.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                        command2.ExecuteNonQuery();
                        cnn2.Close();
                    }
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }


        public static int RoleoServicioSP(Clases.Reserva Reserva, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("RoleoServicioSP", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Reserva.IdNaviera;
            scCommand.Parameters.Add("@IDPUERTODESTINO", SqlDbType.Int, 50).Value = Reserva.IdPuertoDestino;
            scCommand.Parameters.Add("@IDPUERTOORIGEN", SqlDbType.Int, 50).Value = Reserva.IdPuertoOrigen;
            scCommand.Parameters.Add("@IDSETPOINT", SqlDbType.Int, 50).Value = Reserva.IdSetpoint;
            scCommand.Parameters.Add("@IDCOMMODITY", SqlDbType.Int, 50).Value = Reserva.IdCommodity;
            scCommand.Parameters.Add("@IDFREIGHTFORWARDER", SqlDbType.Int, 50).Value = Reserva.IdFreightForwarder;
            scCommand.Parameters.Add("@BOOKING", SqlDbType.VarChar, 100).Value = Reserva.Booking;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 100).Value = Reserva.Viaje;
            scCommand.Parameters.Add("@CONSIGNATARIO", SqlDbType.VarChar, 100).Value = Reserva.Consignatario;
            scCommand.Parameters.Add("@CANTIDADSERVICIOS", SqlDbType.Int, 100).Value = Reserva.CantidadServicios;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@IDNAVE", SqlDbType.Int, 100).Value = Reserva.IdNave;
            scCommand.Parameters.Add("@TEMPERATURA", SqlDbType.Float, 100).Value = Reserva.Temperatura;
            scCommand.Parameters.Add("@SERVICEPROVIDER", SqlDbType.Int, 100).Value = HttpContext.Current.Session["SP"].ToString();
            scCommand.Parameters.Add("@IDSERVICIOEDITAR", SqlDbType.Int, 100).Value = IdServicio;


            if (Reserva.Eta == null)
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETA", SqlDbType.DateTime).Value = Reserva.Eta;
                scCommand.Parameters.Add("@ETAPUERTO", SqlDbType.DateTime).Value = Reserva.Eta;
            }

            if (Reserva.EtaNave == null)
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = Reserva.EtaNave;
            }

            if (Reserva.Etd == null)
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = Reserva.Etd;
            }
            if (Reserva.FechaIniStacking == null)
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAINISTACKING", SqlDbType.DateTime).Value = Reserva.FechaIniStacking;
            }
            if (Reserva.FechaFinStacking == null)
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAFINSTACKING", SqlDbType.DateTime).Value = Reserva.FechaFinStacking;
            }
            scCommand.Parameters.Add("@IDEXPORTADOR", SqlDbType.Int, 100).Value = Reserva.IdExportador;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }
        public static Clases.Reserva GetDatosBookingEdi(int IdReserva)
        {
            SqlConnection cnn;
            Clases.Reserva Reservas = new Clases.Reserva();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarReservaByIdEdi @ID_RESERVA", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Reservas.FreightForwarder = reader["FREIGHTFORWARDER"].ToString();
                    Reservas.Exportador = reader["EXPORTADOR"].ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Reservas;
        }

        public static int ValidarServicioNoTecnica(int IdServicio, int estado)
        {

            SqlConnection cnn;
            int validacion = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.VALIDARSERVICIO @IDSERVICIO, @VALIDADO, @USUARIO ", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@VALIDADO", estado);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    validacion = Convert.ToInt32(reader["VALIDACION"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return validacion;
        }

        public static void ServicioParaFacturar(int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ServicioParaFacturar @IDSERVICIO, @USUARIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static ResultadoLeaktest GetResultadoLeaktestByContenedor(string contenedor)
        {
            SqlConnection cnn;
            Clases.ResultadoLeaktest resultado = new Clases.ResultadoLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarLeaktestPorContenedor @CONTENEDOR", cnn);
                command.Parameters.AddWithValue("@CONTENEDOR", contenedor);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["FECHAREALIZACION"] != DBNull.Value)
                    {
                        resultado.FechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    }

                    resultado.Tiempo = reader["TIEMPO"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return resultado;
        }

        public static ResultadoLeaktest GetResultadoLeaktestByContenedorSP(string contenedor)
        {
            SqlConnection cnn;
            Clases.ResultadoLeaktest resultado = new Clases.ResultadoLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarLeaktestPorContenedorSP @CONTENEDOR", cnn);
                command.Parameters.AddWithValue("@CONTENEDOR", contenedor);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["FECHAREALIZACION"] != DBNull.Value)
                    {
                        resultado.FechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    }

                    resultado.Tiempo = reader["TIEMPO"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return resultado;
        }

        public static Clases.ServicioCompleto GetServicioByController(int Controlador)
        {
            SqlConnection cnn;
            Clases.ServicioCompleto Servicio = new Clases.ServicioCompleto();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select * from AplicacionServicio_Servicio1 where ID_CONTROLADOR = @IDCONTROLADOR and ID_RESERVA = (select MAX(id_reserva) from AplicacionServicio_Servicio1 where ID_CONTROLADOR = @IDCONTROLADOR)", cnn);
                command.Parameters.AddWithValue("@IDCONTROLADOR", Controlador);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Servicio.IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]);
                    Servicio.IdPuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Servicio;
        }

        public static List<Clases.Validacion> GetServiceData() // SE CAE PTL
        {
            MySqlConnection cnn;
            List<Clases.Validacion> Servicios = new List<Clases.Validacion>();
            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("select IFNULL(prometeo.controller.esn,'') AS esn, prometeo.service.id, IFNULL(prometeo.service.bookingNumber,'') AS bookingNumber, IFNULL(prometeo.service.idComoditie,0) AS idComoditie, concat(prometeo.container.prefix, prometeo.service.containerNumber) as Contenedor, DATE_FORMAT(prometeo.service.travelStartDate,'%Y-%m-%d') as travelStartDate, prometeo.container.prefix, prometeo.historyService.serviceData, max(prometeo.historyService.id) as MAXID from prometeo.service, prometeo.logistic, prometeo.controller, prometeo.container, prometeo.historyService where prometeo.service.id = prometeo.logistic.idService and prometeo.service.id = prometeo.historyService.idService and prometeo.service.travelStartDate is not null and prometeo.service.travelStartDate >= '2018-08-07' and prometeo.logistic.idController = prometeo.controller.id and (prometeo.logistic.idControllerStatus = 1 or prometeo.logistic.idControllerStatus = 2) and prometeo.service.idContainer = prometeo.container.id and prometeo.historyService.serviceData is not null and prometeo.historyService.serviceData <> '' group by prometeo.historyService.idService order by prometeo.service.travelStartDate desc;", cnn);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Servicios.Add(new Clases.Validacion
                    {
                        controlador = reader["esn"].ToString(),
                        IdServicio = Convert.ToInt32(reader["id"]),
                        Booking = reader["bookingNumber"].ToString(),
                        IdCommodity = Convert.ToInt32(reader["idComoditie"]),
                        Contenedor = reader["Contenedor"].ToString(),
                        FechaInicioServicio = Convert.ToDateTime(reader["travelStartDate"]),
                        ServiceData = reader["serviceData"].ToString()
                    });
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }


        public static DateTime? HoraFecha(string Fecha)
        {
            if (Fecha != "")
            {
                var src = DateTime.Now;
                var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
                var hola = hm.ToString();
                hola = Fecha.Substring(11);
                var hola2 = Fecha.ToString().Substring(0, 10);
                var fecha = hola2 + " " + hola;
                return Convert.ToDateTime(fecha);
            }
            else
            {
                return null;
            }
        }

        public static string GetGestion(int idServicio)
        {
            SqlConnection cnn;
            string Estado = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ISNULL(GESTIONSERVICIO,'') AS GESTIONSERVICIO FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO=@ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", idServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estado = reader["GESTIONSERVICIO"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Estado;
        }

        public static List<Clases.Facturacion> GetServiciosFacturacion()
        {
            SqlConnection cnn;
            List<Clases.Facturacion> Servicios = new List<Clases.Facturacion>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosFacturar", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etd = null;
                    DateTime? fechafacturacion = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechacontrolador = null;


                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAFACTURACION"] != DBNull.Value)
                    {
                        fechafacturacion = Convert.ToDateTime(reader["FECHAFACTURACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechacontrolador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    Servicios.Add(new Clases.Facturacion
                    {
                        Checkbox = reader["CHECKBOX"].ToString(),
                        Continente = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        Pais = reader["NOMBREPAISORIGEN"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PaisDeposito = reader["PAISDEPOSITO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE1"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Tratamiento = reader["TRATAMIENTOCO2"].ToString(),
                        FechaControlador = fechacontrolador,
                        Booking = reader["BOOKING"].ToString(),
                        Tamano = Convert.ToInt32(reader["TAMANO"]),
                        Etd = etd,
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoFacturacion = Convert.ToInt32(reader["ESTADOFACTURACION"]),
                        CodigoSoftland = reader["CODIGOSOFTLAND"].ToString(),
                        NumeroFactura = reader["NUMEROFACTURA"].ToString(),
                        Precio = Convert.ToInt32(reader["PRECIO"]),
                        TipoCambio = Convert.ToDecimal(reader["TIPOCAMBIO"]),
                        FechaFacturacion = fechafacturacion,
                        Gestion = reader["GESTION"].ToString(),
                        FechaGestion = fechaGestion,
                        CentroCosto = reader["CENTROCOSTO"].ToString(),
                        NotaCredito = Convert.ToInt32(reader["NOTACREDITO"]),
                        TipoCambioNota = Convert.ToDecimal(reader["TIPOCAMBIONOTA"]),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Semana = Convert.ToInt32(reader["SEMANA"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static int EditarCeldaFacturacion(int IdServicio, string Campo, string Valor, string Columna, int IdCentro, int Tamano)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                if (Campo == "Factura")
                {
                    command = new SqlCommand("EXEC dbo.NumeroFactura @FACTURA, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@FACTURA", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Precio")
                {
                    command = new SqlCommand("EXEC dbo.Precio @PRECIO, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@PRECIO", Convert.ToInt32(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "TipoCambio")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET TIPOCAMBIO = @TIPOCAMBIO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TIPOCAMBIO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Gestion")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET GESTION = @GESTION, USUARIOACCION = @USUARIO, FECHAGESTION = SYSDATETIME() WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@GESTION", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "PrecioNota")
                {
                    command = new SqlCommand("EXEC dbo.PrecioNotaCredito @PRECIO, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@PRECIO", Convert.ToInt32(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "TipoCambioNota")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET TIPOCAMBIONOTA = @TIPOCAMBIO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TIPOCAMBIO", Valor);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Centro")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET IDCENTRO = @IDCENTRO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDCENTRO", IdCentro);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tamano")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET TAMANO = @TAMANO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TAMANO", Tamano);
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Fecha")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET FECHAFACTURACION = @FECHA, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FechaUltimaGestion")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET FECHAGESTION = @FECHA, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(Valor));
                    command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }

                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static string GetGestionFacturacion(int idServicio)
        {
            SqlConnection cnn;
            string Estado = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ISNULL(GESTION,'') AS GESTION FROM AplicacionServicio_Facturacion WHERE IDSERVICIO=@ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", idServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estado = reader["GESTION"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Estado;
        }

        public static List<Clases.CentroCostos> GetCentroCostos()
        {
            SqlConnection cnn;
            List<Clases.CentroCostos> Centros = new List<Clases.CentroCostos>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetCentroCostos", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Centros.Add(new Clases.CentroCostos
                    {
                        IdCentro = Convert.ToInt32(reader["IDCENTRO"]),
                        Nombre = reader["NOMBRE"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Centros;
        }

        public static int EdicionMasivaFactura(int Servicios, string Factura = "", string Precio = "", string TipoCambio = "", int Centro = 0, string PrecioNota = "", string TipoCambioNota = "", int Tamano = 0, string Fecha = "", string Gestion = "", string FechaGestion = "")
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                //CAMBIOS DE DATOS QUE CAMBIAN PARA TODOS LOS SERVICIOS DE UN BOOKING
                if (Factura != "")
                {
                    command = new SqlCommand("EXEC dbo.NumeroFactura @FACTURA, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@FACTURA", Factura);
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());

                }
                else if (Precio != "")
                {
                    command = new SqlCommand("EXEC dbo.Precio @PRECIO, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@PRECIO", Convert.ToInt32(Precio));
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (TipoCambio != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET TIPOCAMBIO = @TIPOCAMBIO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TIPOCAMBIO", TipoCambio);
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Centro != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET IDCENTRO = @IDCENTRO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@IDCENTRO", Convert.ToInt32(Centro));
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }

                else if (PrecioNota != "")
                {
                    command = new SqlCommand("EXEC dbo.PrecioNotaCredito @PRECIO, @IDSERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@PRECIO", Convert.ToInt32(PrecioNota));
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (TipoCambioNota != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET TIPOCAMBIONOTA = @TIPOCAMBIO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TIPOCAMBIO", TipoCambioNota);
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Tamano != 0)
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET TAMANO = @TAMANO, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@TAMANO", Convert.ToInt32(Tamano));
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Fecha != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET FECHAFACTURACION = @FECHA, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(Fecha));
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Gestion != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET GESTION = @GESTION, USUARIOACCION = @USUARIO, FECHAGESTION = SYSDATETIME() WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@GESTION", Gestion);
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (FechaGestion != "")
                {
                    command = new SqlCommand("UPDATE APLICACIONSERVICIO_FACTURACION SET FECHAGESTION = @FECHA, USUARIOACCION = @USUARIO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                    command.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(FechaGestion));
                    command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }

                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static List<Clases.Facturacion> GetServiciosFacturacionById(int servicio)
        {
            SqlConnection cnn;
            List<Clases.Facturacion> Servicios = new List<Clases.Facturacion>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosFacturarById @SERVICIO", cnn);
                command.Parameters.AddWithValue("@SERVICIO", servicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etd = null;
                    DateTime? fechacontrolador = null;


                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechacontrolador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    Servicios.Add(new Clases.Facturacion
                    {
                        Continente = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        Pais = reader["NOMBREPAISORIGEN"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE1"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Tratamiento = reader["TRATAMIENTOCO2"].ToString(),
                        FechaControlador = fechacontrolador,
                        Booking = reader["BOOKING"].ToString(),
                        Etd = etd,
                        Exportador = reader["EXPORTADOR"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Facturacion> GetServiciosById(int servicio)
        {
            SqlConnection cnn;
            List<Clases.Facturacion> Servicios = new List<Clases.Facturacion>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetServiciosById @SERVICIO", cnn);
                command.Parameters.AddWithValue("@SERVICIO", servicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etd = null;
                    DateTime? fechacontrolador = null;


                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechacontrolador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    Servicios.Add(new Clases.Facturacion
                    {
                        Continente = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        Pais = reader["NOMBREPAISORIGEN"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE1"].ToString(),
                        Viaje = reader["VIAJE2"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Tratamiento = reader["TRATAMIENTOCO2"].ToString(),
                        FechaControlador = fechacontrolador,
                        Booking = reader["BOOKING"].ToString(),
                        Etd = etd,
                        Exportador = reader["EXPORTADOR"].ToString(),
                        IdServicio = Convert.ToInt32(reader["IDSERVICIO"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static int EliminarFacturas(int Servicios)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                //CAMBIOS DE DATOS QUE CAMBIAN PARA TODOS LOS SERVICIOS DE UN BOOKING

                command = new SqlCommand("EXEC dbo.EliminarFactura @IDSERVICIO, @USUARIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());


                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static List<Clases.Facturacion> GetServiciosFacturacionHistorica()
        {
            SqlConnection cnn;
            List<Clases.Facturacion> Servicios = new List<Clases.Facturacion>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosFacturarHistorico", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etd = null;
                    DateTime? fechafacturacion = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechacontrolador = null;


                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAFACTURACION"] != DBNull.Value)
                    {
                        fechafacturacion = Convert.ToDateTime(reader["FECHAFACTURACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechacontrolador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    Servicios.Add(new Clases.Facturacion
                    {
                        Checkbox = reader["CHECKBOX"].ToString(),
                        Continente = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        Pais = reader["NOMBREPAISORIGEN"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE1"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Tratamiento = reader["TRATAMIENTOCO2"].ToString(),
                        FechaControlador = fechacontrolador,
                        Booking = reader["BOOKING"].ToString(),
                        Tamano = Convert.ToInt32(reader["TAMANO"]),
                        Etd = etd,
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoFacturacion = Convert.ToInt32(reader["ESTADOFACTURACION"]),
                        CodigoSoftland = reader["CODIGOSOFTLAND"].ToString(),
                        NumeroFactura = reader["NUMEROFACTURA"].ToString(),
                        Precio = Convert.ToInt32(reader["PRECIO"]),
                        TipoCambio = Convert.ToDecimal(reader["TIPOCAMBIO"]),
                        FechaFacturacion = fechafacturacion,
                        Gestion = reader["GESTION"].ToString(),
                        FechaGestion = fechaGestion,
                        CentroCosto = reader["CENTROCOSTO"].ToString(),
                        NotaCredito = Convert.ToInt32(reader["NOTACREDITO"]),
                        TipoCambioNota = Convert.ToDecimal(reader["TIPOCAMBIONOTA"]),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Semana = Convert.ToInt32(reader["SEMANA"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Facturacion> FiltrarFacturados(int Inicio, int Fin)
        {
            SqlConnection cnn;
            List<Clases.Facturacion> Servicios = new List<Clases.Facturacion>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosFacturarFiltro @INICIO, @FIN", cnn);
                command.Parameters.AddWithValue("@INICIO", Inicio);
                command.Parameters.AddWithValue("@FIN", Fin);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etd = null;
                    DateTime? fechafacturacion = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechacontrolador = null;


                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHAFACTURACION"] != DBNull.Value)
                    {
                        fechafacturacion = Convert.ToDateTime(reader["FECHAFACTURACION"]);
                    }

                    if (reader["FECHAGESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHAGESTION"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechacontrolador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    Servicios.Add(new Clases.Facturacion
                    {
                        Checkbox = reader["CHECKBOX"].ToString(),
                        Continente = reader["CONTINENTEPUERTODESTINO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        Pais = reader["NOMBREPAISORIGEN"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE1"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Tratamiento = reader["TRATAMIENTOCO2"].ToString(),
                        FechaControlador = fechacontrolador,
                        Booking = reader["BOOKING"].ToString(),
                        Tamano = Convert.ToInt32(reader["TAMANO"]),
                        Etd = etd,
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EstadoFacturacion = Convert.ToInt32(reader["ESTADOFACTURACION"]),
                        CodigoSoftland = reader["CODIGOSOFTLAND"].ToString(),
                        NumeroFactura = reader["NUMEROFACTURA"].ToString(),
                        Precio = Convert.ToInt32(reader["PRECIO"]),
                        TipoCambio = Convert.ToDecimal(reader["TIPOCAMBIO"]),
                        FechaFacturacion = fechafacturacion,
                        Gestion = reader["GESTION"].ToString(),
                        FechaGestion = fechaGestion,
                        CentroCosto = reader["CENTROCOSTO"].ToString(),
                        NotaCredito = Convert.ToInt32(reader["NOTACREDITO"]),
                        TipoCambioNota = Convert.ToDecimal(reader["TIPOCAMBIONOTA"]),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Semana = Convert.ToInt32(reader["SEMANA"]),
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }
        public static int FacturarServicio(int Servicios)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                //CAMBIOS DE DATOS QUE CAMBIAN PARA TODOS LOS SERVICIOS DE UN BOOKING

                command = new SqlCommand("EXEC dbo.FacturarServicio @IDSERVICIO, @USUARIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", Servicios);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());


                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static List<Clases.ServicioCompleto> GetServiciosLegal()
        {
            SqlConnection cnn;
            List<Clases.ServicioCompleto> Servicios = new List<Clases.ServicioCompleto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosLegal @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string UltimaNave = "";
                    DateTime? fechaControlador = null;
                    DateTime? fechaAprobacion = null;
                    DateTime? eta = null;
                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if (reader["FECHAAPROBACION"] != DBNull.Value)
                    {
                        fechaAprobacion = Convert.ToDateTime(reader["FECHAAPROBACION"]);
                    }

                    if (reader["ETA"] != DBNull.Value)
                    {
                        eta = Convert.ToDateTime(reader["ETA"]);
                    }


                    if (reader["NAVE3"] != DBNull.Value)
                    {
                        UltimaNave = reader["NAVE3"].ToString();
                    }
                    else if (reader["NAVE2"] != DBNull.Value)
                    {
                        UltimaNave = reader["NAVE2"].ToString();
                    }
                    else
                    {
                        UltimaNave = reader["NAVE1"].ToString();
                    }

                    Servicios.Add(new Clases.ServicioCompleto
                    {
                        Booking = reader["BOOKING"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Temperatura = reader["TEMPERATURA"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Setpoint = reader["SETPOINT"].ToString(),
                        EstadoServicio = reader["ESTADOSERVICO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        UltimaNave = UltimaNave,
                        Checkbox = reader["CHECKBOX"].ToString(),
                        FechaControlador = fechaControlador,
                        Controlador = reader["CONTROLADOR"].ToString(),
                        FechaAprobacion = fechaAprobacion,
                        EstadoAprobacion = Convert.ToInt32(reader["APROBACION"]),
                        Exportador = reader["EXPORTADOR"].ToString(),
                        EtaPuerto = eta
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static int AprobarServicio(int IdServicio, int Notificado)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIOSLEGAL SET USUARIO=@USUARIO, APROBACION = 1, FECHAAPROBACION = SYSDATETIME(), NOTIFICADO=@NOTIFICADO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@NOTIFICADO", Notificado);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int DesaprobarServicio(int IdServicio, int Notificado)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIOSLEGAL SET USUARIO=@USUARIO, APROBACION = 0, FECHAAPROBACION = SYSDATETIME(), NOTIFICADO=@NOTIFICADO WHERE IDSERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@NOTIFICADO", Notificado);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int GetNotificado(int Servicio)
        {
            SqlConnection cnn;
            int Notificado = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ISNULL(NOTIFICADO,0) AS NOTIFICADO FROM APLICACIONSERVICIO_SERVICIOSLEGAL WHERE IDSERVICIO = @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", Servicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Notificado = Convert.ToInt32(reader["NOTIFICADO"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Notificado;
        }

        public static string GetExtension(int Servicio)
        {
            SqlConnection cnn;
            string Extension = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT EXTENSION FROM APLICACIONSERVICIO_SERVICIOSLEGAL WHERE IDSERVICIO = @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", Servicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Extension = reader["EXTENSION"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Extension;
        }

        public static int CrearExtension(int Servicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            string cadenaEncriptada = Encrypt.GetMD5(Servicio.ToString());

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GuardarExtensionServicio @ID_SERVICIO, @EXTENSION, @USUARIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", Servicio);
                command.Parameters.AddWithValue("@EXTENSION", cadenaEncriptada);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static int GenerarAlertaServicio(int Alerta, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int respuesta = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_HistoricoAlertas (ID_ALERTA, ID_SERVICIO, FECHA_ALERTA, ACTIVO) VALUES(@ALERTA, @ID_SERVICIO, SYSDATETIME(), 1)", cnn);
                command.Parameters.AddWithValue("@ALERTA", Alerta);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                respuesta = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static int obtenerIdServicioPorESN(string ESN, int tipo)
        {
            int respuesta = 0;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command;
                if (tipo == 1)
                {
                    command = new SqlCommand("SELECT TOP 1 ID_SERVICIO FROM AplicacionServicio_Servicio1 WHERE ID_CONTROLADOR=(SELECT ID_CONTROLADOR FROM AplicacionServicio_Controlador WHERE NUMCONTROLADOR = @ESN) ORDER BY ID_SERVICIO DESC;", cnn);
                }
                else
                {
                    command = new SqlCommand("SELECT TOP 1 ID_SERVICIO FROM AplicacionServicio_Servicio1 WHERE ID_MODEM=(SELECT ID_MODEM FROM AplicacionServicio_Modem WHERE NUMMODEM = @ESN) ORDER BY ID_SERVICIO DESC;", cnn);
                }
                command.Parameters.AddWithValue("@ESN", ESN);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID_SERVICIO"] != DBNull.Value)
                    {
                        respuesta = Convert.ToInt32(reader["ID_SERVICIO"]);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return respuesta;
        }

        public static int actualizarServicioControladorEnergizado(int IdServicio, bool validado)
        {
            int respuesta = 0;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Servicio1 SET CONTROLADOR_ENERGIZADO = @VALOR WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                if (validado) command.Parameters.AddWithValue("@VALOR", 1);
                else command.Parameters.AddWithValue("@VALOR", 0);
                respuesta = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return respuesta;
        }

        public static bool VerificarEstadoControladorEnergizadoServicio(int IdServicio)
        {
            bool respuesta = false;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ISNULL(CONTROLADOR_ENERGIZADO,0) AS CONTROLADOR_ENERGIZADO FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["CONTROLADOR_ENERGIZADO"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(reader["CONTROLADOR_ENERGIZADO"]) == 1)
                            respuesta = true;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return respuesta;
        }

        public static int ObtenerAlertaControlador(int Alerta, string controlador)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int respuesta = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) AS ALERTAS_ACTIVAS FROM AplicacionServicio_HistoricoAlertas WHERE ID_ALERTA=@ALERTA AND ID_CONTROLADOR=(SELECT ID_CONTROLADOR FROM AplicacionServicio_Controlador WHERE NUMCONTROLADOR = @ESN) AND ACTIVO=1", cnn);
                command.Parameters.AddWithValue("@ALERTA", Alerta);
                command.Parameters.AddWithValue("@ESN", controlador);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ALERTAS_ACTIVAS"] != DBNull.Value)
                    {
                        respuesta = Convert.ToInt32(reader["ALERTAS_ACTIVAS"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static int GenerarAlertaControlador(int Alerta, string controlador)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int respuesta = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_HistoricoAlertas (ID_ALERTA, ID_CONTROLADOR, FECHA_ALERTA, ACTIVO) VALUES(@ALERTA, (SELECT ID_CONTROLADOR FROM AplicacionServicio_Controlador WHERE NUMCONTROLADOR = @ESN), SYSDATETIME(), 1)", cnn);
                command.Parameters.AddWithValue("@ALERTA", Alerta);
                command.Parameters.AddWithValue("@ESN", controlador);
                respuesta = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static int DeshabilitarAlertaServicio(int Alerta, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int respuesta = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_HistoricoAlertas SET ACTIVO=0 WHERE ID_SERVICIO=@ID_SERVICIO AND ID_ALERTA=@ALERTA", cnn);
                command.Parameters.AddWithValue("@ALERTA", Alerta);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                respuesta = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static int DeshabilitarAlertaControlador(int Alerta, string controlador)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int respuesta = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_HistoricoAlertas SET ACTIVO=0 WHERE ID_CONTROLADOR=(SELECT ID_CONTROLADOR FROM AplicacionServicio_Controlador WHERE NUMCONTROLADOR = @ESN) AND ID_ALERTA=@ALERTA", cnn);
                command.Parameters.AddWithValue("@ALERTA", Alerta);
                command.Parameters.AddWithValue("@ESN", controlador);
                respuesta = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static string ObtenerStringAlertasServicio(int IdServicio = 0)
        {
            SqlConnection cnn;
            List<Clases.CambioReserva> cambios = new List<Clases.CambioReserva>();
            cnn = new SqlConnection(connectionString);
            string alertas = "";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAlertaActivaServicio @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ALERTA"] != DBNull.Value && reader["ALERTA"].ToString() != "")
                    {
                        alertas = reader["ALERTA"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return alertas;
        }

        public static int ObtenerAlertaServicio(int Alerta, int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int respuesta = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) AS ALERTAS_ACTIVAS FROM AplicacionServicio_HistoricoAlertas WHERE ID_ALERTA=@ALERTA AND ID_SERVICIO=@ID_SERVICIO AND ACTIVO=1", cnn);
                command.Parameters.AddWithValue("@ALERTA", Alerta);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ALERTAS_ACTIVAS"] != DBNull.Value)
                    {
                        respuesta = Convert.ToInt32(reader["ALERTAS_ACTIVAS"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static Clases.Servicio ObtenerInfoServicio(int IdServicio)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Servicio servicio = new Clases.Servicio();
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC ConsultoUnServicio @IDSERVICIO=@ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["PAISORIGEN"] != DBNull.Value)
                    {
                        servicio.NombrePaisOrigen = reader["PAISORIGEN"].ToString();
                    }

                    if (reader["CONTENEDOR"] != DBNull.Value)
                    {
                        servicio.Contenedor = reader["CONTENEDOR"].ToString();
                    }

                    if (reader["CONTROLADOR"] != DBNull.Value)
                    {
                        servicio.Controlador = reader["CONTROLADOR"].ToString();
                    }

                    if (reader["COMMODITY"] != DBNull.Value)
                    {
                        servicio.Commodity = reader["COMMODITY"].ToString();
                    }

                    if (reader["SETPOINT"] != DBNull.Value)
                    {
                        servicio.Setpoint = reader["SETPOINT"].ToString();
                    }

                    if (reader["FECHAMODEM"] != DBNull.Value)
                    {
                        servicio.FechaInstModem = Convert.ToDateTime(reader["FECHAMODEM"]);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return servicio;
        }

        public class Encrypt
        {
            public static string GetMD5(string str)
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = md5.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        public static List<Clases.CambioReserva> VerCambiosReserva()
        {
            SqlConnection cnn;
            List<Clases.CambioReserva> cambios = new List<Clases.CambioReserva>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.VerCambiosReserva", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? Fecha = null;
                    DateTime? Fecha2 = null;

                    if (reader["FECHA_CREACION"] != DBNull.Value)
                    {
                        Fecha = Convert.ToDateTime(reader["FECHA_CREACION"]);
                    }

                    if (reader["FECHA_EDICION"] != DBNull.Value)
                    {
                        Fecha2 = Convert.ToDateTime(reader["FECHA_EDICION"]);
                    }

                    cambios.Add(new Clases.CambioReserva
                    {
                        Booking = reader["BOOKING"].ToString(),
                        Item = reader["ITEM"].ToString(),
                        ValorActual = reader["VALOR_ACTUAL"].ToString(),
                        ValorNuevo = reader["VALOR_NUEVO"].ToString(),
                        Estado = Convert.ToInt32(reader["ESTADO"]),
                        Fecha_Creacion = Fecha,
                        Fecha_Edicion = Fecha2
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return cambios;
        }

        public static int AccionCambiosReserva(string Accion, string Booking, string Item, string ValorActual, string ValorEdi)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                string ValorEdi2 = "";

                if (Item == "SETPOINT")
                {
                    string[] prueba = ValorEdi.Split(':');
                    string value1 = prueba[1];
                    string value2 = prueba[2];
                    int index1 = value1.IndexOf('%');
                    int index2 = value2.IndexOf('%');
                    ValorEdi = value1.Substring(1, index1 - 1);
                    ValorEdi2 = value2.Substring(1, index2 - 1);
                }

                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("EXEC AccionCambiosPendientesEdi @ACCION, @BOOKING, @ITEM, @VALORACTUAL, @VALOREDI, @VALOREDI2", cnn);
                command.Parameters.AddWithValue("@ACCION", Accion);
                command.Parameters.AddWithValue("@BOOKING", Booking);
                command.Parameters.AddWithValue("@ITEM", Item);
                command.Parameters.AddWithValue("@VALORACTUAL", ValorActual);
                command.Parameters.AddWithValue("@VALOREDI", ValorEdi);
                command.Parameters.AddWithValue("@VALOREDI2", ValorEdi2);
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public static bool ObtenerValidacionPreZarpe(int IdServicio = 0)
        {
            SqlConnection cnn;
            List<Clases.CambioReserva> cambios = new List<Clases.CambioReserva>();
            cnn = new SqlConnection(connectionString);
            bool respuesta = false;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT VALIDACIONPREZARPE FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO =  @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["VALIDACIONPREZARPE"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(reader["VALIDACIONPREZARPE"]) == 0)
                        {
                            respuesta = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static bool ObtenerAlertaServicio(int IdServicio = 0)
        {
            SqlConnection cnn;
            List<Clases.CambioReserva> cambios = new List<Clases.CambioReserva>();
            cnn = new SqlConnection(connectionString);
            bool retorno = false;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAlertaActivaServicio @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ALERTA"] != DBNull.Value && reader["ALERTA"].ToString() != "")
                    {
                        retorno = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return retorno;
        }

        public static bool ObtenerDamageReportByServicio(int IdServicio = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            bool retorno = false;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarDamageReportByServicio @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["DAMAGE_REPORT"]) == 1)
                    {
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return retorno;
        }

        public static int ObtenerSetpointCO2(int IdServicio = 0)
        {
            SqlConnection cnn;
            List<Clases.CambioReserva> cambios = new List<Clases.CambioReserva>();
            cnn = new SqlConnection(connectionString);
            bool retorno = false;
            int co2 = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT CANTCO2 FROM AplicacionServicio_Setpoint WHERE ID_SETPOINT=(SELECT ID_SETPOINT FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO=@ID_SERVICIO)", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["CANTCO2"] != DBNull.Value && reader["CANTCO2"].ToString() != "")
                    {
                        co2 = Convert.ToInt32(reader["CANTCO2"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return co2;
        }

        public static int ObtenerSetpointO2(int IdServicio = 0)
        {
            SqlConnection cnn;
            List<Clases.CambioReserva> cambios = new List<Clases.CambioReserva>();
            cnn = new SqlConnection(connectionString);
            bool retorno = false;
            int o2 = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT CANTO2 FROM AplicacionServicio_Setpoint WHERE ID_SETPOINT=(SELECT ID_SETPOINT FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO=@ID_SERVICIO)", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["CANTO2"] != DBNull.Value && reader["CANTO2"].ToString() != "")
                    {
                        o2 = Convert.ToInt32(reader["CANTO2"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return o2;
        }


        // Valida que los Status Read del controlador cercanos a la fecha de inst. controlador, posean mediciones dentro de rango de aceptación
        // Solo almacena las mediciones que cumplan regla de aceptación, en caso de que ninguna cumpla regla almacena ultima medición.

        public static Setpoint ValidarGasificado(int IdServicio, string Controlador, string FechasInicio, int SetpointCO2 = 0, int SetpointO2 = 0)
        {
            Setpoint respuesta = new Setpoint();
            MySqlConnection cnn;
            List<Clases.Validacion> Servicios = new List<Clases.Validacion>();
            cnn = new MySqlConnection(connectionStringTecnica);
            string TimeSpan = "";
            string userID = "";
            DateTime fecha_inicio = new DateTime();
            DateTime fecha_termino = new DateTime();
            string appfrom = "";
            double primera_muestra_co2 = 0;
            double primera_muestra_o2 = 0;
            int contador_muestras = 0;
            string usuarioValidacion = HttpContext.Current.Session["User"].ToString().ToUpper();

            if (FechasInicio != "")
            {
                fecha_inicio = Convert.ToDateTime(FechasInicio).AddDays(-5);
                fecha_termino = Convert.ToDateTime(FechasInicio).AddDays(5);
                if (fecha_termino > DateTime.Now)
                {
                    fecha_termino = DateTime.Now;
                }
            }


            try
            {
                cnn.Open();
                string query = "SELECT data FROM `msgProc` WHERE `data` LIKE '%" + Controlador + "%' AND registerDate >= '" + fecha_inicio.ToString("yyyy-MM-dd HH:mm:ss") + "' AND registerDate <= '" + fecha_termino.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY id DESC;";
                MySqlCommand command = new MySqlCommand(query, cnn);
                command.CommandTimeout = 30;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string data = reader["data"].ToString();
                    TimeSpan = "";
                    userID = "";
                    appfrom = "";
                    //usuarioValidacion = "";
                    if (data != "")
                    {
                        dynamic logDes = JsonConvert.DeserializeObject(data);
                        //dynamic logDes = JsonConvert.DeserializeObject("{'TS': '20200727 - 223956','USERID': '','LOCATION': 'SCL','GPS': [-33.4,-70.1],'FROM': 'APP3','VER': 'v1.0.13.3','ESN_MODEM':'886B0FF40962','CELL_DATA':{'CI':85615,'LAC':17502,'MCC':714,'MNC':3},'RSSI':5,'LOG_INFO':{'LOGID':112,'MAC':'886B0F2DBD60','INFO':{'CMD':50,'DGV':3,'ESN':'886B0F2DBD60','IMEI':'0000000000','TRN':5,'DATE':'20200727','TIME':'223928','O2':13568,'CO2':8110,'HUM':0,'PRES':1014,'TEMP':73,'BAT':1000,'MOD':1,'VALV':3,'SCRUB':'2.2.2.2','FWV':'1.6.9.11','HWV':'123B1L1W0','TION':316,'TRTI':316,'NREC':463,'TREC':3853,'TST':1,'CRC':35}}}");
                        if (IsValidJson(Convert.ToString(logDes)))
                        {

                            TimeSpan = logDes.TS;
                            userID = logDes.USERID;
                            appfrom = logDes.FROM;
                        }

                        if (IsValidJson(Convert.ToString(logDes.LOG_INFO)))
                        {
                            dynamic logInfo = JsonConvert.DeserializeObject(Convert.ToString(logDes.LOG_INFO));
                            string numControlador = logInfo.MAC;
                            if (IsValidJson(Convert.ToString(logInfo.INFO)))
                            {
                                dynamic info = JsonConvert.DeserializeObject(Convert.ToString(logInfo.INFO));
                                double co2 = Convert.ToDouble(info.CO2);
                                double o2 = Convert.ToDouble(info.O2);
                                string numC2 = info.ESN;
                                co2 = co2 / 1000;
                                o2 = o2 / 1000;
                                respuesta.CO2 = co2;
                                respuesta.O2 = o2;

                                if (numControlador.Trim() == Controlador.Trim() && appfrom == "APP1")
                                {
                                    if (co2 > 0 && o2 > 0)
                                    {
                                        contador_muestras++;
                                        if (contador_muestras == 1)
                                        {
                                            primera_muestra_co2 = co2;
                                            primera_muestra_o2 = o2;
                                        }

                                        bool origenServicioChile = false;
                                        origenServicioChile = validarOrigenServicioChile(IdServicio);
                                        if (origenServicioChile)
                                        {
                                            if (co2 >= (SetpointCO2 - 1) && o2 <= (SetpointO2 + 1))
                                            {
                                                AlmacenarRegistroGasificacion(IdServicio, co2, o2, userID, TimeSpan);
                                                respuesta.Activo = 1;
                                                break;
                                            }
                                            else
                                            {
                                                respuesta.Activo = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (co2 >= (SetpointCO2 + 2) && o2 <= (SetpointO2 + 1))
                                            {
                                                AlmacenarRegistroGasificacion(IdServicio, co2, o2, userID, TimeSpan);
                                                respuesta.Activo = 1;
                                                break;
                                            }
                                            else
                                            {
                                                respuesta.Activo = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                cnn.Close();

                if (respuesta.Activo == 0)
                {
                    respuesta.CO2 = primera_muestra_co2;
                    respuesta.O2 = primera_muestra_o2;
                    AlmacenarRegistroGasificacion(IdServicio, primera_muestra_co2, primera_muestra_o2, userID, TimeSpan);
                }

            }
            catch (Exception ex)
            {
                if (usuarioValidacion == "CASTORGA" || usuarioValidacion == "MVARAS" || usuarioValidacion == "MGALVEZ" || usuarioValidacion == "JRUIZ")
                {
                    respuesta.Activo = 1;
                }
                else
                {
                    respuesta.Activo = 0;
                }

            }
            finally
            {
                cnn.Close();
            }


            if (usuarioValidacion == "CASTORGA" || usuarioValidacion == "MVARAS" || usuarioValidacion == "MGALVEZ" || usuarioValidacion == "JRUIZ")
            {
                respuesta.Activo = 1;
            }

            return respuesta;
        }

        public static bool validarOrigenServicioChile(int id_servicio)
        {
            bool respuesta = false;

            Clases.Servicio servicio = new Clases.Servicio();
            servicio = ServicioModelo.ConsultarServicioPorId(id_servicio);

            if (servicio.NombrePaisOrigen == "CHILE")
            {
                respuesta = true;
            }

            return respuesta;
        }

        public static Setpoint ValidarGasificadoAdmin(int IdServicio, string Controlador, string FechasInicio, int SetpointCO2 = 0, int SetpointO2 = 0)
        {
            Setpoint respuesta = new Setpoint();
            MySqlConnection cnn;
            List<Clases.Validacion> Servicios = new List<Clases.Validacion>();
            cnn = new MySqlConnection(connectionStringTecnica);
            string TimeSpan = "";
            string userID = "";
            DateTime fecha = new DateTime();
            DateTime fecha_inicio = new DateTime();
            DateTime fecha_termino = new DateTime();
            string appfrom = "";
            string usuarioValidacion = "";

            if (FechasInicio != "")
            {
                fecha = Convert.ToDateTime(FechasInicio); //.ToString("yyyy-MM-dd");
                fecha_inicio = Convert.ToDateTime(FechasInicio).AddDays(-5);
                fecha_termino = Convert.ToDateTime(FechasInicio).AddDays(5);
                if (fecha_termino > DateTime.Now)
                {
                    fecha_termino = DateTime.Now;
                }
            }


            usuarioValidacion = HttpContext.Current.Session["user"].ToString();
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT data FROM `msgProc` WHERE `data` LIKE '%" + Controlador + "%' AND registerDate >= '" + fecha_inicio.ToString("yyyy-MM-dd HH:mm:ss") + "' AND registerDate <= '" + fecha_termino.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY id DESC", cnn);
                command.CommandTimeout = 30;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string data = reader["data"].ToString();
                    TimeSpan = "";
                    userID = "";
                    appfrom = "";
                    usuarioValidacion = "";
                    if (data != "")
                    {
                        dynamic logDes = JsonConvert.DeserializeObject(data);
                        //dynamic logDes = JsonConvert.DeserializeObject("{'TS': '20200727 - 223956','USERID': '','LOCATION': 'SCL','GPS': [-33.4,-70.1],'FROM': 'APP3','VER': 'v1.0.13.3','ESN_MODEM':'886B0FF40962','CELL_DATA':{'CI':85615,'LAC':17502,'MCC':714,'MNC':3},'RSSI':5,'LOG_INFO':{'LOGID':112,'MAC':'886B0F2DBD60','INFO':{'CMD':50,'DGV':3,'ESN':'886B0F2DBD60','IMEI':'0000000000','TRN':5,'DATE':'20200727','TIME':'223928','O2':13568,'CO2':8110,'HUM':0,'PRES':1014,'TEMP':73,'BAT':1000,'MOD':1,'VALV':3,'SCRUB':'2.2.2.2','FWV':'1.6.9.11','HWV':'123B1L1W0','TION':316,'TRTI':316,'NREC':463,'TREC':3853,'TST':1,'CRC':35}}}");
                        if (IsValidJson(Convert.ToString(logDes)))
                        {

                            TimeSpan = logDes.TS;
                            userID = logDes.USERID;
                            appfrom = logDes.FROM;
                        }

                        if (IsValidJson(Convert.ToString(logDes.LOG_INFO)))
                        {
                            dynamic logInfo = JsonConvert.DeserializeObject(Convert.ToString(logDes.LOG_INFO));
                            string numControlador = logInfo.MAC;
                            if (IsValidJson(Convert.ToString(logInfo.INFO)))
                            {
                                dynamic info = JsonConvert.DeserializeObject(Convert.ToString(logInfo.INFO));
                                double co2 = Convert.ToDouble(info.CO2);
                                double o2 = Convert.ToDouble(info.O2);
                                string numC2 = info.ESN;
                                co2 = co2 / 1000;
                                o2 = o2 / 1000;
                                respuesta.CO2 = co2;
                                respuesta.O2 = o2;

                                if (numControlador.Trim() == Controlador.Trim() && appfrom == "APP1")
                                {
                                    if (co2 > 0 && o2 > 0)
                                    {
                                        AlmacenarRegistroGasificacion(IdServicio, co2, o2, userID, TimeSpan);
                                        respuesta.Activo = 1;
                                    }
                                }
                            }
                        }
                    }
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                respuesta.Activo = 1;
            }
            finally
            {
                cnn.Close();
            }

            respuesta.Activo = 1;
            return respuesta;
        }

        public static void AlmacenarRegistroGasificacion(int IdServicio, double co2, double o2, string usuario, string fecha)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            string ano = fecha.Substring(0, 4);
            string mes = fecha.Substring(4, 2);
            string dia = fecha.Substring(6, 2);

            string hora = fecha.Substring(9, 2);
            string minutos = fecha.Substring(11, 2);
            string segundos = fecha.Substring(13, 2);
            DateTime? fechaGas = Convert.ToDateTime(dia + "-" + mes + "-" + ano + " " + hora + ":" + minutos + ":" + segundos);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("INSERT INTO AplicacionServicio_GasificacionServicio(ID_SERVICIO, CO2_GASIFICADO, O2_GASIFICADO, USUARIO_GASIFICADOR, FECHA_GASIFICACION) VALUES(@IDSERVICIO, @CO2, @O2, @USUARIO, @FECHA)", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@CO2", co2);
                command.Parameters.AddWithValue("@O2", o2);
                command.Parameters.AddWithValue("@USUARIO", usuario);
                command.Parameters.AddWithValue("@FECHA", fechaGas);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static int ObtenerEstadoControladorAppTecnica(string Controlador)
        {
            int estado_controlador = 0;
            MySqlConnection cnn;
            cnn = new MySqlConnection(connectionStringTecnica);

            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT a.idControllerStatus AS idControllerStatus FROM prometeo.logistic a WHERE a.idController = (SELECT id FROM prometeo.controller WHERE esn = '" + Controlador + "' ORDER BY id DESC LIMIT 1) ORDER BY a.id DESC LIMIT 1;", cnn);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    estado_controlador = Convert.ToInt32(reader["idControllerStatus"]); // 1: SLEEP, 2: TRAVELING, 3: OFF, 4: OUT OF ORDER
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                cnn.Close();
            }

            return estado_controlador;
        }

        public static int ObtenerEstadoModemAppTecnica(string Modem)
        {
            int estado_modem = 0;
            MySqlConnection cnn;
            cnn = new MySqlConnection(connectionStringTecnica);

            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT a.idModemStatus AS idModemStatus FROM prometeo.logisticModem a WHERE a.idModem = (SELECT id FROM prometeo.modem WHERE esn = '" + Modem + "' ORDER BY id DESC LIMIT 1) ORDER BY a.id DESC LIMIT 1;", cnn);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    estado_modem = Convert.ToInt32(reader["idModemStatus"]); // 1: STORAGE, 2: PRE SERVICE, 3: ON SERVICE TRIP, 4: RETRIEVAL, 5: LABORATORY
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                cnn.Close();
            }

            return estado_modem;
        }

        public static bool ComprobarConexionModemControlador(string Controlador, string Modem, string Contenedor, string FechasInicio)
        {
            bool ConexionControladorModem = false;
            MySqlConnection cnn;
            cnn = new MySqlConnection(connectionStringTecnica);
            string TimeSpan = "";
            string userID = "";
            string fecha = "";
            string appfrom = "";
            string usuarioValidacion = "";
            string modemAsociado = "";
            DateTime fecha_actual = DateTime.Now;
            DateTime fecha_inicio = fecha_actual.AddDays(-2);

            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT data FROM `msgProc` WHERE log='112' AND data LIKE '%" + Controlador.Trim() + "%' AND registerDate >= '" + fecha_inicio.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY id DESC", cnn);
                command.CommandTimeout = 30;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string data = reader["data"].ToString();
                    TimeSpan = "";
                    userID = "";
                    appfrom = "";
                    modemAsociado = "";

                    if (data != "")
                    {
                        dynamic logDes = JsonConvert.DeserializeObject(data);
                        //dynamic logDes = JsonConvert.DeserializeObject("{'TS': '20200727 - 223956','USERID': '','LOCATION': 'SCL','GPS': [-33.4,-70.1],'FROM': 'APP3','VER': 'v1.0.13.3','ESN_MODEM':'886B0FF40962','CELL_DATA':{'CI':85615,'LAC':17502,'MCC':714,'MNC':3},'RSSI':5,'LOG_INFO':{'LOGID':112,'MAC':'886B0F2DBD60','INFO':{'CMD':50,'DGV':3,'ESN':'886B0F2DBD60','IMEI':'0000000000','TRN':5,'DATE':'20200727','TIME':'223928','O2':13568,'CO2':8110,'HUM':0,'PRES':1014,'TEMP':73,'BAT':1000,'MOD':1,'VALV':3,'SCRUB':'2.2.2.2','FWV':'1.6.9.11','HWV':'123B1L1W0','TION':316,'TRTI':316,'NREC':463,'TREC':3853,'TST':1,'CRC':35}}}");
                        if (IsValidJson(Convert.ToString(logDes)))
                        {
                            TimeSpan = logDes.TS;
                            userID = logDes.USERID;
                            appfrom = logDes.FROM;
                            modemAsociado = logDes.ESN_MODEM;
                        }

                        if (IsValidJson(Convert.ToString(logDes.LOG_INFO)))
                        {
                            dynamic logInfo = JsonConvert.DeserializeObject(Convert.ToString(logDes.LOG_INFO));
                            string numControlador = logInfo.MAC;
                            if (IsValidJson(Convert.ToString(logInfo.INFO)))
                            {
                                dynamic info = JsonConvert.DeserializeObject(Convert.ToString(logInfo.INFO));
                                double co2 = Convert.ToDouble(info.CO2);
                                double o2 = Convert.ToDouble(info.O2);
                                string numC2 = info.ESN;
                                co2 = co2 / 1000;
                                o2 = o2 / 1000;

                                if (numControlador.Trim() == Controlador.Trim() && appfrom == "APP3" && modemAsociado == Modem.Trim())
                                {
                                    ConexionControladorModem = true;
                                    return ConexionControladorModem;
                                }
                            }

                        }


                    }

                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                cnn.Close();
            }

            return ConexionControladorModem;
        }

        public static Clases.LogisticModem ObtenerInfoModem(string NumModem)
        {
            MySqlConnection cnn;
            Clases.LogisticModem infoModem = new Clases.LogisticModem();

            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT a.Container_modem, a.Container_ctrl, a.ptlLastComm, a.ctrlLastComm FROM prometeo.logisticModem a WHERE a.idModem = (SELECT id FROM prometeo.modem WHERE esn = @Modem ORDER BY id DESC LIMIT 1) ORDER BY a.id DESC LIMIT 1;", cnn);
                command.Parameters.Add("@Modem", MySqlDbType.VarChar, 50).Value = NumModem;
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    infoModem.NumModem = NumModem;
                    infoModem.Contenedor_modem = reader["Container_modem"].ToString();
                    infoModem.Contenedor_controlador = reader["Container_ctrl"].ToString();
                    infoModem.FechaUltConexionModemPTL = Convert.ToDateTime(reader["ptlLastComm"]);
                    infoModem.FechaUltConexionModemController = Convert.ToDateTime(reader["ctrlLastComm"]);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return infoModem;
        }

        public static Clases.InfoServicioPTL ObtenerInfoServicioPTL_SegunControlador(string NumControlador)
        {
            MySqlConnection cnn;
            Clases.InfoServicioPTL infoServicio = new Clases.InfoServicioPTL();

            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                string query = "select IFNULL(prometeo.controller.esn,'') AS esn, " +
                    "prometeo.modem.esn AS modem, prometeo.service.id AS id, " +
                    "IFNULL(prometeo.service.bookingNumber, '') AS bookingNumber, " +
                    "IFNULL(prometeo.service.idComoditie, 0) AS idComoditie, " +
                    "IFNULL(prometeo.service.idOriginPort, 0) AS OriginPort, " +
                    "concat(prometeo.container.prefix, prometeo.service.containerNumber) as Contenedor, " +
                    "DATE_FORMAT(prometeo.service.travelStartDate, '%Y-%m-%d') as travelStartDate, " +
                    "prometeo.service.lastDataDownload as lastDataDownload, " +
                    "prometeo.service.setPointO2, prometeo.service.setPointCO2Ventilacion, prometeo.service.setPointCO2Scrubber, " +
                    "prometeo.service.faultAnalysisResult AS faultAnalysisResult " +
                    "from(prometeo.service " +
                    "INNER JOIN prometeo.logistic ON prometeo.service.id = prometeo.logistic.idService " +
                    "INNER JOIN prometeo.controller ON prometeo.logistic.idController = prometeo.controller.id " +
                    "INNER JOIN prometeo.container ON prometeo.service.idContainer = prometeo.container.id) " +
                    "LEFT JOIN prometeo.modem ON prometeo.logistic.idModem = prometeo.modem.id " +
                    "WHERE prometeo.service.travelStartDate >= CAST('2018-12-31' AS DATE) " +
                    "AND prometeo.controller.esn = @CONTROLADOR";

                cnn.Open();
                MySqlCommand command = new MySqlCommand(query, cnn);
                command.Parameters.Add("@CONTROLADOR", MySqlDbType.VarChar, 50).Value = NumControlador;
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["id"] != DBNull.Value)
                    {
                        infoServicio.id = Convert.ToInt32(reader["id"]);
                    }
                    if (reader["faultAnalysisResult"] != DBNull.Value)
                    {
                        infoServicio.faultAnalysisResult = reader["faultAnalysisResult"].ToString();
                    }
                    if (reader["lastDataDownload"] != DBNull.Value)
                    {
                        infoServicio.lastDataDownload = Convert.ToDateTime(reader["lastDataDownload"]);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return infoServicio;
        }

        public static List<Clases.ResultadoFaultAnalysisPTL> ObtenerResultadosFaultAnalysisPTL(int id_servicio_ptl)
        {
            MySqlConnection cnn;
            List<Clases.ResultadoFaultAnalysisPTL> lista_ResultadoFaultAnalysisPTL = new List<Clases.ResultadoFaultAnalysisPTL>();

            cnn = new MySqlConnection(connectionStringTecnica);
            try
            {
                string query = "SELECT * FROM `analysisResults` WHERE `idService` = @ID_SERVICIO";

                cnn.Open();
                MySqlCommand command = new MySqlCommand(query, cnn);
                command.Parameters.Add("@ID_SERVICIO", MySqlDbType.Int32).Value = id_servicio_ptl;
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Clases.ResultadoFaultAnalysisPTL resultado = new Clases.ResultadoFaultAnalysisPTL();

                    if (reader["id"] != DBNull.Value)
                    {
                        resultado.id = Convert.ToInt32(reader["id"]);
                    }
                    if (reader["idSampleClasification"] != DBNull.Value)
                    {
                        resultado.idSampleClasification = Convert.ToInt32(reader["idSampleClasification"]);
                    }
                    if (reader["nrStretch"] != DBNull.Value)
                    {
                        resultado.nrStretch = Convert.ToInt32(reader["nrStretch"]);
                    }
                    if (reader["faultTime"] != DBNull.Value)
                    {
                        resultado.faultTime = float.Parse(reader["faultTime"].ToString());
                    }
                    if (reader["faultTimePercent"] != DBNull.Value)
                    {
                        resultado.faultTimePercent = float.Parse(reader["faultTimePercent"].ToString());
                    }
                    if (reader["analysisDate"] != DBNull.Value)
                    {
                        resultado.analysisDate = Convert.ToDateTime(reader["analysisDate"]);
                    }
                    if (reader["faultCode"] != DBNull.Value)
                    {
                        resultado.faultCode = Convert.ToInt32(reader["faultCode"]);
                    }
                    if (reader["idCodeResult"] != DBNull.Value)
                    {
                        resultado.idCodeResult = Convert.ToInt32(reader["idCodeResult"]);
                    }
                    if (reader["faultResult"] != DBNull.Value)
                    {
                        resultado.faultResult = reader["faultResult"].ToString();
                    }

                    lista_ResultadoFaultAnalysisPTL.Add(resultado);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return lista_ResultadoFaultAnalysisPTL;
        }

        public static int ActualizarValidacionPrezarpeServicio(int Valor, int IdServicio)
        {
            int respuesta = 0;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_SERVICIO1 SET VALIDACIONPREZARPE=@VALIDACIONPREZARPE, FECHAVALIDACIONPREZARPE = SYSDATETIME(), USUARIOACCION = @USUARIO WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@VALIDACIONPREZARPE", Valor);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                respuesta = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            if (Valor == 0)
            {
                DeshabilitarAlertaServicio(37, IdServicio);
            }

            return respuesta;
        }

        public static int ObtenerScrubberServicio(int IdServicio)
        {
            int respuesta = -1;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT SCRUBBER FROM APLICACIONSERVICIO_SERVICIO1 WHERE ID_SERVICIO = @IDSERVICIO", cnn);
                command.Parameters.AddWithValue("@IDSERVICIO", IdServicio);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["SCRUBBER"] != DBNull.Value)
                    {
                        respuesta = Convert.ToInt32(reader["SCRUBBER"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }

        public static int EnviarCorreoAlertaScrubber(int IdServicio, string contenedor, string commodity, string setpoint)
        {
            int resultado_correo = 0;
            string Tabla = "";
            Tabla += "<tr>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + IdServicio.ToString() + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + contenedor + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + commodity + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse; text-align:center'>" + setpoint + "</td>" +
                    "</tr>";

            string Correo = "<p>Estimados,</p>" +
                            "<p>Se ha ingresado un contenedor al cual no se le registró Scrubber en su leaktest, sin embargo el servicio posee características de un servicio con Scrubber.</p>" +
                            "<p>A continuación los datos del servicio: </p>" +
                            "<table style='border: 1px solid black; border-collapse: collapse;  width:100%'>" +
                                "<thead >" +
                                    "<tr>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>ID SERVICIO</ th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>CONTENEDOR</th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>COMMODITY</th>" +
                                        "<th style='border: 1px solid black; border-collapse: collapse; text-align:center'>SETPOINT</th>" +
                                    "</tr>" +
                                "</thead>" +
                                "<tbody>" + Tabla +
                                "</tbody>" +
                            "</table>" +
                            "<p>" + "Favor realizar las gestiones pertinentes." + "</p>";

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add("mvaras@liventusglobal.com");
            mmsg.To.Add("mgalvez@liventusglobal.com");
            mmsg.To.Add("castorga@liventusglobal.com");
            mmsg.To.Add("jruiz@liventusglobal.com");
            mmsg.Bcc.Add("baros@liventusglobal.com");
            mmsg.Bcc.Add("rcontreras@liventusglobal.com");

            //Asunto
            mmsg.Subject = "Alerta automática - Contenedor sin Scrubber - Liventus S.A.";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            mmsg.Body = Correo;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;
            mmsg.From = new System.Net.Mail.MailAddress("appservicios@liventusglobal.com");
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential("appservicios@liventusglobal.com", "Huc01455");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "outlook.office365.com";

            try
            {
                cliente.Send(mmsg);
                resultado_correo = 1;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                resultado_correo = 0;
            }

            return resultado_correo;
        }

        public static int ValidacionScrubber(int IdServicio, string Contenedor)
        {
            int resultado = 0;

            int scrubber_servicio = ObtenerScrubberServicio(IdServicio);

            if (scrubber_servicio == 1) // SERVICIO DEBE POSEER SCRUBBER
            {
                Clases.Contenedor info_cont = ContenedorModelo.GetInfoContenedor(Contenedor);

                //Obtener campo SCRUBBER del resultado del leaktest del contenedor
                List<Clases.ReservaResultadoLeaktest> resultado_leaktest = LeaktestModelo.GetResultadoLeaktestByIdContenedor(info_cont.IdContenedor);

                int resultado_scrubber_leaktest = resultado_leaktest[0].Scrubber;
                if (resultado_scrubber_leaktest == 1) // SERVICIO SIN REGISTRO SCRUBBER EN LEAKTEST
                {
                    int cantidad_alertas_scrubber = ObtenerAlertaServicio(40, IdServicio);
                    if (cantidad_alertas_scrubber == 0)
                    {
                        resultado = 1;
                        int respuesta_alerta = GenerarAlertaServicio(40, IdServicio);
                        Clases.Servicio info_serv = ObtenerInfoServicio(IdServicio);
                        int respuesta_correo = EnviarCorreoAlertaScrubber(IdServicio, Contenedor, info_serv.Commodity, info_serv.Setpoint);
                    }
                }
            }
            else
            {
                int cantidad_alertas_scrubber = ObtenerAlertaServicio(40, IdServicio);
                if (cantidad_alertas_scrubber != 0)
                {
                    DeshabilitarAlertaServicio(40, IdServicio);
                }
            }
            return resultado;
        }

    }
}