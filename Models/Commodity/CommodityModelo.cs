using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Commodity
{
    public class CommodityModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearCommodity(string Nombre, int Estado)
        {

            List<Clases.Commodity> ListaCommodity = new List<Clases.Commodity>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaCommodity = GetCommodity();
            for (int i = 0; i < ListaCommodity.Count(); i++)
            {
                if (ListaCommodity[i].Nombre.ToUpper() == Nombre.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Commodity(NOMBRE, ESTADO, USUARIO) VALUES(@NOMBRE,@ESTADO,@USUARIO)", cnn);
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

        public static Clases.Commodity GetIdCommodity(string Nombre)
        {
            SqlConnection cnn;
            Clases.Commodity Commodity = new Clases.Commodity();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Commodity Where AplicacionServicio_Commodity.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Commodity.Nombre = reader["NOMBRE"].ToString();
                    Commodity.Id = Convert.ToInt32(reader["ID_COMMODITY"]);
                    Commodity.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return Commodity;
        }

        public static string GetNombreCommodity(int IdCommodity=0)
        {
            SqlConnection cnn;
            string nombre = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT NOMBRE FROM AplicacionServicio_Commodity Where AplicacionServicio_Commodity.ID_COMMODITY = @ID_COMMODITY", cnn);
                command.Parameters.AddWithValue("@ID_COMMODITY", IdCommodity);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    nombre = reader["NOMBRE"].ToString();
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

        public static List<Clases.Commodity> GetCommodity()
        {
            SqlConnection cnn;
            List<Clases.Commodity> Commodity = new List<Clases.Commodity>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Commodity WHERE ESTADO = 0 ORDER BY NOMBRE ASC", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Commodity.Add(new Clases.Commodity
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_COMMODITY"]),
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
            return Commodity;
        }

        public static List<Clases.Commodity> GetCommodityActivo()
        {
            SqlConnection cnn;
            List<Clases.Commodity> Commodity = new List<Clases.Commodity>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Commodity where ESTADO = 0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Commodity.Add(new Clases.Commodity
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_COMMODITY"]),
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
            return Commodity;
        }

        public static int EditarCommodity(string Nombre, int Estado, int IdCommodity)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Commodity Commodity = new Clases.Commodity();
            Commodity = GetIdCommodity(Nombre);
            List<Clases.Commodity> ListaCommodity = new List<Clases.Commodity>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaCommodity = GetCommodity();
            for (int i = 0; i < ListaCommodity.Count(); i++)
            {
                if (ListaCommodity[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaCommodity[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Commodity SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, USUARIO = @USUARIO WHERE ID_COMMODITY = @IDCOMMODITY", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDCOMMODITY", IdCommodity);
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
        public static int GetIdTecnica(string Nombre)
        {
            SqlConnection cnn;
            Clases.Commodity Commodity = new Clases.Commodity();
            cnn = new SqlConnection(connectionString);
            int Id = 0;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select IDPLATAFORMATECNICA from AplicacionServicio_RelacionCommodity where NOMBREPLATAFORMATECNICA = @NOMBRE;", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                     Id = Convert.ToInt32(reader["IDPLATAFORMATECNICA"]);
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
            return Id;
        }

    }
}