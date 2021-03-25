using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Deadline
{
    public class DeadLineModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int CrearDeadLine(string Descripcion, int IdPais, int DiasLimite, int Estado)
        {

            List<Clases.DeadLine> ListaDeadLine = new List<Clases.DeadLine>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaDeadLine = GetDeadLine();
            for (int i = 0; i < ListaDeadLine.Count(); i++)
            {
                if (ListaDeadLine[i].Descripcion.ToUpper() == Descripcion.ToUpper() && ListaDeadLine[i].IdPais == IdPais && ListaDeadLine[i].DiasLimite == DiasLimite && ListaDeadLine[i].Activo == Estado)
                {                
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Deadline(ID_PAIS, DESCRIPCION, DIASLIMITE, ESTADO, USUARIO) VALUES(@IDPAIS, @DESCRIPCION, @DIASLIMITE, @ESTADO, @USUARIO)", cnn);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);
                command.Parameters.AddWithValue("@DESCRIPCION", Descripcion.ToUpper());
                command.Parameters.AddWithValue("@DIASLIMITE", DiasLimite);
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

        public static List<Clases.DeadLine> GetDeadLine()
        {
            SqlConnection cnn;
            List<Clases.DeadLine> DeadLine = new List<Clases.DeadLine>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Deadline.* , AplicacionServicio_Pais.NOMBRE as NOMBREPAIS FROM AplicacionServicio_Deadline, AplicacionServicio_Pais WHERE AplicacionServicio_Deadline.ID_PAIS = AplicacionServicio_Pais.ID_PAIS", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DeadLine.Add(new Clases.DeadLine
                    {
                        Descripcion = reader["DESCRIPCION"].ToString(),
                        Id = Convert.ToInt32(reader["ID_DEADLINE"]),
                        IdPais = Convert.ToInt32(reader["ID_PAIS"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombrePais = reader["NOMBREPAIS"].ToString(),
                        DiasLimite = Convert.ToInt32(reader["DIASLIMITE"])
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
            return DeadLine;
        }

        public static Clases.DeadLine GetIdDeadLine(string Nombre)
        {
            SqlConnection cnn;
            Clases.DeadLine DeadLine = new Clases.DeadLine();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Deadline Where AplicacionServicio_Deadline.DESCRIPCION = @DESCRIPCION", cnn);
                command.Parameters.AddWithValue("@DESCRIPCION", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    DeadLine.Descripcion = reader["DESCRIPCION"].ToString();
                    DeadLine.Id = Convert.ToInt32(reader["ID_DEADLINE"]);
                    DeadLine.Activo = Convert.ToInt32(reader["ESTADO"]);
                    DeadLine.DiasLimite = Convert.ToInt32(reader["DIASLIMITE"]);
                    DeadLine.IdPais = Convert.ToInt32(reader["ID_PAIS"]);

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
            return DeadLine;
        }

        public static int GetDias(int IdPais) {
            SqlConnection cnn;
            int DiasLimite = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select DIASLIMITE from AplicacionServicio_Deadline where ID_PAIS = @IDPAIS;", cnn);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DiasLimite = Convert.ToInt32(reader["DIASLIMITE"]);
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
            return DiasLimite;
        }
        public static int EditarDeadLine(string Nombre, int IdPais, int DiasLimite, int Estado, int IdDeadLine)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.DeadLine DeadLine = new Clases.DeadLine();
            DeadLine = GetIdDeadLine(Nombre);
            List<Clases.DeadLine> ListaDeadLine = new List<Clases.DeadLine>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaDeadLine = GetDeadLine();
            for (int i = 0; i < ListaDeadLine.Count(); i++)
            {
                if (ListaDeadLine[i].Descripcion.ToUpper() == Nombre.ToUpper() && ListaDeadLine[i].IdPais == IdPais && ListaDeadLine[i].Activo == Estado && ListaDeadLine[i].DiasLimite == DiasLimite)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Deadline SET DESCRIPCION = @DESCRIPCION, ESTADO = @ESTADO, ID_PAIS = @IDPAIS, DIASLIMITE = @DIASLIMITE, USUARIO = @USUARIO WHERE ID_DEADLINE = @IDDEADLINE", cnn);
                command.Parameters.AddWithValue("@DESCRIPCION", Nombre);
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDDEADLINE", IdDeadLine);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);
                command.Parameters.AddWithValue("@DIASLIMITE", DiasLimite);
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

        public static void AuditoriaDeadLine(int Id, string Usuario, string Operacion)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_AuditoriaDeadline(ID_DEADLINE, USUARIO, FECHA, OPERACION) VALUES(@IDDEADLINE,@USUARIO,SYSDATETIME(),@OPERACION)", cnn);
                command.Parameters.AddWithValue("@IDDEADLINE", Id);
                command.Parameters.AddWithValue("@USUARIO", Usuario);
                command.Parameters.AddWithValue("@OPERACION", Operacion);


                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
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