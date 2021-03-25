using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Ciudad
{
    public class CiudadModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearCiudad(string Nombre, int idPais, int Estado)
        {

            List<Clases.Ciudad> ListaCiudades = new List<Clases.Ciudad>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaCiudades = GetCiudades();
            for (int i = 0; i < ListaCiudades.Count(); i++)
            {
                if (ListaCiudades[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaCiudades[i].IdPais == idPais)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Ciudad(ID_PAIS, NOMBRE, USUARIO, ESTADO) VALUES(@IDPAIS, @NOMBRE, @USUARIO, @ESTADO)", cnn);
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

        public static List<Clases.Ciudad> GetCiudades()
        {
            SqlConnection cnn;
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Ciudad.* , AplicacionServicio_Pais.NOMBRE as NOMBREPAIS FROM AplicacionServicio_Ciudad, AplicacionServicio_Pais WHERE AplicacionServicio_Ciudad.ID_PAIS = AplicacionServicio_Pais.ID_PAIS", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ciudades.Add(new Clases.Ciudad
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_CIUDAD"]),
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
            return Ciudades;
        }

        public static List<Clases.Ciudad> GetCiudadesSP()
        {
            SqlConnection cnn;
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarCiudadSP @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ciudades.Add(new Clases.Ciudad
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_CIUDAD"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
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
            return Ciudades;
        }

        public static List<Clases.Ciudad> GetCiudadesActivas()
        {
            SqlConnection cnn;
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ciudad.*, pais.NOMBRE NOMBREPAIS FROM AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais WHERE ciudad.ESTADO = 0 and ciudad.ID_PAIS = pais.ID_PAIS ORDER BY NOMBRE ASC", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ciudades.Add(new Clases.Ciudad
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_CIUDAD"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
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
            return Ciudades;
        }

        public static List<Clases.Ciudad> GetCiudadesPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ciudad.*, pais.NOMBRE NOMBREPAIS FROM AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais WHERE ciudad.ESTADO = 0 AND ciudad.ID_PAIS = @IDPAIS AND pais.ID_PAIS = ciudad.ID_PAIS ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ciudades.Add(new Clases.Ciudad
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_CIUDAD"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
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
            return Ciudades;
        }

        public static List<Clases.Ciudad> GetCiudadesContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ciudad.*, pais.NOMBRE NOMBREPAIS FROM AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais WHERE ciudad.ESTADO = 0 AND pais.ID_PAIS = ciudad.ID_PAIS AND pais.ID_CONTINENTE=@ID_CONTINENTE ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ciudades.Add(new Clases.Ciudad
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_CIUDAD"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
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
            return Ciudades;
        }

        public static List<Clases.Ciudad> GetInfoCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.Ciudad> Ciudades = new List<Clases.Ciudad>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select pais.NOMBRE NOMBREPAIS, pais.ID_PAIS from AplicacionServicio_Pais pais, AplicacionServicio_Ciudad ciudad where ciudad.ID_CIUDAD = @IDCIUDAD and ciudad.ID_PAIS = pais.ID_PAIS;", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ciudades.Add(new Clases.Ciudad
                    {
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
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
            return Ciudades;
        }

        public static Clases.Ciudad GetIdCiudad(string Nombre)
        {
            SqlConnection cnn;
            Clases.Ciudad Ciudad = new Clases.Ciudad();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Ciudad Where AplicacionServicio_Ciudad.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Ciudad.Nombre = reader["NOMBRE"].ToString();
                    Ciudad.Id = Convert.ToInt32(reader["ID_Pais"]);
                    Ciudad.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return Ciudad;
        }

        public static int EditarCiudad(string Nombre, int IdPais, int Estado, int IdCiudad)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Ciudad Ciudad = new Clases.Ciudad();
            Ciudad = GetIdCiudad(Nombre);
            List<Clases.Ciudad> ListaCiudades = new List<Clases.Ciudad>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaCiudades = GetCiudades();
            for (int i = 0; i < ListaCiudades.Count(); i++)
            {
                if (ListaCiudades[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaCiudades[i].IdPais == IdPais && ListaCiudades[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Ciudad SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_PAIS = @IDPAIS, USUARIO = @USUARIO WHERE ID_CIUDAD = @IDCIUDAD", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
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