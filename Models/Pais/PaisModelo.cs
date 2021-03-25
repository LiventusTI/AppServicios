using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Pais
{
    public class PaisModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearPais(string Nombre, int IdContinente, int Estado)
        {

            List<Clases.Pais> ListaPaises = new List<Clases.Pais>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaPaises = GetPaises();
            for (int i = 0; i < ListaPaises.Count(); i++)
            {
                if (ListaPaises[i].Nombre.ToUpper() == Nombre.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Pais(ID_CONTINENTE, NOMBRE, ESTADO, USUARIO) VALUES(@IDCONTINENTE, @NOMBRE, @ESTADO, @USUARIO)", cnn);
                command.Parameters.AddWithValue("@IDCONTINENTE", IdContinente);
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
        public static int CrearContinente(string Nombre, int Estado)
        {

            List<Clases.Continente> ListaContinentes = new List<Clases.Continente>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaContinentes = GetContinentes();
            for (int i = 0; i < ListaContinentes.Count(); i++)
            {
                if (ListaContinentes[i].Nombre.ToUpper() == Nombre.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_Continente(NOMBRE, ESTADO, USUARIO) VALUES(@NOMBRE,@ESTADO,@USUARIO)", cnn);
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
        public static List<Clases.Pais> GetPaises()
        {
            SqlConnection cnn;
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Pais.* , AplicacionServicio_Continente.NOMBRE as NOMBRECONTINENTE FROM AplicacionServicio_Pais, AplicacionServicio_Continente WHERE AplicacionServicio_Pais.ID_CONTINENTE = AplicacionServicio_Continente.ID_CONTINENTE", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Paises.Add(new Clases.Pais
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PAIS"]),
                        IdContinente = Convert.ToInt32(reader["ID_CONTINENTE"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
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
            return Paises;
        }

        public static List<Clases.Pais> GetPaisesSP()
        {
            SqlConnection cnn;
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarPaisSP @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Paises.Add(new Clases.Pais
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PAIS"])
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
            return Paises;
        }
        public static List<Clases.Pais> GetPaisesContinente(int IdContinente)
        {
            SqlConnection cnn;
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Pais.* , AplicacionServicio_Continente.NOMBRE as NOMBRECONTINENTE FROM AplicacionServicio_Pais, AplicacionServicio_Continente WHERE AplicacionServicio_Pais.ID_CONTINENTE = AplicacionServicio_Continente.ID_CONTINENTE AND AplicacionServicio_Pais.ID_CONTINENTE=@ID_CONTINENTE", cnn);
                command.Parameters.AddWithValue("@ID_CONTINENTE", IdContinente);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Paises.Add(new Clases.Pais
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PAIS"]),
                        IdContinente = Convert.ToInt32(reader["ID_CONTINENTE"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
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
            return Paises;
        }
        public static List<Clases.Pais> GetPaisesActivos()
        {
            SqlConnection cnn;
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Pais WHERE AplicacionServicio_Pais.ESTADO = 0 ORDER BY NOMBRE ASC", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Paises.Add(new Clases.Pais
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PAIS"]),
                        IdContinente = Convert.ToInt32(reader["ID_CONTINENTE"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreContinente = ""
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
            return Paises;
        }
        public static List<Clases.Continente> GetContinentes()
        {
            SqlConnection cnn;
            List<Clases.Continente> Continente = new List<Clases.Continente>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Continente", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Continente.Add(new Clases.Continente
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_CONTINENTE"]),
                        Activo = Convert.ToInt32(reader["ESTADO"])
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
            return Continente;
        }
        public static int GetIdPaisByPuerto(int IdPuerto)
        {
            SqlConnection cnn;
            int IdPais = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select AplicacionServicio_Pais.ID_PAIS as IDPAIS From AplicacionServicio_Puertos, AplicacionServicio_Ciudad, AplicacionServicio_Pais where AplicacionServicio_Puertos.ID_CIUDAD = AplicacionServicio_Ciudad.ID_CIUDAD AND AplicacionServicio_Ciudad.ID_PAIS = AplicacionServicio_Pais.ID_PAIS AND AplicacionServicio_Puertos.ID_PUERTOORIGEN = @IDPUERTO", cnn);
                command.Parameters.AddWithValue("@IDPUERTO", IdPuerto);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IdPais = Convert.ToInt32(reader["IDPAIS"]);
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
            return IdPais;
        }
        public static List<Clases.Continente> GetContinentesActivos()
        {
            SqlConnection cnn;
            List<Clases.Continente> Continente = new List<Clases.Continente>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Continente WHERE  AplicacionServicio_Continente.ESTADO = 0 ", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Continente.Add(new Clases.Continente
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_CONTINENTE"]),
                        Activo = Convert.ToInt32(reader["ESTADO"])
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
            return Continente;
        }
        public static int EditarContinente(string Nombre, int Estado, int IdContinente)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Continente Continente = new Clases.Continente();
            Continente = GetIdContinente(Nombre);
            List<Clases.Continente> ListaContinentes = new List<Clases.Continente>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaContinentes = GetContinentes();
            for (int i = 0; i < ListaContinentes.Count(); i++)
            {
                if (ListaContinentes[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaContinentes[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Continente SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, USUARIO = @USUARIO WHERE ID_CONTINENTE = @IDCONTINENTE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDCONTINENTE", IdContinente);
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
        public static int EditarPais(string Nombre, int IdContinente, int Estado, int IdPais)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            Clases.Pais Pais = new Clases.Pais();
            Pais = GetIdPais(Nombre);
            List<Clases.Pais> ListaPaises = new List<Clases.Pais>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaPaises = GetPaises();
            for (int i = 0; i < ListaPaises.Count(); i++)
            {
                if (ListaPaises[i].Nombre.ToUpper() == Nombre.ToUpper() && ListaPaises[i].IdContinente == IdContinente && ListaPaises[i].Activo == Estado)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_Pais SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, ID_CONTINENTE = @IDCONTINENTE, USUARIO = @USUARIO WHERE ID_PAIS = @IDPAIS", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDCONTINENTE", IdContinente);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);
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
        public static Clases.Continente GetIdContinente(string Nombre)
        {
            SqlConnection cnn;
            Clases.Continente Continente = new Clases.Continente();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Continente Where AplicacionServicio_Continente.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Continente.Nombre = reader["NOMBRE"].ToString();
                    Continente.Id = Convert.ToInt32(reader["ID_CONTINENTE"]);
                    Continente.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return Continente;
        }
        public static Clases.Pais GetIdPais(string Nombre)
        {
            SqlConnection cnn;
            Clases.Pais Pais = new Clases.Pais();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_Pais Where AplicacionServicio_Pais.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Pais.Nombre = reader["NOMBRE"].ToString();
                    Pais.Id = Convert.ToInt32(reader["ID_Pais"]);
                    Pais.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return Pais;
        }
        public static List<Clases.Pais> GetInfoPais(int IdPais)
        {
            SqlConnection cnn;
            List<Clases.Pais> Paises = new List<Clases.Pais>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select * from aplicacionservicio_Pais where id_pais = @IDPAIS", cnn);
                command.Parameters.AddWithValue("@IDPAIS", IdPais);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Paises.Add(new Clases.Pais
                    {
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
            return Paises;
        }
        public static Clases.Continente GetNombreContienenteByPuerto(int Puerto)
        {
            SqlConnection cnn;
            Clases.Continente Continente = new Clases.Continente();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("(SELECT NOMBRE from APLICACIONSERVICIO_CONTINENTE where ID_CONTINENTE = (SELECT ID_CONTINENTE from APLICACIONSERVICIO_PAIS where ID_PAIS = (SELECT ID_PAIS FROM APLICACIONSERVICIO_CIUDAD WHERE ID_CIUDAD = (SELECT ID_CIUDAD FROM APLICACIONSERVICIO_PUERTOS WHERE ID_PUERTOORIGEN = @PUERTO))))", cnn);
                command.Parameters.AddWithValue("@PUERTO", Puerto);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Continente.Nombre = reader["NOMBRE"].ToString();
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
            return Continente;
        }
        public static Clases.Pais GetNombrePais(int TipoLugar, int Lugar)
        {
            SqlConnection cnn;
            Clases.Pais Pais = new Clases.Pais();
            
            cnn = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand("");
                cnn.Open();
                if (TipoLugar == 1) {
                    command = new SqlCommand("SELECT ISNULL(NOMBRE, ' ') AS NOMBRE FROM APLICACIONSERVICIO_PAIS WHERE ID_PAIS = (SELECT ID_PAIS FROM APLICACIONSERVICIO_CIUDAD WHERE ID_CIUDAD = (SELECT ID_CIUDAD FROM APLICACIONSERVICIO_DEPOSITO WHERE ID_DEPOSITO = @LUGAR))", cnn);
                }
                else if (TipoLugar == 2) {
                    command = new SqlCommand("SELECT ISNULL(NOMBRE, ' ') AS NOMBRE FROM APLICACIONSERVICIO_PAIS WHERE ID_PAIS = (SELECT ID_PAIS FROM APLICACIONSERVICIO_CIUDAD WHERE ID_CIUDAD = (SELECT ID_CIUDAD FROM APLICACIONSERVICIO_PACKING WHERE ID_PACKING = @LUGAR))", cnn);
                }
                else if (TipoLugar == 3) {
                    command = new SqlCommand("SELECT ISNULL(NOMBRE, ' ') AS NOMBRE FROM APLICACIONSERVICIO_PAIS WHERE ID_PAIS = (SELECT ID_PAIS FROM APLICACIONSERVICIO_CIUDAD WHERE ID_CIUDAD = (SELECT ID_CIUDAD FROM APLICACIONSERVICIO_ANTEPUERTO WHERE ID_ANTEPUERTO = @LUGAR))", cnn);
                }
                else if (TipoLugar == 4) {
                    command = new SqlCommand("SELECT ISNULL(NOMBRE, ' ') AS NOMBRE FROM APLICACIONSERVICIO_PAIS WHERE ID_PAIS = (SELECT ID_PAIS FROM APLICACIONSERVICIO_CIUDAD WHERE ID_CIUDAD = (SELECT ID_CIUDAD FROM APLICACIONSERVICIO_PUERTOS WHERE ID_PUERTOORIGEN = @LUGAR))", cnn);
                }
                else if (TipoLugar == 6) {
                    command = new SqlCommand("SELECT ISNULL(NOMBRE, ' ') AS NOMBRE FROM APLICACIONSERVICIO_PAIS WHERE ID_PAIS = (SELECT ID_PAIS FROM APLICACIONSERVICIO_CIUDAD WHERE ID_CIUDAD = (SELECT ID_CIUDAD FROM APLICACIONSERVICIO_BODEGA WHERE ID_BODEGA = @LUGAR))", cnn);
                }
                command.Parameters.AddWithValue("@LUGAR", Lugar);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Pais.Nombre = reader["NOMBRE"].ToString();
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
            return Pais;
        }
    }
}