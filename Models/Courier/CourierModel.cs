using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Courier
{
    public class CourierModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearCourier(string Nombre, int Estado)
        {

            List<Clases.Courier> ListaCourier = new List<Clases.Courier>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaCourier = GetCourier();
            for (int i = 0; i < ListaCourier.Count(); i++)
            {
                if (ListaCourier[i].NombreCourier.ToUpper() == Nombre.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Courier(NOMBRE, ESTADO, USUARIO) VALUES(@NOMBRE, @ESTADO, @USUARIO)", cnn);
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

        public static List<Clases.Courier> GetCourier()
        {
            SqlConnection cnn;
            List<Clases.Courier> Couriers = new List<Clases.Courier>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Courier.* FROM AplicacionServicio_Courier", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Couriers.Add(new Clases.Courier
                    {
                        NombreCourier = reader["NOMBRE"].ToString(),
                        Id_Courier = Convert.ToInt32(reader["ID_COURIER"]),
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
            return Couriers;
        }

        public static List<Clases.Courier> GetCourierActivos()
        {
            SqlConnection cnn;
            List<Clases.Courier> Couriers = new List<Clases.Courier>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Courier WHERE AplicacionServicio_Courier.ESTADO = 0 ORDER BY NOMBRE ASC", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Couriers.Add(new Clases.Courier
                    {
                        NombreCourier = reader["NOMBRE"].ToString(),
                        Id_Courier = Convert.ToInt32(reader["ID_COURIER"]),
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
            return Couriers;
        }

        public static Clases.Courier GetIdCourier(string Nombre)
        {
            SqlConnection cnn;
            Clases.Courier Courier = new Clases.Courier();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Courier Where AplicacionServicio_Courier.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Courier.NombreCourier = reader["NOMBRE"].ToString();
                    Courier.Id_Courier = Convert.ToInt32(reader["ID_COURIER"]);
                    Courier.Estado = Convert.ToInt32(reader["ESTADO"]);
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
            return Courier;
        }

        public static int EditarCourier(string Nombre, int Estado, int IdCourier)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Courier Courier = new Clases.Courier();
            Courier = GetIdCourier(Nombre);
            List<Clases.Courier> ListaCouriers = new List<Clases.Courier>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaCouriers = GetCourier();
            for (int i = 0; i < ListaCouriers.Count(); i++)
            {
                if (ListaCouriers[i].NombreCourier.ToUpper() == Nombre.ToUpper() && ListaCouriers[i].Estado == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Courier SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, USUARIO = @USUARIO WHERE ID_COURIER = @IDCOURIER", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDCOURIER", IdCourier);
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