using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Plataforma.Models.Alerta
{
    public class AlertaModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static List<Clases.Alerta> ConsultarAlertasEnTransito()
        {
            SqlConnection cnn;
            List<Clases.Alerta> Lista_Alertas = new List<Clases.Alerta>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "EXEC dbo.ConsultarAlertasEnTransito";
                SqlCommand command = new SqlCommand(consulta, cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Alerta alerta = new Clases.Alerta();

                    if (reader["ID_REGISTRO"] != DBNull.Value)
                    {
                        alerta.Id = Convert.ToInt32(reader["ID_REGISTRO"]);
                    }

                    if (reader["FECHA_ALERTA"] != DBNull.Value)
                    {
                        alerta.Fecha = Convert.ToDateTime(reader["FECHA_ALERTA"]);
                    }

                    if (reader["DESCRIPCION"] != DBNull.Value)
                    {
                        alerta.Descripcion = reader["DESCRIPCION"].ToString();
                    }

                    if (reader["ID_SERVICIO"] != DBNull.Value)
                    {
                        alerta.IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]);
                    }

                    if (reader["CONTROLADOR"] != DBNull.Value)
                    {
                        alerta.Controlador = reader["CONTROLADOR"].ToString();
                    }

                    if (reader["MODEM"] != DBNull.Value)
                    {
                        alerta.Modem = reader["MODEM"].ToString();
                    }

                    if (reader["ACTIVO"] != DBNull.Value)
                    {
                        alerta.Activo = Convert.ToInt32(reader["ACTIVO"]);
                    }

                    Lista_Alertas.Add(alerta);
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
            return Lista_Alertas;
        }

        public static int ActivarDesactivarAlerta(int id_registro_alerta)
        {
            SqlConnection cnn;
            int respuesta = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "EXEC dbo.ActivarDesactivarAlerta @ID_REGISTRO";
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.Parameters.AddWithValue("@ID_REGISTRO", id_registro_alerta);
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
    }
}