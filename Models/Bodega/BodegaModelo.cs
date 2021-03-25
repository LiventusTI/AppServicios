using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Bodega
{
    public class BodegaModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearBodega(string NombreBodega, int IdCiudad, int Estado, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {
            List<Clases.Bodega> ListaBodegas = new List<Clases.Bodega>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            //ListaBodegas = GetBodegas();
            //for (int i = 0; i < ListaBodegas.Count(); i++)
            //{
            //    if (ListaBodegas[i].Nombre.ToUpper() == NombreBodega.ToUpper() && ListaBodegas[i].IdCiudad == IdCiudad && ListaBodegas[i].Activo == Estado)
            //    {
            //        return 2;
            //    }
            //}
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Bodega(ID_CIUDAD, NOMBRE, ESTADO, LATITUD, LONGITUD, RADIO) VALUES(@IDCIUDAD, @NOMBRE, @ESTADO, @LATITUD, @LONGITUD, @RADIO)", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                command.Parameters.AddWithValue("@NOMBRE", NombreBodega.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
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

        public static int EditarBodega(string NombreBodega = "", int IdCiudad = 0, int Estado = 0, int IdBodega = 0, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //List<Clases.Bodega> ListaBodegas = new List<Clases.Bodega>();
            //Validar Si Existe Naviera con el mismo Nombre
            //ListaBodegas = GetBodegas();
            //for (int i = 0; i < ListaBodegas.Count(); i++)
            //{
            //    if (ListaBodegas[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaBodegas[i].IdCiudad == IdCiudad && ListaBodegas[i].Activo == Estado)
            //    {
            //        return 2;
            //    }
            //}
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Bodega SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_CIUDAD = @IDCIUDAD, LATITUD=@LATITUD, LONGITUD=@LONGITUD, RADIO=@RADIO WHERE ID_BODEGA = @IDBODEGA", cnn);
                command.Parameters.AddWithValue("@NOMBRE", NombreBodega.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                command.Parameters.AddWithValue("@LATITUD", Latitud);
                command.Parameters.AddWithValue("@LONGITUD", Longitud);
                command.Parameters.AddWithValue("@RADIO", Radio);
                command.Parameters.AddWithValue("@IDBODEGA", IdBodega);


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

        public static List<Clases.Bodega> GetBodegas()
        {
            SqlConnection cnn;
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Bodega.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Bodega, AplicacionServicio_Ciudad WHERE AplicacionServicio_Bodega.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bodegas.Add(new Clases.Bodega
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_BODEGA"]),
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
            return Bodegas;
        }

        public static List<Clases.Bodega> GetBodegasCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Bodega.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Bodega, AplicacionServicio_Ciudad WHERE AplicacionServicio_Bodega.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD AND AplicacionServicio_Bodega.ID_CIUDAD=@ID_CIUDAD", cnn);
                command.Parameters.AddWithValue("@ID_CIUDAD", IdCiudad);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bodegas.Add(new Clases.Bodega
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_BODEGA"]),
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
            return Bodegas;
        }

        public static List<Clases.Bodega> GetBodegasPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Bodega.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Bodega, AplicacionServicio_Ciudad WHERE AplicacionServicio_Bodega.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD AND AplicacionServicio_Ciudad.ID_PAIS=@ID_PAIS", cnn);
                command.Parameters.AddWithValue("@ID_PAIS", IdPais);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bodegas.Add(new Clases.Bodega
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_BODEGA"]),
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
            return Bodegas;
        }

        public static List<Clases.Bodega> GetBodegasContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Bodega.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Bodega, AplicacionServicio_Ciudad, AplicacionServicio_Pais WHERE AplicacionServicio_Bodega.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD AND AplicacionServicio_Ciudad.ID_PAIS=AplicacionServicio_Pais.ID_PAIS AND AplicacionServicio_Pais.ID_CONTINENTE=@ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bodegas.Add(new Clases.Bodega
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_BODEGA"]),
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
            return Bodegas;
        }

        public static List<Clases.Bodega> GetInfoBodega(int IdBodega)
        {
            SqlConnection cnn;
            List<Clases.Bodega> Bodegas = new List<Clases.Bodega>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select continente.ID_CONTINENTE, continente.NOMBRE AS NOMBRECONTINENTE, ciudad.ID_CIUDAD, ciudad.NOMBRE as NOMBRECIUDAD, pais.ID_PAIS, pais.NOMBRE NOMBREPAIS from AplicacionServicio_Bodega bodega,AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais, AplicacionServicio_Continente continente where bodega.ID_BODEGA = @IDBODEGA and bodega.ID_CIUDAD = ciudad.ID_CIUDAD and ciudad.ID_PAIS = pais.ID_PAIS and pais.ID_CONTINENTE=continente.ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@IDBODEGA", IdBodega);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bodegas.Add(new Clases.Bodega
                    {
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        IdContinente = Convert.ToInt32(reader["ID_CONTINENTE"]),
                        NombreContinente = reader["NOMBRECONTINENTE"].ToString()
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
            return Bodegas;
        }
    }
}