using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Exportador
{
    public class ExportadorModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearExportador(string Nombre, int Estado, int IdPais)
        {

            List<Clases.Exportador> ListaExportador = new List<Clases.Exportador>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaExportador = GetExportador();
            for (int i = 0; i < ListaExportador.Count(); i++)
            {
                if (ListaExportador[i].NombreExportador.ToUpper() == Nombre.ToUpper() && ListaExportador[i].IdPais == IdPais)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Exportador(NOMBRE, ESTADO, USUARIO, ID_PAIS) VALUES(@NOMBRE, @ESTADO, @USUARIO, @IDPAIS)", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@IDPAIS", IdPais);


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

        public static List<Clases.Exportador> GetExportador()
        {
            SqlConnection cnn;
            List<Clases.Exportador> Exportadores = new List<Clases.Exportador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Exportador.*, AplicacionServicio_Pais.Nombre as NOMBREPAIS FROM AplicacionServicio_Exportador, AplicacionServicio_Pais WHERE AplicacionServicio_Exportador.ID_PAIS = AplicacionServicio_Pais.ID_PAIS ", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Exportadores.Add(new Clases.Exportador
                    {
                        NombreExportador = reader["NOMBRE"].ToString(),
                        Id_Exportador = Convert.ToInt32(reader["ID_EXPORTADOR"]),
                        Estado = Convert.ToInt32(reader["ESTADO"]),
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
            return Exportadores;
        }

        public static List<Clases.Exportador> GetExportadorActivos()
        {
            SqlConnection cnn;
            List<Clases.Exportador> Exportadores = new List<Clases.Exportador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Exportador.ID_EXPORTADOR, Exportador.NOMBRE NOMBRE_EXPORTADOR, Exportador.ESTADO, Pais.ID_PAIS, Pais.NOMBRE NOMBRE_PAIS FROM AplicacionServicio_Exportador Exportador, AplicacionServicio_Pais Pais WHERE Exportador.ID_PAIS=Pais.ID_PAIS AND Exportador.ESTADO=0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Exportadores.Add(new Clases.Exportador
                    {
                        NombreExportador = reader["NOMBRE_EXPORTADOR"].ToString(),
                        Id_Exportador = Convert.ToInt32(reader["ID_EXPORTADOR"]),
                        Estado = Convert.ToInt32(reader["ESTADO"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        NombrePais = reader["NOMBRE_PAIS"].ToString()
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

            return Exportadores;
        }

        public static Clases.Exportador GetIdExportador(string Nombre)
        {
            SqlConnection cnn;
            Clases.Exportador Exportador = new Clases.Exportador();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Exportador Where AplicacionServicio_Exportador.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Exportador.NombreExportador = reader["NOMBRE"].ToString();
                    Exportador.Id_Exportador = Convert.ToInt32(reader["ID_EXPORTADOR"]);
                    Exportador.Estado = Convert.ToInt32(reader["ESTADO"]);
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
            return Exportador;
        }

        public static string GetNombreExportador(int IdExportador = 0)
        {
            SqlConnection cnn;
            string nombre = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT NOMBRE FROM AplicacionServicio_Exportador Where AplicacionServicio_Exportador.ID_EXPORTADOR = @ID_EXPORTADOR", cnn);
                command.Parameters.AddWithValue("@ID_EXPORTADOR", IdExportador);
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

        public static List<Clases.Exportador> GetInfoExportador(int IdExportador)
        {
            SqlConnection cnn;
            List<Clases.Exportador> Exportador = new List<Clases.Exportador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT Exportador.ID_EXPORTADOR, Exportador.NOMBRE NOMBRE_EXPORTADOR, Pais.ID_PAIS, Pais.NOMBRE FROM AplicacionServicio_Exportador Exportador, AplicacionServicio_Pais Pais WHERE Exportador.ID_PAIS=Pais.ID_PAIS AND Exportador.ID_EXPORTADOR=@ID_EXPORTADOR", cnn);
                command.Parameters.AddWithValue("@ID_EXPORTADOR", IdExportador);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Exportador.Add(new Clases.Exportador
                    {
                        NombreExportador = reader["NOMBRE_EXPORTADOR"].ToString(),
                        NombrePais = reader["NOMBRE"].ToString(),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Id_Exportador = Convert.ToInt32(reader["ID_EXPORTADOR"])
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
            return Exportador;
        }

        public static int EditarExportador(string Nombre, int Estado, int IdExportador, int IdPais)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Exportador Exportador = new Clases.Exportador();
            Exportador = GetIdExportador(Nombre);
            List<Clases.Exportador> ListaExportador = new List<Clases.Exportador>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaExportador = GetExportador();
            for (int i = 0; i < ListaExportador.Count(); i++)
            {
                if (ListaExportador[i].NombreExportador.ToUpper() == Nombre.ToUpper() && ListaExportador[i].Estado == Estado && ListaExportador[i].IdPais == IdPais)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Exportador SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, USUARIO = @USUARIO, ID_PAIS = @IDPAIS WHERE ID_EXPORTADOR = @IDEXPORTADOR", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDEXPORTADOR", IdExportador);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                command.Parameters.AddWithValue("@IDPAIS", IdPais);


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

        public static List<Clases.Exportador> GetExportadorPais(int idPais)
        {
            SqlConnection cnn;
            List<Clases.Exportador> Exportadores = new List<Clases.Exportador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_EXPORTADOR, E.NOMBRE AS NOMBRE, P.NOMBRE AS NOMBREPAIS FROM AplicacionServicio_Exportador E, AplicacionServicio_Pais P WHERE E.ESTADO = 0 AND E.ID_PAIS = @IDPAIS AND P.ID_PAIS= @IDPAIS ORDER BY NOMBRE ASC", cnn);
                command.Parameters.AddWithValue("@IDPAIS", idPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Exportadores.Add(new Clases.Exportador
                    {
                        NombreExportador = reader["NOMBRE"].ToString(),
                        Id_Exportador = Convert.ToInt32(reader["ID_EXPORTADOR"]),
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
            return Exportadores;
        }


    }
}