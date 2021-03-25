using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Terminal
{
    public class TerminalModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearTerminal(string NombreTerminal, int IdCiudad, int Estado, string Latitud = "", string Longitud = "", string Radio = "")
        {
            List<Clases.Terminal> ListaTerminales = new List<Clases.Terminal>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaTerminales = GetTerminales();
            for (int i = 0; i < ListaTerminales.Count(); i++)
            {
                if (ListaTerminales[i].Nombre.ToUpper() == NombreTerminal.ToUpper() && ListaTerminales[i].IdCiudad == IdCiudad && ListaTerminales[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Terminal(ID_CIUDAD, NOMBRE, ESTADO, USUARIO, LATITUD, LONGITUD, RADIO) VALUES(@IDCIUDAD, @NOMBRE, @ESTADO, @USUARIO, @LATITUD, @LONGITUD, @RADIO)", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                command.Parameters.AddWithValue("@NOMBRE", NombreTerminal.ToUpper());
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


        public static List<Clases.Terminal> GetTerminales()
        {
            SqlConnection cnn;
            List<Clases.Terminal> Terminales = new List<Clases.Terminal>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Terminal.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Terminal, AplicacionServicio_Ciudad WHERE AplicacionServicio_Terminal.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Terminales.Add(new Clases.Terminal
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_TERMINAL"]),
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
            return Terminales;
        }

        public static List<Clases.Terminal> GetTerminalCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.Terminal> Terminales = new List<Clases.Terminal>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Terminal.ID_TERMINAL, Terminal.ID_CIUDAD, Terminal.NOMBRE, Terminal.ESTADO, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Terminal Terminal, AplicacionServicio_Ciudad Ciudad WHERE Terminal.ESTADO = 0 AND Terminal.ID_CIUDAD=Ciudad.ID_CIUDAD AND Terminal.ID_CIUDAD=@IDCIUDAD ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Terminales.Add(new Clases.Terminal
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_TERMINAL"]),
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
            return Terminales;
        }

        public static List<Clases.Terminal> GetTerminalPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.Terminal> Terminales = new List<Clases.Terminal>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Terminal.ID_TERMINAL, Terminal.ID_CIUDAD, Terminal.NOMBRE, Terminal.ESTADO, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Terminal Terminal, AplicacionServicio_Ciudad Ciudad WHERE Terminal.ESTADO = 0 AND Terminal.ID_CIUDAD=Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=@ID_PAIS ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_PAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Terminales.Add(new Clases.Terminal
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_TERMINAL"]),
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
            return Terminales;
        }

        public static List<Clases.Terminal> GetTerminalContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.Terminal> Terminales = new List<Clases.Terminal>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Terminal.ID_TERMINAL, Terminal.ID_CIUDAD, Terminal.NOMBRE, Terminal.ESTADO, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Terminal Terminal, AplicacionServicio_Ciudad Ciudad, AplicacionServicio_Pais Pais WHERE Antepuerto.ESTADO = 0 AND Antepuerto.ID_CIUDAD=Ciudad.ID_CIUDAD AND Ciudad.ID_PAIS=Pais.ID_PAIS AND Pais.ID_CONTINENTE=@ID_CONTINENTE ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Terminales.Add(new Clases.Terminal
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_TERMINAL"]),
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
            return Terminales;
        }

        public static int EditarTerminal(string Nombre, int IdCiudad, int Estado, int IdTerminal, string Latitud ="", string Longitud = "", string Radio = "")
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            List<Clases.Terminal> ListaTerminales = new List<Clases.Terminal>();
            //Validar Si Existe Naviera con el mismo Nombre
            //ListaTerminales = GetTerminales();
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
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Terminal SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_CIUDAD = @IDCIUDAD, USUARIO = @USUARIO, LATITUD=@LATITUD, LONGITUD=@LONGITUD, RADIO=@RADIO WHERE ID_TERMINAL = @IDTERMINAL", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDTERMINAL", IdTerminal);
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

        public static List<Clases.Terminal> GetInfoTerminal(int IdTerminal)
        {
            SqlConnection cnn;
            List<Clases.Terminal> Terminales = new List<Clases.Terminal>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select continente.ID_CONTINENTE, continente.NOMBRE AS NOMBRECONTINENTE, ciudad.ID_CIUDAD, ciudad.NOMBRE as NOMBRECIUDAD, pais.ID_PAIS, pais.NOMBRE NOMBREPAIS from AplicacionServicio_Terminal Terminal,AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais, AplicacionServicio_Continente continente where Terminal.ID_TERMINAL = @IDTERMINAL and Terminal.ID_CIUDAD = ciudad.ID_CIUDAD and ciudad.ID_PAIS = pais.ID_PAIS and pais.ID_CONTINENTE=continente.ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@IDTERMINAL", IdTerminal);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Terminales.Add(new Clases.Terminal
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
            return Terminales;
        }

        public static List<Clases.Terminal> GetTerminalesActivos()
        {
            SqlConnection cnn;
            List<Clases.Terminal> Terminales = new List<Clases.Terminal>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Terminal.*, Ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Terminal Terminal, AplicacionServicio_Ciudad Ciudad WHERE Terminal.ESTADO = 0 AND Terminal.ID_CIUDAD=Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Terminales.Add(new Clases.Terminal
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_TERMINAL"]),
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
            return Terminales;
        }

    }
}