using Plataforma.Models.Bateria;
using Plataforma.Models.Contenedor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Leaktest
{
    public class LeaktestModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearLeaktest(int Naviera, int Deposito, DateTime? Eta, int Estado, int Maquinaria, int cantidad, int Maquinaria1, int cantidad1, int Maquinaria2, int cantidad2, int Maquinaria3, int cantidad3, int tiporeserva, string Hora, int CantidadScrubber, string Comentario)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            int Scrubber = 0;

            if (CantidadScrubber != 0) {
                Scrubber = 0;
            }else
            {
                Scrubber = 1;
            }

            SqlCommand scCommand = new SqlCommand("IngresarReservaLeaktest", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDDEPOSITO", SqlDbType.Int, 50).Value = Deposito;
            scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Naviera;
            scCommand.Parameters.Add("@FECHAESTIMADA", SqlDbType.DateTime, 50).Value = Eta;
            scCommand.Parameters.Add("@ESTADO", SqlDbType.Int, 50).Value = Estado;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@IDMAQUINARIA", SqlDbType.Int, 50).Value = Maquinaria;
            scCommand.Parameters.Add("@CANTIDAD", SqlDbType.Int, 50).Value = cantidad;
            scCommand.Parameters.Add("@IDMAQUINARIA1", SqlDbType.Int, 50).Value = Maquinaria1;
            scCommand.Parameters.Add("@CANTIDAD1", SqlDbType.Int, 50).Value = cantidad1;
            scCommand.Parameters.Add("@IDMAQUINARIA2", SqlDbType.Int, 50).Value = Maquinaria2;
            scCommand.Parameters.Add("@CANTIDAD2", SqlDbType.Int, 50).Value = cantidad2;
            scCommand.Parameters.Add("@IDMAQUINARIA3", SqlDbType.Int, 50).Value = Maquinaria3;
            scCommand.Parameters.Add("@CANTIDAD3", SqlDbType.Int, 50).Value = cantidad3;
            scCommand.Parameters.Add("@TIPORESERVA", SqlDbType.Int, 50).Value = tiporeserva;
            scCommand.Parameters.Add("@HORA", SqlDbType.VarChar, 100).Value = Hora;
            scCommand.Parameters.Add("@CANTIDADSCRUBBER", SqlDbType.VarChar, 100).Value = CantidadScrubber;
            scCommand.Parameters.Add("@ESTADOSCRUBBER", SqlDbType.VarChar, 100).Value = Scrubber;
            scCommand.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 500).Value = Comentario;
            scCommand.Parameters.Add("@IDRESERVA", SqlDbType.Int).Direction = ParameterDirection.Output;


            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                scCommand.ExecuteNonQuery();
                result = Convert.ToInt32(scCommand.Parameters["@IDRESERVA"].Value);
                if (result == 0)
                {
                    return -1;
                }
                else {
                    return result;
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

        public static Clases.ReservaLeaktestCompleta GetLeaktest()
        {
            SqlConnection cnn;
            Clases.ReservaLeaktestCompleta Reservas = new Clases.ReservaLeaktestCompleta();
            
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarSolicitudesLeaktest", cnn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? fechaEstimada = null;

                    if (reader["FECHAESTIMADA"] != DBNull.Value)
                    {
                        fechaEstimada = Convert.ToDateTime(reader["FECHAESTIMADA"]);
                    }

                    Reservas.Reservas.Add(new Clases.ReservaLeaktest
                    {
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreNaviera = reader["NOMBRENAVIERA"].ToString(),
                        TipoReserva = reader["TIPORESERVA"].ToString(),
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        FechaEstimadaRealizacion = fechaEstimada,
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Hora = reader["HORA"].ToString(),
                        CantidadScrubber = Convert.ToInt32(reader["CANTIDADSCRUBBER"]),
                        Comentario = reader["COMENTARIO"].ToString(),
                    });
                    Reservas.CantidadTotal.Add(Convert.ToInt32(reader["CANTIDADPORAPROBAR"]));
                    Reservas.CantidadPorContenedores.Add(Convert.ToInt32(reader["CANTIDADPORCONTENEDOR"]));
                    Reservas.Ralizados.Add(Convert.ToInt32(reader["REALIZADOS"]));
                    Reservas.Estado.Add(reader["ESTADO"].ToString());
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

        public static Clases.ReservaLeaktestCompleta GetInfoSolicitudLeaktest(int IdSolicitud)
        {
            SqlConnection cnn;
            Clases.ReservaLeaktestCompleta Reservas = new Clases.ReservaLeaktestCompleta();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA,APLICACIONSERVICIO_RESERVALEAKTEST.HORA,APLICACIONSERVICIO_RESERVALEAKTEST.CANTIDADSCRUBBER,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAESTIMADA,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAREGISTRO,APLICACIONSERVICIO_NAVIERA.NOMBRE AS NOMBRENAVIERA,APLICACIONSERVICIO_DEPOSITO.NOMBRE AS NOMBREDEPOSITO,APLICACIONSERVICIO_CIUDAD.NOMBRE AS NOMBRECIUDAD,APLICACIONSERVICIO_PAIS.NOMBRE AS NOMBREPAIS,APLICACIONSERVICIO_ESTADORESERVALEAKTEST.DESCRIPCION AS ESTADO,APLICACIONSERVICIO_TIPORESERVALEAKTEST.DESCRIPCION AS TIPORESERVA,(SELECT ISNULL(SUM(APLICACIONSERVICIO_DETALLERESERVALEAKTEST.CANTIDAD),0) FROM APLICACIONSERVICIO_DETALLERESERVALEAKTEST WHERE APLICACIONSERVICIO_DETALLERESERVALEAKTEST.ID_RESERVA = APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA) AS CANTIDADPORABPROBAR, (SELECT ISNULL(COUNT(*),0) FROM APLICACIONSERVICIO_DETALLERESERVACONTENEDOR WHERE APLICACIONSERVICIO_DETALLERESERVACONTENEDOR.ID_RESERVA = APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA) AS CANTIDADPORCONTENEDOR FROM APLICACIONSERVICIO_RESERVALEAKTEST,APLICACIONSERVICIO_TIPORESERVALEAKTEST,APLICACIONSERVICIO_NAVIERA,APLICACIONSERVICIO_DEPOSITO,APLICACIONSERVICIO_CIUDAD,APLICACIONSERVICIO_PAIS,APLICACIONSERVICIO_ESTADORESERVALEAKTEST WHERE APLICACIONSERVICIO_RESERVALEAKTEST.ID_NAVIERA = APLICACIONSERVICIO_NAVIERA.ID_NAVIERA AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_DEPOSITO = APLICACIONSERVICIO_DEPOSITO.ID_DEPOSITO AND APLICACIONSERVICIO_DEPOSITO.ID_CIUDAD = APLICACIONSERVICIO_CIUDAD.ID_CIUDAD AND APLICACIONSERVICIO_CIUDAD.ID_PAIS = APLICACIONSERVICIO_PAIS.ID_PAIS AND APLICACIONSERVICIO_RESERVALEAKTEST.ESTADO = APLICACIONSERVICIO_ESTADORESERVALEAKTEST.ID_ESTADO AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_TIPORESERVA = APLICACIONSERVICIO_TIPORESERVALEAKTEST.ID_TIPORESERVA AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA = @IDRESERVA ORDER BY APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA ASC;", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdSolicitud);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Reservas.Reservas.Add(new Clases.ReservaLeaktest
                    {
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreNaviera = reader["NOMBRENAVIERA"].ToString(),
                        TipoReserva = reader["TIPORESERVA"].ToString(),
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        FechaEstimadaRealizacion = Convert.ToDateTime(reader["FECHAESTIMADA"]),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Hora = reader["HORA"].ToString(),
                        CantidadScrubber = Convert.ToInt32(reader["CANTIDADSCRUBBER"]),
                    });
                    Reservas.CantidadTotal.Add(Convert.ToInt32(reader["CANTIDADPORABPROBAR"]));
                    Reservas.CantidadPorContenedores.Add(Convert.ToInt32(reader["CANTIDADPORCONTENEDOR"]));
                    Reservas.Estado.Add(reader["ESTADO"].ToString());
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



        public static Clases.ReservaLeaktestCompleta GetInfoSolicitudLeaktestSP(int IdSolicitud)
        {
            SqlConnection cnn;
            Clases.ReservaLeaktestCompleta Reservas = new Clases.ReservaLeaktestCompleta();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_RESERVA, (SELECT NOMBRE FROM AplicacionServicio_ServiceProvider WHERE ID_SERVICEPROVIDER=R.ID_SERVICEPROVIDER) AS NOMBRESERVICEPROVIDER,(SELECT NOMBRE FROM AplicacionServicio_Naviera WHERE ID_NAVIERA = R.ID_NAVIERA) AS NOMBRENAVIERA,(SELECT NOMBRE FROM AplicacionServicio_Deposito WHERE ID_DEPOSITO = R.ID_DEPOSITO) AS NOMBREDEPOSITO FROM AplicacionServicio_ResultadoLeaktestSP R WHERE ID_RESERVA = @ID_RESERVA;", cnn);
                command.Parameters.AddWithValue("@ID_RESERVA", IdSolicitud);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Reservas.Reservas.Add(new Clases.ReservaLeaktest
                    {
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreNaviera = reader["NOMBRENAVIERA"].ToString(),
                        NombreServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString(),
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
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

        public static List<Clases.ReservaResultadoLeaktest> GetResultadoLeaktestByContenedor(int IdServicio)
        {
            SqlConnection cnn;
            List<Clases.ReservaResultadoLeaktest> Resultados = new List<Clases.ReservaResultadoLeaktest>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarResultadoByContenedor @ID_SERVICIO", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", IdServicio);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Resultados.Add(new Clases.ReservaResultadoLeaktest
                    {
                        IdResultado = Convert.ToInt32(reader["ID_LEAKTEST"]),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Deposito= reader["DEPOSITO"].ToString(),
                        Naviera=reader["NAVIERA"].ToString(),
                        FechaReserva=Convert.ToDateTime(reader["FECHAESTIMADA"]),
                        FechaRealizacion= Convert.ToDateTime(reader["FECHAREALIZACION"]),
                        Maquinaria=reader["MAQUINARIA"].ToString(),
                        Contenedor=reader["CONTENEDOR"].ToString(),
                        EstadoResultado =Convert.ToInt32(reader["ESTADO_RESULTADO"]),
                        Tiempo= reader["TIEMPO"].ToString(),
                        Tecnico = reader["TECNICO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString()
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
            return Resultados;
        }

        public static List<Clases.ReservaResultadoLeaktest> GetResultadoLeaktestByIdContenedor(int IdContenedor)
        {
            SqlConnection cnn;
            List<Clases.ReservaResultadoLeaktest> Resultados = new List<Clases.ReservaResultadoLeaktest>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarResultadoByIdContenedor @ID_CONTENEDOR", cnn);
                command.Parameters.AddWithValue("@ID_CONTENEDOR", IdContenedor);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Resultados.Add(new Clases.ReservaResultadoLeaktest
                    {
                        IdResultado = Convert.ToInt32(reader["ID_LEAKTEST"]),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Deposito = reader["DEPOSITO"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        FechaReserva = Convert.ToDateTime(reader["FECHAESTIMADA"]),
                        FechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]),
                        Maquinaria = reader["MAQUINARIA"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        EstadoResultado = Convert.ToInt32(reader["ESTADO_RESULTADO"]),
                        Tiempo = reader["TIEMPO"].ToString(),
                        Tecnico = reader["TECNICO"].ToString(),
                        ServiceProvider = reader["SERVICEPROVIDER"].ToString(),
                        Scrubber = Convert.ToInt32(reader["SCRUBBER"])
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
            return Resultados;
        }

        public static Clases.ReservaLeaktest GetInfoReservaId(int IdReserva) {

            SqlConnection cnn;
            Clases.ReservaLeaktest ReservaLeaktest = new Clases.ReservaLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_ReservaLeaktest WHERE AplicacionServicio_ReservaLeaktest.ID_RESERVA = @IDRESERVA", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReservaLeaktest.Id = Convert.ToInt32(reader["ID_RESERVA"]);
                    ReservaLeaktest.IdDeposito = Convert.ToInt32(reader["ID_DEPOSITO"]);
                    ReservaLeaktest.IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]);
                    ReservaLeaktest.FechaEstimadaRealizacion = Convert.ToDateTime(reader["FECHAESTIMADA"]);
                    ReservaLeaktest.IdEstado = Convert.ToInt32(reader["ESTADO"]);
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
            return ReservaLeaktest;
        }

        public static int CancelarReservaLeaktest(int IdReserva, string Motivo, string TipoReserva)
        {
            if (TipoReserva == "POR CONTENEDORES" || TipoReserva == "BY CONTAINER NUMBER") {
                int validacion = CancelarReservaLeaktestContenedores(IdReserva, Motivo);

                if (validacion == 0)
                {
                    return 0;
                }
                else {
                    return 1;
                }
            }
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_ReservaLeaktest SET ESTADO = 3, MOTIVO = @MOTIVO WHERE ID_RESERVA = @IDRESERVA", cnn);
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

        public static Clases.DetalleReservaLeaktest GetDetalleReservaLeaktest(int IdReserva)
        {

            SqlConnection cnn;
            Clases.DetalleReservaLeaktest ReservaLeaktest = new Clases.DetalleReservaLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT APLICACIONSERVICIO_RESERVALEAKTEST.MOTIVO,APLICACIONSERVICIO_RESERVALEAKTEST.CANTIDADSCRUBBER, APLICACIONSERVICIO_DETALLERESERVALEAKTEST.CANTIDAD, APLICACIONSERVICIO_DETALLERESERVALEAKTEST.REALIZADOS, APLICACIONSERVICIO_MAQUINARIA.NOMBRE AS NOMBREMAQUINARIA, (SELECT COUNT(*) FROM APLICACIONSERVICIO_RESULTADOLEAKTEST WHERE ID_ESTADO = 0 AND ID_RESERVA = @IDRESERVA AND APLICACIONSERVICIO_RESULTADOLEAKTEST.ID_MAQUINARIA = APLICACIONSERVICIO_MAQUINARIA.ID_MAQUINARIA) AS APROBADOS, (SELECT COUNT(*) + (SELECT COUNT(*) FROM APLICACIONSERVICIO_RESULTADOLEAKTEST WHERE ID_ESTADO = 3 AND ID_RESERVA = @IDRESERVA AND APLICACIONSERVICIO_RESULTADOLEAKTEST.ID_MAQUINARIA = APLICACIONSERVICIO_MAQUINARIA.ID_MAQUINARIA) AS RECHAZADOSVISUAL FROM APLICACIONSERVICIO_RESULTADOLEAKTEST WHERE ID_ESTADO = 1 AND ID_RESERVA = @IDRESERVA AND APLICACIONSERVICIO_RESULTADOLEAKTEST.ID_MAQUINARIA = APLICACIONSERVICIO_MAQUINARIA.ID_MAQUINARIA) AS RECHAZADOS FROM APLICACIONSERVICIO_DETALLERESERVALEAKTEST, APLICACIONSERVICIO_MAQUINARIA, APLICACIONSERVICIO_RESERVALEAKTEST WHERE APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA = @IDRESERVA AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA = APLICACIONSERVICIO_DETALLERESERVALEAKTEST.ID_RESERVA AND APLICACIONSERVICIO_DETALLERESERVALEAKTEST.ID_MAQUINARIA = APLICACIONSERVICIO_MAQUINARIA.ID_MAQUINARIA; ", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReservaLeaktest.NombreMaquinaria.Add(reader["NOMBREMAQUINARIA"].ToString());
                    ReservaLeaktest.Cantidad.Add(Convert.ToInt32(reader["CANTIDAD"]));
                    ReservaLeaktest.Realizados.Add(Convert.ToInt32(reader["REALIZADOS"]));
                    ReservaLeaktest.IdReserva.Add(0);
                    ReservaLeaktest.Motivo = reader["MOTIVO"].ToString();
                    ReservaLeaktest.Aprobados.Add(Convert.ToInt32(reader["APROBADOS"]));
                    ReservaLeaktest.Rechazados.Add(Convert.ToInt32(reader["RECHAZADOS"]));
                    ReservaLeaktest.cantidadscrubber = Convert.ToInt32(reader["CANTIDADSCRUBBER"]);
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
            return ReservaLeaktest;
        }

        public static Clases.DetalleReservaLeaktest GetDetalleReservasLeaktest()
        {

            SqlConnection cnn;
            Clases.DetalleReservaLeaktest ReservaLeaktest = new Clases.DetalleReservaLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_DetalleReservaLeaktest.*, AplicacionServicio_Maquinaria.NOMBRE AS NOMBREMAQUINARIA FROM AplicacionServicio_DetalleReservaLeaktest, AplicacionServicio_Maquinaria WHERE AplicacionServicio_DetalleReservaLeaktest.ID_MAQUINARIA = AplicacionServicio_Maquinaria.ID_MAQUINARIA", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReservaLeaktest.NombreMaquinaria.Add(reader["NOMBREMAQUINARIA"].ToString());
                    ReservaLeaktest.Cantidad.Add(Convert.ToInt32(reader["CANTIDAD"]));
                    ReservaLeaktest.Realizados.Add(Convert.ToInt32(reader["REALIZADOS"]));
                    ReservaLeaktest.IdReserva.Add(Convert.ToInt32(reader["ID_RESERVA"]));
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
            return ReservaLeaktest;
        }

        public static Clases.ResultadosLeaktest GetResultadosLeaktest(int IdReserva)
        {

            SqlConnection cnn;
            Clases.ResultadosLeaktest Resultado = new Clases.ResultadosLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetResultadosLeaktest @IDRESERVA", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.ResultadoLeaktest res = new Clases.ResultadoLeaktest();
                    if (reader["ID_LEAKTEST"] != DBNull.Value) res.Id = Convert.ToInt32(reader["ID_LEAKTEST"]);
                    if (reader["ID_RESERVA"] != DBNull.Value) res.IdReserva = Convert.ToInt32(reader["ID_RESERVA"]);
                    if (reader["ID_ESTADO"] != DBNull.Value) res.Estado = Convert.ToInt32(reader["ID_ESTADO"]);
                    if (reader["FECHAREALIZACION"] != DBNull.Value) res.FechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    if (reader["NOMBRESERVICEPROVIDER"] != DBNull.Value) res.IdServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString();
                    if (reader["TIEMPO"] != DBNull.Value) res.Tiempo = reader["TIEMPO"].ToString();
                    if (reader["PRESION"] != DBNull.Value)
                    {
                        res.Presion = float.Parse(reader["PRESION"].ToString().Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (reader["NOMBREUSUARIO"] != DBNull.Value) res.IdTecnico = reader["NOMBREUSUARIO"].ToString();
                    if (reader["CONTENEDOR"] != DBNull.Value) res.Contenedor = reader["CONTENEDOR"].ToString();
                    if (reader["ANOCONTENEDOR"] != DBNull.Value) res.AnoContenedor = Convert.ToInt32(reader["ANOCONTENEDOR"]);
                    if (reader["NOMBRECAJA"] != DBNull.Value) res.CajaContenedor = reader["NOMBRECAJA"].ToString();
                    if (reader["NOMBREMAQUINARIA"] != DBNull.Value) res.IdMaquinaria = reader["NOMBREMAQUINARIA"].ToString();
                    if (reader["CONTROLADOR"] != DBNull.Value) res.Controlador = reader["CONTROLADOR"].ToString();
                    if (reader["BATERIA"] != DBNull.Value) res.Bateria = reader["BATERIA"].ToString();
                    if (reader["SCRUBBER"] != DBNull.Value) res.Scrubber = Convert.ToInt32(reader["SCRUBBER"]);
                    if (reader["SELLOPERNO1"] != DBNull.Value) res.Selloperno1 = reader["SELLOPERNO1"].ToString();
                    if (reader["SELLOPERNO2"] != DBNull.Value) res.Selloperno2 = reader["SELLOPERNO2"].ToString();
                    if (reader["SELLOTAPA"] != DBNull.Value) res.Sellotapa = reader["SELLOTAPA"].ToString();
                    if (reader["COMENTARIO"] != DBNull.Value) res.Comentario = reader["COMENTARIO"].ToString();
                    if (reader["KIT_CORTINA"] != DBNull.Value) res.KitCortina = Convert.ToInt32(reader["KIT_CORTINA"]);
                    if (reader["MODEM"] != DBNull.Value) res.Modem = reader["MODEM"].ToString();
                    if (reader["BATERIA_MODEM"] != DBNull.Value) res.BateriaModem = reader["BATERIA_MODEM"].ToString();
                    Resultado.Resultados.Add(res);
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
            return Resultado;
        }

        public static int CrearResultadoLeaktest(string contenedor, int cajacontenedor, int anocontenedor, int serviceprovider, int tecnico, float presion, DateTime? fechaejecucion, string controlador, string bateria = ""/*, string modem*/, int maquinaria = 0, int IdReserva = 0, int estado = 0, int TipoReserva = 0, int CantidadScrubber = 0, string Selloperno1 = "", string Selloperno2 = "", string Sellotapa = "", string Comentario = "", int KitCortina = 0, string Modem = "", string BateriaModem = "")
        {
            if (TipoReserva != 3)
            {
                int validacion = ValidarContenedor(contenedor.ToUpper());
                if (validacion == 7)
                {
                    return 7;
                }
                else if (validacion == 4)
                {
                    return 4;
                }
                else if (validacion == 1)
                {
                    return 3;
                }
            }


            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarResultadoLeaktest", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDRESERVA", SqlDbType.Int, 50).Value = IdReserva;
            scCommand.Parameters.Add("@IDESTADO", SqlDbType.Int, 50).Value = estado;
            scCommand.Parameters.Add("@FECHAREALIZACION", SqlDbType.DateTime, 50).Value = fechaejecucion;
            scCommand.Parameters.Add("@IDSERVICEPROVIDER", SqlDbType.Int, 50).Value = serviceprovider;
            scCommand.Parameters.Add("@PRESION", SqlDbType.Float).Value = presion;
            scCommand.Parameters.Add("@IDTECNICO", SqlDbType.Int, 50).Value = tecnico;
            scCommand.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar, 50).Value = contenedor.ToUpper();
            scCommand.Parameters.Add("@ANOCONTENEDOR", SqlDbType.Int, 50).Value = anocontenedor;
            scCommand.Parameters.Add("@CAJACONTENEDOR", SqlDbType.Int, 50).Value = cajacontenedor;
            scCommand.Parameters.Add("@IDMAQUINARIA", SqlDbType.Int, 50).Value = maquinaria;
            scCommand.Parameters.Add("@CONTROLADOR", SqlDbType.VarChar, 100).Value = controlador.ToUpper();
            scCommand.Parameters.Add("@BATERIA", SqlDbType.VarChar, 100).Value = bateria.ToUpper();
            //scCommand.Parameters.Add("@MODEM", SqlDbType.VarChar, 100).Value = controlador.ToUpper();
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString().ToUpper();
            scCommand.Parameters.Add("@SCRUBBER", SqlDbType.Int, 50).Value = CantidadScrubber;
            scCommand.Parameters.Add("@SELLOPERNO1", SqlDbType.VarChar, 50).Value = Selloperno1.ToUpper();
            scCommand.Parameters.Add("@SELLOPERNO2", SqlDbType.VarChar, 50).Value = Selloperno2.ToUpper();
            scCommand.Parameters.Add("@SELLOTAPA", SqlDbType.VarChar, 50).Value = Sellotapa.ToUpper();
            scCommand.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 50).Value = Comentario.ToUpper();
            scCommand.Parameters.Add("@KIT_CORTINA", SqlDbType.Int, 50).Value = KitCortina;
            scCommand.Parameters.Add("@MODEM", SqlDbType.VarChar, 50).Value = Modem.ToUpper();
            scCommand.Parameters.Add("@BATERIA_MODEM", SqlDbType.VarChar, 50).Value = BateriaModem.ToUpper();
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

        public static int ValidarMaquinaria(int IdReserva, int Maquinaria)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select Count(*) from AplicacionServicio_DetalleReservaLeaktest where ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = @MAQUINARIA ", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                command.Parameters.AddWithValue("@MAQUINARIA", Maquinaria);

                Int32 validar = (Int32)command.ExecuteScalar();

                //VALIDAR QUE EXISTA LA MAQUINARIA SELECCIONADA
                if (validar == 0)
                {
                    SqlCommand command1 = new SqlCommand("Select Count(*) from AplicacionServicio_DetalleReservaLeaktest where ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = 5 ", cnn);
                    command1.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                    Int32 validar2 = (Int32)command1.ExecuteScalar();
                    //VALIDAR QUE EXISTA LA MAQUINARIA CUALQUIERA
                    if (validar2 == 0)
                    {
                        return 1;
                    }
                    else {
                        return 0;
                    }                 
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

        public static int ValidarCantidadResultados(int IdReserva, int Maquinaria)
        {
            SqlConnection cnn;
            int resta = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM AplicacionServicio_DetalleReservaLeaktest WHERE ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = 5", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                Int32 validar = (Int32)command.ExecuteScalar();
                if (validar == 0)
                {
                    SqlCommand command1 = new SqlCommand("Select SUM(CANTIDAD) - SUM(ISNULL(REALIZADOS, 0)) AS RESTA from AplicacionServicio_DetalleReservaLeaktest where ID_RESERVA = @IDRESERVA", cnn);
                    command1.Parameters.AddWithValue("@IDRESERVA", IdReserva); 
                    command1.Parameters.AddWithValue("@MAQUINARIA", Maquinaria);

                    SqlDataReader reader = command1.ExecuteReader();

                    while (reader.Read())
                    {
                        resta = Convert.ToInt32(reader["RESTA"]);
                    }
                }
                else {
                    SqlCommand command1 = new SqlCommand("Select CANTIDAD - ISNULL(REALIZADOS, 0) AS RESTA from AplicacionServicio_DetalleReservaLeaktest where ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = 5", cnn);
                    command1.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                    command1.Parameters.AddWithValue("@MAQUINARIA", Maquinaria);

                    SqlDataReader reader = command1.ExecuteReader();

                    while (reader.Read())
                    {
                        resta = Convert.ToInt32(reader["RESTA"]);
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
            return resta;
        }

        public static int ValidarCantidadResultadosPorAprobar(int IdReserva, int Maquinaria)
        {
            SqlConnection cnn;
            int resta = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM AplicacionServicio_DetalleReservaLeaktest WHERE ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = 5", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                Int32 validar = (Int32)command.ExecuteScalar();
                if (validar == 0)
                {
                    SqlCommand command1 = new SqlCommand("Select SUM(CANTIDAD) - SUM(APROBADOS) AS RESTA from AplicacionServicio_DetalleReservaLeaktest where ID_RESERVA = @IDRESERVA", cnn);
                    command1.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                    command1.Parameters.AddWithValue("@MAQUINARIA", Maquinaria);

                    SqlDataReader reader = command1.ExecuteReader();

                    while (reader.Read())
                    {
                        resta = Convert.ToInt32(reader["RESTA"]);
                    }
                }
                else
                {
                    SqlCommand command1 = new SqlCommand("Select CANTIDAD - APROBADOS AS RESTA from AplicacionServicio_DetalleReservaLeaktest where ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = 5", cnn);
                    command1.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                    command1.Parameters.AddWithValue("@MAQUINARIA", Maquinaria);

                    SqlDataReader reader = command1.ExecuteReader();

                    while (reader.Read())
                    {
                        resta = Convert.ToInt32(reader["RESTA"]);
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
            return resta;
        }

        public static void cambiarestadoreserva(int IdReserva)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_ESTADORESERVALEAKTEST SET ESTADO = 4 ", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

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

        public static Clases.ListaComentarios GetComentarios(int IdResultado)
        {

            SqlConnection cnn;
            Clases.ListaComentarios comentarios = new Clases.ListaComentarios();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_NotasResultadoLeaktest WHERE AplicacionServicio_NotasResultadoLeaktest.ID_LEAKTEST = @IDLEAKTEST", cnn);
                command.Parameters.AddWithValue("@IDLEAKTEST", IdResultado);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comentarios.listacomentarios.Add(new Clases.Comentarios
                    {
                        IdComentario = Convert.ToInt32(reader["ID_NOTA"]),
                        descripcion = reader["DESCRIPCION"].ToString(),
                        Fecha = Convert.ToDateTime(reader["FECHA"]),
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
            return comentarios;
        }
    
        public static Clases.ListaArchivos GetArchivos(int IdResultado)
        {

            SqlConnection cnn;
            Clases.ListaArchivos archivos = new Clases.ListaArchivos();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_RegistroArchivosResultadoLeaktest WHERE AplicacionServicio_RegistroArchivosResultadoLeaktest.ID_LEAKTEST = @IDLEAKTEST", cnn);
                command.Parameters.AddWithValue("@IDLEAKTEST", IdResultado);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    archivos.listaarchivos.Add(new Clases.Archivos
                    {
                        IdArchivo = Convert.ToInt32(reader["ID_REGISTRO"]),
                        descripcion = reader["NOMBRE"].ToString(),
                        Fecha = Convert.ToDateTime(reader["FECHA"])
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
            return archivos;
        }

        public static int AgregarNotaResultado(int IdResultado, string Comentario)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarNotaResultado", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDLEAKTEST", SqlDbType.Int, 50).Value = IdResultado;
            scCommand.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar, 500).Value = Comentario;
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

        public static int EliminarNotaLeaktest(int IdNota)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("DELETE APLICACIONSERVICIO_NOTASRESULTADOLEAKTEST WHERE ID_NOTA = @IDNOTA ", cnn);
                command.Parameters.AddWithValue("@IDNOTA", IdNota);

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

        public static Clases.ResultadoLeaktest GetResultadoById(int IdResultado)
        {
            SqlConnection cnn;
            Clases.ResultadoLeaktest Resultado = new Clases.ResultadoLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetResultadoById @IDRESULTADO", cnn);
                command.Parameters.AddWithValue("@IDRESULTADO", IdResultado);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID_LEAKTEST"] != DBNull.Value) Resultado.Id = Convert.ToInt32(reader["ID_LEAKTEST"]);
                    if (reader["ID_RESERVA"] != DBNull.Value) Resultado.IdReserva = Convert.ToInt32(reader["ID_RESERVA"]);
                    if (reader["ID_ESTADO"] != DBNull.Value) Resultado.Estado = Convert.ToInt32(reader["ID_ESTADO"]);
                    if (reader["FECHAREALIZACION"] != DBNull.Value) Resultado.FechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    if (reader["NOMBRESERVICEPROVIDER"] != DBNull.Value) Resultado.IdServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString();
                    if (reader["TIEMPO"] != DBNull.Value) Resultado.Tiempo = reader["TIEMPO"].ToString();
                    if (reader["PRESION"] != DBNull.Value)
                    {
                        Resultado.Presion = float.Parse(reader["PRESION"].ToString().Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    if (reader["NOMBREUSUARIO"] != DBNull.Value) Resultado.IdTecnico = reader["NOMBREUSUARIO"].ToString();
                    if (reader["CONTENEDOR"] != DBNull.Value) Resultado.Contenedor = reader["CONTENEDOR"].ToString();
                    if (reader["ANOCONTENEDOR"] != DBNull.Value) Resultado.AnoContenedor = Convert.ToInt32(reader["ANOCONTENEDOR"]);
                    if (reader["NOMBRECAJA"] != DBNull.Value) Resultado.CajaContenedor = reader["NOMBRECAJA"].ToString();
                    if (reader["NOMBREMAQUINARIA"] != DBNull.Value) Resultado.IdMaquinaria = reader["NOMBREMAQUINARIA"].ToString();
                    if (reader["CONTROLADOR"] != DBNull.Value) Resultado.Controlador = reader["CONTROLADOR"].ToString();
                    if (reader["BATERIA"] != DBNull.Value) Resultado.Bateria = reader["BATERIA"].ToString();
                    if (reader["SELLOPERNO1"] != DBNull.Value) Resultado.Selloperno1 = reader["SELLOPERNO1"].ToString();
                    if (reader["SELLOPERNO2"] != DBNull.Value) Resultado.Selloperno2 = reader["SELLOPERNO2"].ToString();
                    if (reader["SELLOTAPA"] != DBNull.Value) Resultado.Sellotapa = reader["SELLOTAPA"].ToString();
                    if (reader["SCRUBBER"] != DBNull.Value) Resultado.Scrubber = Convert.ToInt32(reader["SCRUBBER"]);
                    if (reader["KIT_CORTINA"] != DBNull.Value) Resultado.KitCortina = Convert.ToInt32(reader["KIT_CORTINA"]);
                    if (reader["MODEM"] != DBNull.Value) Resultado.Modem = reader["MODEM"].ToString();
                    if (reader["BATERIA_MODEM"] != DBNull.Value) Resultado.BateriaModem = reader["BATERIA_MODEM"].ToString();
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
            return Resultado;
        }

        public static int EditarResultadoLeaktest(string contenedor, int cajacontenedor, int anocontenedor, int serviceprovider, int tecnico, float presion, DateTime? fechaejecucion, string controlador, string bateria = "", int maquinaria = 0, int IdReserva = 0, int estado = 0, int IdResultado = 0, int TipoReserva = 0, int CantidadScrubber = 0, string Selloperno1 = "", string Selloperno2 = "", string Sellotapa = "", string Comentario = "", int KitCortina = 0, string Modem = "", string BateriaModem = "")
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            int result = 0;
            SqlCommand scCommand = new SqlCommand("EditarResultadoLeaktest", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDRESERVA", SqlDbType.Int, 50).Value = IdReserva;
            scCommand.Parameters.Add("@IDESTADO", SqlDbType.Int, 50).Value = estado;
            scCommand.Parameters.Add("@FECHAREALIZACION", SqlDbType.DateTime, 50).Value = fechaejecucion;
            scCommand.Parameters.Add("@IDSERVICEPROVIDER", SqlDbType.Int, 50).Value = serviceprovider;
            scCommand.Parameters.Add("@PRESION", SqlDbType.Float).Value = presion;
            scCommand.Parameters.Add("@IDTECNICO", SqlDbType.Int, 50).Value = tecnico;
            scCommand.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar, 50).Value = contenedor;
            scCommand.Parameters.Add("@ANOCONTENEDOR", SqlDbType.Int, 50).Value = anocontenedor;
            scCommand.Parameters.Add("@CAJACONTENEDOR", SqlDbType.Int, 50).Value = cajacontenedor;
            scCommand.Parameters.Add("@IDMAQUINARIA", SqlDbType.Int, 50).Value = maquinaria;
            scCommand.Parameters.Add("@CONTROLADOR", SqlDbType.VarChar, 100).Value = controlador;
            scCommand.Parameters.Add("@BATERIA", SqlDbType.VarChar, 100).Value = bateria;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@IDRESULTADO", SqlDbType.Int, 50).Value = IdResultado;
            scCommand.Parameters.Add("@SCRUBBER", SqlDbType.Int, 50).Value = CantidadScrubber;
            scCommand.Parameters.Add("@SELLOPERNO1", SqlDbType.VarChar, 50).Value = Selloperno1.ToUpper();
            scCommand.Parameters.Add("@SELLOPERNO2", SqlDbType.VarChar, 50).Value = Selloperno2.ToUpper();
            scCommand.Parameters.Add("@SELLOTAPA", SqlDbType.VarChar, 50).Value = Sellotapa.ToUpper();
            scCommand.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 50).Value = Comentario.ToUpper();
            scCommand.Parameters.Add("@KIT_CORTINA", SqlDbType.Int, 50).Value = KitCortina;
            scCommand.Parameters.Add("@MODEM", SqlDbType.VarChar, 50).Value = Modem.ToUpper();
            scCommand.Parameters.Add("@BATERIA_MODEM", SqlDbType.VarChar, 50).Value = BateriaModem.ToUpper();

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
                    //ACTUALIZAR DETALLE RESERVA
                    if (TipoReserva == 1)
                    {
                        SqlCommand command = new SqlCommand("Select	AplicacionServicio_Maquinaria.ID_MAQUINARIA, COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS CANTIDAD from AplicacionServicio_Maquinaria, AplicacionServicio_ResultadoLeaktest GROUP BY AplicacionServicio_Maquinaria.ID_MAQUINARIA", cnn);
                        command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["ID_MAQUINARIA"]) != 5)
                            {
                                Cambiarestadoreservarealizar(Convert.ToInt32(reader["CANTIDAD"]), IdReserva, Convert.ToInt32(reader["ID_MAQUINARIA"]));
                            }
                        }
                    }
                    else if (TipoReserva == 2)
                    {
                        SqlCommand command = new SqlCommand("Select AplicacionServicio_Maquinaria.ID_MAQUINARIA,COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS CANTIDAD,COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_ESTADO = 0 AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS APROBADOS,COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_ESTADO = 1 AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS RECHAZADOS from AplicacionServicio_Maquinaria, AplicacionServicio_ResultadoLeaktest GROUP BY AplicacionServicio_Maquinaria.ID_MAQUINARIA", cnn);
                        command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["ID_MAQUINARIA"]) != 5)
                            {
                                Cambiarestadoreservaaprobar(Convert.ToInt32(reader["CANTIDAD"]), IdReserva, Convert.ToInt32(reader["ID_MAQUINARIA"]), Convert.ToInt32(reader["APROBADOS"]));
                            }
                        }
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

        public static void Cambiarestadoreservarealizar(int Cantidad, int IdReserva, int IdMaquinaria) {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_DETALLERESERVALEAKTEST SET REALIZADOS = @CANTIDAD WHERE ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = @IDMAQUINARIA", cnn);
                command.Parameters.AddWithValue("@CANTIDAD", Cantidad);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                command.Parameters.AddWithValue("@IDMAQUINARIA", IdMaquinaria);

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

        public static void Cambiarestadoreservaaprobar(int Cantidad, int IdReserva, int IdMaquinaria, int Aprobados)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_DETALLERESERVALEAKTEST SET REALIZADOS = @CANTIDAD, APROBADOS = @APROBADOS WHERE ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA = @IDMAQUINARIA", cnn);
                command.Parameters.AddWithValue("@CANTIDAD", Cantidad);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                command.Parameters.AddWithValue("@IDMAQUINARIA", IdMaquinaria);
                command.Parameters.AddWithValue("@APROBADOS", Aprobados);

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

        public static void Cambiarestadoreservacontenedores(int IdReserva, int Realizados)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_DETALLERESERVALEAKTEST SET REALIZADOS = @CANTIDAD WHERE ID_RESERVA = @IDRESERVA", cnn);
                command.Parameters.AddWithValue("@CANTIDAD", Realizados - 1);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

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

        public static List<Clases.DetalleReservaLeaktestEditar> GetReservaById(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.DetalleReservaLeaktestEditar> Detalles = new List<Clases.DetalleReservaLeaktestEditar>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT RESERVA.ID_NAVIERA, RESERVA.HORA,  RESERVA.ID_DEPOSITO, RESERVA.FECHAESTIMADA,RESERVA.ID_TIPORESERVA,RESERVA.CANTIDADSCRUBBER, DETALLE.ID_MAQUINARIA, DETALLE.CANTIDAD, DETALLE.REALIZADOS, ISNULL(DETALLE.APROBADOS,-1) AS APROBADOS FROM APLICACIONSERVICIO_RESERVALEAKTEST RESERVA, APLICACIONSERVICIO_DETALLERESERVALEAKTEST DETALLE WHERE RESERVA.ID_RESERVA = @IDRESERVA AND RESERVA.ID_RESERVA = DETALLE.ID_RESERVA ORDER BY ID_MAQUINARIA ASC", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Detalles.Add(new Clases.DetalleReservaLeaktestEditar
                    {
                        IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]),
                        IdDeposito = Convert.ToInt32(reader["ID_DEPOSITO"]),
                        FechaEstimada = Convert.ToDateTime(reader["FECHAESTIMADA"]),
                        IdTipoReserva = Convert.ToInt32(reader["ID_TIPORESERVA"]),
                        IdMaquinaria = Convert.ToInt32(reader["ID_MAQUINARIA"]),
                        Cantidad = Convert.ToInt32(reader["CANTIDAD"]),
                        Realizados = Convert.ToInt32(reader["REALIZADOS"]),
                        Aprobados = Convert.ToInt32(reader["APROBADOS"]),
                        Hora = reader["HORA"].ToString(),
                        CantidadScrubber = Convert.ToInt32(reader["CANTIDADSCRUBBER"]),
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
            return Detalles;
        }

        public static int EditarReservaLeaktest(int IdReserva, int Naviera, int Deposito, DateTime Eta, int Maquinaria, int cantidad, int Maquinaria1, int cantidad1, int Maquinaria2, int cantidad2, int Maquinaria3, int cantidad3, string Hora, int CantidadScrubber)
        {
            int Scrubber = 0;

            if (CantidadScrubber != 0)
            {
                Scrubber = 0;
            }
            else
            {
                Scrubber = 1;
            }
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EditarReservaLeaktest", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDRESERVA", SqlDbType.Int, 50).Value = IdReserva;
            scCommand.Parameters.Add("@IDDEPOSITO", SqlDbType.Int, 50).Value = Deposito;
            scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Naviera;
            scCommand.Parameters.Add("@FECHAESTIMADA", SqlDbType.DateTime, 50).Value = Eta;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@IDMAQUINARIA", SqlDbType.Int, 50).Value = Maquinaria;
            scCommand.Parameters.Add("@CANTIDAD", SqlDbType.Int, 50).Value = cantidad;
            scCommand.Parameters.Add("@IDMAQUINARIA1", SqlDbType.Int, 50).Value = Maquinaria1;
            scCommand.Parameters.Add("@CANTIDAD1", SqlDbType.Int, 50).Value = cantidad1;
            scCommand.Parameters.Add("@IDMAQUINARIA2", SqlDbType.Int, 50).Value = Maquinaria2;
            scCommand.Parameters.Add("@CANTIDAD2", SqlDbType.Int, 50).Value = cantidad2;
            scCommand.Parameters.Add("@IDMAQUINARIA3", SqlDbType.Int, 50).Value = Maquinaria3;
            scCommand.Parameters.Add("@CANTIDAD3", SqlDbType.Int, 50).Value = cantidad3;
            scCommand.Parameters.Add("@HORA", SqlDbType.VarChar, 100).Value = Hora;
            scCommand.Parameters.Add("@CANTIDADSCRUBBER", SqlDbType.Int, 100).Value = CantidadScrubber;
            scCommand.Parameters.Add("@ESTADOSCRUBBER", SqlDbType.Int, 100).Value = Scrubber;

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

        public static List<Clases.ResultadoLeaktestAll> GetAllResultados()
        {
            SqlConnection cnn;
            List<Clases.ResultadoLeaktestAll> Resultados = new List<Clases.ResultadoLeaktestAll>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoLeaktest", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Resultados.Add(new Clases.ResultadoLeaktestAll
                    {
                        IdLeaktest = Convert.ToInt32(reader["ID_RESULTADO"]),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreMaquinaria = reader["NOMBREMAQUINARIA"].ToString(),
                        NombreServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString(),
                        NombreUsuario = reader["NOMBREUSUARIO"].ToString(),
                        Tiempo = reader["TIEMPO"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        BateriaModem = reader["BATERIAMODEM"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Ano = Convert.ToInt32(reader["ANOCONTENEDOR"]),
                        NombreCaja = reader["CAJACONTENEDOR"].ToString(),
                        Fecha = Convert.ToDateTime(reader["FECHAREALIZACION"]),
                        Estado = Convert.ToInt32(reader["ID_ESTADO"]),
                        Scrubber = Convert.ToInt32(reader["SCRUBBER"]),
                        Selloperno1 = reader["SELLOPERNO1"].ToString(),
                        Selloperno2 = reader["SELLOPERNO2"].ToString(),
                        Sellotapa = reader["SELLOTAPA"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        KitCortina = Convert.ToInt32(reader["KIT_CORTINA"]),

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
            return Resultados;
        }

        public static List<Clases.ResultadoLeaktestAllSP> GetAllResultadosSP()
        {
            SqlConnection cnn;
            List<Clases.ResultadoLeaktestAllSP> Resultados = new List<Clases.ResultadoLeaktestAllSP>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarLeaktestSP", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string EstadoLeak = "";
                    DateTime? fechaEstimada = null;
                    DateTime? fechaRealizacion = null;
                    DateTime? fechaControlador = null;
                    DateTime? fechaBateriaModem = null;
                    string val = "";
                    string panelReutilizado = "";
                    int prevalidacion = 1;

                    if (reader["PANELREUTILIZADO"] != DBNull.Value)
                    {
                        int panel = Convert.ToInt32(reader["PANELREUTILIZADO"]);

                        if (panel == 0)
                        {
                            panelReutilizado = "NO";
                        }
                        else if (panel == 1)
                        {
                            panelReutilizado = "SI";
                        }
                    }
                    else
                    {
                        panelReutilizado = "NO";
                    }

                    if (reader["VALIDADO"] != DBNull.Value)
                    {
                        int Validado = Convert.ToInt32(reader["VALIDADO"]);

                        if (Validado == 0)
                        {
                            val = "NO";
                        }
                        else if (Validado == 1)
                        {
                            val = "SI";
                        }
                    }

                    if (reader["ID_ESTADO"] != DBNull.Value)
                    {
                        int Estado = Convert.ToInt32(reader["ID_ESTADO"]);

                        if (Estado == 0)
                        {
                            EstadoLeak = "APPROVED";
                        }
                        else if (Estado == 1)
                        {
                            EstadoLeak = "REJECTED";
                        }
                        else if (Estado == 2)
                        {
                            EstadoLeak = "TO PERFORM";
                        }
                        else if (Estado == 3)
                        {
                            EstadoLeak = "VISUALLY REJECTED";
                        }
                        else if (Estado == 4)
                        {
                            EstadoLeak = "CANCELLED";
                        }
                    }

                    if (reader["FECHAESTIMADA"] != DBNull.Value)
                    {
                        fechaEstimada = Convert.ToDateTime(reader["FECHAESTIMADA"]);
                    }

                    if (reader["FECHAREALIZACION"] != DBNull.Value)
                    {
                        fechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    }

                    if (reader["FECHA_CONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHA_CONTROLADOR"]);
                    }

                    if (reader["FECHA_BATERIA_MODEM"] != DBNull.Value)
                    {
                        fechaBateriaModem = Convert.ToDateTime(reader["FECHA_BATERIA_MODEM"]);
                    }

                    if (reader["PREVALIDACIONTECNICA"] != DBNull.Value)
                    {
                        prevalidacion = Convert.ToInt32(reader["PREVALIDACIONTECNICA"]);
                    }


                    Resultados.Add(new Clases.ResultadoLeaktestAllSP
                    {
                        IdResultado = Convert.ToInt32(reader["ID_RESULTADO"]),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Naviera = reader["NOMBRENAVIERA"].ToString(),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreMaquinaria = reader["NOMBREMAQUINARIA"].ToString(),
                        NombreServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString(),
                        NombreUsuario = reader["NOMBREUSUARIO"].ToString(),
                        Commodity = reader["NOMBRECOMMODITY"].ToString(),
                        Tiempo = reader["TIEMPO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Ano = reader["ANOCONTENEDOR"].ToString(),
                        FechaRealizacion = fechaRealizacion,
                        EstadoLeaktest = EstadoLeak,
                        Selloperno1 = reader["SELLOPERNO1"].ToString(),
                        Selloperno2 = reader["SELLOPERNO2"].ToString(),
                        Sellotapa = reader["SELLOTAPA"].ToString(),
                        Checkbox = reader["CHECKBOX"].ToString(),
                        FechaEstimada = fechaEstimada,
                        HoraEstimada = reader["HORAESTIMADA"].ToString(),
                        Comentario = reader["COMENTARIO"].ToString(),
                        Validado = val,
                        PanelReutilizado = panelReutilizado,
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        FechaControlador = fechaControlador,
                        Modem = reader["MODEM"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        FechaBateriaModem = fechaBateriaModem,
                        PreValidacionTecnica = prevalidacion
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
            return Resultados;
        }

        public static List<Clases.ResultadoLeaktestAllSP> GetAllHistoricoResultadosSP(int ResultadosSP)
        {
            SqlConnection cnn;
            List<Clases.ResultadoLeaktestAllSP> Resultados = new List<Clases.ResultadoLeaktestAllSP>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarHistoricoLeaktestSP", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_RESULTADO", SqlDbType.Int, 50).Value = ResultadosSP;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string EstadoLeak = "";
                    DateTime? fechaEstimada = null;
                    DateTime? fechaRealizacion = null;
                    DateTime? fechaAccion=null;
                    DateTime? fechaControlador = null;
                    string val = "";
                    string panelReutilizado = "";

                    if (reader["PANELREUTILIZADO"] != DBNull.Value)
                    {
                        int panel = Convert.ToInt32(reader["PANELREUTILIZADO"]);

                        if (panel == 0)
                        {
                            panelReutilizado = "NO";
                        }
                        else if (panel == 1)
                        {
                            panelReutilizado = "SI";
                        }
                    }
                    else
                    {
                        panelReutilizado = "NO";
                    }

                    if (reader["VALIDADO"] != DBNull.Value)
                    {
                        int Validado = Convert.ToInt32(reader["VALIDADO"]);

                        if (Validado == 0)
                        {
                            val = "NO";
                        }
                        else if (Validado == 1)
                        {
                            val = "SI";
                        }
                    }

                    if (reader["ID_ESTADO"] != DBNull.Value)
                    {
                        int Estado = Convert.ToInt32(reader["ID_ESTADO"]);

                        if (Estado == 0)
                        {
                            EstadoLeak = "APPROVED";
                        }
                        else if (Estado == 1)
                        {
                            EstadoLeak = "REJECTED";
                        }
                        else if (Estado == 2)
                        {
                            EstadoLeak = "TO PERFORM";
                        }
                        else if (Estado == 3)
                        {
                            EstadoLeak = "VISUALLY REJECTED";
                        }
                        else if (Estado == 4)
                        {
                            EstadoLeak = "CANCELLED";
                        }
                    }

                    if (reader["FECHAESTIMADA"] != DBNull.Value)
                    {
                        fechaEstimada = Convert.ToDateTime(reader["FECHAESTIMADA"]);
                    }

                    if (reader["FECHAREALIZACION"] != DBNull.Value)
                    {
                        fechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    }

                    if (reader["FECHAACCION"] != DBNull.Value)
                    {
                        fechaAccion = Convert.ToDateTime(reader["FECHAACCION"]);
                    }

                    if (reader["FECHA_CONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHA_CONTROLADOR"]);
                    }

                    Resultados.Add(new Clases.ResultadoLeaktestAllSP
                    {
                        IdResultado = Convert.ToInt32(reader["ID_RESULTADO"]),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Naviera = reader["NOMBRENAVIERA"].ToString(),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreMaquinaria = reader["NOMBREMAQUINARIA"].ToString(),
                        NombreServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString(),
                        NombreUsuario = reader["NOMBREUSUARIO"].ToString(),
                        Commodity = reader["NOMBRECOMMODITY"].ToString(),
                        Tiempo = reader["TIEMPO"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Ano = reader["ANOCONTENEDOR"].ToString(),
                        FechaRealizacion = fechaRealizacion,
                        EstadoLeaktest = EstadoLeak,
                        Selloperno1 = reader["SELLOPERNO1"].ToString(),
                        Selloperno2 = reader["SELLOPERNO2"].ToString(),
                        Sellotapa = reader["SELLOTAPA"].ToString(),
                        FechaEstimada = fechaEstimada,
                        HoraEstimada = reader["HORAESTIMADA"].ToString(),
                        Comentario = reader["COMENTARIO"].ToString(),
                        Validado = val,
                        PanelReutilizado = panelReutilizado,
                        FechaAccion=fechaAccion,
                        UsuarioAccion= reader["USUARIOACCION"].ToString(),
                        Controlador= reader["CONTROLADOR"].ToString(),
                        Bateria= reader["BATERIA"].ToString(),
                        FechaControlador=fechaControlador,
                        PreValidacionTecnica=Convert.ToInt32(reader["PREVALIDACIONTECNICA"])
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
            return Resultados;
        }

        public static int EditarCelda(int IdResultado, string Campo, string Valor, string Columna, string BateriaControlador = "", DateTime? FechaAsociacionBateriaControlador = null, string BateriaModem = "", DateTime? FechaAsociacionBateriaModem = null)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                if (Campo == "ServiceProvider")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_SERVICEPROVIDER=@ID_SERVICEPROVIDER, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_SERVICEPROVIDER", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Naviera")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_NAVIERA=@ID_NAVIERA, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_NAVIERA", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Deposito")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_DEPOSITO=@ID_DEPOSITO, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_DEPOSITO", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FechaEstimada")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET FECHAESTIMADA=@FECHAESTIMADA, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@FECHAESTIMADA", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "HoraEstimada")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET HORAESTIMADA=@HORAESTIMADA, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@HORAESTIMADA", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Commodity")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_COMMODITY=@ID_COMMODITY, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_COMMODITY", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Contenedor")
                {
                    command = new SqlCommand("EXEC dbo.AsignarContenedorSP @CONTENEDOR, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@CONTENEDOR", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Maquinaria")
                {
                    command = new SqlCommand("EXEC dbo.AsignarMaquinariaSP @ID_MAQUINARIA, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ID_MAQUINARIA", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "AnoContenedor")
                {
                    command = new SqlCommand("EXEC dbo.AsignarAnoContenedorSP @ANOCONTENEDOR, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ANOCONTENEDOR", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tecnico")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_TECNICO=@ID_TECNICO, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_TECNICO", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Tiempo")
                {
                    command = new SqlCommand("EXEC dbo.AsignarTiempoLeaktestSP @TIEMPO, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@TIEMPO", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FechaRealizacion")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET FECHAREALIZACION=@FECHAREALIZACION, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@FECHAREALIZACION", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Estado")
                {
                    command = new SqlCommand("EXEC dbo.AsignarEstadoContenedorSP @ID_ESTADO, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ID_ESTADO", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "SelloPanel1")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET SELLOPERNO1=@SELLOPERNO1, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@SELLOPERNO1", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "SelloPanel2")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET SELLOPERNO2=@SELLOPERNO2, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@SELLOPERNO2", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "SelloSecurity")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET SELLOTAPA=@SELLOTAPA, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@SELLOTAPA", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Comentario")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET COMENTARIO=@COMENTARIO, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@COMENTARIO", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "PanelReutilizado")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET PANELREUTILIZADO=@PANELREUTILIZADO, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@PANELREUTILIZADO", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "Controlador")
                {
                    command = new SqlCommand("EXEC dbo.AsignarControladorLeaktestSP @CONTROLADOR, @ID_RESULTADO, @USUARIO, @BATERIA, @FECHA_ASOCIACION", cnn);
                    command.Parameters.AddWithValue("@CONTROLADOR", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    command.Parameters.AddWithValue("@BATERIA", BateriaControlador);
                    command.Parameters.AddWithValue("@FECHA_ASOCIACION", FechaAsociacionBateriaControlador);
                }
                else if (Campo == "Modem")
                {
                    command = new SqlCommand("EXEC dbo.AsignarModemLeaktestSP @MODEM, @ID_RESULTADO, @USUARIO, @BATERIA, @FECHA_ASOCIACION", cnn);
                    command.Parameters.AddWithValue("@MODEM", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                    command.Parameters.AddWithValue("@BATERIA", BateriaModem);
                    command.Parameters.AddWithValue("@FECHA_ASOCIACION", FechaAsociacionBateriaModem);
                }
                else if (Campo == "Bateria")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET BATERIA=@BATERIA, FECHA_CONTROLADOR=SYSDATETIME(), USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@BATERIA", Valor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Campo == "FechaControlador")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET FECHA_CONTROLADOR=@FECHA_CONTROLADOR, USUARIOACCION=@USUARIO WHERE ID_RESULTADO=@ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@FECHA_CONTROLADOR", HoraFecha(Valor));
                    command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }


                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    if (Campo == "Controlador")
                    {
                        BateriaModel.AsociarBateria(BateriaControlador, Valor, "CONTROLADOR", FechaAsociacionBateriaControlador);
                    }
                    if (Campo == "Modem")
                    {
                        BateriaModel.AsociarBateria(BateriaModem, Valor, "MODEM", FechaAsociacionBateriaModem);
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

        public static int AgregarArchivoLeaktest(int IdResultado, string Nombre)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarArchivoResultado", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDLEAKTEST", SqlDbType.Int, 50).Value = IdResultado;
            scCommand.Parameters.Add("@NOMBRE", SqlDbType.VarChar, 500).Value = Nombre;
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

        public static int AgregarLeaktestSP(string Estado = "", int AnoContenedor = 0, string Tiempo = "", DateTime? FechaEjecucion = null, int ServiceProvider = 0, int Deposito = 0, int Naviera = 0, int Commodity = 0, DateTime? FechaEstimada = null, string HoraEstimada = "", int Tecnico = 0, int Maquinaria = 0, int Cantidad = 0, int Maquinaria1 = 0, int Cantidad1 = 0, int Maquinaria2 = 0, int Cantidad2 = 0, int Maquinaria3 = 0, int Cantidad3 = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarLeaktestSP", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@ANOCONTENEDOR", SqlDbType.Int, 50).Value = AnoContenedor;
            scCommand.Parameters.Add("@TIEMPO", SqlDbType.VarChar, 50).Value = Tiempo;
            if (FechaEjecucion == null)
            {
                scCommand.Parameters.Add("@FECHAEJECUCION", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAEJECUCION", SqlDbType.DateTime, 50).Value = FechaEjecucion;
            }

            scCommand.Parameters.Add("@ESTADO", SqlDbType.VarChar, 1).Value = Estado;
            scCommand.Parameters.Add("@ID_SERVICEPROVIDER", SqlDbType.Int, 50).Value = ServiceProvider;
            scCommand.Parameters.Add("@ID_NAVIERA", SqlDbType.Int, 50).Value = Naviera;
            scCommand.Parameters.Add("@ID_DEPOSITO", SqlDbType.Int, 50).Value = Deposito;
            scCommand.Parameters.Add("@ID_COMMODITY", SqlDbType.Int, 50).Value = Commodity;
            if(FechaEstimada==null)
            {
                scCommand.Parameters.Add("@FECHAESTIMADA", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHAESTIMADA", SqlDbType.DateTime, 50).Value = FechaEstimada;
            }
            
            scCommand.Parameters.Add("@HORAESTIMADA", SqlDbType.VarChar, 50).Value = HoraEstimada;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@ID_TECNICO", SqlDbType.Int, 50).Value = Tecnico;
            scCommand.Parameters.Add("@IDMAQUINARIA", SqlDbType.Int, 50).Value = Maquinaria;
            scCommand.Parameters.Add("@CANTIDAD", SqlDbType.Int, 50).Value = Cantidad;
            scCommand.Parameters.Add("@IDMAQUINARIA1", SqlDbType.Int, 50).Value = Maquinaria1;
            scCommand.Parameters.Add("@CANTIDAD1", SqlDbType.Int, 50).Value = Cantidad1;
            scCommand.Parameters.Add("@IDMAQUINARIA2", SqlDbType.Int, 50).Value = Maquinaria2;
            scCommand.Parameters.Add("@CANTIDAD2", SqlDbType.Int, 50).Value = Cantidad2;
            scCommand.Parameters.Add("@IDMAQUINARIA3", SqlDbType.Int, 50).Value = Maquinaria3;
            scCommand.Parameters.Add("@CANTIDAD3", SqlDbType.Int, 50).Value = Cantidad3;
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

        public static int EliminarArchivoLeaktest(int IdArchivo)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("DELETE APLICACIONSERVICIO_REGISTROARCHIVOSRESULTADOLEAKTEST WHERE ID_REGISTRO = @IDARCHIVO ", cnn);
                command.Parameters.AddWithValue("@IDARCHIVO", IdArchivo);

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

        public static List<Clases.EstadoReservaLeaktest> GetEstadosReserva()
        {
            SqlConnection cnn;
            List<Clases.EstadoReservaLeaktest> Estados = new List<Clases.EstadoReservaLeaktest>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_EstadoReservaLeaktest WHERE AplicacionServicio_EstadoReservaLeaktest.ESTADO = 0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estados.Add(new Clases.EstadoReservaLeaktest
                    {
                        Descripcion = reader["DESCRIPCION"].ToString(),
                        Id = Convert.ToInt32(reader["ID_ESTADO"]),
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
            return Estados;
        }

        public static int EliminarResultado(int IdResultado, int TipoReserva, int IdReserva)
        {
            
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                int validar = EliminarRegistroContenedores(IdResultado); 
                if (validar == 0)
                {
                    return 1;
                }
                else
                {
                    if (TipoReserva == 1)
                    {
                        SqlCommand command1 = new SqlCommand("Select AplicacionServicio_Maquinaria.ID_MAQUINARIA, COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA  THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS CANTIDAD from AplicacionServicio_Maquinaria, AplicacionServicio_ResultadoLeaktest GROUP BY AplicacionServicio_Maquinaria.ID_MAQUINARIA", cnn);
                        command1.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                        SqlDataReader reader = command1.ExecuteReader();
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["ID_MAQUINARIA"]) != 5)
                            {
                                Cambiarestadoreservarealizar(Convert.ToInt32(reader["CANTIDAD"]), IdReserva, Convert.ToInt32(reader["ID_MAQUINARIA"]));
                            }
                        }
                    }
                    else if (TipoReserva == 2)
                    {
                        SqlCommand command2 = new SqlCommand("Select AplicacionServicio_Maquinaria.ID_MAQUINARIA,COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS CANTIDAD,COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_ESTADO = 0 AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS APROBADOS,COUNT( CASE WHEN AplicacionServicio_Maquinaria.ID_MAQUINARIA = AplicacionServicio_ResultadoLeaktest.ID_MAQUINARIA AND AplicacionServicio_ResultadoLeaktest.ID_ESTADO = 1 AND AplicacionServicio_ResultadoLeaktest.ID_RESERVA = @IDRESERVA THEN AplicacionServicio_Maquinaria.ID_MAQUINARIA END) AS RECHAZADOS from AplicacionServicio_Maquinaria, AplicacionServicio_ResultadoLeaktest GROUP BY AplicacionServicio_Maquinaria.ID_MAQUINARIA", cnn);
                        command2.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                        SqlDataReader reader = command2.ExecuteReader();
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["ID_MAQUINARIA"]) != 5)
                            {
                                Cambiarestadoreservaaprobar(Convert.ToInt32(reader["CANTIDAD"]), IdReserva, Convert.ToInt32(reader["ID_MAQUINARIA"]), Convert.ToInt32(reader["APROBADOS"]));
                            }
                        }
                    }
                    else if (TipoReserva == 3) {
                        SqlCommand command2 = new SqlCommand("select REALIZADOS from AplicacionServicio_DetalleReservaLeaktest where ID_RESERVA = @IDRESERVA;", cnn);
                        command2.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                        SqlDataReader reader = command2.ExecuteReader();
                        while (reader.Read())
                        {
                            Cambiarestadoreservacontenedores(IdReserva, Convert.ToInt32(reader["REALIZADOS"]));
                        }
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

        public static void updatedetalleporrealizar(int IdReserva)
        {

            SqlConnection cnn1;
            cnn1 = new SqlConnection(connectionString);
            try
            {
                cnn1.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_DetalleReservaLeaktest SET REALIZADOS = SUM(REALIZADOS) FROM AplicacionServicio_DetalleReservaLeaktest Where ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA <> 5", cnn1);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn1.Close();
            }
        }

        public static void updatedetalleporaprobar(int IdReserva)
        {

            SqlConnection cnn1;
            cnn1 = new SqlConnection(connectionString);
            try
            {
                cnn1.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_DetalleReservaLeaktest SET REALIZADOS = SUM(REALIZADOS), APROBADOS = SUM(APROBADOS) FROM AplicacionServicio_DetalleReservaLeaktest Where ID_RESERVA = @IDRESERVA AND ID_MAQUINARIA <>5", cnn1);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn1.Close();
            }
        }

        public static int EliminarRegistroContenedores(int IdResultado)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EliminarRegistrosContenedores", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDRESULTADO", SqlDbType.Int, 50).Value = IdResultado;

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
                    return 0;
                }
                else
                {
                    return 1;
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

        public static Clases.ReservaLeaktestCompleta GetReservasByFechaCreacion(DateTime FechaInicial, DateTime FechaFin)
        {
            SqlConnection cnn;
            Clases.ReservaLeaktestCompleta Reservas = new Clases.ReservaLeaktestCompleta();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA,APLICACIONSERVICIO_RESERVALEAKTEST.COMENTARIO,APLICACIONSERVICIO_RESERVALEAKTEST.HORA,APLICACIONSERVICIO_RESERVALEAKTEST.CANTIDADSCRUBBER,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAESTIMADA,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAREGISTRO,APLICACIONSERVICIO_NAVIERA.NOMBRE AS NOMBRENAVIERA,APLICACIONSERVICIO_DEPOSITO.NOMBRE AS NOMBREDEPOSITO,APLICACIONSERVICIO_CIUDAD.NOMBRE AS NOMBRECIUDAD,APLICACIONSERVICIO_PAIS.NOMBRE AS NOMBREPAIS,APLICACIONSERVICIO_ESTADORESERVALEAKTEST.DESCRIPCION AS ESTADO,APLICACIONSERVICIO_TIPORESERVALEAKTEST.DESCRIPCION AS TIPORESERVA,(SELECT ISNULL(SUM(APLICACIONSERVICIO_DETALLERESERVALEAKTEST.CANTIDAD),0) FROM APLICACIONSERVICIO_DETALLERESERVALEAKTEST WHERE APLICACIONSERVICIO_DETALLERESERVALEAKTEST.ID_RESERVA = APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA) AS CANTIDADPORABPROBAR, (SELECT ISNULL(COUNT(*),0) FROM APLICACIONSERVICIO_DETALLERESERVACONTENEDOR WHERE APLICACIONSERVICIO_DETALLERESERVACONTENEDOR.ID_RESERVA = APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA) AS CANTIDADPORCONTENEDOR FROM APLICACIONSERVICIO_RESERVALEAKTEST,APLICACIONSERVICIO_TIPORESERVALEAKTEST,APLICACIONSERVICIO_NAVIERA,APLICACIONSERVICIO_DEPOSITO,APLICACIONSERVICIO_CIUDAD,APLICACIONSERVICIO_PAIS,APLICACIONSERVICIO_ESTADORESERVALEAKTEST WHERE APLICACIONSERVICIO_RESERVALEAKTEST.ID_NAVIERA = APLICACIONSERVICIO_NAVIERA.ID_NAVIERA AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_DEPOSITO = APLICACIONSERVICIO_DEPOSITO.ID_DEPOSITO AND APLICACIONSERVICIO_DEPOSITO.ID_CIUDAD = APLICACIONSERVICIO_CIUDAD.ID_CIUDAD AND APLICACIONSERVICIO_CIUDAD.ID_PAIS = APLICACIONSERVICIO_PAIS.ID_PAIS AND APLICACIONSERVICIO_RESERVALEAKTEST.ESTADO = APLICACIONSERVICIO_ESTADORESERVALEAKTEST.ID_ESTADO AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_TIPORESERVA = APLICACIONSERVICIO_TIPORESERVALEAKTEST.ID_TIPORESERVA AND CONVERT(DATE,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAREGISTRO) BETWEEN CONVERT(DATE,@FECHAINICIAL) AND CONVERT(DATE,@FECHAFINAL) ORDER BY APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA ASC;", cnn);
                command.Parameters.AddWithValue("@FECHAINICIAL", FechaInicial);
                command.Parameters.AddWithValue("@FECHAFINAL", FechaFin);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Reservas.Reservas.Add(new Clases.ReservaLeaktest
                    {
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreNaviera = reader["NOMBRENAVIERA"].ToString(),
                        TipoReserva = reader["TIPORESERVA"].ToString(),
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        FechaEstimadaRealizacion = Convert.ToDateTime(reader["FECHAESTIMADA"]),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Hora = reader["HORA"].ToString(),
                        CantidadScrubber = Convert.ToInt32(reader["CANTIDADSCRUBBER"]),
                        Comentario = reader["COMENTARIO"].ToString()
                    });
                    Reservas.CantidadTotal.Add(Convert.ToInt32(reader["CANTIDADPORABPROBAR"]));
                    Reservas.CantidadPorContenedores.Add(Convert.ToInt32(reader["CANTIDADPORCONTENEDOR"]));
                    Reservas.Estado.Add(reader["ESTADO"].ToString());
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

        public static Clases.ReservaLeaktestCompleta GetReservasByFechaEstimada(DateTime FechaInicial, DateTime FechaFin)
        {
            SqlConnection cnn;
            Clases.ReservaLeaktestCompleta Reservas = new Clases.ReservaLeaktestCompleta();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA,APLICACIONSERVICIO_RESERVALEAKTEST.COMENTARIO,APLICACIONSERVICIO_RESERVALEAKTEST.HORA,APLICACIONSERVICIO_RESERVALEAKTEST.CANTIDADSCRUBBER,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAESTIMADA,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAREGISTRO,APLICACIONSERVICIO_NAVIERA.NOMBRE AS NOMBRENAVIERA,APLICACIONSERVICIO_DEPOSITO.NOMBRE AS NOMBREDEPOSITO,APLICACIONSERVICIO_CIUDAD.NOMBRE AS NOMBRECIUDAD,APLICACIONSERVICIO_PAIS.NOMBRE AS NOMBREPAIS,APLICACIONSERVICIO_ESTADORESERVALEAKTEST.DESCRIPCION AS ESTADO,APLICACIONSERVICIO_TIPORESERVALEAKTEST.DESCRIPCION AS TIPORESERVA,(SELECT ISNULL(SUM(APLICACIONSERVICIO_DETALLERESERVALEAKTEST.CANTIDAD),0) FROM APLICACIONSERVICIO_DETALLERESERVALEAKTEST WHERE APLICACIONSERVICIO_DETALLERESERVALEAKTEST.ID_RESERVA = APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA) AS CANTIDADPORABPROBAR, (SELECT ISNULL(COUNT(*),0) FROM APLICACIONSERVICIO_DETALLERESERVACONTENEDOR WHERE APLICACIONSERVICIO_DETALLERESERVACONTENEDOR.ID_RESERVA = APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA) AS CANTIDADPORCONTENEDOR FROM APLICACIONSERVICIO_RESERVALEAKTEST,APLICACIONSERVICIO_TIPORESERVALEAKTEST,APLICACIONSERVICIO_NAVIERA,APLICACIONSERVICIO_DEPOSITO,APLICACIONSERVICIO_CIUDAD,APLICACIONSERVICIO_PAIS,APLICACIONSERVICIO_ESTADORESERVALEAKTEST WHERE APLICACIONSERVICIO_RESERVALEAKTEST.ID_NAVIERA = APLICACIONSERVICIO_NAVIERA.ID_NAVIERA AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_DEPOSITO = APLICACIONSERVICIO_DEPOSITO.ID_DEPOSITO AND APLICACIONSERVICIO_DEPOSITO.ID_CIUDAD = APLICACIONSERVICIO_CIUDAD.ID_CIUDAD AND APLICACIONSERVICIO_CIUDAD.ID_PAIS = APLICACIONSERVICIO_PAIS.ID_PAIS AND APLICACIONSERVICIO_RESERVALEAKTEST.ESTADO = APLICACIONSERVICIO_ESTADORESERVALEAKTEST.ID_ESTADO AND APLICACIONSERVICIO_RESERVALEAKTEST.ID_TIPORESERVA = APLICACIONSERVICIO_TIPORESERVALEAKTEST.ID_TIPORESERVA AND CONVERT(DATE,APLICACIONSERVICIO_RESERVALEAKTEST.FECHAESTIMADA) BETWEEN CONVERT(DATE,@FECHAINICIAL) AND CONVERT(DATE,@FECHAFINAL) ORDER BY APLICACIONSERVICIO_RESERVALEAKTEST.ID_RESERVA ASC;", cnn);
                command.Parameters.AddWithValue("@FECHAINICIAL", FechaInicial);
                command.Parameters.AddWithValue("@FECHAFINAL", FechaFin);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Reservas.Reservas.Add(new Clases.ReservaLeaktest
                    {
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreNaviera = reader["NOMBRENAVIERA"].ToString(),
                        TipoReserva = reader["TIPORESERVA"].ToString(),
                        Id = Convert.ToInt32(reader["ID_RESERVA"]),
                        FechaEstimadaRealizacion = Convert.ToDateTime(reader["FECHAESTIMADA"]),
                        FechaRegistro = Convert.ToDateTime(reader["FECHAREGISTRO"]),
                        Hora = reader["HORA"].ToString(),
                        CantidadScrubber = Convert.ToInt32(reader["CANTIDADSCRUBBER"]),
                        Comentario = reader["COMENTARIO"].ToString()
                    });
                    Reservas.CantidadTotal.Add(Convert.ToInt32(reader["CANTIDADPORABPROBAR"]));
                    Reservas.CantidadPorContenedores.Add(Convert.ToInt32(reader["CANTIDADPORCONTENEDOR"]));
                    Reservas.Estado.Add(reader["ESTADO"].ToString());
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

        public static Clases.ResultadosLeaktest GetResultadosByFecha(DateTime FechaInicial, DateTime FechaFin, int IdReserva)
        {
            SqlConnection cnn;
            Clases.ResultadosLeaktest Resultado = new Clases.ResultadosLeaktest();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetResultadosLeaktestByFechas @IDRESERVA, @FECHAINICIAL, @FECHAFIN", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                command.Parameters.AddWithValue("@FECHAINICIAL", FechaInicial);
                command.Parameters.AddWithValue("@FECHAFIN", FechaFin);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.ResultadoLeaktest res = new Clases.ResultadoLeaktest();
                    if (reader["ID_LEAKTEST"] != DBNull.Value) res.Id = Convert.ToInt32(reader["ID_LEAKTEST"]);
                    if (reader["ID_RESERVA"] != DBNull.Value) res.IdReserva = Convert.ToInt32(reader["ID_RESERVA"]);
                    if (reader["ID_ESTADO"] != DBNull.Value) res.Estado = Convert.ToInt32(reader["ID_ESTADO"]);
                    if (reader["FECHAREALIZACION"] != DBNull.Value) res.FechaRealizacion = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    if (reader["NOMBRESERVICEPROVIDER"] != DBNull.Value) res.IdServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString();
                    if (reader["TIEMPO"] != DBNull.Value) res.Tiempo = reader["TIEMPO"].ToString();
                    if (reader["PRESION"] != DBNull.Value)
                    {
                        res.Presion = float.Parse(reader["PRESION"].ToString().Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (reader["NOMBREUSUARIO"] != DBNull.Value) res.IdTecnico = reader["NOMBREUSUARIO"].ToString();
                    if (reader["CONTENEDOR"] != DBNull.Value) res.Contenedor = reader["CONTENEDOR"].ToString();
                    if (reader["ANOCONTENEDOR"] != DBNull.Value) res.AnoContenedor = Convert.ToInt32(reader["ANOCONTENEDOR"]);
                    if (reader["NOMBRECAJA"] != DBNull.Value) res.CajaContenedor = reader["NOMBRECAJA"].ToString();
                    if (reader["NOMBREMAQUINARIA"] != DBNull.Value) res.IdMaquinaria = reader["NOMBREMAQUINARIA"].ToString();
                    if (reader["CONTROLADOR"] != DBNull.Value) res.Controlador = reader["CONTROLADOR"].ToString();
                    if (reader["BATERIA"] != DBNull.Value) res.Bateria = reader["BATERIA"].ToString();
                    if (reader["SCRUBBER"] != DBNull.Value) res.Scrubber = Convert.ToInt32(reader["SCRUBBER"]);
                    if (reader["SELLOPERNO1"] != DBNull.Value) res.Selloperno1 = reader["SELLOPERNO1"].ToString();
                    if (reader["SELLOPERNO2"] != DBNull.Value) res.Selloperno2 = reader["SELLOPERNO2"].ToString();
                    if (reader["SELLOTAPA"] != DBNull.Value) res.Sellotapa = reader["SELLOTAPA"].ToString();
                    if (reader["COMENTARIO"] != DBNull.Value) res.Comentario = reader["COMENTARIO"].ToString();
                    if (reader["KIT_CORTINA"] != DBNull.Value) res.KitCortina = Convert.ToInt32(reader["KIT_CORTINA"]);
                    if (reader["MODEM"] != DBNull.Value) res.Modem = reader["MODEM"].ToString();
                    if (reader["BATERIA_MODEM"] != DBNull.Value) res.BateriaModem = reader["BATERIA_MODEM"].ToString();
                    Resultado.Resultados.Add(res);
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
            return Resultado;
        }

        public static void ActualizarPreValidacionTecnica(int IdSevicio, int Estado)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("UPDATE APLICACIONSERVICIO_ResultadoLeaktestSP SET PREVALIDACIONTECNICA = @VALIDADO, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @IDRESULTADO", cnn);
                command.Parameters.AddWithValue("@IDRESULTADO", IdSevicio);
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

        public static List<Clases.ResultadoLeaktestAll> GetAllResultadosByFecha(DateTime FechaInicial, DateTime FechaFin)
        {
            SqlConnection cnn;
            List<Clases.ResultadoLeaktestAll> Resultados = new List<Clases.ResultadoLeaktestAll>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoLeaktestPorFechas @FECHAINICIAL, @FECHAFIN", cnn);
                command.Parameters.AddWithValue("@FECHAINICIAL", FechaInicial);
                command.Parameters.AddWithValue("@FECHAFIN", FechaFin);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Resultados.Add(new Clases.ResultadoLeaktestAll
                    {
                        IdLeaktest = Convert.ToInt32(reader["ID_RESULTADO"]),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        NombreDeposito = reader["NOMBREDEPOSITO"].ToString(),
                        NombreMaquinaria = reader["NOMBREMAQUINARIA"].ToString(),
                        NombreServiceProvider = reader["NOMBRESERVICEPROVIDER"].ToString(),
                        NombreUsuario = reader["NOMBREUSUARIO"].ToString(),
                        Tiempo = reader["TIEMPO"].ToString(),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        BateriaModem = reader["BATERIAMODEM"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Ano = Convert.ToInt32(reader["ANOCONTENEDOR"]),
                        NombreCaja = reader["CAJACONTENEDOR"].ToString(),
                        Fecha = Convert.ToDateTime(reader["FECHAREALIZACION"]),
                        Estado = Convert.ToInt32(reader["ID_ESTADO"]),
                        Scrubber = Convert.ToInt32(reader["SCRUBBER"]),
                        Selloperno1 = reader["SELLOPERNO1"].ToString(),
                        Selloperno2 = reader["SELLOPERNO2"].ToString(),
                        Sellotapa = reader["SELLOTAPA"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        KitCortina = Convert.ToInt32(reader["KIT_CORTINA"])
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
            return Resultados;
        }

        public static int CrearLeaktestContenedores(List<string> Contenedores, List<int> Maquinarias, int Naviera, int Deposito, DateTime? Eta, int Estado, int tiporeserva, string Hora = "", int CantidadScrubber = 0, string Comentario = "")
        {

            int Scrubber = 0;

            if (CantidadScrubber != 0)
            {
                Scrubber = 0;
            }
            else
            {
                Scrubber = 1;
            }

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarReservaLeaktestContenedores", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDDEPOSITO", SqlDbType.Int, 50).Value = Deposito;
            scCommand.Parameters.Add("@IDNAVIERA", SqlDbType.Int, 50).Value = Naviera;
            scCommand.Parameters.Add("@FECHAESTIMADA", SqlDbType.DateTime, 50).Value = Eta;
            scCommand.Parameters.Add("@ESTADO", SqlDbType.Int, 50).Value = Estado;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@TIPORESERVA", SqlDbType.Int, 50).Value = tiporeserva;
            scCommand.Parameters.Add("@HORA", SqlDbType.VarChar, 100).Value = Hora;
            scCommand.Parameters.Add("@CANTIDAD", SqlDbType.VarChar, 100).Value = Contenedores.Count();
            scCommand.Parameters.Add("@CANTIDADSCRUBBER", SqlDbType.VarChar, 100).Value = CantidadScrubber;
            scCommand.Parameters.Add("@ESTADOSCRUBBER", SqlDbType.VarChar, 100).Value = Scrubber;
            scCommand.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 500).Value = Comentario;
            var IdReserva = scCommand.Parameters.Add("@IDRESERVA", SqlDbType.Int, 50);
            IdReserva.Direction = ParameterDirection.ReturnValue;
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
                    return -1;
                }
                else
                {
                    var reserva = IdReserva.Value;
                    for ( int i = 0; i < Contenedores.Count(); i++ ) {
                        insertarContenedores(Convert.ToInt32(reserva), Contenedores[i].ToUpper(), Maquinarias[i]);
                    }
                    return Convert.ToInt32(reserva);
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

        public static void insertarContenedores(int IdReserva, string Contenedor, int maquinaria)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarContenedores", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDRESERVA", SqlDbType.Int, 50).Value = IdReserva;
            scCommand.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar, 500).Value = Contenedor;
            scCommand.Parameters.Add("@MAQUINARIA", SqlDbType.Int, 50).Value = maquinaria;
            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();
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

        public static List<Clases.DetalleContenedor> DetalleReservaLeaktestContenedores(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.DetalleContenedor> Detalles = new List<Clases.DetalleContenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Reserva.ID_RESERVA,ReservaLeaktest.CANTIDADSCRUBBER, Maquinaria.NOMBRE AS NOMBREMAQUINARIA, Contenedor.ID_CONTENEDOR, Contenedor.CONTENEDOR, Contenedor.ESTADO, ReservaLeaktest.MOTIVO FROM AplicacionServicio_DetalleReservaContenedor Reserva, AplicacionServicio_Contenedores Contenedor, AplicacionServicio_Maquinaria Maquinaria, AplicacionServicio_ReservaLeaktest ReservaLeaktest where Reserva.ID_RESERVA = @IDRESERVA and Reserva.CONTENEDOR = Contenedor.CONTENEDOR and Reserva.ID_MAQUINARIA = Maquinaria.ID_MAQUINARIA AND Reserva.ID_RESERVA = ReservaLeaktest.ID_RESERVA;", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Detalles.Add(new Clases.DetalleContenedor
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        NombreMaquinaria = reader["NOMBREMAQUINARIA"].ToString(),
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        IdEstado = Convert.ToInt32(reader["ESTADO"]),
                        CantidadScrubber = Convert.ToInt32(reader["CANTIDADSCRUBBER"]),
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
            return Detalles;
        }

        public static List<Clases.DetalleContenedor> GetDetalleReservaContenedores(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.DetalleContenedor> Reservas = new List<Clases.DetalleContenedor>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                    command = new SqlCommand("GetDetalleReservaContenedores", cnn);


                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IDRESERVA", SqlDbType.Int, 50).Value = IdReserva;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Reservas.Add(new Clases.DetalleContenedor
                    {
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        IdEstado = Convert.ToInt32(reader["ESTADO"]),
                        IdMaquinaria = Convert.ToInt32(reader["ID_MAQUINARIA"]),
                        NombreMaquinaria = reader["NOMBREMAQUINARIA"].ToString()
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

        public static int EditarReservaLeaktestContenedores(List<Clases.DetalleContenedor> ContenedoresAntiguos, int IdReserva, List<string> Contenedores, List<string> Maquinarias, int Naviera, int Deposito, DateTime? Eta, string Hora = "", int CantidadScrubber = 0)
        {
            int Scrubber = 0;

            if (CantidadScrubber != 0)
            {
                Scrubber = 0;
            }
            else
            {
                Scrubber = 1;
            }

            SqlConnection cnn1;
            cnn1 = new SqlConnection(connectionString);
            try
            {
                cnn1.Open();
                SqlCommand command1 = new SqlCommand("UPDATE aplicacionservicio_ReservaLeaktest SET ID_DEPOSITO= @IDDEPOSITO, ID_NAVIERA = @IDNAVIERA, FECHAESTIMADA = @FECHA, HORA = @HORA, USUARIO = @USUARIO, CANTIDADSCRUBBER = @CANTIDADSCRUBBER, ESTADOSCRUBBER = @ESTADOSCRUBBER  Where ID_RESERVA = @IDRESERVA", cnn1);
                command1.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                command1.Parameters.AddWithValue("@IDDEPOSITO", Deposito);
                command1.Parameters.AddWithValue("@IDNAVIERA", Naviera);
                command1.Parameters.AddWithValue("@FECHA", Eta);
                command1.Parameters.AddWithValue("@HORA", Hora);
                command1.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command1.Parameters.AddWithValue("@CANTIDADSCRUBBER", CantidadScrubber);
                command1.Parameters.AddWithValue("@ESTADOSCRUBBER", Scrubber);
                command1.ExecuteNonQuery();

                int validar = command1.ExecuteNonQuery();

                if (validar == 1)
                {

                    for (int i = 0; i < Contenedores.Count(); i++)
                    {
                        SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_DetalleReservaContenedor SET CONTENEDOR = @CONTENEDOR, ID_MAQUINARIA = @IDMAQUINARIA Where ID_RESERVA = @IDRESERVA AND CONTENEDOR = @CONTENEDORANTIGUO", cnn1);
                        command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                        command.Parameters.AddWithValue("@CONTENEDOR", Contenedores[i]);
                        command.Parameters.AddWithValue("@CONTENEDORANTIGUO", ContenedoresAntiguos[i].Contenedor);
                        command.Parameters.AddWithValue("@IDMAQUINARIA", Maquinarias[i]);
                        command.ExecuteNonQuery();
                    }

                    return 0;
                }
                else {
                    return 1;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn1.Close();
            }
        }

        public static int ValidarContenedor(string Contenedor)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("ValidarContenedores", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar, 50).Value = Contenedor;
            var Cantidad = scCommand.Parameters.Add("@VALOR", SqlDbType.Int, 50);
            Cantidad.Direction = ParameterDirection.ReturnValue;
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
                    var cantidad = Cantidad.Value;
                    // 7 validacion de que existe contenedor pero ya esta reservador por otra reserva
                    if (Convert.ToInt32(cantidad) == 7) {
                        return 7;
                    }
                    // 4 validacion de que existe contenedor que esta en viaje
                    else if (Convert.ToInt32(cantidad) == 4) {
                        return 4;
                    }
                    // 1 validacion de que existe contenedor que esta Con LeaktestRealizado
                    else if (Convert.ToInt32(cantidad) == 1) {
                        return 1;
                    }
                    else {
                        return 0;
                    }                 
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

        public static int CancelarReservaLeaktestContenedores(int IdReserva, string Motivo)
        {
            int respuesta = 1;

            List<Clases.DetalleContenedor> Contenedores = new List<Clases.DetalleContenedor>();
            Contenedores = ContenedorModelo.GetAllContenedoresByReserva(IdReserva);

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_ReservaLeaktest SET ESTADO = 3, MOTIVO = @MOTIVO WHERE ID_RESERVA = @IDRESERVA", cnn);
                command.Parameters.AddWithValue("@MOTIVO", Motivo);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);
                command.ExecuteNonQuery();

                for (int i = 0; i< Contenedores.Count();  i++) {

                    int respuesta_cambio = ContenedorModelo.CambiarEstadoContenedor(Contenedores[i].Contenedor, 6);
                    //SqlCommand command1 = new SqlCommand("update aplicacionservicio_contenedores set estado = 6 where contenedor = @CONTENEDOR;", cnn);
                    //command1.Parameters.AddWithValue("@CONTENEDOR", Contenedores[i].Contenedor);
                    //command1.ExecuteNonQuery();
                }

                respuesta = 0;

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

        public static List<Clases.TipoSolicitudLeaktest> GetTipoSolicitud()
        { 
            SqlConnection cnn;
            List<Clases.TipoSolicitudLeaktest> Detalles = new List<Clases.TipoSolicitudLeaktest>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM APLICACIONSERVICIO_TIPORESERVALEAKTEST WHERE ID_TIPORESERVA <> 1", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Detalles.Add(new Clases.TipoSolicitudLeaktest
                    {
                        Id = Convert.ToInt32(reader["ID_TIPORESERVA"]),
                        Nombre = reader["DESCRIPCION"].ToString(),
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
            return Detalles;
        }

        public static List<Clases.ResultadoLeaktestAll> GetSolicitudes()
        {
            SqlConnection cnn;
            List<Clases.ResultadoLeaktestAll> Resultados = new List<Clases.ResultadoLeaktestAll>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_RESERVA FROM APLICACIONSERVICIO_RESERVALEAKTEST;", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Resultados.Add(new Clases.ResultadoLeaktestAll
                    {
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
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
            return Resultados;
        }

        public static int TraspasarResultados(string Resultados, int IdAntiguo, int IdNuevo, string Estado, string Maquinaria, string Contenedor)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result, i = 0;
            SqlCommand scCommand = new SqlCommand("TraspasarResultado", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDRESULTADO", SqlDbType.Int, 50).Value = Resultados;
            scCommand.Parameters.Add("@IDANTIGUO", SqlDbType.Int, 50).Value = IdAntiguo;
            scCommand.Parameters.Add("@IDNUEVO", SqlDbType.Int, 50).Value = IdNuevo;
            scCommand.Parameters.Add("@ESTADO", SqlDbType.VarChar, 50).Value = Estado;
            scCommand.Parameters.Add("@MAQUINARIA", SqlDbType.VarChar, 50).Value = Maquinaria;
            scCommand.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar, 50).Value = Contenedor;

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

        public static int ValidarMaquinaria(int IdSolicitud, string IdMaquinaria)
        {
            SqlConnection cnn;
            int Cuenta = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) AS CUENTA FROM APLICACIONSERVICIO_DETALLERESERVALEAKTEST WHERE ID_MAQUINARIA = (SELECT ID_MAQUINARIA FROM APLICACIONSERVICIO_MAQUINARIA WHERE NOMBRE = @IDMAQUINARIA) AND ID_RESERVA = @IDRESERVA", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdSolicitud);
                command.Parameters.AddWithValue("@IDMAQUINARIA", IdMaquinaria);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cuenta = Convert.ToInt32(reader["CUENTA"]);
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
            return Cuenta;
        }

        public static int CancelarLeaktestSP(int Leaktest)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.CancelarLeaktest @ID_LEAKTEST, @USUARIO", cnn);
                command.Parameters.AddWithValue("@ID_LEAKTEST", Leaktest);
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

        public static int DescancelarLeaktestSP(int Leaktest)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.DescancelarLeaktest @ID_LEAKTEST, @USUARIO", cnn);
                command.Parameters.AddWithValue("@ID_LEAKTEST", Leaktest);
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

        public static int EliminarLeaktestSP(int Leaktest)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.EliminarLeaktest @ID_LEAKTEST, @USUARIO", cnn);
                command.Parameters.AddWithValue("@ID_LEAKTEST", Leaktest);
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

        public static int ValidarLeaktestSP(int Leaktest, int Solicitud)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.ValidarLeaktestSP @ID_LEAKTEST, @ID_RESERVA, @USUARIO", cnn);
                command.Parameters.AddWithValue("@ID_LEAKTEST", Leaktest);
                command.Parameters.AddWithValue("@ID_RESERVA", Solicitud);
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

        public static int IngresarSolicitudLeaktestSP(string SP, string Deposito, string Naviera, int cantleak)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int solicitud = 0;
            SqlCommand scCommand = new SqlCommand("IngresarReservaLeaktestSP", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@SERVICEPROVIDER", SqlDbType.VarChar, 50).Value = SP;
            scCommand.Parameters.Add("@DEPOSITO", SqlDbType.VarChar, 50).Value = Deposito;
            scCommand.Parameters.Add("@NAVIERA", SqlDbType.VarChar, 50).Value = Naviera;
            scCommand.Parameters.Add("@CANTIDAD", SqlDbType.Int, 50).Value = cantleak;
            scCommand.Parameters.Add("@REALIZADOS", SqlDbType.Int, 50).Value = cantleak;
            scCommand.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }

                SqlDataReader reader = scCommand.ExecuteReader();
                while (reader.Read())
                {
                    solicitud = Convert.ToInt32(reader["ID_RESERVA"]);
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
            return solicitud;
        }

        public static int EdicionMasivaLeaktest(int Leaktest, int ServiceProvider = 0, int Naviera = 0, int Deposito = 0, DateTime? FechaEstimada = null, string HoraEstimada = "", int Commodity = 0, int Maquinaria = 0,
                                   int AnoContenedor = 0, int Tecnico = 0, string Tiempo = "", DateTime? FechaEjecucion = null, int Estado = 0, string Comentario = "", int PanelReutilizado=0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();
                //CAMBIOS DE DATOS DE LEAKTEST AL RESULTADO ASOCIADO
                if (ServiceProvider != 0)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_SERVICEPROVIDER=@ID_SERVICEPROVIDER, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_SERVICEPROVIDER", ServiceProvider);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());

                }
                else if (Naviera != 0)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_NAVIERA=@ID_NAVIERA, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_NAVIERA", Naviera);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Deposito != 0)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_DEPOSITO = @ID_DEPOSITO, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_DEPOSITO", Deposito);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (FechaEstimada != null)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET FECHAESTIMADA = @FECHAESTIMADA, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@FECHAESTIMADA", FechaEstimada);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (HoraEstimada != "" && HoraEstimada!="--:--" && HoraEstimada!="00:00")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET HORAESTIMADA = @HORAESTIMADA, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@HORAESTIMADA", HoraEstimada);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Commodity != 0)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_COMMODITY = @COMMODITY, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@COMMODITY", Commodity);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Maquinaria != 0)
                {
                    command = new SqlCommand("EXEC dbo.AsignarMaquinariaSP @ID_MAQUINARIA, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ID_MAQUINARIA", Maquinaria);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (AnoContenedor != 0)
                {
                    command = new SqlCommand("EXEC dbo.AsignarAnoContenedorSP @ANOCONTENEDOR, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ANOCONTENEDOR", AnoContenedor);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Tecnico != 0 && Tecnico!=3)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET ID_TECNICO=@ID_TECNICO, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@ID_TECNICO", Tecnico);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Tiempo != "" && Tiempo != "--:--" && Tiempo != "00:00")
                {
                    command = new SqlCommand("EXEC dbo.AsignarTiempoLeaktestSP @TIEMPO, @SERVICIO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@TIEMPO", Tiempo);
                    command.Parameters.AddWithValue("@SERVICIO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (FechaEjecucion != null)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET FECHAREALIZACION=@FECHAREALIZACION, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@FECHAREALIZACION", FechaEjecucion);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Estado != 0)
                {
                    command = new SqlCommand("EXEC dbo.AsignarEstadoContenedorSP @ID_ESTADO, @ID_RESULTADO, @USUARIO", cnn);
                    command.Parameters.AddWithValue("@ID_ESTADO", Estado);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (Comentario != "")
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET COMENTARIO=@COMENTARIO, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@COMENTARIO", Comentario);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
                    command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                }
                else if (PanelReutilizado != 0)
                {
                    command = new SqlCommand("UPDATE AplicacionServicio_ResultadoLeaktestSP SET PANELREUTILIZADO=@PANELREUTILIZADO, USUARIOACCION = @USUARIO WHERE ID_RESULTADO = @ID_RESULTADO", cnn);
                    command.Parameters.AddWithValue("@PANELREUTILIZADO", PanelReutilizado);
                    command.Parameters.AddWithValue("@ID_RESULTADO", Leaktest);
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

        public static int QuitarControlador(string Controlador, int IdResultado)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand("EXEC dbo.QuitarControlador @ID_RESULTADO, @CONTROLADOR", cnn);
                command.Parameters.AddWithValue("@ID_RESULTADO", IdResultado);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);

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

    }
}