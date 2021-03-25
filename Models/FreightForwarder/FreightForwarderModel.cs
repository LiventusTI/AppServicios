using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.FreightForwarder
{
    public class FreightForwarderModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearFreightForwarder(string Nombre, int Estado)
        {

            List<Clases.FreightForwarder> ListaFreightForwarder = new List<Clases.FreightForwarder>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaFreightForwarder = GetFreightForwarder();
            for (int i = 0; i < ListaFreightForwarder.Count(); i++)
            {
                if (ListaFreightForwarder[i].NombreFreightForwarder.ToUpper() == Nombre.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_FreightForwarder(NOMBRE, ESTADO, USUARIO) VALUES(@NOMBRE, @ESTADO, @USUARIO)", cnn);
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

        public static List<Clases.FreightForwarder> GetFreightForwarder()
        {
            SqlConnection cnn;
            List<Clases.FreightForwarder> FreightForwarder = new List<Clases.FreightForwarder>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_FreightForwarder.* FROM AplicacionServicio_FreightForwarder", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FreightForwarder.Add(new Clases.FreightForwarder
                    {
                        NombreFreightForwarder = reader["NOMBRE"].ToString(),
                        Id_FreightForwarder = Convert.ToInt32(reader["ID_FREIGHTFORWARDER"]),
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
            return FreightForwarder;
        }

        public static List<Clases.FreightForwarder> GetFreightForwarderActivos()
        {
            SqlConnection cnn;
            List<Clases.FreightForwarder> FreightForwarder = new List<Clases.FreightForwarder>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_FreightForwarder WHERE AplicacionServicio_FreightForwarder.ESTADO = 0 ORDER BY NOMBRE ASC", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FreightForwarder.Add(new Clases.FreightForwarder
                    {
                        NombreFreightForwarder = reader["NOMBRE"].ToString(),
                        Id_FreightForwarder = Convert.ToInt32(reader["ID_FREIGHTFORWARDER"]),
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
            return FreightForwarder;
        }

        public static Clases.FreightForwarder GetIdFreightForwarder(string Nombre)
        {
            SqlConnection cnn;
            Clases.FreightForwarder FreightForwarder = new Clases.FreightForwarder();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_FreightForwarder Where AplicacionServicio_FreightForwarder.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    FreightForwarder.NombreFreightForwarder = reader["NOMBRE"].ToString();
                    FreightForwarder.Id_FreightForwarder = Convert.ToInt32(reader["ID_FREIGHTFORWARDER"]);
                    FreightForwarder.Estado = Convert.ToInt32(reader["ESTADO"]);
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
            return FreightForwarder;
        }

        public static int EditarFreightForwarder(string Nombre, int Estado, int IdFreightForwarder)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.FreightForwarder FreightForwarder = new Clases.FreightForwarder();
            FreightForwarder = GetIdFreightForwarder(Nombre);
            List<Clases.FreightForwarder> ListaFreightForwarder = new List<Clases.FreightForwarder>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaFreightForwarder = GetFreightForwarder();
            for (int i = 0; i < ListaFreightForwarder.Count(); i++)
            {
                if (ListaFreightForwarder[i].NombreFreightForwarder.ToUpper() == Nombre.ToUpper() && ListaFreightForwarder[i].Estado == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_FreightForwarder SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, USUARIO = @USUARIO WHERE ID_FREIGHTFORWARDER = @IDFREIGHTFORWARDER", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDFREIGHTFORWARDER", IdFreightForwarder);
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
    }
}