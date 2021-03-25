using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Plataforma.Models.Reservas;

namespace Plataforma.Models.Bateria
{
    public class BateriaModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static List<Clases.Bateria> GetBaterias()
        {
            SqlConnection cnn;
            List<Clases.Bateria> Baterias = new List<Clases.Bateria>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "EXEC dbo.ConsultarBaterias";
                SqlCommand command = new SqlCommand(consulta, cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaAsociacion = null;
                    string esn = "";
                    int contDiasTravel = 0;
                    int contDiasSleep = 0;

                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        fechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"]);
                    }

                    if (reader["NUMCONTROLADOR"] != DBNull.Value || reader["NUMCONTROLADOR"].ToString() != "")
                    {
                        esn = reader["NUMCONTROLADOR"].ToString();
                    }
                    else
                    {
                        esn = reader["NUMMODEM"].ToString();
                    }

                    if (reader["CONT_DIAS_TRAVELING"] != DBNull.Value)
                    {
                        contDiasTravel = Convert.ToInt32(reader["CONT_DIAS_TRAVELING"]);
                    }

                    if (reader["CONT_DIAS_SLEEP"] != DBNull.Value)
                    {
                        contDiasSleep = Convert.ToInt32(reader["CONT_DIAS_SLEEP"]);
                    }

                    Clases.Bateria bateria = new Clases.Bateria();

                    bateria.Checkbox = "";
                    bateria.IdBateria = Convert.ToInt32(reader["ID_BATERIA"]);
                    bateria.NumBateria = reader["NUM_BATERIA"].ToString();
                    bateria.UsuarioPrueba = reader["USUARIO_PRUEBA"].ToString();
                    bateria.FechaAsociacion = fechaAsociacion;
                    bateria.DiasInstalacion = Convert.ToInt32(reader["DIAS_INSTALACION"]);
                    bateria.Estado = reader["ESTADOBATERIA"].ToString();
                    bateria.TipoESN = reader["TIPO_ESN"].ToString();
                    bateria.Controlador = reader["NUMCONTROLADOR"].ToString();
                    bateria.Modem = reader["NUMMODEM"].ToString();
                    bateria.ESN = esn;
                    bateria.Modelo = reader["MODELO"].ToString();
                    bateria.CONT_DIAS_TRAVELING= contDiasTravel;
                    bateria.CONT_DIAS_SLEEP = contDiasSleep;
                    Baterias.Add(bateria);
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
            return Baterias;
        }

