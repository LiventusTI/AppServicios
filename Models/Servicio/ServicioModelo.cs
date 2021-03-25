using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Servicio
{
    public class ServicioModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int IngresarPosturaCortina(int IdContenedor, DateTime FechaEstimada, string HoraEstimada, int TipoLugar, int Lugar)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarPosturaCortina", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDCONTENEDOR", SqlDbType.Int, 50).Value = IdContenedor;
            scCommand.Parameters.Add("@FECHAESTIMADA", SqlDbType.DateTime, 50).Value = FechaEstimada;
            scCommand.Parameters.Add("@HORAESTIMADA", SqlDbType.VarChar, 50).Value = HoraEstimada;
            scCommand.Parameters.Add("@TIPOLUGAR", SqlDbType.Int, 50).Value = TipoLugar;
            scCommand.Parameters.Add("@LUGAR", SqlDbType.Int, 50).Value = Lugar;

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

        public static int IngresarInstalacionScrubber(int IdContenedor, DateTime FechaEstimada, string HoraEstimada, int TipoLugar, int Lugar)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("IngresarInstalacionScrubber", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDCONTENEDOR", SqlDbType.Int, 50).Value = IdContenedor;
            scCommand.Parameters.Add("@FECHAESTIMADA", SqlDbType.DateTime, 50).Value = FechaEstimada;
            scCommand.Parameters.Add("@HORAESTIMADA", SqlDbType.VarChar, 50).Value = HoraEstimada;
            scCommand.Parameters.Add("@TIPOLUGAR", SqlDbType.Int, 50).Value = TipoLugar;
            scCommand.Parameters.Add("@LUGAR", SqlDbType.Int, 50).Value = Lugar;

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

        public static List<Clases.ProgramacionServicio> ConsultarProgramacionServicio()
        {
            SqlConnection cnn;
            List<Clases.ProgramacionServicio> Programacion = new List<Clases.ProgramacionServicio>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                command = new SqlCommand("ConsultarProgramacionServicio", cnn);


                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Programacion.Add(new Clases.ProgramacionServicio
                    {
                        IdRegistro = Convert.ToInt32(reader["ID_REGISTRO"]),
                        FechaEstimada = Convert.ToDateTime(reader["FECHAESTIMADA"].ToString()),
                        HoraEstimada = reader["HORAESTIMADA"].ToString(),
                        TipoServicio = reader["TIPOSERVICIO"].ToString(),
                        TipoLugar = reader["NOMBRE_TIPO_LUGAR"].ToString(),
                        Lugar = reader["LUGAR"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString()
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
            return Programacion;
        }

        public static List<Clases.ProgramacionServicio> GetInformacionProgramarServicio(int IdRegistro)
        {
            SqlConnection cnn;
            List<Clases.ProgramacionServicio> Programacion = new List<Clases.ProgramacionServicio>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                command = new SqlCommand("GetInformacionProgramarServicio", cnn);


                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IDREGISTRO", SqlDbType.Int, 50).Value = IdRegistro;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Programacion.Add(new Clases.ProgramacionServicio
                    {
                        IdRegistro = Convert.ToInt32(reader["ID_REGISTRO"]),
                        FechaEstimada = Convert.ToDateTime(reader["FECHAESTIMADA"].ToString()),
                        HoraEstimada = reader["HORAESTIMADA"].ToString(),
                        TipoServicio = reader["TIPOSERVICIO"].ToString(),
                        IdTipoLugar = Convert.ToInt32(reader["ID_TIPOLUGAR"]),
                        IdLugar = Convert.ToInt32(reader["ID_LUGAR"]),
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"])
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
            return Programacion;
        }

        public static Clases.Servicio ConsultarServicioPorId(int id_servicio)
        {
            SqlConnection cnn;
            Clases.Servicio servicio = new Clases.Servicio();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                command = new SqlCommand("ConsultoUnServicio", cnn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IDSERVICIO", SqlDbType.Int).Value = id_servicio;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["PUERTOORIGEN"] != DBNull.Value) servicio.NombrePuertoOrigen = reader["PUERTOORIGEN"].ToString();
                    if (reader["PAISORIGEN"] != DBNull.Value) servicio.NombrePaisOrigen = reader["PAISORIGEN"].ToString();
                    if (reader["PUERTODESTINO"] != DBNull.Value) servicio.NombrePuertoDestino = reader["PUERTODESTINO"].ToString();
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
    }
}