using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Puertos
{
    public class PuertoModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearPuertos(string NombrePuerto, int IdCiudad, int Estado, string Latitud = "", string Longitud = "", string Radio = "")
        {

            List<Clases.PuertoDestino> ListaPuertoDestino = new List<Clases.PuertoDestino>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaPuertoDestino = GetPuertos();
            for (int i = 0; i < ListaPuertoDestino.Count(); i++)
            {
                if (ListaPuertoDestino[i].Nombre.ToUpper() == NombrePuerto.ToUpper() && ListaPuertoDestino[i].IdCiudad == IdCiudad && ListaPuertoDestino[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Puertos(ID_CIUDAD, NOMBRE, ESTADO, USUARIO, LATITUD, LONGITUD, RADIO) VALUES(@IDCIUDAD, @NOMBRE, @ESTADO, @USUARIO, @LATITUD, @LONGITUD, @RADIO)", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                command.Parameters.AddWithValue("@NOMBRE", NombrePuerto.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
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

        public static List<Clases.PuertoDestino> GetPuertos()
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Puertos.* , AplicacionServicio_Ciudad.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Puertos, AplicacionServicio_Ciudad WHERE AplicacionServicio_Puertos.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD AND AplicacionServicio_Puertos.ESTADO=0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
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
            return PuertoDestino;
        }

        public static List<Clases.PuertoDestino> GetPuertosSP()
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarPuertosSP @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
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
            return PuertoDestino;
        }

        public static List<Clases.PuertoOrigen> GetPuertoOrigenCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Puertos.ID_PUERTOORIGEN, Puertos.ID_CIUDAD, Puertos.NOMBRE NOMBRE, Puertos.ESTADO, Ciudad.ID_CIUDAD, Ciudad.NOMBRE NOMBRE_CIUDAD FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad WHERE Puertos.ESTADO = 0 AND Puertos.ID_CIUDAD = Ciudad.ID_CIUDAD AND Puertos.ID_CIUDAD = @IDCIUDAD", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoOrigen.Add(new Clases.PuertoOrigen
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString()
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
            return PuertoOrigen;
        }

        public static List<Clases.PuertoOrigen> GetPuertosOrigenPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Puertos.ID_PUERTOORIGEN, Puertos.NOMBRE NOMBRE, Puertos.ESTADO, Ciudad.ID_CIUDAD, Ciudad.NOMBRE NOMBRE_CIUDAD FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad WHERE Puertos.ESTADO = 0 AND Puertos.ID_CIUDAD = Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS = @ID_PAIS", cnn);
                command.Parameters.AddWithValue("@ID_PAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoOrigen.Add(new Clases.PuertoOrigen
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString()
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
            return PuertoOrigen;
        }

        public static List<Clases.PuertoOrigen> GetPuertosOrigenContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Puertos.ID_PUERTOORIGEN, Puertos.NOMBRE NOMBRE, Puertos.ESTADO, Ciudad.ID_CIUDAD, Ciudad.NOMBRE NOMBRE_CIUDAD FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad, AplicacionServicio_Pais Pais WHERE Puertos.ESTADO = 0 AND Puertos.ID_CIUDAD = Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=Pais.ID_PAIS AND Pais.ID_CONTINENTE = @ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoOrigen.Add(new Clases.PuertoOrigen
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString()
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
            return PuertoOrigen;
        }

        public static List<Clases.PuertoDestino> GetPuertosDestinoContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Puertos.ID_PUERTOORIGEN, Puertos.NOMBRE NOMBRE, Puertos.ESTADO, Ciudad.ID_CIUDAD, Ciudad.NOMBRE NOMBRE_CIUDAD FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad, AplicacionServicio_Pais Pais WHERE Puertos.ESTADO = 0 AND Puertos.ID_CIUDAD = Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS = Pais.ID_PAIS AND Pais.ID_CONTINENTE = @ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString()
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
            return PuertoDestino;
        }

        public static List<Clases.PuertoDestino> GetPuertosDestinoPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Puertos.ID_PUERTOORIGEN, Puertos.NOMBRE NOMBRE, Puertos.ESTADO, Ciudad.ID_CIUDAD, Ciudad.NOMBRE NOMBRE_CIUDAD FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad WHERE Puertos.ESTADO = 0 AND Puertos.ID_CIUDAD = Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS = @ID_PAIS", cnn);
                command.Parameters.AddWithValue("@ID_PAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString()
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
            return PuertoDestino;
        }

        public static List<Clases.PuertoOrigen> GetPuertoDestinoCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Puertos.ID_PUERTOORIGEN, Puertos.ID_CIUDAD, Puertos.NOMBRE NOMBRE, Puertos.ESTADO, Ciudad.ID_CIUDAD, Ciudad.NOMBRE NOMBRE_CIUDAD FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad WHERE Puertos.ESTADO = 0 AND Puertos.ID_CIUDAD = Ciudad.ID_CIUDAD AND Puertos.ID_CIUDAD = " + IdCiudad, cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoOrigen.Add(new Clases.PuertoOrigen
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString()
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
            return PuertoOrigen;
        }

        public static Clases.PuertoDestino GetIdPuertoDestino(string Nombre)
        {
            SqlConnection cnn;
            Clases.PuertoDestino PuertoDestino = new Clases.PuertoDestino();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Puertos Where AplicacionServicio_Puertos.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    PuertoDestino.Nombre = reader["NOMBRE"].ToString();
                    PuertoDestino.Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]);
                    PuertoDestino.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return PuertoDestino;
        }

        public static List<Clases.PuertoOrigen> GetInfoPuertoOrigen(int IdPuertoOrigen)
        {
            SqlConnection cnn;
            List<Clases.PuertoOrigen> PuertoOrigen = new List<Clases.PuertoOrigen>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT continente.ID_CONTINENTE, continente.NOMBRE AS NOMBRECONTINENTE, Puertos.ID_PUERTOORIGEN, Puertos.NOMBRE NOMBRE, Ciudad.ID_CIUDAD, Ciudad.Nombre NOMBRE_CIUDAD, Pais.ID_PAIS, Pais.Nombre NOMBRE_PAIS FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad, AplicacionServicio_Pais Pais, AplicacionServicio_Continente continente WHERE Puertos.ID_CIUDAD=Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=Pais.ID_PAIS AND Puertos.ID_PUERTOORIGEN = @ID_PUERTO and Pais.ID_CONTINENTE = continente.ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@ID_PUERTO", IdPuertoOrigen);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoOrigen.Add(new Clases.PuertoOrigen
                    {
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombrePais = reader["NOMBRE_PAIS"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
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
            return PuertoOrigen;
        }

        public static List<Clases.PuertoDestino> GetInfoPuertoDestino(int IdPuertoDestino)
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT continente.ID_CONTINENTE, continente.NOMBRE AS NOMBRECONTINENTE, Puertos.ID_PUERTOORIGEN, Puertos.NOMBRE NOMBRE, Ciudad.ID_CIUDAD, Ciudad.Nombre NOMBRE_CIUDAD, Pais.ID_PAIS, Pais.Nombre NOMBRE_PAIS FROM AplicacionServicio_Puertos Puertos, AplicacionServicio_Ciudad Ciudad, AplicacionServicio_Pais Pais, AplicacionServicio_Continente continente WHERE Puertos.ID_CIUDAD = Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS = Pais.ID_PAIS AND Puertos.ID_PUERTOORIGEN = @ID_PUERTO and Pais.ID_CONTINENTE = continente.ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@ID_PUERTO", IdPuertoDestino);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombrePais = reader["NOMBRE_PAIS"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
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
            return PuertoDestino;
        }

        public static Clases.PuertoOrigen GetIdPuertoOrigen(string Nombre)
        {
            SqlConnection cnn;
            Clases.PuertoOrigen PuertoOrigen = new Clases.PuertoOrigen();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Puertos Where AplicacionServicio_Puertos.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    PuertoOrigen.Nombre = reader["NOMBRE"].ToString();
                    PuertoOrigen.Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]);
                    PuertoOrigen.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return PuertoOrigen;
        }

        public static string GetNombrePuerto(int IdPuerto=0)
        {
            SqlConnection cnn;
            string puerto = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT NOMBRE FROM AplicacionServicio_Puertos Where AplicacionServicio_Puertos.ID_PUERTOORIGEN = @ID_PUERTOORIGEN", cnn);
                command.Parameters.AddWithValue("@ID_PUERTOORIGEN", IdPuerto);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    puerto = reader["NOMBRE"].ToString();
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
            return puerto;
        }

        public static int EditarPuerto(string Nombre, int IdCiudad, int Estado, int IdPuerto, string Latitud = "", string Longitud = "", string Radio = "")
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.PuertoDestino PuertoDestino = new Clases.PuertoDestino();
            PuertoDestino = GetIdPuertoDestino(Nombre);
            List<Clases.PuertoDestino> ListaPuertoDestino = new List<Clases.PuertoDestino>();
            //Validar Si Existe Naviera con el mismo Nombre
            //ListaPuertoDestino = GetPuertos();
            //for (int i = 0; i < ListaPuertoDestino.Count(); i++)
            //{
            //    if (ListaPuertoDestino[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaPuertoDestino[i].IdCiudad == IdCiudad && ListaPuertoDestino[i].Activo == Estado)
            //    {
            //        return 2;
            //    }
            //}
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Puertos SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_CIUDAD = @IDCIUDAD, USUARIO = @USUARIO, LATITUD=@LATITUD, LONGITUD=@LONGITUD, RADIO=@RADIO WHERE ID_PUERTOORIGEN = @IDPUERTO", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDPUERTO", IdPuerto);
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

        public static List<Clases.PuertoDestino> GetDestinosViaje(int IdPuertoOrigen)
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetDestinosViaje @USUARIO, @PUERTOORIGEN", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@PUERTOORIGEN", IdPuertoOrigen);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTODESTINO"]),
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
            return PuertoDestino;
        }

    }
}