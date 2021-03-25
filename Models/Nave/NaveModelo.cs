using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Nave
{
    public class NaveModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearNave(string NombreNave, int Estado, int IdNaviera)
        {

            List<Clases.Nave> ListaNave = new List<Clases.Nave>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaNave = GetNaves();
            for (int i = 0; i < ListaNave.Count(); i++)
            {
                if (ListaNave[i].NombreNave.ToUpper() == NombreNave.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Nave(ID_NAVIERA,NOMBRENAVE,ESTADO,USUARIO) VALUES(@IDNAVIERA,@NOMBRE,@ESTADO,@USUARIO)", cnn);
                command.Parameters.AddWithValue("@NOMBRE", NombreNave.ToUpper());
                command.Parameters.AddWithValue("@IDNAVIERA", IdNaviera);
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

        public static List<Clases.Nave> GetNaves()
        {
            SqlConnection cnn;
            List<Clases.Nave> Nave = new List<Clases.Nave>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open(); 
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Nave.ID_NAVE, AplicacionServicio_Nave.NOMBRENAVE, AplicacionServicio_Nave.ESTADO FROM AplicacionServicio_Nave WHERE AplicacionServicio_Nave.ESTADO=0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Nave.Add(new Clases.Nave
                    {
                        NombreNave = reader["NOMBRENAVE"].ToString(),
                        IdNave = Convert.ToInt32(reader["ID_NAVE"]),
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
            return Nave;
        }

        public static List<Clases.Nave> GetNavesActivas()
        {
            SqlConnection cnn;
            List<Clases.Nave> Nave = new List<Clases.Nave>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Nave where AplicacionServicio_Nave.ESTADO = 0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Nave.Add(new Clases.Nave
                    {
                        NombreNave = reader["NOMBRENAVE"].ToString(),
                        IdNave = Convert.ToInt32(reader["ID_NAVE"]),
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
            return Nave;
        }

        public static Clases.Nave GetIdNave(string Nombre)
        {
            SqlConnection cnn;
            Clases.Nave Nave = new Clases.Nave();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Nave Where AplicacionServicio_Nave.NOMBRENAVE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Nave.NombreNave = reader["NOMBRENAVE"].ToString();
                    Nave.IdNave = Convert.ToInt32(reader["ID_NAVE"]);
                    //Nave.IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]);
                    Nave.Estado = Convert.ToInt32(reader["ESTADO"]);
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
            return Nave;
        }

        public static string GetNombreNave(int IdNave=0)
        {
            SqlConnection cnn;
            string nombre = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT NOMBRENAVE FROM AplicacionServicio_Nave Where AplicacionServicio_Nave.ID_NAVE = @ID_NAVE", cnn);
                command.Parameters.AddWithValue("@ID_NAVE", IdNave);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    nombre = reader["NOMBRENAVE"].ToString();
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

        public static int EditarNave(string NombreNave, int Estado, int IdNave, int IdNaviera)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Nave Nave = new Clases.Nave();
            Nave = GetIdNave(NombreNave);
            List<Clases.Nave> ListaNave = new List<Clases.Nave>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaNave = GetNaves();
            for (int i = 0; i < ListaNave.Count(); i++)
            {
                if (ListaNave[i].NombreNave.ToUpper() == NombreNave.ToUpper() && ListaNave[i].Estado == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Nave SET NOMBRENAVE = @NOMBRE, ESTADO = @ESTADO, ID_NAVIERA = @IDNAVIERA, USUARIO = @USUARIO WHERE ID_NAVE = @IDNAVE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", NombreNave.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDNAVE", IdNave);
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