        public static string GetBateriaByControlador(string Controlador)
        {
            SqlConnection cnn;
            string Bateria = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarBateriaPorControlador";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@CONTROLADOR", SqlDbType.VarChar, 50).Value = Controlador;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bateria = reader["NUM_BATERIA"].ToString();
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
            return Bateria;
        }

        public static string GetBateriaByModem(string Modem)
        {
            SqlConnection cnn;
            string Bateria = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarBateriaPorModem";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@MODEM", SqlDbType.VarChar, 50).Value = Modem;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bateria = reader["NUM_BATERIA"].ToString();
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
            return Bateria;
        }

        public static List<Clases.Bateria> GetBateriasCargadas()
        {
            SqlConnection cnn;
            List<Clases.Bateria> Baterias = new List<Clases.Bateria>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "EXEC dbo.ConsultarBateriasCargadas";
                SqlCommand command = new SqlCommand(consulta, cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaAsociacion = null;
                    string esn = "";

                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        fechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"]);
                    }

                    if (reader["NUMCONTROLADOR"] != DBNull.Value || reader["NUMCONTROLADOR"].ToString() != "")
                    {
                        esn = reader["NUMCONTROLADOR"].ToString();
                    }
                    else
                    {
                        esn = reader["NUMMODEM"].ToString();
                    }


                    Baterias.Add(new Clases.Bateria
                    {
                        Checkbox = "",
                        IdBateria = Convert.ToInt32(reader["ID_BATERIA"]),
                        NumBateria = reader["NUM_BATERIA"].ToString(),
                        UsuarioPrueba = reader["USUARIO_PRUEBA"].ToString(),
                        FechaAsociacion = fechaAsociacion,
                        DiasInstalacion = Convert.ToInt32(reader["DIAS_INSTALACION"]),
                        Estado = reader["ESTADOBATERIA"].ToString(),
                        TipoESN = reader["TIPO_ESN"].ToString(),
                        Controlador = reader["NUMCONTROLADOR"].ToString(),
                        Modem = reader["NUMMODEM"].ToString(),
                        ESN = esn,
                        Modelo = reader["MODELO"].ToString(),
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
            return Baterias;
        }

        public static Clases.Bateria ObtenerEstadoBateria(string NumBateria = "")
        {
            SqlConnection cnn;
            Clases.Bateria bateria = new Clases.Bateria();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarBateriaPorNumero";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NUM_BATERIA", SqlDbType.VarChar, 50).Value = NumBateria;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaAsociacion = null;
                    string esn = "";

                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        fechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"]);
                    }

                    if (reader["NUMCONTROLADOR"] != DBNull.Value || reader["NUMCONTROLADOR"].ToString() != "")
                    {
                        esn = reader["NUMCONTROLADOR"].ToString();
                    }
                    else
                    {
                        esn = reader["NUMMODEM"].ToString();
                    }


                    bateria.IdBateria = Convert.ToInt32(reader["ID_BATERIA"]);
                    bateria.NumBateria = reader["NUM_BATERIA"].ToString();
                    bateria.FechaAsociacion = fechaAsociacion;
                    bateria.DiasInstalacion = Convert.ToInt32(reader["DIAS_INSTALACION"]);
                    bateria.Estado = reader["ESTADOBATERIA"].ToString();
                    bateria.TipoESN = reader["TIPO_ESN"].ToString();
                    bateria.Controlador = reader["NUMCONTROLADOR"].ToString();
                    bateria.Modem = reader["NUMMODEM"].ToString();
                    bateria.ESN = esn;
                    bateria.UsuarioAsociacion = reader["USUARIO_ASOCIACION"].ToString();
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
            return bateria;
        }

        public static Clases.Bateria GetEquipoAsociado(string NumBateria = "")
        {
            SqlConnection cnn;
            Clases.Bateria bateria = new Clases.Bateria();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarEquipoESNAsociado";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NUMBATERIA", SqlDbType.VarChar, 50).Value = NumBateria;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaAsociacion = null;
                    string esn = "";

                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        fechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"]);
                    }

                    if (reader["NUMCONTROLADOR"] != DBNull.Value || reader["NUMCONTROLADOR"].ToString() != "")
                    {
                        esn = reader["NUMCONTROLADOR"].ToString();
                    }
                    else
                    {
                        esn = reader["NUMMODEM"].ToString();
                    }


                    bateria.IdBateria = Convert.ToInt32(reader["ID_BATERIA"]);
                    bateria.NumBateria = reader["NUM_BATERIA"].ToString();
                    bateria.FechaAsociacion = fechaAsociacion;
                    bateria.DiasInstalacion = Convert.ToInt32(reader["DIAS_INSTALACION"]);
                    bateria.Estado = reader["ESTADOBATERIA"].ToString();
                    bateria.TipoESN = reader["TIPO_ESN"].ToString();
                    bateria.Controlador = reader["NUMCONTROLADOR"].ToString();
                    bateria.Modem = reader["NUMMODEM"].ToString();
                    bateria.ESN = esn;
                    bateria.UsuarioAsociacion = reader["USUARIO_ASOCIACION"].ToString();
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
            return bateria;
        }

        public static Clases.Bateria GetEquipoAsociadoById(int IdBateria = 0)
        {
            SqlConnection cnn;
            Clases.Bateria bateria = new Clases.Bateria();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarEquipoESNAsociadoById";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_BATERIA", SqlDbType.Int, 50).Value = IdBateria;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaAsociacion = null;
                    string esn = "";

                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        fechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"]);
                    }

                    if (reader["NUMCONTROLADOR"] != DBNull.Value || reader["NUMCONTROLADOR"].ToString() != "")
                    {
                        esn = reader["NUMCONTROLADOR"].ToString();
                    }
                    else
                    {
                        esn = reader["NUMMODEM"].ToString();
                    }


                    bateria.IdBateria = Convert.ToInt32(reader["ID_BATERIA"]);
                    bateria.NumBateria = reader["NUM_BATERIA"].ToString();
                    bateria.FechaAsociacion = fechaAsociacion;
                    bateria.DiasInstalacion = Convert.ToInt32(reader["DIAS_INSTALACION"]);
                    bateria.Estado = reader["ESTADOBATERIA"].ToString();
                    bateria.TipoESN = reader["TIPO_ESN"].ToString();
                    bateria.Controlador = reader["NUMCONTROLADOR"].ToString();
                    bateria.Modem = reader["NUMMODEM"].ToString();
                    bateria.ESN = esn;
                    bateria.UsuarioAsociacion = reader["USUARIO_ASOCIACION"].ToString();
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
            return bateria;
        }

        public static Clases.Bateria GetEquipoAsociadoByNumBateria(string NumBateria = "")
        {
            SqlConnection cnn;
            Clases.Bateria bateria = new Clases.Bateria();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarEquipoESNAsociadoByNumBateria";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NUM_BATERIA", SqlDbType.VarChar, 50).Value = NumBateria;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaAsociacion = null;
                    string esn = "";

                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        fechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"]);
                    }

                    if (reader["NUMCONTROLADOR"] != DBNull.Value || reader["NUMCONTROLADOR"].ToString() != "")
                    {
                        esn = reader["NUMCONTROLADOR"].ToString();
                    }
                    else
                    {
                        esn = reader["NUMMODEM"].ToString();
                    }


                    bateria.IdBateria = Convert.ToInt32(reader["ID_BATERIA"]);
                    bateria.NumBateria = reader["NUM_BATERIA"].ToString();
                    bateria.FechaAsociacion = fechaAsociacion;
                    bateria.DiasInstalacion = Convert.ToInt32(reader["DIAS_INSTALACION"]);
                    bateria.Estado = reader["ESTADOBATERIA"].ToString();
                    bateria.TipoESN = reader["TIPO_ESN"].ToString();
                    bateria.Controlador = reader["NUMCONTROLADOR"].ToString();
                    bateria.Modem = reader["NUMMODEM"].ToString();
                    bateria.ESN = esn;
                    bateria.UsuarioAsociacion = reader["USUARIO_ASOCIACION"].ToString();
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
            return bateria;
        }

        public static int GuardarBaterias(List<Clases.Bateria> Baterias)
        {
            SqlConnection cnn;
            int result = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                foreach (Clases.Bateria bateria in Baterias)
                {
                    cnn.Open();
                    string consulta = "GuardarBateria";
                    SqlCommand command = new SqlCommand(consulta, cnn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@NUMBATERIA", SqlDbType.VarChar, 50).Value = bateria.NumBateria;
                    command.Parameters.Add("@USUARIO_PRUEBA", SqlDbType.VarChar, 50).Value = bateria.UsuarioPrueba;
                    command.Parameters.Add("@TENSION_VACIO", SqlDbType.Float, 50).Value = bateria.TensionVacio;
                    command.Parameters.Add("@VALOR", SqlDbType.Int, 50).Value = bateria.Valor;
                    command.Parameters.Add("@TENSION_CARGA", SqlDbType.Float, 50).Value = bateria.TensionCarga;
                    command.Parameters.Add("@DIFERENCIA_VOLTAJE", SqlDbType.Float, 50).Value = bateria.DiferenciaVoltaje;
                    command.Parameters.Add("@RESULTADO", SqlDbType.VarChar, 50).Value = bateria.Resultado;
                    command.Parameters.Add("@MODELO", SqlDbType.VarChar, 50).Value = bateria.Modelo;
                    command.Parameters.Add("@ESTADO", SqlDbType.VarChar, 50).Value = bateria.Estado;

                    result = command.ExecuteNonQuery();
                    cnn.Close();
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

            return result;
        }

        public static int AsociarBateria(string NumBateria = "", string ESN = "", string TipoESN = "", DateTime? FechaAsociacion = null)
        {
            int estado_dispositivo = 0;
            int contador_traveling = -1;
            int contador_sleep = -1;

            if (TipoESN == "CONTROLADOR")
            {
                bool controladorEnergizado = false;
                controladorEnergizado = ReservaModelo.VerificarControladorEnergizado(ESN);
                int idServicio = ReservaModelo.obtenerIdServicioPorESN(ESN, 1);
                int respuestaEdicionServicio = ReservaModelo.actualizarServicioControladorEnergizado(idServicio, controladorEnergizado);
                if (controladorEnergizado == false)
                {
                    int alerta_controladorEnergizado = ReservaModelo.ObtenerAlertaControlador(39, ESN);
                    if (alerta_controladorEnergizado == 0)
                    {
                        ReservaModelo.GenerarAlertaControlador(39, ESN);
                    }
                }

                estado_dispositivo = ReservaModelo.ObtenerEstadoControladorAppTecnica(ESN);
                if (estado_dispositivo == 2) // 1: SLEEP, 2: TRAVELING, 3: OFF, 4: OUT OF ORDER
                {
                    contador_traveling = 0;
                }
                else if (estado_dispositivo == 1)
                {
                    contador_sleep = 0;
                }
            }
            else
            {
                estado_dispositivo = ReservaModelo.ObtenerEstadoModemAppTecnica(ESN);

                if (estado_dispositivo == 3) // 1: STORAGE, 2: PRE SERVICE, 3: ON SERVICE TRIP, 4: RETRIEVAL, 5: LABORATORY
                {
                    contador_traveling = 0;
                }
                else if (estado_dispositivo == 2)
                {
                    contador_sleep = 0;
                }
            }

            if (FechaAsociacion == null)
            {
                FechaAsociacion = DateTime.Now;
            }


            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("EXEC dbo.AsociarBateria @NUM_BATERIA, @ESN, @TIPO_ESN, @FECHA_ASOCIACION, @USUARIO, @CONTADOR_TRAVELING, @CONTADOR_SLEEP", cnn);
                command.Parameters.AddWithValue("@NUM_BATERIA", NumBateria);
                command.Parameters.AddWithValue("@ESN", ESN);
                command.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
                command.Parameters.AddWithValue("@FECHA_ASOCIACION", FechaAsociacion);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@CONTADOR_TRAVELING", contador_traveling);
                command.Parameters.AddWithValue("@CONTADOR_SLEEP", contador_sleep);

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

        public static int DesasociarBateria(string NumBateria = "", string ESN = "", string TipoESN = "")
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("EXEC dbo.DesasociarBateria @NUM_BATERIA, @ESN, @TIPO_ESN", cnn);
                command.Parameters.AddWithValue("@NUM_BATERIA", NumBateria);
                command.Parameters.AddWithValue("@ESN", ESN);
                command.Parameters.AddWithValue("@TIPO_ESN", TipoESN);

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

        public static int AsociarBateriaPorId(int IdBateria = 0, string ESN = "", string TipoESN = "", DateTime? FechaAsociacion = null)
        {
            int estado_dispositivo = 0;
            int contador_traveling = -1;
            int contador_sleep = -1;

            if (TipoESN == "CONTROLADOR")
            {
                estado_dispositivo = ReservaModelo.ObtenerEstadoControladorAppTecnica(ESN);

                if (estado_dispositivo == 2) // 1: SLEEP, 2: TRAVELING, 3: OFF, 4: OUT OF ORDER
                {
                    contador_traveling = 0;
                }
                else if (estado_dispositivo == 1)
                {
                    contador_sleep = 0;
                }
            }
            else
            {
                estado_dispositivo = ReservaModelo.ObtenerEstadoModemAppTecnica(ESN);

                if (estado_dispositivo == 3) // 1: STORAGE, 2: PRE SERVICE, 3: ON SERVICE TRIP, 4: RETRIEVAL, 5: LABORATORY
                {
                    contador_traveling = 0;
                }
                else if (estado_dispositivo == 2)
                {
                    contador_sleep = 0;
                }
            }

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {

                cnn.Open();
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("EXEC dbo.AsociarBateriaPorId @ID_BATERIA, @ESN, @TIPO_ESN, @FECHA_ASOCIACION, @USUARIO, @CONTADOR_TRAVELING, @CONTADOR_SLEEP", cnn);
                command.Parameters.AddWithValue("@ID_BATERIA", IdBateria);
                command.Parameters.AddWithValue("@ESN", ESN);
                command.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
                command.Parameters.AddWithValue("@FECHA_ASOCIACION", FechaAsociacion);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@CONTADOR_TRAVELING", contador_traveling);
                command.Parameters.AddWithValue("@CONTADOR_SLEEP", contador_sleep);

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

        public static int DescartarBateria(string NumBateria)
        {
            SqlConnection cnn;
            int result = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "DescartarBateria";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NUMBATERIA", SqlDbType.VarChar, 50).Value = NumBateria;

                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        public static int VerificarEstadoBateriaAlerta(string NumBateria)
        {
            SqlConnection cnn;
            int result = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "VerificarEstadoBateriaAlerta";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NUM_BATERIA", SqlDbType.VarChar, 50).Value = NumBateria;

                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        public static int VerificarEstadoBateriaAlertaPorId(int IdBateria = 0)
        {
            SqlConnection cnn;
            int result = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "VerificarEstadoBateriaAlertaPorId";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_BATERIA", SqlDbType.VarChar, 50).Value = IdBateria;

                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        public static List<Clases.Bateria> GetAlertasBaterias(List<Clases.Bateria> Baterias)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            foreach (Clases.Bateria bateria in Baterias)
            {
                try
                {
                    cnn.Open();
                    SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAlertaBateria @ID_BATERIA", cnn);
                    command.Parameters.AddWithValue("@ID_BATERIA", bateria.IdBateria);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string estado = "";
                        estado = bateria.Estado;

                        if (reader["DETALLE_ALERTA"] != DBNull.Value)
                        {
                            bateria.Estado = reader["DETALLE_ALERTA"].ToString() + " - " + estado;
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
            }


            return Baterias;
        }

        public static string ObtenerEstadoBateriaByServicio(int IdServicio = 0)
        {
            SqlConnection cnn;
            string estadoBateria = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarEstadoBateriaByServicio";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_SERVICIO", SqlDbType.Int, 50).Value = IdServicio;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    estadoBateria = reader["ESTADO"].ToString();
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
            return estadoBateria;
        }

        public static int ValidarBateria(string NumBateria = "")
        {
            SqlConnection cnn;
            int estadoBateria = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "ConsultarEstadoBateriaByNumero";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@NUM_BATERIA", SqlDbType.VarChar, 50).Value = NumBateria;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    estadoBateria = Convert.ToInt32(reader["ID_ESTADOBATERIA"]);
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
            return estadoBateria;
        }

    }
}