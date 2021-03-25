using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models
{
    public class SetpointModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearSetpoint(float CO2, float O2, int Estado)
        {

            List<Clases.Setpoint> ListaSetpoint = new List<Clases.Setpoint>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaSetpoint = GetSetpoint();
            for (int i = 0; i < ListaSetpoint.Count(); i++)
            {
                if (ListaSetpoint[i].CO2 == CO2 && ListaSetpoint[i].O2 == O2)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Setpoint(CANTCO2, CANTO2, ESTADO, USUARIO) VALUES(@CANTCO2, @CANTO2, @ESTADO, @USUARIO)", cnn);
                command.Parameters.AddWithValue("@CANTCO2", CO2);
                command.Parameters.AddWithValue("@CANTO2", O2);
                command.Parameters.AddWithValue("@ESTADO", Estado);
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

        public static List<Clases.Setpoint> GetSetpoint()
        {
            SqlConnection cnn;
            List<Clases.Setpoint> Setpoint = new List<Clases.Setpoint>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Setpoint", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Setpoint.Add(new Clases.Setpoint
                    {
                        IdSetpoint = Convert.ToInt32(reader["ID_SETPOINT"]),
                        CO2 = Convert.ToDouble(reader["CANTCO2"]),
                        O2 = Convert.ToDouble(reader["CANTO2"]),
                        Activo = Convert.ToInt32(reader["ESTADO"])
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
            return Setpoint;
        }

        public static string GetNombreSetpoint(int IdSetpoint=0)
        {
            SqlConnection cnn;
            string nombre = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(" SELECT (CONVERT(VARCHAR,CANTCO2) + ' CO2%, ' + CONVERT(VARCHAR,CANTO2) + ' O2%') AS SETPOINT FROM AplicacionServicio_Setpoint WHERE ID_SETPOINT=@ID_SETPOINT", cnn);
                command.Parameters.AddWithValue("@ID_SETPOINT", IdSetpoint);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nombre = reader["SETPOINT"].ToString();
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
            return nombre;
        }

        public static List<Clases.Setpoint> GetSetpointActivos()
        {
            SqlConnection cnn;
            List<Clases.Setpoint> Setpoint = new List<Clases.Setpoint>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Setpoint", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Setpoint.Add(new Clases.Setpoint
                    {
                        IdSetpoint = Convert.ToInt32(reader["ID_SETPOINT"]),
                        CO2 = Convert.ToInt32(reader["CANTCO2"]),
                        O2 = Convert.ToInt32(reader["CANTO2"]),
                        Activo = Convert.ToInt32(reader["ESTADO"])
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
            return Setpoint;
        }

        public static int EditarCommodity(float CO2, float O2, int Estado, int IdSetpoint)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            List<Clases.Setpoint> ListaSetpoint = new List<Clases.Setpoint>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaSetpoint = GetSetpoint();
            for (int i = 0; i < ListaSetpoint.Count(); i++)
            {
                if (ListaSetpoint[i].CO2 == CO2 && ListaSetpoint[i].O2 == O2 && ListaSetpoint[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Setpoint SET CANTCO2 = @CANTCO2, CANTO2 = @CANTO2, ESTADO = @ESTADO, USUARIO = @USUARIO WHERE ID_SETPOINT = @IDSETPOINT", cnn);
                command.Parameters.AddWithValue("@CANTCO2", CO2);
                command.Parameters.AddWithValue("@CANTO2", O2);
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDSETPOINT", IdSetpoint);
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

        public static List<Clases.Setpoint> GetSetpointCommodity(int Commodity)
        {
            SqlConnection cnn;
            List<Clases.Setpoint> Setpoint = new List<Clases.Setpoint>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select aplicacionservicio_relacioncommodity.*,aplicacionservicio_setpoint.* from aplicacionservicio_relacioncommodity, aplicacionservicio_setpoint where aplicacionservicio_setpoint.id_setpoint = aplicacionservicio_relacioncommodity.idsetpoint and aplicacionservicio_relacioncommodity.idaplicacionservicio = @IDCOMMODITY", cnn);
                command.Parameters.AddWithValue("@IDCOMMODITY", Commodity);



                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Setpoint.Add(new Clases.Setpoint
                    {
                        IdSetpoint = Convert.ToInt32(reader["ID_SETPOINT"]),
                        CO2 = Convert.ToDouble(reader["CANTCO2"]),
                        O2 = Convert.ToDouble(reader["CANTO2"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        IdaplicacionServicio = Convert.ToInt32(reader["IDAPLICACIONSERVICIO"]),
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
            return Setpoint;
        }

    }
}