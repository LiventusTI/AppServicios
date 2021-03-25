using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Packing
{
    public class PackingModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearPacking(string NombrePacking, int IdCiudad, int Estado, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {

            List<Clases.Packing> ListaPacking = new List<Clases.Packing>();
            SqlConnection cnn;
            int result = 0;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaPacking = GetPacking();
            for (int i = 0; i < ListaPacking.Count(); i++)
            {
                if (ListaPacking[i].Nombre.ToUpper() == NombrePacking.ToUpper() && ListaPacking[i].IdCiudad == IdCiudad && ListaPacking[i].Activo == Estado)
                {
                    return 2;
                }
            }
                SqlCommand scCommand = new SqlCommand("AgregarPacking", cnn);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.Add("@IDCIUDAD", SqlDbType.Int, 50).Value = IdCiudad;
                scCommand.Parameters.Add("@ESTADO", SqlDbType.Int, 50).Value = Estado;
                scCommand.Parameters.Add("@NOMBREPACKING", SqlDbType.VarChar, 500).Value = NombrePacking;
                scCommand.Parameters.Add("@LATITUD", SqlDbType.Float, 50).Value = Latitud;
                scCommand.Parameters.Add("@LONGITUD", SqlDbType.Float, 50).Value = Longitud;
                scCommand.Parameters.Add("@RADIO", SqlDbType.Float, 50).Value = Radio;
                scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["user"].ToString();
            try
                {
                    if (scCommand.Connection.State == ConnectionState.Closed)
                    {
                        scCommand.Connection.Open();
                    }
                    //scCommand.ExecuteNonQuery();
                    result = scCommand.ExecuteNonQuery();

                    if (result == 0)
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
                    scCommand.Connection.Close();
                }
        }
        public static List<Clases.Packing> GetPacking()
        {
            SqlConnection cnn;
            List<Clases.Packing> Packing = new List<Clases.Packing>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Packing.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Packing, AplicacionServicio_Ciudad WHERE AplicacionServicio_Packing.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Packing.Add(new Clases.Packing
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PACKING"]),
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
            return Packing;
        }
        public static List<Clases.Packing> GetPackingCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.Packing> Packings = new List<Clases.Packing>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_PACKING, NOMBRE FROM AplicacionServicio_Packing WHERE AplicacionServicio_Packing.ESTADO = 0 AND AplicacionServicio_Packing.ID_CIUDAD = @IDCIUDAD ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Packings.Add(new Clases.Packing
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PACKING"])
                        
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
            return Packings;
        }

        public static List<Clases.Packing> GetPackingPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.Packing> Packings = new List<Clases.Packing>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Packing.ID_PACKING, Packing.NOMBRE, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Packing Packing, AplicacionServicio_Ciudad Ciudad WHERE Packing.ESTADO = 0 AND Packing.ID_CIUDAD = Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=@ID_PAIS ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_PAIS", IdPais);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Packings.Add(new Clases.Packing
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PACKING"]),
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
            return Packings;
        }

        public static List<Clases.Packing> GetPackingContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.Packing> Packings = new List<Clases.Packing>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Packing.ID_PACKING, Packing.NOMBRE, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Packing Packing, AplicacionServicio_Ciudad Ciudad, AplicacionServicio_Pais Pais WHERE Packing.ESTADO = 0 AND Packing.ID_CIUDAD = Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=Pais.ID_PAIS AND Pais.ID_CONTINENTE=@ID_CONTINENTE ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Packings.Add(new Clases.Packing
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PACKING"]),
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
            return Packings;
        }
        public static int EditarPacking(string NombrePacking, int IdCiudad, int Estado, int IdPacking, float Latitud = 0, float Longitud = 0, float Radio = 0)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            List<Clases.Packing> ListaPacking = new List<Clases.Packing>();
            //Validar Si Existe Naviera con el mismo Nombre
            //ListaPacking = GetPacking();
            //for (int i = 0; i < ListaPacking.Count(); i++)
            //{
            //    if (ListaPacking[i].Nombre.ToUpper() == NombrePacking.ToUpper() && ListaPacking[i].IdCiudad == IdCiudad && ListaPacking[i].Activo == Estado)
            //    {
            //        return 2;
            //    }
            //}
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Packing SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_CIUDAD = @IDCIUDAD, USUARIO = @USUARIO, LATITUD=@LATITUD, LONGITUD=@LONGITUD, RADIO=@RADIO WHERE ID_PACKING = @IDPACKING", cnn);
                command.Parameters.AddWithValue("@NOMBRE", NombrePacking.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDPACKING", IdPacking);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
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

        public static List<Clases.Packing> GetInfoPacking(int IdPacking)
        {
            SqlConnection cnn;
            List<Clases.Packing> Packings = new List<Clases.Packing>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select continente.ID_CONTINENTE, continente.NOMBRE AS NOMBRECONTINENTE, ciudad.ID_CIUDAD, ciudad.NOMBRE as NOMBRECIUDAD, pais.ID_PAIS, pais.NOMBRE NOMBREPAIS from AplicacionServicio_Packing packing,AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais, AplicacionServicio_Continente continente where packing.ID_PACKING =@IDPACKING and packing.ID_CIUDAD = ciudad.ID_CIUDAD and ciudad.ID_PAIS = pais.ID_PAIS and pais.ID_CONTINENTE=continente.ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@IDPACKING", IdPacking);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Packings.Add(new Clases.Packing
                    {
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        NombreContinente= reader["NOMBRECONTINENTE"].ToString(),
                        IdContinente = Convert.ToInt32(reader["ID_CONTINENTE"]),
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
            return Packings;
        }

        public static List<Clases.Packing> GetPackingActivos()
        {
            SqlConnection cnn;
            List<Clases.Packing> Packing = new List<Clases.Packing>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Packing.*, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Packing Packing, AplicacionServicio_Ciudad Ciudad WHERE Packing.ESTADO = 0 AND Packing.ID_CIUDAD=Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Packing.Add(new Clases.Packing
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PACKING"]),
                        IdCiudad= Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombreCiudad= reader["NOMBRECIUDAD"].ToString()
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
            return Packing;
        }
    }
}