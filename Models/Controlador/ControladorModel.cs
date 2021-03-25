using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.IO;

using System.Data.OleDb;
using System.Configuration;

namespace Plataforma.Models.Controlador
{
    public class ControladorModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo;Connect Timeout=10";

        public static List<Clases.Controlador> GetControladores()
        {
            SqlConnection cnn;
            List<Clases.Controlador> Controladores = new List<Clases.Controlador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_CONTROLADOR, ESTADOTECNICO, NUMCONTROLADOR FROM AplicacionServicio_Controlador", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Controladores.Add(new Clases.Controlador
                    {
                        Id = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        EstadoTecnico = reader["ESTADOTECNICO"].ToString(),
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
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

        public static List<Clases.HistoricoControlador> GetHistoricoControlador()
        {
            SqlConnection cnn;
            List<Clases.HistoricoControlador> Controladores = new List<Clases.HistoricoControlador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarHistoricoControlador", cnn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                DateTime? FechaEnvio = null;
                DateTime? FechaEntrada = null;
                DateTime? FechaRecuperacion = null;
                while (reader.Read())
                {
                    if (reader["FECHAENVIO"] != DBNull.Value)
                    {
                        FechaEnvio = Convert.ToDateTime(reader["FECHAENVIO"]);
                    }
                    else
                    {
                        FechaEnvio = null;
                    }

                    if (reader["FECHAENTRADA"] != DBNull.Value)
                    {
                        FechaEntrada = Convert.ToDateTime(reader["FECHAENTRADA"]);
                    }
                    else
                    {
                        FechaEntrada = null;
                    }

                    if (reader["FECHARECUPERACION"] != DBNull.Value)
                    {
                        FechaRecuperacion = Convert.ToDateTime(reader["FECHARECUPERACION"]);
                    }
                    else
                    {
                        FechaRecuperacion = null;
                    }

                    Controladores.Add(new Clases.HistoricoControlador
                    {
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
                        IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        Modem = reader["MODEM"].ToString(),
                        TipoMovimiento = reader["TIPOMOVIMIENTO"].ToString(),
                        TipoLugarOrigen = reader["TIPOLUGARORIGEN"].ToString(),
                        LugarOrigen = reader["LUGARORIGEN"].ToString(),
                        TipoLugarDestino = reader["TIPOLUGARDESTINO"].ToString(),
                        LugarDestino = reader["LUGARDESTINO"].ToString(),
                        TipoLugarRecuperacion = reader["TIPOLUGARRECUPERACION"].ToString(),
                        LugarRecuperacion = reader["LUGARRECUPERACION"].ToString(),
                        FechaEnvio = FechaEnvio,
                        FechaEntrada = FechaEntrada,
                        IdMovimiento = Convert.ToInt32(reader["ID_MOV_LOGISTICO"]),
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

        public static List<Clases.Controlador> GetControladores1()
        {
            SqlConnection cnn;
            List<Clases.Controlador> Controladores = new List<Clases.Controlador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Controlador", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Controladores.Add(new Clases.Controlador
                    {
                        Id = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
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

        public static List<Clases.MovimientoLogistico> GetControladorById(int IdControlador)
        {
            SqlConnection cnn;
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarControladorById", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CONTROLADOR", SqlDbType.Int, 50).Value = IdControlador;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string Eta = "";

                    if (reader["ETALOGISTICA"] != DBNull.Value)
                    {
                        Eta = Convert.ToDateTime(reader["ETALOGISTICA"]).ToString("dd/MM/yyyy");
                    }
                    else if (reader["ETA"] != DBNull.Value)
                    {
                        Eta = Convert.ToDateTime(reader["ETA"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        Eta = "";
                    }


                    Controladores.Add(new Clases.MovimientoLogistico
                    {
                        Controlador = reader["CONTROLADOR"].ToString(),
                        ContinenteOrigen = reader["CONTINENTE_ORIGEN"].ToString(),
                        ContinenteDestino = reader["CONTINENTE_DESTINO"].ToString(),
                        PaisOrigen = reader["PAIS_ORIGEN"].ToString(),
                        PaisDestino = reader["PAIS_DESTINO"].ToString(),
                        CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                        CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                        TipoNodoOrigen = reader["TIPO_ORIGEN"].ToString(),
                        TipoNodoDestino = reader["TIPO_DESTINO"].ToString(),
                        NodoOrigen = reader["NODO_ORIGEN"].ToString(),
                        NodoDestino = reader["NODO_DESTINO"].ToString(),
                        RetrievalProvider = reader["RETRIEVAL_PROVIDER"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        EtaString = reader["ETA"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Etastring = Eta,
                        UsuarioMovimiento = reader["USUARIO_MOVIMIENTO"].ToString(),
                        UsuarioRecuperacion = reader["USUARIO_RECUPERACION"].ToString(),
                        Prioridad = Convert.ToInt32(reader["PRIORIDAD"]),
                        TipoMovimiento = Convert.ToInt32(reader["TIPO_MOVIMIENTO"])
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

        public static List<Clases.MovimientoLogistico> GetESNById(int IdESN, string Tipo)
        {
            SqlConnection cnn;
            List<Clases.MovimientoLogistico> Controladores = new List<Clases.MovimientoLogistico>();
            cnn = new SqlConnection(connectionString);
            DateTime? fechaenvio = null;
            DateTime? fechaarribo = null;
            DateTime? fecha_ult_acc_log = null;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarESNById", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_ESN", SqlDbType.Int, 50).Value = IdESN;
                command.Parameters.Add("@TIPO", SqlDbType.VarChar, 20).Value = Tipo;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["FECHAENVIO"] != DBNull.Value)
                    {
                        fechaenvio = Convert.ToDateTime(reader["FECHAENVIO"]);
                    }
                    else
                    {
                        fechaenvio = null;
                    }

                    if (reader["FECHAARRIBO"] != DBNull.Value)
                    {
                        fechaarribo = Convert.ToDateTime(reader["FECHAARRIBO"]);
                    }
                    else
                    {
                        fechaarribo = null;
                    }

                    if (reader["FECHA_ULT_ACC_LOG"] != DBNull.Value)
                    {
                        fecha_ult_acc_log = Convert.ToDateTime(reader["FECHA_ULT_ACC_LOG"]);
                    }
                    else
                    {
                        fecha_ult_acc_log = null;
                    }

                    Controladores.Add(new Clases.MovimientoLogistico
                    {
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        ContinenteOrigen = reader["CONTINENTE_ORIGEN"].ToString(),
                        ContinenteDestino = reader["CONTINENTE_DESTINO"].ToString(),
                        PaisOrigen = reader["PAIS_ORIGEN"].ToString(),
                        PaisDestino = reader["PAIS_DESTINO"].ToString(),
                        CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                        CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                        TipoNodoOrigen = reader["TIPO_ORIGEN"].ToString(),
                        TipoNodoDestino = reader["TIPO_DESTINO"].ToString(),
                        NodoOrigen = reader["NODO_ORIGEN"].ToString(),
                        NodoDestino = reader["NODO_DESTINO"].ToString(),
                        RetrievalProvider = reader["RETRIEVAL_PROVIDER"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        Viaje = reader["VIAJE"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        EtaString = reader["ETA"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Estado = reader["ESTADO"].ToString(),
                        FechaEnvio = fechaenvio,
                        NumeroEnvio = reader["NUMEROENVIO"].ToString(),
                        UsuarioRecuperacion = reader["USUARIO"].ToString(),
                        EmpresaTransporte = reader["EMPRESATRANSPORTE"].ToString(),
                        Prioritario = reader["PRIORITARIO"].ToString(),
                        FechaArribo = fechaarribo,
                        FechaUltAccLog = fecha_ult_acc_log,
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

        public static int AgregarIngreso(Clases.MovimientoLogistico MovLogistico)
        {
            if (MovLogistico.FechaRecuperacion == null)
            {
                MovLogistico.FechaRecuperacion = Convert.ToDateTime("01-01-1990");
            }
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarIngreso", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NUMCONTROLADOR", SqlDbType.VarChar, 50).Value = MovLogistico.Controlador;
            scCommand.Parameters.Add("@NUMMODEM", SqlDbType.VarChar, 50).Value = MovLogistico.Modem;
            scCommand.Parameters.Add("@FECHA_ARRIBO", SqlDbType.DateTime, 100).Value = MovLogistico.FechaArribo;
            scCommand.Parameters.Add("@TIPO_NODO_DESTINO", SqlDbType.Int, 50).Value = MovLogistico.IdTipoNodoDestino;
            scCommand.Parameters.Add("@NODO_DESTINO", SqlDbType.Int, 50).Value = MovLogistico.IdNodoDestino;
            scCommand.Parameters.Add("@TIPO_MOVIMIENTO", SqlDbType.Int, 50).Value = MovLogistico.TipoMovimiento;
            scCommand.Parameters.Add("@NOTA", SqlDbType.VarChar, 255).Value = MovLogistico.Nota;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 255).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@INVOLUCRADOS", SqlDbType.Int, 50).Value = MovLogistico.Involucrados;
            if(MovLogistico.Bateria==null)
            {
                scCommand.Parameters.Add("@BATERIA", SqlDbType.VarChar, 50).Value = "";
            }
            else
            {
                scCommand.Parameters.Add("@BATERIA", SqlDbType.VarChar, 50).Value = MovLogistico.Bateria;
            }
            

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

        public static int AgregarSalida(Clases.MovimientoLogistico MovLogistico/*, int Fallas*/)
        {
            if (MovLogistico.FechaRecuperacion == null)
            {
                MovLogistico.FechaRecuperacion = Convert.ToDateTime("01-01-1990");
            }

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarSalida", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NUMCONTROLADOR", SqlDbType.VarChar, 50).Value = MovLogistico.Controlador;
            scCommand.Parameters.Add("@NUMMODEM", SqlDbType.VarChar, 50).Value = MovLogistico.Modem;
            scCommand.Parameters.Add("@FECHA_ENVIO", SqlDbType.DateTime, 100).Value = MovLogistico.FechaEnvio;
            scCommand.Parameters.Add("@ETA", SqlDbType.DateTime, 100).Value = MovLogistico.Eta;
            scCommand.Parameters.Add("@NUMERO_ENVIO", SqlDbType.VarChar, 20).Value = MovLogistico.NumeroEnvio;
            scCommand.Parameters.Add("@EMPRESA_TRANSPORTE", SqlDbType.VarChar, 50).Value = MovLogistico.EmpresaTransporte;
            scCommand.Parameters.Add("@TIPO_NODO_DESTINO", SqlDbType.Int, 50).Value = MovLogistico.IdTipoNodoDestino;
            scCommand.Parameters.Add("@NODO_DESTINO", SqlDbType.Int, 50).Value = MovLogistico.IdNodoDestino;
            scCommand.Parameters.Add("@TIPO_MOVIMIENTO", SqlDbType.Int, 50).Value = MovLogistico.TipoMovimiento;
            scCommand.Parameters.Add("@NOTA", SqlDbType.VarChar, 255).Value = MovLogistico.Nota;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 20).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@ID_NAVE", SqlDbType.Int, 50).Value = MovLogistico.IdNave;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 20).Value = MovLogistico.Viaje;
            scCommand.Parameters.Add("@RETRIEVALPROVIDER", SqlDbType.VarChar, 20).Value = MovLogistico.RetrievalProvider;
            scCommand.Parameters.Add("@FECHARECUPERACION", SqlDbType.DateTime, 100).Value = MovLogistico.FechaRecuperacion;
            //scCommand.Parameters.Add("@FALLAS", SqlDbType.Int, 50).Value = Fallas;
            scCommand.Parameters.Add("@INVOLUCRADOS", SqlDbType.Int, 50).Value = MovLogistico.Involucrados;
            if(MovLogistico.Bateria==null)
            {
                scCommand.Parameters.Add("@BATERIA", SqlDbType.VarChar, 50).Value = "";
            }
            else
            {
                scCommand.Parameters.Add("@BATERIA", SqlDbType.VarChar, 50).Value = MovLogistico.Bateria;
            }
            

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

        public static int AgregarSalidaNodo(Clases.MovimientoLogistico MovLogistico)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarSalidaNodo", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NUMCONTROLADOR", SqlDbType.VarChar, 50).Value = MovLogistico.Controlador;
            scCommand.Parameters.Add("@NUMMODEM", SqlDbType.VarChar, 50).Value = MovLogistico.Modem;
            scCommand.Parameters.Add("@ETA", SqlDbType.DateTime, 100).Value = MovLogistico.Eta;
            scCommand.Parameters.Add("@TIPO_NODO_DESTINO", SqlDbType.VarChar, 50).Value = MovLogistico.TipoNodoDestino;
            scCommand.Parameters.Add("@NODO_DESTINO", SqlDbType.VarChar, 50).Value = MovLogistico.NodoDestino;
            scCommand.Parameters.Add("@TIPO_MOVIMIENTO", SqlDbType.Int, 50).Value = MovLogistico.TipoMovimiento;
            scCommand.Parameters.Add("@NOTA", SqlDbType.VarChar, 255).Value = MovLogistico.Nota;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 255).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@NAVE", SqlDbType.VarChar, 50).Value = MovLogistico.Nave;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 20).Value = MovLogistico.Viaje;
            scCommand.Parameters.Add("@INVOLUCRADOS", SqlDbType.Int, 50).Value = MovLogistico.Involucrados;

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

        public static int AgregarPerdida(int Controlador, int TipoPerdida=1, string TipoESN="", DateTime? FechaPerdida=null, string ComentarioPerdida="")
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EXEC dbo.AgregarPerdida @ID_ESN, @TIPO_ESN, @TIPO_PERDIDA, @FECHAPERDIDA, @COMENTARIO", cnn);
            scCommand.Parameters.AddWithValue("@ID_ESN", Controlador);
            scCommand.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
            scCommand.Parameters.AddWithValue("@TIPO_PERDIDA", TipoPerdida);
            scCommand.Parameters.AddWithValue("@FECHAPERDIDA", FechaPerdida);
            scCommand.Parameters.AddWithValue("@COMENTARIO", ComentarioPerdida);
            scCommand.CommandType = CommandType.Text;

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

        public static int AgregarNoPerdida(int Controlador, string TipoESN)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EXEC dbo.AgregarNoPerdida @ID_ESN, @TIPO_ESN", cnn);
            scCommand.Parameters.AddWithValue("@ID_ESN", Controlador);
            scCommand.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
            scCommand.CommandType = CommandType.Text;

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

        public static int AgregarPriorizacion(int ESN, string TipoESN, int Requerimiento)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EXEC dbo.AgregarPriorizacion @ID_ESN, @TIPO_ESN, @REQUERIMIENTO", cnn);
            scCommand.Parameters.AddWithValue("@ID_ESN", ESN);
            scCommand.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
            scCommand.Parameters.AddWithValue("@REQUERIMIENTO", Requerimiento);
            scCommand.CommandType = CommandType.Text;

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

        public static int AgregarNoPriorizacion(int ESN, string TipoESN)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EXEC dbo.AgregarNoPriorizacion @ID_ESN, @TIPO_ESN", cnn);
            scCommand.Parameters.AddWithValue("@ID_ESN", ESN);
            scCommand.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
            scCommand.CommandType = CommandType.Text;

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

        public static int AgregarRecuperacion(Clases.RecuperacionControlador Recuperacion)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarRecuperacion", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NUMCONTROLADOR", SqlDbType.VarChar, 50).Value = Recuperacion.Controlador;
            scCommand.Parameters.Add("@NUMMODEM", SqlDbType.VarChar, 50).Value = Recuperacion.Modem;
            scCommand.Parameters.Add("@ID_RETRIEVAL_PROVIDER", SqlDbType.Int, 50).Value = Recuperacion.IdRetrievalProvider;
            scCommand.Parameters.Add("@FECHA_RECUPERACION", SqlDbType.DateTime, 100).Value = Recuperacion.FechaRecuperacion;
            scCommand.Parameters.Add("@TIPO_NODO_RECUPERACION", SqlDbType.Int, 50).Value = Recuperacion.TipoNodoRecupera;
            scCommand.Parameters.Add("@NODO_RECUPERACION", SqlDbType.Int, 50).Value = Recuperacion.NodoRecupera;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 255).Value = HttpContext.Current.Session["user"].ToString();
            scCommand.Parameters.Add("@INVOLUCRADOS", SqlDbType.Int, 50).Value = Recuperacion.Involucrados;

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

        public static Clases.Controlador GetIdControlador(string Controlador)
        {
            SqlConnection cnn;
            Clases.Controlador Controladores = new Clases.Controlador();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_CONTROLADOR FROM APLICACIONSERVICIO_CONTROLADOR WHERE NUMCONTROLADOR = @CONTROLADOR", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Controladores.Id = Convert.ToInt32(reader["ID_CONTROLADOR"]);
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

        public static Clases.Bateria GetBateriaByControlador(string Controlador)
        {
            SqlConnection cnn;
            Clases.Bateria AsociacionBateria = new Clases.Bateria();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAsociacionBateriaPorControlador @CONTROLADOR", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AsociacionBateria.Controlador = Controlador;
                    AsociacionBateria.NumBateria = reader["NUM_BATERIA"].ToString();
                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        AsociacionBateria.FechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"].ToString());
                    }
                    else
                    {
                        AsociacionBateria.FechaAsociacion = null;
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
            return AsociacionBateria;
        }

        public static Clases.Bateria GetBateriaByModem(string Modem)
        {
            SqlConnection cnn;
            Clases.Bateria AsociacionBateria = new Clases.Bateria();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAsociacionBateriaPorModem @MODEM", cnn);
                command.Parameters.AddWithValue("@MODEM", Modem);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AsociacionBateria.Modem = Modem;
                    AsociacionBateria.NumBateria = reader["NUM_BATERIA"].ToString();
                    if (reader["FECHA_ASOCIACION"] != DBNull.Value)
                    {
                        AsociacionBateria.FechaAsociacion = Convert.ToDateTime(reader["FECHA_ASOCIACION"].ToString());
                    }
                    else
                    {
                        AsociacionBateria.FechaAsociacion = null;
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
            return AsociacionBateria;
        }

        public static Clases.Controlador GetIdControladorByContenedor(int Contenedor)
        {
            SqlConnection cnn;
            Clases.Controlador Controlador = new Clases.Controlador();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_CONTROLADOR from APLICACIONSERVICIO_MOVIMIENTOLOGISTICO where ID_MOV_LOGISTICO = (select max(ID_MOV_LOGISTICO) from AplicacionServicio_MovimientoLogistico where ID_CONTENEDOR = @IDCONTENEDOR);", cnn);
                command.Parameters.AddWithValue("@IDCONTENEDOR", Contenedor);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Controlador.Id = Convert.ToInt32(reader["ID_CONTROLADOR"]);
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
            return Controlador;
        }

        public static int ValidarContenedor(string Contenedor)
        {
            SqlConnection cnn;
            int Existe = 1;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ValidarContenedor", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar, 100).Value = Contenedor;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Existe = Convert.ToInt32(reader["EXISTE"]);
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

            return Existe;
        }

        public static int ValidarContenedorById(int IdContenedor)
        {
            SqlConnection cnn;
            int Estado = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ValidarContenedorById", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CONTENEDOR", SqlDbType.Int, 50).Value = IdContenedor;
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

        public static int ValidarControladorById(int IdControlador)
        {
            SqlConnection cnn;
            int Estado = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ValidarControladorById", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CONTROLADOR", SqlDbType.Int, 50).Value = IdControlador;
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

        public static int ValidarControladorByIdEditar(int IdControlador, int IdServicio)
        {
            SqlConnection cnn;
            int Estado = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ValidarControladorByIdEditar", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID_CONTROLADOR", SqlDbType.Int, 50).Value = IdControlador;
                command.Parameters.Add("@ID_SERVICIO", SqlDbType.Int, 50).Value = IdServicio;
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

        public static int ValidarControlador(string Controlador)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarControlador @CONTROLADOR;", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
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

        public static int ValidarControladorDeposito(string Controlador, int IdServicio)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarControlador @CONTROLADOR;", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
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

            if (cantidad == 1)
            {
                int id_commodity = ObtenerCommodityProgramado(Controlador);
                // int id_commodity = 24;
                int flag_vali_commodity = ValidarCommodity(id_commodity, IdServicio);
                if (flag_vali_commodity != 1)
                {
                    cantidad = 3;
                }
            }

            return cantidad;
        }

        public static int ObtenerCommodityProgramado(string Controlador)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(connectionStringTecnica);
            int Id_Commodity = 0;
            try
            {
                cnn.Open();
                MySqlCommand command = new MySqlCommand("SELECT idComoditie AS id_commodity FROM prometeo.service WHERE id = (SELECT idService FROM prometeo.historyLogistic WHERE idController = (SELECT id FROM prometeo.controller WHERE esn = '" + Controlador + "' ORDER BY id DESC LIMIT 1) ORDER BY id DESC LIMIT 1);", cnn);
                command.CommandTimeout = 10;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Id_Commodity = Convert.ToInt32(reader["id_commodity"]);
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

            return Id_Commodity;
        }

        public static int ValidarCommodity(int Id_Commodity, int Id_Servicio)
        {
            SqlConnection cnn;
            int resultado = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarCommodity @ID_COMMODITY, @ID_SERVICIO;", cnn);
                command.Parameters.Add("@ID_COMMODITY", SqlDbType.Int, 50).Value = Id_Commodity;
                command.Parameters.Add("@ID_SERVICIO", SqlDbType.Int, 50).Value = Id_Servicio;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    resultado = Convert.ToInt32(reader["RESULTADO"]);
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

        public static int ValidarModem(string Modem)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarModem @MODEM;", cnn);
                command.Parameters.AddWithValue("@MODEM", Modem);
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

        public static int ValidarESN(string ESN)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarESN @ESN;", cnn);
                command.Parameters.AddWithValue("@ESN", ESN);
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

        public static string ValidarTipoESN(string ESN)
        {
            SqlConnection cnn;
            string tipo = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarTipoESN @ESN;", cnn);
                command.Parameters.AddWithValue("@ESN", ESN);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tipo = reader["TIPO_ESN"].ToString();
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
            return tipo;
        }

        public static Clases.ESN GetIdESN(string ESN)
        {
            SqlConnection cnn;
            Clases.ESN ESNs = new Clases.ESN();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarIdESN @ESN", cnn);
                command.Parameters.AddWithValue("@ESN", ESN);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ESNs.Id = Convert.ToInt32(reader["ID_ESN"]);
                    ESNs.TipoESN = reader["TIPO"].ToString();
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
            return ESNs;
        }

        public static int GuardarNotificacion(int idRP, int idCoordinador, int cantidadControladores, int estado)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("GuardarNotificacion", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@ID_RETRIEVAL_PROVIDER", SqlDbType.Int, 50).Value = idRP;
            scCommand.Parameters.Add("@ID_COORDINADOR", SqlDbType.Int, 50).Value = idCoordinador;
            scCommand.Parameters.Add("@CANTIDAD_CONTROLADORES", SqlDbType.Int, 50).Value = cantidadControladores;
            scCommand.Parameters.Add("@ESTADO", SqlDbType.Int, 50).Value = estado;

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

        public static List<Clases.Notificaciones> GetHistorialNotificaciones()
        {
            SqlConnection cnn;
            List<Clases.Notificaciones> Notificaciones = new List<Clases.Notificaciones>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistorialNotificaciones", cnn);
                DateTime? fechaNotificacion = null;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["FECHA_NOTIFICACION"] != DBNull.Value)
                    {
                        fechaNotificacion = Convert.ToDateTime(reader["FECHA_NOTIFICACION"]);
                    }
                    else
                    {
                        fechaNotificacion = null;
                    }



                    Notificaciones.Add(new Clases.Notificaciones()
                    {
                        IdRetrieval = Convert.ToInt32(reader["ID_RETRIEVAL_PROVIDER"]),
                        NombreRP = reader["NOMBRE"].ToString(),
                        FechaNotificacion = fechaNotificacion,
                        CoordinadorRP = reader["COORDINADOR_RP"].ToString(),
                        CantidadControladores = Convert.ToInt32(reader["CANTIDAD_CONTROLADORES"]),
                        Naviera = reader["NAVIERA"].ToString(),
                        NodoRP = reader["NODO_RP"].ToString(),
                        TipoNotificacion = reader["TIPO_NOTIFICACION"].ToString(),
                        EstadoNotificacion = reader["ESTADO"].ToString(),
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
            return Notificaciones;
        }

        public static int ValidarControladorServicio(int Controlador)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(ID_SERVICIO) AS CANTIDAD FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO=(SELECT MAX(ID_SERVICIO) FROM AplicacionServicio_Servicio1 WHERE ID_CONTROLADOR=@CONTROLADOR)", cnn);
                command.Parameters.AddWithValue("@CONTROLADOR", Controlador);
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

        public static int ValidarModemServicio(int Modem)
        {
            SqlConnection cnn;
            int cantidad = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(ID_SERVICIO) AS CANTIDAD FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO=(SELECT MAX(ID_SERVICIO) FROM AplicacionServicio_Servicio1 WHERE ID_MODEM=@MODEM)", cnn);
                command.Parameters.AddWithValue("@MODEM", Modem);
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

        public static List<Clases.Controladores> GetControladoresLogistica1()
        {
            SqlConnection cnn;
            List<Clases.Controladores> LogisticaControladores = new List<Clases.Controladores>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarControladorLogistica1 @USUARIO", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
                command.CommandTimeout = 999;
                DateTime? FechaPerdida = null;
                DateTime? Eta = null;
                DateTime? FechaArribo = null;
                DateTime? FechaEnvio = null;
                DateTime? FechaPrimeraNotificacion = null;
                DateTime? FechaNotificacionReal = null;
                DateTime? FechaRecuperacion = null;
                DateTime? FechaEtr = null;
                DateTime? FechaPriorizacion = null;
                DateTime? FechaControlador = null;
                DateTime? FechaUltUbicacion = null;
                string perdido = "";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["FECHAPERDIDA"] != DBNull.Value)
                    {
                        FechaPerdida = Convert.ToDateTime(reader["FECHAPERDIDA"]);
                    }
                    else
                    {
                        FechaPerdida = null;
                    }

                    if (reader["ETA"] != DBNull.Value)
                    {
                        Eta = Convert.ToDateTime(reader["ETA"]);
                    }
                    else
                    {
                        Eta = null;
                    }

                    if (reader["FECHAARRIBO"] != DBNull.Value)
                    {
                        FechaArribo = Convert.ToDateTime(reader["FECHAARRIBO"]);
                    }
                    else
                    {
                        FechaArribo = null;
                    }

                    if (reader["FECHASALIDA"] != DBNull.Value)
                    {
                        FechaEnvio = Convert.ToDateTime(reader["FECHASALIDA"]);
                    }
                    else
                    {
                        FechaEnvio = null;
                    }

                    if (reader["FECHAPRIMERANOTIFICACION"] != DBNull.Value)
                    {
                        FechaPrimeraNotificacion = Convert.ToDateTime(reader["FECHAPRIMERANOTIFICACION"]);
                    }
                    else
                    {
                        FechaPrimeraNotificacion = null;
                    }

                    if (reader["FECHANOTIFICACIONREAL"] != DBNull.Value)
                    {
                        FechaNotificacionReal = Convert.ToDateTime(reader["FECHANOTIFICACIONREAL"]);
                    }
                    else
                    {
                        FechaNotificacionReal = null;
                    }

                    if (reader["FECHARECUPERACION"] != DBNull.Value)
                    {
                        FechaRecuperacion = Convert.ToDateTime(reader["FECHARECUPERACION"]);
                    }
                    else
                    {
                        FechaRecuperacion = null;
                    }

                    if (reader["FECHAETR"] != DBNull.Value)
                    {
                        FechaEtr = Convert.ToDateTime(reader["FECHAETR"]);
                    }
                    else
                    {
                        FechaEtr = null;
                    }

                    if (reader["FECHAPRIORIZACION"] != DBNull.Value)
                    {
                        FechaPriorizacion = Convert.ToDateTime(reader["FECHAPRIORIZACION"]);
                    }
                    else
                    {
                        FechaPriorizacion = null;
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        FechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }
                    else
                    {
                        FechaControlador = null;
                    }

                    if (reader["PERDIDO"] == DBNull.Value || reader["PERDIDO"].ToString() == "")
                    {
                        perdido = "SIN PERDIDA";
                    }else
                    {
                        perdido = reader["PERDIDO"].ToString();
                    }

                    if (reader["FECHA_ULT_UBICACION"] != DBNull.Value)
                    {
                        FechaUltUbicacion = Convert.ToDateTime(reader["FECHA_ULT_UBICACION"]);
                    }
                    else
                    {
                        FechaUltUbicacion = null;
                    }

                    LogisticaControladores.Add(new Clases.Controladores()
                    {
                        IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
                        NumModem = reader["NUMMODEM"].ToString(),
                        DamageReportTxt = reader["DAMAGEREPORT"].ToString(),
                        TransitoNodoTxt = reader["TRANSITO"].ToString(),
                        PerdidoTxt = perdido,
                        ComentarioPerdida = reader["COMENTARIO_PERDIDA"].ToString(),
                        FechaPerdida = FechaPerdida,
                        PrioritarioTxt = reader["PRIORITARIO"].ToString(),
                        EstadoRecuperacion = reader["ESTADORECUPERACION"].ToString(),
                        Nota = reader["NOTA"].ToString(),
                        Eta = Eta,
                        NumeroEnvio = reader["NUMEROENVIO"].ToString(),
                        EmpresaTransporte = reader["EMPRESATRANSPORTE"].ToString(),
                        FechaArribo = FechaArribo,
                        DiasEnNodo = Convert.ToInt32(reader["DIASENNODO"]),
                        ContinenteOrigen = reader["CONTINENTEORIGEN"].ToString(),
                        PaisOrigen = reader["PAISORIGEN"].ToString(),
                        CiudadOrigen = reader["CIUDADORIGEN"].ToString(),
                        TipoNodoOrigen = reader["TIPOLUGARORIGEN"].ToString(),
                        NodoOrigen = reader["LUGARORIGEN"].ToString(),
                        ContinenteDestino = reader["CONTINENTEDESTINO"].ToString(),
                        PaisDestino = reader["PAISDESTINO"].ToString(),
                        CiudadDestino = reader["CIUDADDESTINO"].ToString(),
                        TipoNodoDestino = reader["TIPOLUGARDESTINO"].ToString(),
                        NodoDestino = reader["LUGARDESTINO"].ToString(),
                        EtaVencido = reader["ETAVENCIDO"].ToString(),
                        DiasEtaVencido = Convert.ToInt32(reader["DIASETAVENCIDO"]),
                        FechaEnvio = FechaEnvio,
                        RetrievalProvider = reader["RETRIEVAL"].ToString(),
                        LugarRecuperacion = reader["LUGARRECUPERACION"].ToString(),
                        ContinenteRecuperacion = reader["CONTINENTERECUPERACION"].ToString(),
                        NumNotificaciones = Convert.ToInt32(reader["NUMNOTIFICACIONES"]),
                        RetrievalProviderNoti = reader["RETRIEVALNOTIFICADO"].ToString(),
                        FechaPrimeraNotificacion = FechaPrimeraNotificacion,
                        FechaNotificacionReal = FechaNotificacionReal,
                        ReintentoRecupera = Convert.ToInt32(reader["CANTIDADRECUPERACIONES"]),
                        FechaRecuperacion = FechaRecuperacion,
                        Etr = FechaEtr,
                        EtrVencido = reader["ETRVENCIDO"].ToString(),
                        DiasEtrVencido = Convert.ToInt32(reader["DIASETRVENCIDO"]),
                        NotaRecuperacion = reader["NOTARECUPERACION"].ToString(),
                        IdServicio = Convert.ToInt32(reader["IDSERVICIO"]),
                        EstadoServicio = reader["ESTADOSERVICIO"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        TratamientoCO2 = reader["TRATAMIENTO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EstadoRetrieval = "",
                        AnoServicio = reader["ANOSERVICIO"].ToString(),
                        FechaPriorizacion = FechaPriorizacion,
                        Requerimiento = Convert.ToInt32(reader["REQUERIMIENTO"]),
                        FechaControlador = FechaControlador,
                        UltimaUbicacion= reader["ULTIMA_UBICACION"].ToString(),
                        FechaUltUbicacion = FechaUltUbicacion
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
            return LogisticaControladores;
        }

        public static List<Clases.Controladores> GetControladoresDeposito()
        {
            MySqlConnection cnnMysql;
            SqlConnection cnn;
            SqlConnection cnn2;
            List<Clases.Controladores> LogisticaControladores = new List<Clases.Controladores>();
            cnn = new SqlConnection(connectionString);
            cnn2 = new SqlConnection(connectionString);
            cnnMysql = new MySqlConnection(connectionStringTecnica);


            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarControladorDeposito", cnn);
                DateTime? FechaPerdida = null;
                DateTime? Eta = null;
                DateTime? FechaArribo = null;
                DateTime? FechaEnvio = null;
                DateTime? FechaPrimeraNotificacion = null;
                DateTime? FechaNotificacionReal = null;
                DateTime? FechaRecuperacion = null;
                DateTime? FechaEtr = null;
                DateTime? FechaPriorizacion = null;
                DateTime? FechaControlador = null;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Clases.Validacion> Servicios = new List<Clases.Validacion>();

                    if (reader["FECHAPERDIDA"] != DBNull.Value)
                    {
                        FechaPerdida = Convert.ToDateTime(reader["FECHAPERDIDA"]);
                    }
                    else
                    {
                        FechaPerdida = null;
                    }

                    if (reader["ETA"] != DBNull.Value)
                    {
                        Eta = Convert.ToDateTime(reader["ETA"]);
                    }
                    else
                    {
                        Eta = null;
                    }

                    if (reader["FECHAARRIBO"] != DBNull.Value)
                    {
                        FechaArribo = Convert.ToDateTime(reader["FECHAARRIBO"]);
                    }
                    else
                    {
                        FechaArribo = null;
                    }

                    if (reader["FECHASALIDA"] != DBNull.Value)
                    {
                        FechaEnvio = Convert.ToDateTime(reader["FECHASALIDA"]);
                    }
                    else
                    {
                        FechaEnvio = null;
                    }

                    if (reader["FECHAPRIMERANOTIFICACION"] != DBNull.Value)
                    {
                        FechaPrimeraNotificacion = Convert.ToDateTime(reader["FECHAPRIMERANOTIFICACION"]);
                    }
                    else
                    {
                        FechaPrimeraNotificacion = null;
                    }

                    if (reader["FECHANOTIFICACIONREAL"] != DBNull.Value)
                    {
                        FechaNotificacionReal = Convert.ToDateTime(reader["FECHANOTIFICACIONREAL"]);
                    }
                    else
                    {
                        FechaNotificacionReal = null;
                    }

                    if (reader["FECHARECUPERACION"] != DBNull.Value)
                    {
                        FechaRecuperacion = Convert.ToDateTime(reader["FECHARECUPERACION"]);
                    }
                    else
                    {
                        FechaRecuperacion = null;
                    }

                    if (reader["FECHAETR"] != DBNull.Value)
                    {
                        FechaEtr = Convert.ToDateTime(reader["FECHAETR"]);
                    }
                    else
                    {
                        FechaEtr = null;
                    }

                    if (reader["FECHAPRIORIZACION"] != DBNull.Value)
                    {
                        FechaPriorizacion = Convert.ToDateTime(reader["FECHAPRIORIZACION"]);
                    }
                    else
                    {
                        FechaPriorizacion = null;
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        FechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }
                    else
                    {
                        FechaControlador = null;
                    }

                    try
                    {
                        cnnMysql.Open();
                        string controlador = reader["NUMCONTROLADOR"].ToString();
                        MySqlCommand commandTecnica = new MySqlCommand("SELECT IFNULL(prometeo.service.idComoditie,0) AS idComoditie, DATE_FORMAT(prometeo.service.travelStartDate,'%Y-%m-%d') as travelStartDate FROM prometeo.service WHERE id = (SELECT idService FROM prometeo.logistic WHERE prometeo.logistic.idController = (SELECT id FROM prometeo.controller WHERE esn = '" + controlador + "' ORDER BY id DESC LIMIT 1) AND prometeo.logistic.idLogisticStatus = 8 AND prometeo.logistic.idControllerStatus = 2 ORDER BY id DESC LIMIT 1) AND prometeo.service.travelStartDate IS NOT NULL;", cnnMysql);
                        commandTecnica.CommandTimeout = 10;
                        MySqlDataReader readerTecnica = commandTecnica.ExecuteReader();

                        while (readerTecnica.Read())
                        {
                            Servicios.Add(new Clases.Validacion
                            {
                                IdCommodity = Convert.ToInt32(readerTecnica["idComoditie"]),
                                FechaInicioServicio = Convert.ToDateTime(readerTecnica["travelStartDate"])
                            });
                        }
                        cnnMysql.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        cnnMysql.Close();
                    }

                    string nombreCommodityTecnica = "";
                    int idCommodityTecnica = 0;
                    DateTime? fechaProgramacion = null;
                    if (Servicios.Count > 0)
                    {
                        cnn2.Open();
                        SqlCommand command2 = new SqlCommand("SELECT NOMBREPLATAFORMATECNICA FROM AplicacionServicio_RelacionCommodity WHERE IDPLATAFORMATECNICA=" + Servicios[0].IdCommodity, cnn2);
                        SqlDataReader reader2 = command2.ExecuteReader();
                        while (reader2.Read())
                        {
                            nombreCommodityTecnica = reader2["NOMBREPLATAFORMATECNICA"].ToString();


                        }
                        idCommodityTecnica = Servicios[0].IdCommodity;
                        fechaProgramacion = Servicios[0].FechaInicioServicio;
                        cnn2.Close();
                    }

                    LogisticaControladores.Add(new Clases.Controladores()
                    {
                        IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
                        NumModem = reader["NUMMODEM"].ToString(),
                        DamageReportTxt = reader["DAMAGEREPORT"].ToString(),
                        TransitoNodoTxt = reader["TRANSITO"].ToString(),
                        PerdidoTxt = reader["PERDIDO"].ToString(),
                        FechaPerdida = FechaPerdida,
                        PrioritarioTxt = reader["PRIORITARIO"].ToString(),
                        EstadoRecuperacion = reader["ESTADORECUPERACION"].ToString(),
                        Nota = reader["NOTA"].ToString(),
                        Eta = Eta,
                        NumeroEnvio = reader["NUMEROENVIO"].ToString(),
                        EmpresaTransporte = reader["EMPRESATRANSPORTE"].ToString(),
                        FechaArribo = FechaArribo,
                        DiasEnNodo = Convert.ToInt32(reader["DIASENNODO"]),
                        ContinenteOrigen = reader["CONTINENTEORIGEN"].ToString(),
                        PaisOrigen = reader["PAISORIGEN"].ToString(),
                        CiudadOrigen = reader["CIUDADORIGEN"].ToString(),
                        TipoNodoOrigen = reader["TIPOLUGARORIGEN"].ToString(),
                        NodoOrigen = reader["LUGARORIGEN"].ToString(),
                        ContinenteDestino = reader["CONTINENTEDESTINO"].ToString(),
                        PaisDestino = reader["PAISDESTINO"].ToString(),
                        CiudadDestino = reader["CIUDADDESTINO"].ToString(),
                        TipoNodoDestino = reader["TIPOLUGARDESTINO"].ToString(),
                        NodoDestino = reader["LUGARDESTINO"].ToString(),
                        EtaVencido = reader["ETAVENCIDO"].ToString(),
                        DiasEtaVencido = Convert.ToInt32(reader["DIASETAVENCIDO"]),
                        FechaEnvio = FechaEnvio,
                        RetrievalProvider = reader["RETRIEVAL"].ToString(),
                        LugarRecuperacion = reader["LUGARRECUPERACION"].ToString(),
                        ContinenteRecuperacion = reader["CONTINENTERECUPERACION"].ToString(),
                        NumNotificaciones = Convert.ToInt32(reader["NUMNOTIFICACIONES"]),
                        RetrievalProviderNoti = reader["RETRIEVALNOTIFICADO"].ToString(),
                        FechaPrimeraNotificacion = FechaPrimeraNotificacion,
                        FechaNotificacionReal = FechaNotificacionReal,
                        ReintentoRecupera = Convert.ToInt32(reader["CANTIDADRECUPERACIONES"]),
                        FechaRecuperacion = FechaRecuperacion,
                        Etr = FechaEtr,
                        EtrVencido = reader["ETRVENCIDO"].ToString(),
                        DiasEtrVencido = Convert.ToInt32(reader["DIASETRVENCIDO"]),
                        NotaRecuperacion = reader["NOTARECUPERACION"].ToString(),
                        IdServicio = Convert.ToInt32(reader["IDSERVICIO"]),
                        EstadoServicio = reader["ESTADOSERVICIO"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        TratamientoCO2 = reader["TRATAMIENTO"].ToString(),
                        Usuario = reader["USUARIO"].ToString(),
                        EstadoRetrieval = "",
                        AnoServicio = reader["ANOSERVICIO"].ToString(),
                        FechaPriorizacion = FechaPriorizacion,
                        Requerimiento = Convert.ToInt32(reader["REQUERIMIENTO"]),
                        FechaControlador = FechaControlador,
                        TipoCommodity = nombreCommodityTecnica,
                        IdTipoCommodity = idCommodityTecnica,
                        FechaProgramacion = fechaProgramacion
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
            return LogisticaControladores;
        }

        public static List<Clases.Controladores> GetControladoresLogisticaRetrieval()
        {
            SqlConnection cnn;
            List<Clases.Controladores> LogisticaControladores = new List<Clases.Controladores>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarControladorLogisticaRP", cnn);
                command.CommandTimeout = 999;
                DateTime? Eta = null;
                DateTime? FechaUltimaAccionLogistica = null;
                DateTime? FechaArribo = null;
                DateTime? FechaUltUbicacion = null;
                string perdido = "";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ETA"] != DBNull.Value)
                    {
                        Eta = Convert.ToDateTime(reader["ETA"]);
                    }
                    else
                    {
                        Eta = null;
                    }

                    if (reader["FECHAARRIBO"] != DBNull.Value)
                    {
                        FechaArribo = Convert.ToDateTime(reader["FECHAARRIBO"]);
                    }
                    else
                    {
                        FechaArribo = null;
                    }

                    if (reader["FECHA_ULT_ACC_LOG_CONTROLADOR"] != DBNull.Value)
                    {
                        FechaUltimaAccionLogistica = Convert.ToDateTime(reader["FECHA_ULT_ACC_LOG_CONTROLADOR"]);
                    }
                    else if (reader["FECHA_ULT_ACC_LOG_MODEM"] != DBNull.Value)
                    {
                        FechaUltimaAccionLogistica = Convert.ToDateTime(reader["FECHA_ULT_ACC_LOG_MODEM"]);
                    }
                    else
                    {
                        FechaUltimaAccionLogistica = null;
                    }

                    if (reader["PERDIDO"] == DBNull.Value || reader["PERDIDO"].ToString() == "")
                    {
                        perdido = "SIN PERDIDA";
                    }else
                    {
                        perdido = reader["PERDIDO"].ToString();
                    }


                    if (reader["FECHA_ULT_UBICACION"] != DBNull.Value)
                    {
                        FechaUltUbicacion = Convert.ToDateTime(reader["FECHA_ULT_UBICACION"]);
                    }
                    else
                    {
                        FechaUltUbicacion = null;
                    }

                    LogisticaControladores.Add(new Clases.Controladores()
                    {
                        IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
                        NumModem = reader["NUMMODEM"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),

                        ContinenteOrigen = reader["CONTINENTEORIGEN"].ToString(),
                        TipoNodoOrigen= reader["TIPOLUGARORIGEN"].ToString(),
                        NodoOrigen = reader["LUGARORIGEN"].ToString(),
                        TipoNodoDestino = reader["TIPOLUGARDESTINO"].ToString(),
                        NodoDestino = reader["LUGARDESTINO"].ToString(),
                        Eta = Eta,
                        FechaArribo = FechaArribo,

                        AnoServicio = reader["ANOSERVICIO"].ToString(),
                        DamageReportTxt = reader["DAMAGEREPORT"].ToString(),
                        PrioritarioTxt = reader["PRIORITARIO"].ToString(),
                        PerdidoTxt = perdido,
                        ComentarioPerdida= reader["COMENTARIO_PERDIDA"].ToString(),
                        RetrievalProviderNoti = reader["RETRIEVALNOTIFICADO"].ToString(),
                        FechaUltimaAccionLogistica = FechaUltimaAccionLogistica,
                        RetrievalProvider = reader["RETRIEVAL"].ToString(),
                        UsuarioMovimiento = reader["USUARIOMOVIMIENTO"].ToString(),
                        Usuario = reader["USUARIORECUPERACION"].ToString(),
                        EstadoRecuperacion = reader["ESTADORECUPERACION"].ToString(),
                        NombreRPNotificado= reader["NOMBRE_RETRIEVAL_NOTIFICADO"].ToString(),

                        // Ayudan a generar estado retrieval
                        EstadoServicio = reader["ESTADOSERVICIO"].ToString(),
                        TransitoNodoTxt = reader["TRANSITO"].ToString(),
                        PaisDestino = reader["PAISDESTINO"].ToString(),
                        EmpresaTransporte = reader["EMPRESATRANSPORTE"].ToString(),
                        NumeroEnvio = reader["NUMEROENVIO"].ToString(),
                        UltimaUbicacion= reader["ULTIMA_UBICACION"].ToString(),
                        FechaUltUbicacion = FechaUltUbicacion,
                        DetalleAlerta = reader["ALERTA"].ToString()
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
            return LogisticaControladores;
        }

        public static List<Clases.Controladores> GetAlertasEquipos(List<Clases.Controladores> Controladores)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            foreach (Clases.Controladores controlador in Controladores)
            {
                try
                {
                    cnn.Open();
                    SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAlertaByEquipo @NUMCONTROLADOR, @NUMMODEM", cnn);
                    command.Parameters.AddWithValue("@NUMCONTROLADOR", controlador.NumControlador);
                    command.Parameters.AddWithValue("@NUMMODEM", controlador.NumModem);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        controlador.DetalleAlerta = reader["DETALLE_ALERTA"].ToString();
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


            return Controladores;
        }

        public static DateTime GetFecha(int IdMovimiento)
        {
            SqlConnection cnn;
            DateTime Fecha = new DateTime();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT FECHA FROM APLICACIONSERVICIO_AUDITORIAMOVIMIENTOLOGISTICO WHERE ID_MOV_LOGISTICO = @IDMOVIMIENTO", cnn);
                command.Parameters.AddWithValue("@IDMOVIMIENTO", IdMovimiento);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Fecha = Convert.ToDateTime(reader["FECHA"]);
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
            return Fecha;
        }

        public static int AgregarNotificacion(int Controlador, int Retrieval, string Usuario)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarNotificacion", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NUMCONTROLADOR", SqlDbType.Int, 20).Value = Controlador;
            scCommand.Parameters.Add("@RETRIEVALPROVIDER", SqlDbType.Int, 20).Value = Retrieval;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = Usuario;

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

        public static int AgregarNotificacion(int IdESN, string TipoESN, int Retrieval, string Usuario, int Involucrados = 0)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarNotificacion", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@ID_ESN", SqlDbType.Int, 20).Value = IdESN;
            scCommand.Parameters.Add("@TIPO_ESN", SqlDbType.VarChar, 20).Value = TipoESN;
            scCommand.Parameters.Add("@RETRIEVALPROVIDER", SqlDbType.Int, 20).Value = Retrieval;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = Usuario;
            scCommand.Parameters.Add("@INVOLUCRADOS", SqlDbType.Int, 20).Value = Involucrados;
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

        public static Clases.MovimientoLogistico GetMovimientoByController(int Controlador)
        {
            SqlConnection cnn;
            Clases.MovimientoLogistico Servicio = new Clases.MovimientoLogistico();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select * from AplicacionServicio_MovimientoLogistico where ID_CONTROLADOR = @IDCONTROLADOR and ID_MOV_LOGISTICO = (select MAX(ID_MOV_LOGISTICO) from AplicacionServicio_MovimientoLogistico where ID_CONTROLADOR = @IDCONTROLADOR)", cnn);
                command.Parameters.AddWithValue("@IDCONTROLADOR", Controlador);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["NODO_DESTINO"] != DBNull.Value)
                    {
                        Servicio.IdNodoDestino = Convert.ToInt32(reader["NODO_DESTINO"]);
                    }
                    else
                    {
                        Servicio.IdNodoDestino = 0;
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
            return Servicio;
        }

        public static string GetBookingControlador(int IdControlador)
        {
            SqlConnection cnn;
            string Booking = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT BOOKING FROM APLICACIONSERVICIO_SERVICIO1 WHERE ID_SERVICIO = (SELECT MAX(ID_SERVICIO) from APLICACIONSERVICIO_SERVICIO1 WHERE ID_CONTROLADOR = @IDCONTROLADOR);", cnn);
                command.Parameters.AddWithValue("@IDCONTROLADOR", IdControlador);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Booking = reader["BOOKING"].ToString();
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
            return Booking;
        }

        public static int AgregarDamageReport(int esn, string tipoESN)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EXEC dbo.AsignarDamageReport @ESN, @TIPO_ESN", cnn);
            scCommand.Parameters.AddWithValue("@ESN", esn);
            scCommand.Parameters.AddWithValue("@TIPO_ESN", esn);
            scCommand.CommandType = CommandType.Text;

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

        public static int QuitarDamageReport(int Controlador)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("UPDATE AplicacionServicio_Controlador SET DAMAGE_REPORT = NULL WHERE ID_CONTROLADOR= @CONTROLADOR", cnn);
            scCommand.Parameters.AddWithValue("@CONTROLADOR", Controlador);
            scCommand.CommandType = CommandType.Text;

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

        public static int EliminarMovLogistico(int IdESN, string TipoESN)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EXEC dbo.EliminarMovLogistico @ID_ESN, @TIPO_ESN", cnn);
            scCommand.Parameters.AddWithValue("@ID_ESN", IdESN);
            scCommand.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
            scCommand.CommandType = CommandType.Text;

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

        public static string GetBookingESN(int IdESN, string TipoESN)
        {
            SqlConnection cnn;
            string Booking = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarBookingByESN @ID_ESN, @TIPO_ESN", cnn);
                command.Parameters.AddWithValue("@ID_ESN", IdESN);
                command.Parameters.AddWithValue("@TIPO_ESN", TipoESN);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Booking = reader["BOOKING"].ToString();
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
            return Booking;
        }

        public static List<Clases.Controladores> GetControladoresAutomaticos()
        {
            SqlConnection cnn;
            List<Clases.Controladores> Controladores = new List<Clases.Controladores>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("GetControladoresAutomaticos", cnn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Controladores.Add(new Clases.Controladores
                    {
                        IdControlador = Convert.ToInt32(reader["ID_CONTROLADOR"]),
                        NumControlador = reader["NUMCONTROLADOR"].ToString(),
                        NumModem = reader["NUMMODEM"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        NodoOrigen = reader["LUGARORIGEN"].ToString(),
                        NodoDestino = reader["LUGARDESTINO"].ToString(),
                        RetrievalProvider = reader["RETRIEVAL"].ToString()
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

        public static int CambiarMovimientoLogisticos(string Controlador, string Modem, DateTime FechaRecuperacion, DateTime FechaSalida, string NumeroGuia, int Courier, int RetrievalProvider, int TipoNodo, int Nodo, int Coordinador)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("CambiarMovimientoLogisticos", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@CONTROLADOR", SqlDbType.VarChar, 100).Value = Controlador;
            scCommand.Parameters.Add("@MODEM", SqlDbType.VarChar, 100).Value = Modem;
            scCommand.Parameters.Add("@FECHARECUPERACION", SqlDbType.DateTime, 50).Value = FechaRecuperacion;
            scCommand.Parameters.Add("@FECHASALIDA", SqlDbType.DateTime, 50).Value = FechaSalida;
            scCommand.Parameters.Add("@NUMEROGUIA", SqlDbType.VarChar, 100).Value = NumeroGuia;
            scCommand.Parameters.Add("@COURIER", SqlDbType.Int, 20).Value = Courier;
            scCommand.Parameters.Add("@RETRIEVAL", SqlDbType.Int, 20).Value = RetrievalProvider;
            scCommand.Parameters.Add("@TIPONODO", SqlDbType.Int, 20).Value = TipoNodo;
            scCommand.Parameters.Add("@NODO", SqlDbType.Int, 20).Value = Nodo;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.Int, 20).Value = Coordinador;
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

        public static Clases.Notificacion ValidarNotificacion(string Controlador, string Modem, int Retrieval)
        {
            SqlConnection cnn;
            Clases.Notificacion Validacion = new Clases.Notificacion();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarNotificacion @NUMCONTROLADOR, @MODEM, @RETRIEVALPROVIDER", cnn);
                command.Parameters.AddWithValue("@NUMCONTROLADOR", Controlador);
                command.Parameters.AddWithValue("@MODEM", Modem);
                command.Parameters.AddWithValue("@RETRIEVALPROVIDER", Retrieval);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Validacion.Validacion = Convert.ToInt32(reader["VALIDACION"]);
                    Validacion.Email = reader["USUARIO"].ToString();
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
            return Validacion;
        }

        public static int[] GetCountMatriz(int IdPais, int IdCiudad, int IdTipoLocalidad, int IdLocalidad)
        {
            int[] contador = new int[10];

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0, i = 0;
            SqlCommand command = new SqlCommand("ContadorControladores", cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@filtroTipoLugar", SqlDbType.Int, 20).Value = IdTipoLocalidad;
            command.Parameters.Add("@filtroLugar", SqlDbType.Int, 20).Value = IdLocalidad;
            command.Parameters.Add("@filtroCiudad", SqlDbType.Int, 20).Value = IdCiudad;
            command.Parameters.Add("@filtroPais", SqlDbType.Int, 20).Value = IdPais;
            try
            {
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contador[i] = Convert.ToInt32(reader["ID_tipo_movimiento"]);
                    contador[i + 1] = Convert.ToInt32(reader["cant_estado_cero"]);
                    contador[i + 2] = Convert.ToInt32(reader["cant_estado_uno"]);
                    contador[i + 3] = Convert.ToInt32(reader["cant_estado_dos"]);
                    contador[i + 4] = Convert.ToInt32(reader["cant_estado_tres"]);
                    i = i + 5; // ALMACENAR EN VECTOR LOS REGISTROS DEL CONTADOR
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }

            return contador;
        }

        public static int[] GetCountMatrizModem(int IdPais, int IdCiudad, int IdTipoLocalidad, int IdLocalidad)
        {
            int[] contador = new int[10];

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0, i = 0;
            SqlCommand command = new SqlCommand("ContadorModems", cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@filtroTipoLugar", SqlDbType.Int, 20).Value = IdTipoLocalidad;
            command.Parameters.Add("@filtroLugar", SqlDbType.Int, 20).Value = IdLocalidad;
            command.Parameters.Add("@filtroCiudad", SqlDbType.Int, 20).Value = IdCiudad;
            command.Parameters.Add("@filtroPais", SqlDbType.Int, 20).Value = IdPais;
            try
            {
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contador[i] = Convert.ToInt32(reader["ID_tipo_movimiento"]);
                    contador[i + 1] = Convert.ToInt32(reader["cant_estado_cero"]);
                    contador[i + 2] = Convert.ToInt32(reader["cant_estado_uno"]);
                    contador[i + 3] = Convert.ToInt32(reader["cant_estado_dos"]);
                    contador[i + 4] = Convert.ToInt32(reader["cant_estado_tres"]);
                    i = i + 5; // ALMACENAR EN VECTOR LOS REGISTROS DEL CONTADOR
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }

            return contador;
        }
    }
}