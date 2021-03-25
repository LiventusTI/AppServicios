using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Sensor
{
    public class SensorModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static List<Clases.Sensor> GetSensores()
        {
            SqlConnection cnn;
            List<Clases.Sensor> Sensores = new List<Clases.Sensor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarSensores", cnn);
              
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaVencimiento = null;

                    if (reader["FECHA_VENCIMIENTO"] !=DBNull.Value)
                    {
                        fechaVencimiento = Convert.ToDateTime(reader["FECHA_VENCIMIENTO"].ToString());
                    }

                    Sensores.Add(new Clases.Sensor()
                    {
                        Alerta= reader["ALERTA"].ToString(),
                        IdSensor = Convert.ToInt32(reader["ID_SENSOR"]),
                        NumeroSerie= reader["NUM_SERIE_SENSOR"].ToString(),
                        Marca = reader["MARCA"].ToString(),
                        Modelo = reader["MODELO"].ToString(),
                        EstadoLogistico = reader["ESTADO_LOGISTICO"].ToString(),
                        TipoNodoOrigen = reader["TIPO_NODO_ORIGEN"].ToString(),
                        PaisOrigen = reader["PAIS_ORIGEN"].ToString(),
                        CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                        NodoOrigen = reader["NODO_ORIGEN"].ToString(),
                        TipoNodoDestino = reader["TIPO_NODO_DESTINO"].ToString(),
                        PaisDestino = reader["PAIS_DESTINO"].ToString(),
                        CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                        NodoDestino = reader["NODO_DESTINO"].ToString(),
                        FechaVencimiento= fechaVencimiento
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
            return Sensores;
        }

        public static List<Clases.Sensor> FiltrarSensores(int Pais = 0, int Ciudad = 0, int TipoNodo = 0, int Nodo = 0, int Marca = 0, int Modelo = 0)
        {
            SqlConnection cnn;
            List<Clases.Sensor> Sensores = new List<Clases.Sensor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("FiltrarSensores", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PAIS", SqlDbType.VarChar, 50).Value = Pais;
                command.Parameters.Add("@CIUDAD", SqlDbType.Int, 50).Value = Ciudad;
                command.Parameters.Add("@TIPO_NODO", SqlDbType.Int, 50).Value = TipoNodo;
                command.Parameters.Add("@NODO", SqlDbType.Int, 50).Value = Nodo;
                command.Parameters.Add("@MARCA", SqlDbType.Int, 50).Value = Marca;
                command.Parameters.Add("@MODELO", SqlDbType.Int, 50).Value = Modelo;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fechaVencimiento = null;

                    if (reader["FECHA_VENCIMIENTO"] != DBNull.Value)
                    {
                        fechaVencimiento = Convert.ToDateTime(reader["FECHA_VENCIMIENTO"].ToString());
                    }

                    Sensores.Add(new Clases.Sensor()
                    {
                        Alerta = reader["ALERTA"].ToString(),
                        IdSensor = Convert.ToInt32(reader["ID_SENSOR"]),
                        NumeroSerie = reader["NUM_SERIE_SENSOR"].ToString(),
                        Marca = reader["MARCA"].ToString(),
                        Modelo = reader["MODELO"].ToString(),
                        EstadoLogistico = reader["ESTADO_LOGISTICO"].ToString(),
                        TipoNodoOrigen = reader["TIPO_NODO_ORIGEN"].ToString(),
                        PaisOrigen = reader["PAIS_ORIGEN"].ToString(),
                        CiudadOrigen = reader["CIUDAD_ORIGEN"].ToString(),
                        NodoOrigen = reader["NODO_ORIGEN"].ToString(),
                        TipoNodoDestino = reader["TIPO_NODO_DESTINO"].ToString(),
                        PaisDestino = reader["PAIS_DESTINO"].ToString(),
                        CiudadDestino = reader["CIUDAD_DESTINO"].ToString(),
                        NodoDestino = reader["NODO_DESTINO"].ToString(),
                        FechaVencimiento = fechaVencimiento
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
            return Sensores;
        }

        public static List<Clases.MarcaSensor> GetMarcaSensor()
        {
            SqlConnection cnn;
            List<Clases.MarcaSensor> Marcas = new List<Clases.MarcaSensor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMarcaSensor", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Marcas.Add(new Clases.MarcaSensor
                    {
                        IdMarca = Convert.ToInt32(reader["ID_MARCA"]),
                        Nombre = reader["NOMBRE"].ToString()
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
            return Marcas;
        }

        public static List<Clases.ModeloSensor> GetModeloSensor()
        {
            SqlConnection cnn;
            List<Clases.ModeloSensor> Modelos = new List<Clases.ModeloSensor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarModeloSensor", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Modelos.Add(new Clases.ModeloSensor
                    {
                        IdModelo = Convert.ToInt32(reader["ID_MODELO"]),
                        Nombre = reader["NOMBRE"].ToString()
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
            return Modelos;
        }

        public static int AgregarSensor(string NumeroSerie = "", int Marca = 0, int Modelo = 0)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarSensor", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NUMERO_SENSOR", SqlDbType.VarChar, 50).Value = NumeroSerie;
            scCommand.Parameters.Add("@MARCA", SqlDbType.Int, 50).Value = Marca;
            scCommand.Parameters.Add("@MODELO", SqlDbType.Int, 50).Value = Modelo;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
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

        public static int AgregarMovSensor(string NumeroSerie = "", int TipoNodoDestino = 0, int NodoDestino = 0)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarMovLogisticoSensor", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NUMERO_SENSOR", SqlDbType.VarChar, 50).Value = NumeroSerie;
            scCommand.Parameters.Add("@TIPO_NODO_DESTINO", SqlDbType.Int, 50).Value = TipoNodoDestino;
            scCommand.Parameters.Add("@NODO_DESTINO", SqlDbType.Int, 50).Value = NodoDestino;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
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
    }
}