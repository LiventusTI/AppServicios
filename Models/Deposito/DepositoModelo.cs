using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Deposito
{
    public class DepositoModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearDeposito(string NombreDeposito, int IdCiudad, int Estado, string Latitud = "", string Longitud = "", string Radio = "")
        {

            List<Clases.Deposito> ListaDeposito = new List<Clases.Deposito>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaDeposito = GetDepositos();
            for (int i = 0; i < ListaDeposito.Count(); i++)
            {
                if (ListaDeposito[i].Nombre.ToUpper() == NombreDeposito.ToUpper() && ListaDeposito[i].IdCiudad == IdCiudad && ListaDeposito[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Deposito(ID_CIUDAD, NOMBRE, ESTADO, USUARIO, LATITUD, LONGITUD, RADIO) VALUES(@IDCIUDAD, @NOMBRE, @ESTADO, @USUARIO, @LATITUD, @LONGITUD, @RADIO)", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);
                command.Parameters.AddWithValue("@NOMBRE", NombreDeposito.ToUpper());
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

        public static List<Clases.Deposito> GetDepositos()
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Deposito.* , AplicacionServicio_CIUDAD.NOMBRE as NOMBRECIUDAD FROM AplicacionServicio_Deposito, AplicacionServicio_Ciudad WHERE AplicacionServicio_Deposito.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_DEPOSITO"]),
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
            return Depositos;
        }

        public static List<Clases.Deposito> GetDepositosSP()
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT D.ID_DEPOSITO, D.NOMBRE, C.NOMBRE AS NOMBRECIUDAD FROM AplicacionServicio_Deposito D, AplicacionServicio_Ciudad C, AplicacionServicio_Pais P, AplicacionServicio_Usuario U, AplicacionServicio_ServiceProvider S WHERE D.ID_CIUDAD=C.ID_CIUDAD AND C.ID_PAIS=P.ID_PAIS AND P.ID_PAIS=S.ID_PAIS AND S.ID_SERVICEPROVIDER=U.ID_SERVICEPROVIDER AND U.NOMBREUSUARIO=@USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_DEPOSITO"]),
                        NombreCiudad= reader["NOMBRECIUDAD"].ToString(),
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
            return Depositos;
        }

        public static List<Clases.Deposito> GetLugares()
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarLugares", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Nombre = reader["NOMBRE_LUGAR"].ToString(),
                        Id = Convert.ToInt32(reader["ID_LUGAR"]),
                        NombreCiudad = reader["NOMBRE_CIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"])
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
            return Depositos;
        }

        public static Clases.Deposito GetIdDeposito(string Nombre)
        {
            SqlConnection cnn;
            Clases.Deposito Deposito = new Clases.Deposito();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Deposito Where AplicacionServicio_Deposito.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Deposito.Nombre = reader["NOMBRE"].ToString();
                    Deposito.Id = Convert.ToInt32(reader["ID_DEPOSITO"]);
                    Deposito.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return Deposito;
        }

        public static List<Clases.Deposito> GetDepositoCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT deposito.ID_DEPOSITO, deposito.NOMBRE, ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Deposito deposito, AplicacionServicio_Ciudad ciudad WHERE deposito.ESTADO = 0 AND deposito.ID_CIUDAD = @IDCIUDAD and deposito.ID_CIUDAD = ciudad.ID_CIUDAD ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@IDCIUDAD", IdCiudad);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_DEPOSITO"]),
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
            return Depositos;
        }

        public static List<Clases.Deposito> GetDepositoPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT deposito.ID_DEPOSITO, deposito.NOMBRE, ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Deposito deposito, AplicacionServicio_Ciudad ciudad WHERE deposito.ESTADO = 0 AND deposito.ID_CIUDAD = ciudad.ID_CIUDAD AND ciudad.ID_PAIS=@ID_PAIS ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_PAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_DEPOSITO"]),
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
            return Depositos;
        }

        public static List<Clases.Deposito> GetDepositoContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT deposito.ID_DEPOSITO, deposito.NOMBRE, ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Deposito deposito, AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais WHERE deposito.ESTADO = 0 AND deposito.ID_CIUDAD = ciudad.ID_CIUDAD AND ciudad.ID_PAIS=pais.ID_PAIS AND pais.ID_CONTINENTE=@ID_CONTINENTE ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_DEPOSITO"]),
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
            return Depositos;
        }

        public static int EditarDeposito(string Nombre, int IdCiudad, int Estado, int IdDeposito, string Latitud = "", string Longitud = "", string Radio = "")
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Deposito Deposito = new Clases.Deposito();
            Deposito = GetIdDeposito(Nombre);
            List<Clases.Deposito> ListaDepositos = new List<Clases.Deposito>();
            //Validar Si Existe Naviera con el mismo Nombre
            //ListaDepositos = GetDepositos();
            //for (int i = 0; i < ListaDepositos.Count(); i++)
            //{
            //    if (ListaDepositos[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaDepositos[i].IdCiudad == IdCiudad && ListaDepositos[i].Activo == Estado)
            //    {
            //        return 2;
            //    }
            //}
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Deposito SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_CIUDAD = @IDCIUDAD, USUARIO = @USUARIO, LATITUD=@LATITUD, LONGITUD=@LONGITUD, RADIO=@RADIO WHERE ID_DEPOSITO = @IDDEPOSITO", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDDEPOSITO", IdDeposito);
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

        public static List<Clases.Deposito> GetDepositosActivos()
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT deposito.*, ciudad.NOMBRE NOMBRECIUDAD FROM AplicacionServicio_Deposito deposito, AplicacionServicio_Ciudad ciudad WHERE deposito.ESTADO = 0 and deposito.ID_CIUDAD = ciudad.ID_CIUDAD;", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_DEPOSITO"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
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
            return Depositos;
        }

        public static List<Clases.Deposito> GetInfoDeposito(int IdDeposito)
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select continente.ID_CONTINENTE, continente.NOMBRE AS NOMBRECONTINENTE, ciudad.ID_CIUDAD, ciudad.NOMBRE as NOMBRECIUDAD, pais.ID_PAIS, pais.NOMBRE NOMBREPAIS from AplicacionServicio_Deposito deposito,AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais, AplicacionServicio_Continente continente where deposito.ID_DEPOSITO = @IDDEPOSITO and deposito.ID_CIUDAD = ciudad.ID_CIUDAD and ciudad.ID_PAIS = pais.ID_PAIS and pais.ID_CONTINENTE=continente.ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@IDDEPOSITO", IdDeposito);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        IdContinente= Convert.ToInt32(reader["ID_CONTINENTE"]),
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
            return Depositos;
        }

        public static List<Clases.Deposito> GetDepositoByPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.Deposito> Depositos = new List<Clases.Deposito>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select deposito.ID_DEPOSITO, deposito.NOMBRE NOMBREDEPOSITO, ciudad.ID_CIUDAD, ciudad.NOMBRE NOMBRECIUDAD, pais.ID_PAIS, pais.NOMBRE NOMBREPAIS from AplicacionServicio_Deposito deposito, AplicacionServicio_Ciudad ciudad, AplicacionServicio_Pais pais where pais.ID_PAIS = @IDPAIS and deposito.ID_CIUDAD = ciudad.ID_CIUDAD and ciudad.ID_PAIS = pais.ID_PAIS", cnn);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Depositos.Add(new Clases.Deposito
                    {
                        Id = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Nombre = reader["NOMBREDEPOSITO"].ToString(),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString(),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"])
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
            return Depositos;
        }

     

    }
}