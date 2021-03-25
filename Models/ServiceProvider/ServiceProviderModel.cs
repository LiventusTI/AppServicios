using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.ServiceProvider
{
    public class ServiceProviderModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearServiceProvider(string Nombre, int idPais, int Estado)
        {

            List<Clases.ServiceProvider> ListaServiceProvider = new List<Clases.ServiceProvider>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaServiceProvider = GetServiceProvider();
            for (int i = 0; i < ListaServiceProvider.Count(); i++)
            {
                if (ListaServiceProvider[i].NombreServiceProvider.ToUpper() == Nombre.ToUpper() && ListaServiceProvider[i].IdPais == idPais)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_ServiceProvider(NOMBRE, ESTADO, ID_PAIS, USUARIO) VALUES(@NOMBRE, @ESTADO, @IDPAIS, @USUARIO)", cnn);
                command.Parameters.AddWithValue("@IDPAIS", idPais);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
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

        public static List<Clases.ServiceProvider> GetServiceProvider()
        {
            SqlConnection cnn;
            List<Clases.ServiceProvider> ServiceProvider = new List<Clases.ServiceProvider>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_ServiceProvider.* , AplicacionServicio_Pais.NOMBRE as NOMBREPAIS FROM AplicacionServicio_ServiceProvider, AplicacionServicio_Pais WHERE AplicacionServicio_ServiceProvider.ID_PAIS = AplicacionServicio_Pais.ID_PAIS", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ServiceProvider.Add(new Clases.ServiceProvider
                    {
                        NombreServiceProvider = reader["NOMBRE"].ToString(),
                        IdServiceProvider = Convert.ToInt32(reader["ID_SERVICEPROVIDER"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombrePais = reader["NOMBREPAIS"].ToString()
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
            return ServiceProvider;
        }

        public static List<Clases.ServiceProvider> GetServiceProviderActivas()
        {
            SqlConnection cnn;
            List<Clases.ServiceProvider> ServiceProvider = new List<Clases.ServiceProvider>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_ServiceProvider WHERE AplicacionServicio_ServiceProvider.ESTADO = 0 ORDER BY ID_SERVICEPROVIDER ASC", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ServiceProvider.Add(new Clases.ServiceProvider
                    {
                        NombreServiceProvider = reader["NOMBRE"].ToString(),
                        IdServiceProvider = Convert.ToInt32(reader["ID_SERVICEPROVIDER"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombrePais = ""
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
            return ServiceProvider;
        }

        public static List<Clases.ServiceProvider> GetServiceProviderPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.ServiceProvider> ServiceProvider = new List<Clases.ServiceProvider>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_ServiceProvider WHERE AplicacionServicio_ServiceProvider.ESTADO = 0 AND AplicacionServicio_ServiceProvider.ID_PAIS = @IDPAIS ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ServiceProvider.Add(new Clases.ServiceProvider
                    {
                        NombreServiceProvider = reader["NOMBRE"].ToString(),
                        IdServiceProvider = Convert.ToInt32(reader["ID_SERVICEPROVIDER"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombrePais = ""
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
            return ServiceProvider;
        }

        public static Clases.ServiceProvider GetIdServiceProvider(string Nombre)
        {
            SqlConnection cnn;
            Clases.ServiceProvider ServiceProvider = new Clases.ServiceProvider();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_ServiceProvider Where AplicacionServicio_ServiceProvider.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ServiceProvider.NombreServiceProvider = reader["NOMBRE"].ToString();
                    ServiceProvider.IdServiceProvider = Convert.ToInt32(reader["ID_SERVICEPROVIDER"]);
                    ServiceProvider.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return ServiceProvider;
        }

        public static int EditarServiceProvider(string Nombre, int IdPais, int Estado, int IdServiceProvider)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.ServiceProvider ServiceProvider = new Clases.ServiceProvider();
            ServiceProvider = GetIdServiceProvider(Nombre);
            List<Clases.ServiceProvider> ListaServiceProvider = new List<Clases.ServiceProvider>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaServiceProvider = GetServiceProvider();
            for (int i = 0; i < ListaServiceProvider.Count(); i++)
            {
                if (ListaServiceProvider[i].NombreServiceProvider.ToUpper() == Nombre.ToUpper() && ListaServiceProvider[i].IdPais == IdPais && ListaServiceProvider[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_ServiceProvider SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_PAIS = @IDPAIS, USUARIO = @USUARIO WHERE ID_SERVICEPROVIDER = @IDSERVICEPROVIDER", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);
                command.Parameters.AddWithValue("@IDSERVICEPROVIDER", IdServiceProvider);
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

        public static int GetIdServiceProvider2(string Nombre)
        {
            SqlConnection cnn;
            int id = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_SERVICEPROVIDER FROM AplicacionServicio_ServiceProvider Where AplicacionServicio_ServiceProvider.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID_SERVICEPROVIDER"]);
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
            return id;
        }
    }
}