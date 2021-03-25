using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Maquinaria
{
    public class MaquinariaModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearMaquinaria(string Nombre, int Estado)
        {

            List<Clases.Maquinaria> ListaMaquinarias = new List<Clases.Maquinaria>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaMaquinarias = GetMaquinarias();
            for (int i = 0; i < ListaMaquinarias.Count(); i++)
            {
                if (ListaMaquinarias[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaMaquinarias[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Maquinaria(NOMBRE, ESTADO, USUARIO) VALUES(@NOMBRE,@ESTADO,@USUARIO)", cnn);
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

        public static List<Clases.Maquinaria> GetMaquinarias()
        {
            SqlConnection cnn;
            List<Clases.Maquinaria> Maquinaria = new List<Clases.Maquinaria>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Maquinaria", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Maquinaria.Add(new Clases.Maquinaria
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_MAQUINARIA"]),
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
            return Maquinaria;
        }

        public static List<Clases.Maquinaria> GetMaquinariasActivas()
        {
            SqlConnection cnn;
            List<Clases.Maquinaria> Maquinaria = new List<Clases.Maquinaria>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Maquinaria WHERE AplicacionServicio_Maquinaria.Estado = 0 ", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Maquinaria.Add(new Clases.Maquinaria
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_MAQUINARIA"]),
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
            return Maquinaria;
        }

        public static int EditarMaquinaria(string Nombre, int Estado, int IdMaquinaria)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Maquinaria Maquinaria = new Clases.Maquinaria();
            Maquinaria = GetIdMaquinaria(Nombre);
            List<Clases.Maquinaria> ListaMaquinarias = new List<Clases.Maquinaria>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaMaquinarias = GetMaquinarias();
            for (int i = 0; i < ListaMaquinarias.Count(); i++)
            {
                if (ListaMaquinarias[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaMaquinarias[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Maquinaria SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, @USUARIO = USUARIO WHERE ID_MAQUINARIA = @IDMAQUINARIA", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDMAQUINARIA", IdMaquinaria);
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

        public static Clases.Maquinaria GetIdMaquinaria(string Nombre)
        {
            SqlConnection cnn;
            Clases.Maquinaria Maquinaria = new Clases.Maquinaria();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Maquinaria Where AplicacionServicio_Maquinaria.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Maquinaria.Nombre = reader["NOMBRE"].ToString();
                    Maquinaria.Id = Convert.ToInt32(reader["ID_MAQUINARIA"]);
                    Maquinaria.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return Maquinaria;
        }

        public static void AuditoriaMaquinaria(int Id, string Usuario, string Operacion)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_AuditoriaMaquinaria(ID_MAQUINARIA, USUARIO, FECHA, OPERACION) VALUES(@IDMAQUINARIA,@USUARIO,SYSDATETIME(),@OPERACION)", cnn);
                command.Parameters.AddWithValue("@IDMAQUINARIA", Id);
                command.Parameters.AddWithValue("@USUARIO", Usuario);
                command.Parameters.AddWithValue("@OPERACION", Operacion);


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

        public static int GetIdMaquinaria2(string Nombre)
        {
            SqlConnection cnn;
            int id = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_MAQUINARIA FROM AplicacionServicio_Maquinaria Where AplicacionServicio_Maquinaria.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID_MAQUINARIA"]);
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