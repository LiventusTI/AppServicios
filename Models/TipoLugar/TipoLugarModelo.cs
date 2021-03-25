using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Plataforma.Models.TipoLugar
{
    public class TipoLugarModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static List<Clases.TipoLugar> GetTipoLugares()
        {
            SqlConnection cnn;
            List<Clases.TipoLugar> TipoLugares = new List<Clases.TipoLugar>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_TIPO_LUGAR, NOMBRE_TIPO_LUGAR, ESTADO FROM AplicacionServicio_TipoLugar", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TipoLugares.Add(new Clases.TipoLugar
                    {
                        Id = Convert.ToInt32(reader["ID_TIPO_LUGAR"]),
                        Nombre = reader["NOMBRE_TIPO_LUGAR"].ToString(),
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
            return TipoLugares;
        }

        public static Clases.TipoLugar GetTipoNodoPorNombre(string nombreLugar)
        {
            SqlConnection cnn;
            Clases.TipoLugar TipoLugar = new Clases.TipoLugar();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP 1 ID_TIPO_LUGAR, NOMBRE_TIPO_LUGAR, ESTADO FROM AplicacionServicio_TipoLugar WHERE NOMBRE_TIPO_LUGAR = @NOMBRE_TIPO_LUGAR ORDER BY ID_TIPO_LUGAR DESC", cnn);
                command.Parameters.AddWithValue("@NOMBRE_TIPO_LUGAR", nombreLugar);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TipoLugar.Id = Convert.ToInt32(reader["ID_TIPO_LUGAR"]);
                    TipoLugar.Nombre = reader["NOMBRE_TIPO_LUGAR"].ToString();
                    TipoLugar.Estado = Convert.ToInt32(reader["ESTADO"]);
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
            return TipoLugar;
        }


        public static Clases.Nodo GetNodoPorNombre(string nombreLugar)
        {
            SqlConnection cnn;
            Clases.Nodo Nodo = new Clases.Nodo();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT TOP 1 ID_PUERTOORIGEN FROM AplicacionServicio_Puertos WHERE NOMBRE = @NOMBRE_LUGAR ORDER BY ID_PUERTOORIGEN DESC", cnn);
                command.Parameters.AddWithValue("@NOMBRE_LUGAR", nombreLugar);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Nodo.Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]);
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
            return Nodo;
        }

    }
}