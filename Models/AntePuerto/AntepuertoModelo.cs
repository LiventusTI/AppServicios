using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.AntePuerto
{
    public class AntepuertoModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearAntepuerto(string NombreAntepuerto, int IdCiudad, int Estado, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            List<Clases.AntePuerto> ListaAntepuertos = new List<Clases.AntePuerto>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaAntepuertos = GetAntepuertos();
            for (int i = 0; i < ListaAntepuertos.Count(); i++)
            {
                if (ListaAntepuertos[i].Nombre.ToUpper() == NombreAntepuerto.ToUpper() && ListaAntepuertos[i].IdCiudad == IdCiudad && ListaAntepuertos[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_AntePuerto(ID_CIUDAD, NOMBRE, ESTADO, USUARIO, LATITUD, LONGITUD, RADIO) VALUES(@IDCIUDAD, @NOMBRE, @ESTADO, @USUARIO, @LATITUD, @LONGITUD, @RADIO)", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                command.Parameters.AddWithValue("@NOMBRE", NombreAntepuerto.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@LATITUD", Latitud);
                command.Parameters.AddWithValue("@LONGITUD", Longitud);
                command.Parameters.AddWithValue("@RADIO", Radio);
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

        public static List<Clases.AntePuerto> GetAntepuertos()
        {
            SqlConnection cnn;
            List<Clases.AntePuerto> Antepuertos = new List<Clases.AntePuerto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_AntePuerto.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_AntePuerto, AplicacionServicio_Ciudad WHERE AplicacionServicio_AntePuerto.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Antepuertos.Add(new Clases.AntePuerto
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_ANTEPUERTO"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        Latitud = reader["LATITUD"].ToString(),
                        Longitud = reader["LONGITUD"].ToString(),
                        Radio = reader["RADIO"].ToString(),
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
            return Antepuertos;
        }

        public static List<Clases.AntePuerto> GetAntepuertoCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.AntePuerto> Antepuertos = new List<Clases.AntePuerto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Antepuerto.ID_ANTEPUERTO, Antepuerto.ID_CIUDAD, Antepuerto.NOMBRE, Antepuerto.ESTADO, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_AntePuerto Antepuerto, AplicacionServicio_Ciudad Ciudad WHERE Antepuerto.ESTADO = 0 AND Antepuerto.ID_CIUDAD=Ciudad.ID_CIUDAD AND Antepuerto.ID_CIUDAD=@IDCIUDAD ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Antepuertos.Add(new Clases.AntePuerto
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_ANTEPUERTO"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString()
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
            return Antepuertos;
        }

        public static List<Clases.AntePuerto> GetAntepuertoPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.AntePuerto> Antepuertos = new List<Clases.AntePuerto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Antepuerto.ID_ANTEPUERTO, Antepuerto.ID_CIUDAD, Antepuerto.NOMBRE, Antepuerto.ESTADO, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_AntePuerto Antepuerto, AplicacionServicio_Ciudad Ciudad WHERE Antepuerto.ESTADO = 0 AND Antepuerto.ID_CIUDAD=Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=@ID_PAIS ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_PAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Antepuertos.Add(new Clases.AntePuerto
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_ANTEPUERTO"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString()
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
            return Antepuertos;
        }

        public static List<Clases.AntePuerto> GetAntepuertoContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.AntePuerto> Antepuertos = new List<Clases.AntePuerto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Antepuerto.ID_ANTEPUERTO, Antepuerto.ID_CIUDAD, Antepuerto.NOMBRE, Antepuerto.ESTADO, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_AntePuerto Antepuerto, AplicacionServicio_Ciudad Ciudad, AplicacionServicio_Pais Pais WHERE Antepuerto.ESTADO = 0 AND Antepuerto.ID_CIUDAD=Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=Pais.ID_PAIS AND Pais.ID_CONTINENTE=@ID_CONTINENTE ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Antepuertos.Add(new Clases.AntePuerto
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_ANTEPUERTO"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString()
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
            return Antepuertos;
        }

        public static int EditarAntePuerto(string Nombre, int IdCiudad, int Estado, int IdAntepuerto, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            List<Clases.AntePuerto> ListaAntePuertos = new List<Clases.AntePuerto>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaAntePuertos = GetAntepuertos();
            //for (int i = 0; i < ListaAntePuertos.Count(); i++)
            //{
            //    if (ListaAntePuertos[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaAntePuertos[i].IdCiudad == IdCiudad && ListaAntePuertos[i].Activo == Estado)
            //    {
            //        return 2;
            //    }
            //}
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_AntePuerto SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_CIUDAD = @IDCIUDAD, USUARIO = @USUARIO, LATITUD=@LATITUD, LONGITUD=@LONGITUD, RADIO=@RADIO WHERE ID_ANTEPUERTO = @IDANTEPUERTO", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDANTEPUERTO", IdAntepuerto);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@LATITUD", Latitud);
                command.Parameters.AddWithValue("@LONGITUD", Longitud);
                command.Parameters.AddWithValue("@RADIO", Radio);

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

        public static List<Clases.AntePuerto> GetInfoAntepuerto(int IdAntepuerto)
        {
            SqlConnection cnn;
            List<Clases.AntePuerto> Antepuertos = new List<Clases.AntePuerto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select continente.ID_CONTINENTE, continente.NOMBRE AS NOMBRECONTINENTE, ciudad.ID_CIUDAD, ciudad.NOMBRE as NOMBRECIUDAD, pais.ID_PAIS, pais.NOMBRE NOMBREPAIS from AplicacionServicio_Antepuerto antepuerto,AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais, AplicacionServicio_Continente continente where antepuerto.ID_ANTEPUERTO = @IDANTEPUERTO and antepuerto.ID_CIUDAD = ciudad.ID_CIUDAD and ciudad.ID_PAIS = pais.ID_PAIS and pais.ID_CONTINENTE=continente.ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@IDANTEPUERTO", IdAntepuerto);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Antepuertos.Add(new Clases.AntePuerto
                    {
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        NombreContinente = reader["NOMBRECONTINENTE"].ToString(),
                        IdContinente = Convert.ToInt32(reader["ID_CONTINENTE"])
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
            return Antepuertos;
        }

        public static List<Clases.AntePuerto> GetAntePuertosActivos()
        {
            SqlConnection cnn;
            List<Clases.AntePuerto> AntePuerto = new List<Clases.AntePuerto>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Antepuerto.*, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_AntePuerto Antepuerto, AplicacionServicio_Ciudad Ciudad WHERE Antepuerto.ESTADO = 0 AND Antepuerto.ID_CIUDAD=Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AntePuerto.Add(new Clases.AntePuerto
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_ANTEPUERTO"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString()
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
            return AntePuerto;
        }

    }
}