using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Retrieval
{
    public class RetrievalModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearServiceProvider(string NombreRetrievalProvider, string Email, string Telefono, int Diasprearribo, int Diaspostarribo, int Estado, string Email1 = null, string Email2 = null, string Telefono1 = null, string Telefono2 = null, int Diasprearribo1 = 0, int Diasprearribo2 = 0)
        {

            List<Clases.RetrievalProvider> ListaRetrievalProvider = new List<Clases.RetrievalProvider>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaRetrievalProvider = GetRetrievalProvider();
            for (int i = 0; i < ListaRetrievalProvider.Count(); i++)
            {
                if (ListaRetrievalProvider[i].NombreRetrievalProvider.ToUpper() == NombreRetrievalProvider.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO AplicacionServicio_RetrievalProvider(NOMBRE, ESTADO, CORREO, CORREO1, CORREO2, TELEFONO, TELEFONO1, TELEFONO2, DIAPREARRIBO, DIAPREARRIBO1, DIAPREARRIBO2, DIAPOSTARRIBO, USUARIO) VALUES(@NOMBRE, @ESTADO, @CORREO, @CORREO1, @CORREO2, @TELEFONO, @TELEFONO1, @TELEFONO2, @DIAPREARRIBO, @DIAPREARRIBO1, @DIAPREARRIBO2, @DIAPOSTARRIBO, @USUARIO)", cnn);
                command.Parameters.AddWithValue("@NOMBRE", NombreRetrievalProvider.ToUpper());
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@CORREO", Email);
                command.Parameters.AddWithValue("@CORREO1", Email1);
                command.Parameters.AddWithValue("@CORREO2", Email2);
                command.Parameters.AddWithValue("@TELEFONO", Telefono);
                command.Parameters.AddWithValue("@TELEFONO1", Telefono1);
                command.Parameters.AddWithValue("@TELEFONO2", Telefono2);
                command.Parameters.AddWithValue("@DIAPREARRIBO", Diasprearribo);
                command.Parameters.AddWithValue("@DIAPREARRIBO1", Diasprearribo1);
                command.Parameters.AddWithValue("@DIAPREARRIBO2", Diasprearribo2);
                command.Parameters.AddWithValue("@DIAPOSTARRIBO", Diaspostarribo);
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
        public static List<Clases.RetrievalProvider> GetRetrievalProvider()
        {
            SqlConnection cnn;
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RetrievalProvider.Add(new Clases.RetrievalProvider
                    {
                        NombreRetrievalProvider = reader["NOMBRE"].ToString(),
                        IdRetrievalProvider = Convert.ToInt32(reader["ID_RETRIEVAL"]),
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
            return RetrievalProvider;
        }
        public static List<Clases.RetrievalProvider> GetRetrievalProviderActivas()
        {
            SqlConnection cnn;
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider WHERE AplicacionServicio_RetrievalProvider.ESTADO = 0 ORDER BY NOMBRE ASC", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RetrievalProvider.Add(new Clases.RetrievalProvider
                    {
                        NombreRetrievalProvider = reader["NOMBRE"].ToString(),
                        IdRetrievalProvider = Convert.ToInt32(reader["ID_RETRIEVAL"]),
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
            return RetrievalProvider;
        }
        public static List<Clases.RetrievalProvider> GetRetrievalProviderByNaviera(int IdNaviera)
        {
            SqlConnection cnn;
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_RETRIEVAL, NOMBRE, ESTADO FROM AplicacionServicio_RetrievalProvider INNER JOIN AplicacionServicio_RetrievalProviderNaviera ON ID_RETRIEVAL=ID_RETRIEVAL_PROVIDER WHERE AplicacionServicio_RetrievalProviderNaviera.ID_NAVIERA=@ID_NAVIERA", cnn);
                command.Parameters.AddWithValue("@ID_NAVIERA", IdNaviera);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RetrievalProvider.Add(new Clases.RetrievalProvider
                    {
                        NombreRetrievalProvider = reader["NOMBRE"].ToString(),
                        IdRetrievalProvider = Convert.ToInt32(reader["ID_RETRIEVAL"]),
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
            return RetrievalProvider;
        }
        public static List<Clases.RetrievalProvider> GetRetrievalProviderByNodo(int TipoNodo, int Nodo)
        {
            SqlConnection cnn;
            List<Clases.RetrievalProvider> RetrievalProvider = new List<Clases.RetrievalProvider>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = null;
            try
            {
                cnn.Open();
                switch (TipoNodo)
                {
                    case 1:
                        command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider WHERE AplicacionServicio_RetrievalProvider.ESTADO = 0 AND AplicacionServicio_RetrievalProvider.ID_RETRIEVAL=(SELECT ID_RETRIEVAL_PROVIDER FROM AplicacionServicio_Deposito WHERE ID_DEPOSITO=@ID_NODO) ORDER BY NOMBRE ASC", cnn);
                        break;
                    case 2:
                        command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider WHERE AplicacionServicio_RetrievalProvider.ESTADO = 0 AND AplicacionServicio_RetrievalProvider.ID_RETRIEVAL=(SELECT ID_RETRIEVAL_PROVIDER FROM AplicacionServicio_Packing WHERE ID_PACKING=@ID_NODO) ORDER BY NOMBRE ASC", cnn);
                        break;
                    case 3:
                        command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider WHERE AplicacionServicio_RetrievalProvider.ESTADO = 0 AND AplicacionServicio_RetrievalProvider.ID_RETRIEVAL=(SELECT ID_RETRIEVAL_PROVIDER FROM AplicacionServicio_Antepuerto WHERE ID_ANTEPUERTO=@ID_NODO) ORDER BY NOMBRE ASC", cnn);
                        break;
                    case 4:
                        command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider WHERE AplicacionServicio_RetrievalProvider.ESTADO = 0 AND AplicacionServicio_RetrievalProvider.ID_RETRIEVAL=(SELECT ID_RETRIEVAL_PROVIDER FROM AplicacionServicio_Puertos WHERE ID_PUERTOORIGEN=@ID_NODO) ORDER BY NOMBRE ASC", cnn);
                        break;
                    case 6:
                        command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider WHERE AplicacionServicio_RetrievalProvider.ESTADO = 0 AND AplicacionServicio_RetrievalProvider.ID_RETRIEVAL=(SELECT ID_RETRIEVAL_PROVIDER FROM AplicacionServicio_Bodega WHERE ID_BODEGA=@ID_NODO) ORDER BY NOMBRE ASC", cnn);
                        break;
                }
                
                command.Parameters.AddWithValue("@ID_NODO", Nodo);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(reader["ID_RETRIEVAL"] is DBNull)
                    {
                        RetrievalProvider.Add(new Clases.RetrievalProvider
                        {
                            IdRetrievalProvider = 0
                        });
                    }else
                    {
                        RetrievalProvider.Add(new Clases.RetrievalProvider
                        {
                            NombreRetrievalProvider = reader["NOMBRE"].ToString(),
                            IdRetrievalProvider = Convert.ToInt32(reader["ID_RETRIEVAL"]),
                            Activo = Convert.ToInt32(reader["ESTADO"])
                        });
                    } 
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
            return RetrievalProvider;
        }
        public static Clases.RetrievalProvider GetIdRetrievalProvider(string Nombre)
        {
            SqlConnection cnn;
            Clases.RetrievalProvider RetrievalProvider = new Clases.RetrievalProvider();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM AplicacionServicio_RetrievalProvider Where AplicacionServicio_RetrievalProvider.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    RetrievalProvider.NombreRetrievalProvider = reader["NOMBRE"].ToString();
                    RetrievalProvider.IdRetrievalProvider = Convert.ToInt32(reader["ID_RETRIEVAL"]);
                    RetrievalProvider.Activo = Convert.ToInt32(reader["ESTADO"]);
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
            return RetrievalProvider;
        }
        public static int EditarRetrievalProvider(string NombreRetrievalProvider, string Email, string Telefono, int Diasprearribo, int Diaspostarribo, int Estado, int idRetrievalProvider, string Email1 = null, string Email2 = null, string Telefono1 = null, string Telefono2 = null, int Diasprearribo1 = 0, int Diasprearribo2 = 0)
        {

            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            List<Clases.RetrievalProvider> ListaRetrievalProvider = new List<Clases.RetrievalProvider>();
            //Validar Si Existe Naviera con el mismo Nombre
            ListaRetrievalProvider = GetRetrievalProvider();
            for (int i = 0; i < ListaRetrievalProvider.Count(); i++)
            {
                if (ListaRetrievalProvider[i].NombreRetrievalProvider.ToUpper() == NombreRetrievalProvider.ToUpper())
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE AplicacionServicio_RetrievalProvider SET NOMBRE = @NOMBRE, ESTADO = @ESTADO, CORREO = @CORREO, CORREO1 = @CORREO1, CORREO2 = @CORREO2, TELEFONO = @TELEFONO, TELEFONO1 = @TELEFONO1, TELEFONO2 = @TELEFONO2, DIAPREARRIBO = @DIAPREARRIBO, DIAPREARRIBO1 = @DIAPREARRIBO1, DIAPREARRIBO2 = @DIAPREARRIBO2, DIAPOSTARRIBO = @DIAPOSTARRIBO, USUARIO = @USUARIO WHERE ID_RETRIEVAL = @IDRETRIEVAL", cnn);
                command.Parameters.AddWithValue("@NOMBRE", NombreRetrievalProvider.ToUpper());
                command.Parameters.AddWithValue("@CORREO", Email);
                command.Parameters.AddWithValue("@CORREO1", Email1);
                command.Parameters.AddWithValue("@CORREO2", Email2);
                command.Parameters.AddWithValue("@TELEFONO", Telefono);
                command.Parameters.AddWithValue("@TELEFONO1", Telefono1);
                command.Parameters.AddWithValue("@TELEFONO2", Telefono2);
                command.Parameters.AddWithValue("@DIAPREARRIBO", Diasprearribo);
                command.Parameters.AddWithValue("@DIAPREARRIBO1", Diasprearribo1);
                command.Parameters.AddWithValue("@DIAPREARRIBO2", Diasprearribo2);
                command.Parameters.AddWithValue("@DIAPOSTARRIBO", Diaspostarribo);
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.AddWithValue("@IDRETRIEVAL", idRetrievalProvider);
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
        public static int GetIdRetrievalProvider2(string Nombre)
        {
            SqlConnection cnn;
            int id = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_RETRIEVAL FROM AplicacionServicio_RetrievalProvider Where AplicacionServicio_RetrievalProvider.NOMBRE = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID_RETRIEVAL"]);
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
            return id;
        }
        public static Clases.RetrievalProvider GetRetrievalProviderById(int IdRetrieval)
        {
            SqlConnection cnn;
            Clases.RetrievalProvider RetrievalProvider = new Clases.RetrievalProvider();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarRetrievalProvider @ID_RETRIEVAL", cnn);
                command.Parameters.AddWithValue("@ID_RETRIEVAL", IdRetrieval);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RetrievalProvider.NombreRetrievalProvider = reader["NOMBRE"].ToString();
                    RetrievalProvider.Correo = reader["CORREO"].ToString();
                    RetrievalProvider.Correo1 = reader["CORREO1"].ToString();
                    RetrievalProvider.Correo2 = reader["CORREO2"].ToString();
                    RetrievalProvider.Telefono = reader["TELEFONO"].ToString();
                    RetrievalProvider.Telefono1 = reader["TELEFONO1"].ToString();
                    RetrievalProvider.Telefono2 = reader["TELEFONO2"].ToString();
                    RetrievalProvider.Diaprearribo = Convert.ToInt32(reader["DIAPREARRIBO"]);
                    RetrievalProvider.Diaprearribo1 = Convert.ToInt32(reader["DIAPREARRIBO1"]);
                    RetrievalProvider.Diaprearribo2 = Convert.ToInt32(reader["DIAPREARRIBO2"]);
                    RetrievalProvider.Diapostarribo = Convert.ToInt32(reader["DIAPOSTARRIBO"]);
                    RetrievalProvider.IdNodo = Convert.ToInt32(reader["ID_NODO"]);
                    //RetrievalProvider.IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cnn.Close();
            }
            return RetrievalProvider;
        }
        public static List<Clases.Naviera> GetNavierasRetrieval(int IdRetrieval)
        {
            SqlConnection cnn;
            List<Clases.Naviera> NavierasRetrievalProvider = new List<Clases.Naviera>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select naviera.nombre, naviera.id_naviera from aplicacionservicio_retrievalprovidernaviera retrievalnaviera, aplicacionservicio_naviera naviera where retrievalnaviera.id_retrieval_provider = @IDRETRIEVAL and retrievalnaviera.ID_NAVIERA = naviera.ID_NAVIERA;", cnn);
                command.Parameters.AddWithValue("@IDRETRIEVAL", IdRetrieval);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NavierasRetrievalProvider.Add(new Clases.Naviera
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_NAVIERA"]),
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
            return NavierasRetrievalProvider;
        }
        public static List<Clases.AsociacionNodoRetrieval> GetNodosRetrieval(int IdRetrieval)
        {
            SqlConnection cnn;
            List<Clases.AsociacionNodoRetrieval> Nodos = new List<Clases.AsociacionNodoRetrieval>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetNodosRetrieval @IDRETRIEVAL", cnn);
                command.Parameters.AddWithValue("@IDRETRIEVAL", IdRetrieval);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Nodos.Add(new Clases.AsociacionNodoRetrieval
                    {
                        NombreNodo = reader["NOMBRE"].ToString(),
                        IdTipoLugar = Convert.ToInt32(reader["ID_TIPONODO"]),
                        IdNodo = Convert.ToInt32(reader["ID_NODO"]),
                        Dias = Convert.ToInt32(reader["DIASETR"]),
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

            return Nodos;
        }
        public static int AgregarNavieraRetrieval(int IdNaviera, int IdRetrieval)
        {

            List<Clases.AsociacionNavieraRetrieval> ListaRetrievalProvider = new List<Clases.AsociacionNavieraRetrieval>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaRetrievalProvider = GetAsociacionRetrievalProvider();
            for (int i = 0; i < ListaRetrievalProvider.Count(); i++)
            {
                if (ListaRetrievalProvider[i].IdNaviera == IdNaviera && ListaRetrievalProvider[i].IdRetrievalProvider == IdRetrieval)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO aplicacionservicio_retrievalprovidernaviera(ID_NAVIERA, ID_RETRIEVAL_PROVIDER) VALUES(@IDNAVIERA, @IDRETRIEVAL)", cnn);
                command.Parameters.AddWithValue("@IDNAVIERA", IdNaviera);
                command.Parameters.AddWithValue("@IDRETRIEVAL", IdRetrieval);

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
        public static int AgregarNodoRetrieval(int IdTipoLugar, int IdNodo, int Dias, int IdRetrieval)
        {

            List<Clases.AsociacionNodoRetrieval> ListaRetrievalProvider = new List<Clases.AsociacionNodoRetrieval>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            //Validar Si Existe Naviera con el mismo Nombre
            ListaRetrievalProvider = GetAsociacionNodoRetrievalProvider();
            for (int i = 0; i < ListaRetrievalProvider.Count(); i++)
            {
                if (ListaRetrievalProvider[i].IdTipoLugar == IdTipoLugar && ListaRetrievalProvider[i].IdNodo == IdNodo && ListaRetrievalProvider[i].IdRetrieval == IdRetrieval)
                {
                    return 2;
                }
            }
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO aplicacionservicio_retrievalnodo(ID_TIPONODO, ID_NODO, DIASETR, ID_RETRIEVAL) VALUES(@IDTIPONODO, @IDNODO, @DIAS, @IDRETRIEVAL)", cnn);
                command.Parameters.AddWithValue("@IDTIPONODO", IdTipoLugar);
                command.Parameters.AddWithValue("@IDNODO", IdNodo);
                command.Parameters.AddWithValue("@DIAS", Dias);
                command.Parameters.AddWithValue("@IDRETRIEVAL", IdRetrieval);

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
        public static List<Clases.AsociacionNavieraRetrieval> GetAsociacionRetrievalProvider()
        {
            SqlConnection cnn;
            List<Clases.AsociacionNavieraRetrieval> NavierasRetrievalProvider = new List<Clases.AsociacionNavieraRetrieval>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select * from aplicacionservicio_retrievalprovidernaviera", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NavierasRetrievalProvider.Add(new Clases.AsociacionNavieraRetrieval
                    {
                        IdNaviera = Convert.ToInt32(reader["ID_NAVIERA"]),
                        IdRetrievalProvider = Convert.ToInt32(reader["ID_RETRIEVAL_PROVIDER"]),
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
            return NavierasRetrievalProvider;
        }
        public static List<Clases.AsociacionNodoRetrieval> GetAsociacionNodoRetrievalProvider()
        {
            SqlConnection cnn;
            List<Clases.AsociacionNodoRetrieval> NodosRetrievalProvider = new List<Clases.AsociacionNodoRetrieval>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select * from aplicacionservicio_retrievalnodo", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NodosRetrievalProvider.Add(new Clases.AsociacionNodoRetrieval
                    {
                        IdTipoLugar = Convert.ToInt32(reader["ID_TIPONODO"]),
                        IdNodo = Convert.ToInt32(reader["ID_NODO"]),
                        IdRetrieval = Convert.ToInt32(reader["ID_RETRIEVAL"])
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
            return NodosRetrievalProvider;
        }
        public static int GetDias(int IdNaviera, int IdPuerto)
        {
            SqlConnection cnn;
            int Dias = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select	AplicacionServicio_RetrievalProvider.DIAPREARRIBO from	AplicacionServicio_RetrievalProvider, AplicacionServicio_RetrievalProviderNaviera  where AplicacionServicio_RetrievalProvider.ID_RETRIEVAL = AplicacionServicio_RetrievalProviderNaviera.ID_RETRIEVAL_PROVIDER and AplicacionServicio_RetrievalProvider.IDPUERTO = @IDPUERTO and AplicacionServicio_RetrievalProviderNaviera.ID_NAVIERA = @IDNAVIERA;", cnn);
                command.Parameters.AddWithValue("@IDPUERTO", IdPuerto);
                command.Parameters.AddWithValue("@IDNAVIERA", IdNaviera);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Dias = Convert.ToInt32(reader["DIAPREARRIBO"]);
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
            return Dias;
        }
    }
}