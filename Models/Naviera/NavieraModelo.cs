using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Naviera
{
    public class NavieraModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearNaviera(string Nombre, int Estado) {

            List<Clases.Naviera> ListaNavieras = new List<Clases.Naviera>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaNavieras = GetNavieras();
            for (int i = 0; i < ListaNavieras.Count(); i++) {
                if (ListaNavieras[i].Nombre.ToUpper() == Nombre.ToUpper()) {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Naviera(NOMBRE, ESTADO, USUARIO) VALUES(@NOMBRE,@ESTADO,@USUARIO)", cnn);
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

        public static List<Clases.Naviera> GetNavieras() {
            SqlConnection cnn;
            List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Naviera", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    Naviera.Add(new Clases.Naviera
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_NAVIERA"]),
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
            return Naviera;
        }

        public static List<Clases.Naviera> GetNavierasActivas()
        {
            SqlConnection cnn;
            List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Naviera Where AplicacionServicio_Naviera.ESTADO = 0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Naviera.Add(new Clases.Naviera
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_NAVIERA"]),
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
            return Naviera;
        }

        public static Clases.Naviera GetIdNaviera(string Nombre)
        {
            SqlConnection cnn;
            Clases.Naviera Naviera = new Clases.Naviera();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Naviera Where AplicacionServicio_Naviera.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Naviera.Nombre = reader["NOMBRE"].ToString();
                    Naviera.Id = Convert.ToInt32(reader["ID_NAVIERA"]);
                    Naviera.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return Naviera;
        }

        public static string GetNombreNaviera(int IdNaviera)
        {
            SqlConnection cnn;
            string Nombre = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT NOMBRE FROM AplicacionServicio_Naviera Where AplicacionServicio_Naviera.ID_NAVIERA = @ID_NAVIERA", cnn);
                command.Parameters.AddWithValue("@ID_NAVIERA", IdNaviera);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Nombre = reader["NOMBRE"].ToString();
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
            return Nombre;
        }

        public static int EditarNaviera(string Nombre, int Estado, int IdNaviera)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Naviera Naviera = new Clases.Naviera();
            Naviera = GetIdNaviera(Nombre);
            List<Clases.Naviera> ListaNavieras = new List<Clases.Naviera>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaNavieras = GetNavieras();
            for (int i = 0; i < ListaNavieras.Count(); i++)
            {
                if (ListaNavieras[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaNavieras[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Naviera SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, USUARIO = @USUARIO WHERE ID_NAVIERA = @IDNAVIERA", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDNAVIERA", IdNaviera);
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